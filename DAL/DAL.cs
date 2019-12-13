using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;

using DAL;
using Common.ExtensionMethods;

namespace Desene
{
    public static class DAL
    {
        public static BindingList<MovieShortInfo> MoviesData;
        public static List<CachedMovieStills> CachedMoviesStills = new List<CachedMovieStills>();
        public static MovieTechnicalDetails CurrentMTD;
        public static MovieTechnicalDetails NewMTD;
        public static List<string> MovieThemes = new List<string>();
        public static string SectionDetails = string.Empty;
        public static SeriesType SeriesType = SeriesType.Final;


        public static OperationResult LoadBaseDbValues()
        {
            var result = new OperationResult();

            try
            {
                using (var conn = new SqlCeConnection(Constants.ConnectionString))
                {
                    conn.Open();

                    var commandSource = new SqlCeCommand("SELECT DISTINCT Theme FROM FileDetail WHERE Theme IS NOT NULL AND Theme <> ''ORDER BY Theme", conn);

                    using (var reader = commandSource.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MovieThemes.Add(reader["Theme"].ToString());
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }
        }

        public static BindingList<MovieShortInfo> GetMoviesGridData()
        {
            var result = new BindingList<MovieShortInfo>();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource = new SqlCeCommand(@"
                    SELECT
	                    Id,
	                    FileName,
	                    CASE
                            WHEN Poster IS NULL THEN CONVERT(BIT, 0)
                            ELSE CONVERT(BIT, 1)
	                    END AS HasPoster,

	                    CASE
	                        WHEN Quality IS NULL THEN 'sd?'
	                        ELSE Quality
	                    END AS Quality

                    FROM FileDetail
                    WHERE ParentId IS NULL
                    ORDER BY FileName", conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new MovieShortInfo
                        {
                            Id = (int)reader["Id"],
                            FileName = reader["FileName"].ToString(),
                            HasPoster = (bool)reader["HasPoster"],
                            Quality = reader["Quality"].ToString()
                        });
                    }
                }
            }

            return result;
        }

        public static List<SeriesEpisodesShortInfo> GetSeriesInfo()
        {
            var result = new List<SeriesEpisodesShortInfo>();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                /*
                 *SELECT fd1.Id, fd1.FileName, sum()
                    FROM FileDetail fd1
	                    LEFT OUTER JOIN FileDetail fd2 ON fd1.Id = fd2.ParentId
                    WHERE fd1.ParentId = -1
                    ORDER BY fd1.FileName
                 */
                var cmd = new SqlCeCommand("SELECT * FROM FileDetail WHERE ParentId = @displayType ORDER BY FileName", conn);
                cmd.Parameters.AddWithValue("@displayType", (int)SeriesType);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SeriesEpisodesShortInfo
                        {
                            Id = (int)reader["Id"],
                            FileName = reader["FileName"].ToString(),
                            Season = -1,
                            SeriesId = (int)reader["Id"],
                            IsSeries = true
                        });
                    }
                }
            }

            return result;
        }

        public static string GetSeriesTitleFromId(int seriesId)
        {
            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource = new SqlCeCommand(string.Format("SELECT FileName FROM FileDetail WHERE Id = {0}", seriesId), conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader["FileName"].ToString();
                    }
                }
            }

            return "unknown!";
        }

        public static List<SeriesEpisodesShortInfo> GetSeasonsForSeries(int seriesId)
        {
            var result = new List<SeriesEpisodesShortInfo>();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource = new SqlCeCommand(string.Format("SELECT DISTINCT Season FROM FileDetail WHERE ParentId = {0} order by Season", seriesId), conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var seasonVal = int.Parse(reader["Season"].ToString());

                        result.Add(new SeriesEpisodesShortInfo
                        {
                            Id = seriesId,
                            FileName = seasonVal > 0 ? string.Format("Season {0}", seasonVal) : "Specials",
                            Theme = string.Empty,
                            Quality = string.Empty,
                            Season = seasonVal,
                            SeriesId = seriesId,
                            IsSeason = true
                        });
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Called when the Sesson treeview item is expanded
        /// </summary>
        /// <param name="seriesId"></param>
        /// <param name="seasonVal"></param>
        /// <returns></returns>
        public static List<SeriesEpisodesShortInfo> GetEpisodesInSeason(int seriesId, int seasonVal)
        {
            var result = new List<SeriesEpisodesShortInfo>();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource = new SqlCeCommand(string.Format("SELECT * FROM FileDetail WHERE ParentId = {0} AND Season = {1} ORDER BY FileName", seriesId, seasonVal), conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SeriesEpisodesShortInfo
                        {
                            Id = (int)reader["Id"],
                            FileName = reader["FileName"].ToString(),
                            Theme = reader["Theme"].ToString(),
                            Quality = reader["Quality"].ToString(),
                            Season = seasonVal,
                            SeriesId = (int)reader["ParentId"],
                            IsEpisode = true
                        });
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Provide data to the SeriesEpisodes (summary) gridview
        /// </summary>
        /// <param name="seriesId"></param>
        /// <returns></returns>
        public static DataTable GetEpisodesInSeries(int seriesId)
        {
            var result = new DataTable();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource =
                    new SqlCeCommand(
                        string.Format(@"
                            SELECT
                                CASE
                                    WHEN fd.Season = -2 THEN 'Specials'
                                    ELSE fd.Season
                                END AS Season2,
                                fd.*
                            FROM FileDetail fd
                            WHERE ParentId = {0}
                            ORDER BY Season2, FileName", seriesId), conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    result.Load(reader);
                }
            }

            return result;
        }

        public static List<SeriesEpisodesShortInfo> GetFilteredSeriesEpisodes(string filterBy)
        {
            var result = new List<SeriesEpisodesShortInfo>();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                var sqlString =
                    string.Format(@"
                        SELECT
                            fd2.FileName AS SeriesName,
                            fd.*
                        FROM FileDetail fd
                            LEFT OUTER JOIN FileDetail fd2 ON fd.ParentId = fd2.id
                        WHERE fd.ParentId IS NOT NULL AND fd.FileName LIKE '%{0}%'
                        ORDER BY ParentId DESC", filterBy);

                var commandSource = new SqlCeCommand(sqlString, conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var parentId = (int)reader["ParentId"];

                        if (parentId > 0)
                        {
                            if (!result.Any(r => r.IsSeries && r.Id == parentId))
                            {
                                result.Add(new SeriesEpisodesShortInfo
                                {
                                    Id = parentId,
                                    FileName = reader["SeriesName"].ToString(),
                                    Season = -1,
                                    SeriesId = parentId,
                                    IsSeries = true
                                });
                            }

                            result.Add(new SeriesEpisodesShortInfo
                            {
                                Id = (int)reader["Id"],
                                FileName = reader["FileName"].ToString(),
                                Theme = reader["Theme"].ToString(),
                                Quality = reader["Quality"].ToString(),
                                Season = int.Parse(reader["Season"].ToString()),
                                SeriesId = parentId,
                                IsEpisode = true
                            });
                        }
                        else
                        {
                            var seriesId = (int)reader["Id"];

                            if (!result.Any(r => r.IsSeries && r.Id == seriesId))
                            {
                                result.Add(new SeriesEpisodesShortInfo
                                {
                                    Id = seriesId,
                                    FileName = reader["FileName"].ToString(),
                                    Season = -1,
                                    SeriesId = seriesId,
                                    IsSeries = true
                                });
                            }
                        }
                    }
                }

                var seasonsInSeriesInFilterResult =
                    result.Where(r => r.IsEpisode)
                          .GroupBy(g => new
                          {
                              g.SeriesId,
                              g.Season
                          })
                          .Select(g => g.FirstOrDefault())
                          .ToList();

                foreach (var seasonData in seasonsInSeriesInFilterResult)
                {
                    result.Add(new SeriesEpisodesShortInfo
                    {
                        Id = seasonData.SeriesId,
                        FileName = string.Format("Season {0}", seasonData.Season),
                        Season = seasonData.Season,
                        SeriesId = seasonData.SeriesId,
                        IsSeason = true
                    });
                }
            }

            return result.OrderBy(o => o.FileName).ThenBy(o => o.Season).ToList();
        }

        public static OperationResult InsertSeries(string title, string descriptionLink, string recommended,
            string recommendedLink, string notes, byte[] poster, string trailer)
        {
            var result = new OperationResult();

            try
            {
                using (var conn = new SqlCeConnection(Constants.ConnectionString))
                {
                    conn.Open();

                    const string insertString = @"
                        INSERT INTO FileDetail (
                            FileName,
                            InsertedDate,
                            Recommended,
                            RecommendedLink,
                            DescriptionLink,
                            Poster,
                            Notes,
                            Trailer,
                            ParentId)
                        VALUES (
                            @FileName,
                            @InsertedDate,
                            @Recommended,
                            @RecommendedLink,
                            @DescriptionLink,
                            @Poster,
                            @Notes,
                            @Trailer,
                            @displayType)";

                    var cmd = new SqlCeCommand(insertString, conn);
                    cmd.Parameters.AddWithValue("@FileName", title);
                    cmd.Parameters.AddWithValue("@InsertedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Recommended", recommended);
                    cmd.Parameters.AddWithValue("@RecommendedLink", recommendedLink);
                    cmd.Parameters.AddWithValue("@DescriptionLink", descriptionLink);
                    cmd.Parameters.AddWithValue("@Poster", poster ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Notes", notes);
                    cmd.Parameters.AddWithValue("@Trailer", trailer);
                    cmd.Parameters.AddWithValue("@displayType", (int)SeriesType);

                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Select @@Identity";

                    result.AdditionalDataReturn = (int)(decimal)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }

            return result;
        }

        //Import Episodes, Add Series or Single Movies
        public static OperationResult InsertMTD(MovieTechnicalDetails mtd, FilesImportParams eip)
        {
            var result = new OperationResult();

            try
            {
                using (var conn = new SqlCeConnection(Constants.ConnectionString))
                {
                    conn.Open();
                    SqlCeCommand cmd;

                    #region Checks

                    if (eip != null)
                    {
                        var checks = "SELECT COUNT(*) FROM FileDetail WHERE ParentId = @ParentId AND FileName = @FileName AND FileSize = @FileSize";

                        cmd = new SqlCeCommand(checks, conn);
                        cmd.Parameters.AddWithValue("@ParentId", eip.ParentId);
                        cmd.Parameters.AddWithValue("@FileName", mtd.FileName);
                        cmd.Parameters.AddWithValue("@FileSize", mtd.FileSize);

                        var count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                            return result.FailWithMessage("A file with exactly the same name and size already exists in the selected series. The file details were not added to the database!");
                    }
                    else
                    {
                        var checks = "SELECT COUNT(*) FROM FileDetail WHERE FileName = @FileName and ParentId IS NULL";

                        cmd = new SqlCeCommand(checks, conn);
                        cmd.Parameters.AddWithValue("@FileName", mtd.FileName);

                        var count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                            return result.FailWithMessage("A movie with exactly the same name already exists in the collection. The file details were not added to the database!");
                    }

                    #endregion

                    #region FileDetail

                    var insertString = @"
                        INSERT INTO FileDetail (
                            FileName,
                            Year,
                            Format,
                            Encoded_Application,
                            FileSize,
                            FileSize2,
                            Duration,
                            TitleEmbedded,
                            CoverEmbedded,
                            Season,
                            InsertedDate,
                            LastChangeDate,
                            Quality,
                            ParentId,
                            AudioLanguages,
                            SubtitleLanguages,

                            DescriptionLink,
                            Recommended,
                            RecommendedLink,
                            Theme,
                            Notes,
                            Trailer,
                            StreamLink,
                            Poster
                        )
                        VALUES (
                            @FileName,
                            @Year,
                            @Format,
                            @Encoded_Application,
                            @FileSize,
                            @FileSize2,
                            @Duration,
                            @TitleEmbedded,
                            @CoverEmbedded,
                            @Season,
                            @InsertedDate,
                            @LastChangeDate,
                            @Quality,
                            @ParentId,
                            @AudioLanguages,
                            @SubtitleLanguages,

                            @DescriptionLink,
                            @Recommended,
                            @RecommendedLink,
                            @Theme,
                            @Notes,
                            @Trailer,
                            @StreamLink,
                            @Poster
                        )";

                    cmd = new SqlCeCommand(insertString, conn);
                    cmd.Parameters.AddWithValue("@FileName", mtd.FileName);
                    cmd.Parameters.AddWithValue("@Year", eip == null ? mtd.Year ?? (object)DBNull.Value : eip.Year);
                    cmd.Parameters.AddWithValue("@Format", mtd.Format);
                    cmd.Parameters.AddWithValue("@Encoded_Application", mtd.Encoded_Application);
                    cmd.Parameters.AddWithValue("@FileSize", mtd.FileSize);                                                 //as int value, for totals calculations
                    cmd.Parameters.AddWithValue("@FileSize2", mtd.FileSize2.Replace("GiB", "Gb").Replace("MiB", "Mb"));     //GUI
                    cmd.Parameters.AddWithValue("@Duration", mtd.DurationAsDateTime);
                    cmd.Parameters.AddWithValue("@TitleEmbedded", mtd.Title);
                    cmd.Parameters.AddWithValue("@CoverEmbedded", mtd.Cover);
                    cmd.Parameters.AddWithValue("@Season", eip == null ? (object)DBNull.Value : eip.Season);
                    cmd.Parameters.AddWithValue("@InsertedDate", mtd.InsertedDate ?? DateTime.Now);
                    cmd.Parameters.AddWithValue("@LastChangeDate", mtd.LastChangeDate == null ? (object)DBNull.Value : mtd.LastChangeDate);
                    cmd.Parameters.AddWithValue("@Quality", GetQualityStrFromSize(mtd));
                    cmd.Parameters.AddWithValue("@ParentId", eip == null ? (object)DBNull.Value : eip.ParentId);

                    var audioLanguages = string.Join(", ", mtd.AudioStreams.Select(a => a.Language == "" ? "?" : a.Language).Distinct());
                    cmd.Parameters.AddWithValue("@AudioLanguages", audioLanguages);
                    var subtitleLanguages = string.Join(", ", mtd.SubtitleStreams.Select(a => a.Language == "" ? "?" : a.Language).Distinct());
                    cmd.Parameters.AddWithValue("@SubtitleLanguages", subtitleLanguages);

                    cmd.Parameters.AddWithValue("@DescriptionLink", mtd.DescriptionLink ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Recommended", mtd.Recommended ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@RecommendedLink", mtd.RecommendedLink ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Theme", mtd.Theme ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Notes", mtd.Notes ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Trailer", mtd.Trailer ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@StreamLink", mtd.StreamLink ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Poster", mtd.Poster ?? (object)DBNull.Value);

                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Select @@Identity";

                    var newFileDetailId = (int)(decimal)cmd.ExecuteScalar();
                    result.AdditionalDataReturn = newFileDetailId;

                    #endregion

                    #region Movie Stills

                    insertString = @"
                        INSERT INTO Thumbnails (
                            FileDetailId,
                            MovieStill)
                        VALUES (
                            @FileDetailId,
                            @MovieStill)";

                    foreach (var movieStill in mtd.MovieStills)
                    {
                        if (movieStill == null) continue;

                        cmd = new SqlCeCommand(insertString, conn);
                        cmd.Parameters.AddWithValue("@FileDetailId", newFileDetailId);
                        cmd.Parameters.AddWithValue("@MovieStill", movieStill);
                        cmd.ExecuteNonQuery();
                    }

                    #endregion

                    #region VideoStreams

                    insertString = @"
                        INSERT INTO VideoStream (
                            FileDetailId,
                            [Index],
                            Format,
                            Format_Profile,
                            BitRateMode,
                            BitRate,
                            Width,
                            Height,
                            FrameRate_Mode,
                            FrameRate,
                            Delay,
                            StreamSize,
                            TitleEmbedded,
                            Language)
                        VALUES (
                            @FileDetailId,
                            @Index,
                            @Format,
                            @Format_Profile,
                            @BitRateMode,
                            @BitRate,
                            @Width,
                            @Height,
                            @FrameRate_Mode,
                            @FrameRate,
                            @Delay,
                            @StreamSize,
                            @TitleEmbedded,
                            @Language)";

                    var index = 1;

                    foreach (var videoStream in mtd.VideoStreams)
                    {
                        cmd = new SqlCeCommand(insertString, conn);
                        cmd.Parameters.AddWithValue("@FileDetailId", newFileDetailId);
                        cmd.Parameters.AddWithValue("@Index", index);
                        cmd.Parameters.AddWithValue("@Format", videoStream.Format);
                        cmd.Parameters.AddWithValue("@Format_Profile", videoStream.Format_Profile);
                        cmd.Parameters.AddWithValue("@BitRateMode", videoStream.BitRateMode);
                        cmd.Parameters.AddWithValue("@BitRate", videoStream.BitRate);
                        cmd.Parameters.AddWithValue("@Width", videoStream.Width);
                        cmd.Parameters.AddWithValue("@Height", videoStream.Height);
                        cmd.Parameters.AddWithValue("@FrameRate_Mode", videoStream.FrameRate_Mode);
                        cmd.Parameters.AddWithValue("@FrameRate", videoStream.FrameRate);
                        cmd.Parameters.AddWithValue("@Delay", videoStream.Delay);
                        cmd.Parameters.AddWithValue("@StreamSize", videoStream.StreamSize);
                        cmd.Parameters.AddWithValue("@TitleEmbedded", videoStream.Title);
                        cmd.Parameters.AddWithValue("@Language", videoStream.Language);
                        cmd.ExecuteNonQuery();

                        index++;
                    }

                    #endregion

                    #region AudioStreams

                    insertString = @"
                        INSERT INTO AudioStream (
                            FileDetailId,
                            [Index],
                            Format,
                            BitRate,
                            Channel,
                            ChannelPosition,
                            SamplingRate,
                            Resolution,
                            Delay,
                            Video_Delay,
                            StreamSize,
                            TitleEmbedded,
                            Language)
                        VALUES (
                            @FileDetailId,
                            @Index,
                            @Format,
                            @BitRate,
                            @Channel,
                            @ChannelPosition,
                            @SamplingRate,
                            @Resolution,
                            @Delay,
                            @Video_Delay,
                            @StreamSize,
                            @TitleEmbedded,
                            @Language)";

                    index = 1;

                    foreach (var audioStream in mtd.AudioStreams)
                    {
                        cmd = new SqlCeCommand(insertString, conn);
                        cmd.Parameters.AddWithValue("@FileDetailId", newFileDetailId);
                        cmd.Parameters.AddWithValue("@Index", index);
                        cmd.Parameters.AddWithValue("@Format", audioStream.Format);
                        cmd.Parameters.AddWithValue("@BitRate", audioStream.BitRate);
                        cmd.Parameters.AddWithValue("@Channel", audioStream.Channel);
                        cmd.Parameters.AddWithValue("@ChannelPosition", audioStream.ChannelPosition);
                        cmd.Parameters.AddWithValue("@SamplingRate", audioStream.SamplingRate);
                        cmd.Parameters.AddWithValue("@Resolution", audioStream.Resolution);
                        cmd.Parameters.AddWithValue("@Delay", audioStream.Delay);
                        cmd.Parameters.AddWithValue("@Video_Delay", audioStream.Video_Delay);
                        cmd.Parameters.AddWithValue("@StreamSize", audioStream.StreamSize);
                        cmd.Parameters.AddWithValue("@TitleEmbedded", audioStream.Title); ;
                        cmd.Parameters.AddWithValue("@Language", audioStream.Language);
                        cmd.ExecuteNonQuery();

                        index++;
                    }

                    #endregion

                    #region TextStream

                    insertString = @"
                        INSERT INTO SubtitleStream (
                            FileDetailId,
                            [Index],
                            Format,
                            StreamSize,
                            TitleEmbedded,
                            Language)
                        VALUES (
                            @FileDetailId,
                            @Index,
                            @Format,
                            @StreamSize,
                            @TitleEmbedded,
                            @Language)";

                    index = 1;

                    foreach (var subtitleStream in mtd.SubtitleStreams)
                    {
                        cmd = new SqlCeCommand(insertString, conn);
                        cmd.Parameters.AddWithValue("@FileDetailId", newFileDetailId);
                        cmd.Parameters.AddWithValue("@Index", index);
                        cmd.Parameters.AddWithValue("@Format", subtitleStream.Format);
                        cmd.Parameters.AddWithValue("@StreamSize", subtitleStream.StreamSize);
                        cmd.Parameters.AddWithValue("@TitleEmbedded", subtitleStream.Title); ;
                        cmd.Parameters.AddWithValue("@Language", subtitleStream.Language);
                        cmd.ExecuteNonQuery();

                        index++;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }

            return result;
        }

        //Import Movies
        public static OperationResult InsertMTD2(MovieTechnicalDetails mtd, FilesImportParams eip)
        {
            var result = new OperationResult();

            try
            {
                using (var conn = new SqlCeConnection(Constants.ConnectionString))
                {
                    conn.Open();
                    SqlCeCommand cmd;
                    var insertString = string.Empty;

                    var descriptionLink = string.Empty;
                    var recommended = string.Empty;
                    var recommendedLink = string.Empty;
                    var year = string.Empty;
                    var theme = string.Empty;
                    var notes = string.Empty;
                    var trailer = string.Empty;
                    var streamLink = string.Empty;
                    var nlAudioSource = string.Empty;
                    byte[] poster = null;

                    var existingFileInfo = MoviesData.FirstOrDefault(md => md.FileName == mtd.FileName);
                    if (existingFileInfo != null)
                    {
                        if (eip.PreserveManuallySetData)
                        {
                            cmd = new SqlCeCommand(string.Format("SELECT * FROM FileDetail WHERE Id = {0}", existingFileInfo.Id), conn);

                            using (var reader = cmd.ExecuteResultSet(ResultSetOptions.Scrollable))
                            {
                                while (reader.Read())
                                {
                                    descriptionLink = reader["DescriptionLink"].ToString();
                                    recommended = reader["Recommended"].ToString();
                                    recommendedLink = reader["RecommendedLink"].ToString();
                                    year = reader["Year"].ToString();
                                    theme = reader["Theme"].ToString();
                                    notes = reader["Notes"].ToString();
                                    trailer = reader["Trailer"].ToString();
                                    streamLink = reader["StreamLink"].ToString();
                                    poster = reader["Poster"] == DBNull.Value ? null : (byte[])reader["Poster"];
                                    nlAudioSource = reader["NlAudioSource"].ToString();
                                }
                            }
                        }

                        result = RemoveData(existingFileInfo.Id.ToString(), conn);

                        if (!result.Success)
                            return result.FailWithMessage(result.CustomErrorMessage);
                    }

                    #region FileDetail

                    insertString = @"
                        INSERT INTO FileDetail (
                            FileName,
                            Year,
                            Format,
                            Encoded_Application,
                            FileSize,
                            FileSize2,
                            Duration,
                            TitleEmbedded,
                            CoverEmbedded,
                            Season,
                            InsertedDate,
                            Quality,
                            ParentId,
                            AudioLanguages,
                            SubtitleLanguages,

                            DescriptionLink,
                            Recommended,
                            RecommendedLink,
                            Theme,
                            Notes,
                            Trailer,
                            StreamLink,
                            Poster,
                            NlAudioSource
                        )
                        VALUES (
                            @FileName,
                            @Year,
                            @Format,
                            @Encoded_Application,
                            @FileSize,
                            @FileSize2,
                            @Duration,
                            @TitleEmbedded,
                            @CoverEmbedded,
                            @Season,
                            @InsertedDate,
                            @Quality,
                            @ParentId,
                            @AudioLanguages,
                            @SubtitleLanguages,

                            @DescriptionLink,
                            @Recommended,
                            @RecommendedLink,
                            @Theme,
                            @Notes,
                            @Trailer,
                            @StreamLink,
                            @Poster,
                            @NlAudioSource
                        )";

                    cmd = new SqlCeCommand(insertString, conn);
                    cmd.Parameters.AddWithValue("@FileName", mtd.FileName);
                    cmd.Parameters.AddWithValue("@Year", string.IsNullOrEmpty(year) ? (object)DBNull.Value : year);
                    cmd.Parameters.AddWithValue("@Format", mtd.Format);
                    cmd.Parameters.AddWithValue("@Encoded_Application", mtd.Encoded_Application);
                    cmd.Parameters.AddWithValue("@FileSize", mtd.FileSize);                                                 //as int value, for totals calculations
                    cmd.Parameters.AddWithValue("@FileSize2", mtd.FileSize2.Replace("GiB", "Gb").Replace("MiB", "Mb"));     //GUI
                    cmd.Parameters.AddWithValue("@Duration", mtd.DurationAsDateTime);
                    cmd.Parameters.AddWithValue("@TitleEmbedded", mtd.Title);
                    cmd.Parameters.AddWithValue("@CoverEmbedded", mtd.Cover);
                    cmd.Parameters.AddWithValue("@Season", DBNull.Value);
                    cmd.Parameters.AddWithValue("@InsertedDate", DateTime.Now);


                    cmd.Parameters.AddWithValue("@Quality", GetQualityStrFromSize(mtd));
                    cmd.Parameters.AddWithValue("@ParentId", DBNull.Value);

                    var audioLanguages = string.Join(", ", mtd.AudioStreams.Select(a => a.Language == "" ? "?" : a.Language).Distinct());
                    cmd.Parameters.AddWithValue("@AudioLanguages", audioLanguages);
                    var subtitleLanguages = string.Join(", ", mtd.SubtitleStreams.Select(a => a.Language == "" ? "?" : a.Language).Distinct());
                    cmd.Parameters.AddWithValue("@SubtitleLanguages", subtitleLanguages);

                    cmd.Parameters.AddWithValue("@DescriptionLink", string.IsNullOrEmpty(descriptionLink) ? (object)DBNull.Value : descriptionLink);
                    cmd.Parameters.AddWithValue("@Recommended", string.IsNullOrEmpty(recommended) ? (object)DBNull.Value : recommended);
                    cmd.Parameters.AddWithValue("@RecommendedLink", string.IsNullOrEmpty(recommendedLink) ? (object)DBNull.Value : recommendedLink);
                    cmd.Parameters.AddWithValue("@Theme", string.IsNullOrEmpty(theme) ? (object)DBNull.Value : theme);
                    cmd.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes);
                    cmd.Parameters.AddWithValue("@Trailer", string.IsNullOrEmpty(trailer) ? (object)DBNull.Value : trailer);
                    cmd.Parameters.AddWithValue("@StreamLink", string.IsNullOrEmpty(streamLink) ? (object)DBNull.Value : streamLink);
                    cmd.Parameters.AddWithValue("@Poster", poster ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NlAudioSource", string.IsNullOrEmpty(nlAudioSource) ? (object)DBNull.Value : nlAudioSource);

                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Select @@Identity";

                    var newFileDetailId = (int)(decimal)cmd.ExecuteScalar();

                    #endregion

                    #region Movie Stills

                    insertString = @"
                        INSERT INTO Thumbnails (
                            FileDetailId,
                            MovieStill)
                        VALUES (
                            @FileDetailId,
                            @MovieStill)";

                    foreach (var movieStill in mtd.MovieStills)
                    {
                        if (movieStill == null) continue;

                        cmd = new SqlCeCommand(insertString, conn);
                        cmd.Parameters.AddWithValue("@FileDetailId", newFileDetailId);
                        cmd.Parameters.AddWithValue("@MovieStill", movieStill);
                        cmd.ExecuteNonQuery();
                    }

                    #endregion

                    #region VideoStreams

                    insertString = @"
                        INSERT INTO VideoStream (
                            FileDetailId,
                            [Index],
                            Format,
                            Format_Profile,
                            BitRateMode,
                            BitRate,
                            Width,
                            Height,
                            FrameRate_Mode,
                            FrameRate,
                            Delay,
                            StreamSize,
                            TitleEmbedded,
                            Language)
                        VALUES (
                            @FileDetailId,
                            @Index,
                            @Format,
                            @Format_Profile,
                            @BitRateMode,
                            @BitRate,
                            @Width,
                            @Height,
                            @FrameRate_Mode,
                            @FrameRate,
                            @Delay,
                            @StreamSize,
                            @TitleEmbedded,
                            @Language)";

                    var index = 1;

                    foreach (var videoStream in mtd.VideoStreams)
                    {
                        cmd = new SqlCeCommand(insertString, conn);
                        cmd.Parameters.AddWithValue("@FileDetailId", newFileDetailId);
                        cmd.Parameters.AddWithValue("@Index", index);
                        cmd.Parameters.AddWithValue("@Format", videoStream.Format);
                        cmd.Parameters.AddWithValue("@Format_Profile", videoStream.Format_Profile);
                        cmd.Parameters.AddWithValue("@BitRateMode", videoStream.BitRateMode);
                        cmd.Parameters.AddWithValue("@BitRate", videoStream.BitRate);
                        cmd.Parameters.AddWithValue("@Width", videoStream.Width);
                        cmd.Parameters.AddWithValue("@Height", videoStream.Height);
                        cmd.Parameters.AddWithValue("@FrameRate_Mode", videoStream.FrameRate_Mode);
                        cmd.Parameters.AddWithValue("@FrameRate", videoStream.FrameRate);
                        cmd.Parameters.AddWithValue("@Delay", videoStream.Delay);
                        cmd.Parameters.AddWithValue("@StreamSize", videoStream.StreamSize);
                        cmd.Parameters.AddWithValue("@TitleEmbedded", videoStream.Title);
                        cmd.Parameters.AddWithValue("@Language", videoStream.Language);
                        cmd.ExecuteNonQuery();

                        index++;
                    }

                    #endregion

                    #region AudioStreams

                    insertString = @"
                        INSERT INTO AudioStream (
                            FileDetailId,
                            [Index],
                            Format,
                            BitRate,
                            Channel,
                            ChannelPosition,
                            SamplingRate,
                            Resolution,
                            Delay,
                            Video_Delay,
                            StreamSize,
                            TitleEmbedded,
                            Language)
                        VALUES (
                            @FileDetailId,
                            @Index,
                            @Format,
                            @BitRate,
                            @Channel,
                            @ChannelPosition,
                            @SamplingRate,
                            @Resolution,
                            @Delay,
                            @Video_Delay,
                            @StreamSize,
                            @TitleEmbedded,
                            @Language)";

                    index = 1;

                    foreach (var audioStream in mtd.AudioStreams)
                    {
                        cmd = new SqlCeCommand(insertString, conn);
                        cmd.Parameters.AddWithValue("@FileDetailId", newFileDetailId);
                        cmd.Parameters.AddWithValue("@Index", index);
                        cmd.Parameters.AddWithValue("@Format", audioStream.Format);
                        cmd.Parameters.AddWithValue("@BitRate", audioStream.BitRate);
                        cmd.Parameters.AddWithValue("@Channel", audioStream.Channel);
                        cmd.Parameters.AddWithValue("@ChannelPosition", audioStream.ChannelPosition);
                        cmd.Parameters.AddWithValue("@SamplingRate", audioStream.SamplingRate);
                        cmd.Parameters.AddWithValue("@Resolution", audioStream.Resolution);
                        cmd.Parameters.AddWithValue("@Delay", audioStream.Delay);
                        cmd.Parameters.AddWithValue("@Video_Delay", audioStream.Video_Delay);
                        cmd.Parameters.AddWithValue("@StreamSize", audioStream.StreamSize);
                        cmd.Parameters.AddWithValue("@TitleEmbedded", audioStream.Title); ;
                        cmd.Parameters.AddWithValue("@Language", audioStream.Language);
                        cmd.ExecuteNonQuery();

                        index++;
                    }

                    #endregion

                    #region TextStream

                    insertString = @"
                        INSERT INTO SubtitleStream (
                            FileDetailId,
                            [Index],
                            Format,
                            StreamSize,
                            TitleEmbedded,
                            Language)
                        VALUES (
                            @FileDetailId,
                            @Index,
                            @Format,
                            @StreamSize,
                            @TitleEmbedded,
                            @Language)";

                    index = 1;

                    foreach (var subtitleStream in mtd.SubtitleStreams)
                    {
                        cmd = new SqlCeCommand(insertString, conn);
                        cmd.Parameters.AddWithValue("@FileDetailId", newFileDetailId);
                        cmd.Parameters.AddWithValue("@Index", index);
                        cmd.Parameters.AddWithValue("@Format", subtitleStream.Format);
                        cmd.Parameters.AddWithValue("@StreamSize", subtitleStream.StreamSize);
                        cmd.Parameters.AddWithValue("@TitleEmbedded", subtitleStream.Title); ;
                        cmd.Parameters.AddWithValue("@Language", subtitleStream.Language);
                        cmd.ExecuteNonQuery();

                        index++;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }

            return result;
        }

        private static int GetFileSize(string fileSize2)
        {
            //todo: wrong value on Save ... commented for the moment
            var fileSize = 0;

            try
            {
                if (fileSize2.Trim() != string.Empty && fileSize2.ToUpper().EndsWith("GB") || fileSize2.ToUpper().EndsWith("MB"))
                {
                    var isGb = fileSize2.ToUpper().IndexOf("GB") > 0;

                    var valueAsString = fileSize2.ToUpper().Replace("GB", "").Replace("MB", "").Replace(" ", "");

                    if (valueAsString != string.Empty)
                    {
                        var valueAsNumber = decimal.Parse(valueAsString);
                        fileSize = (int)(valueAsNumber * 1024 * 1024);

                        if (isGb)
                            fileSize = fileSize * 1024;
                    }
                }
            }
            catch (Exception)
            {
                //MsgBox.Show(
                //    string.Format("An error occurred while processing the file size changes! Please make sure you're using a compatible format, like 1,25Gb or 567Mb{0}{0}{1}",
                //        Environment.NewLine, OperationResult.GetErrorMessage(ex, true)),
                //    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return fileSize;
        }

        public static OperationResult SaveMTD(bool fullSave = true)
        {
            var result = new OperationResult();

            try
            {
                using (var conn = new SqlCeConnection(Constants.ConnectionString))
                {
                    conn.Open();

                    #region FileDetail

                    /*
                        ,FileSize = @FileSize
                        ,FileSize2 = @FileSize2
                     */
                    var updateString = @"
                        UPDATE FileDetail
                           SET FileName = @FileName
                               ,Year = @Year
                               ,Format = @Format
                               ,Encoded_Application = @Encoded_Application
                               ,Duration = @Duration
                               ,TitleEmbedded = @TitleEmbedded
                               ,Season = @Season
                               ,Quality = @Quality
                               ,AudioLanguages = @AudioLanguages
                               ,SubtitleLanguages = @SubtitleLanguages
                               ,LastChangeDate = @LastChangeDate
                               ,Poster = @Poster
                               ,Notes = @Notes
                               ,Trailer = @Trailer
                               ,DescriptionLink = @DescriptionLink
                               ,Recommended = @Recommended
                               ,RecommendedLink = @RecommendedLink
                               ,Theme = @Theme
                         WHERE Id = @Id";

                    var cmd = new SqlCeCommand(updateString, conn);
                    cmd.Parameters.AddWithValue("@FileName", CurrentMTD.FileName);
                    cmd.Parameters.AddWithValue("@Year", CurrentMTD.Year);
                    cmd.Parameters.AddWithValue("@Format", CurrentMTD.Format);
                    cmd.Parameters.AddWithValue("@Encoded_Application", CurrentMTD.Encoded_Application);
                    //cmd.Parameters.AddWithValue("@FileSize", GetFileSize(CurrentMTD.FileSize2));     //as int value, for totals calculations
                    //cmd.Parameters.AddWithValue("@FileSize2", CurrentMTD.FileSize2);                 //GUI

                    //todo: save the value from DurationAsInt if the TryParse fails
                    cmd.Parameters.AddWithValue("@Duration",
                        new DateTime(1970, 1, 1).Add(
                            TimeSpan.TryParse(CurrentMTD.DurationFormatted, out var durationAsTime)
                                ? durationAsTime
                                : new TimeSpan()
                            )
                        );

                    cmd.Parameters.AddWithValue("@TitleEmbedded",
                        CurrentMTD.HasTitle
                            ? string.IsNullOrEmpty(CurrentMTD.Title)
                                ? "?"
                                : CurrentMTD.Title
                            : string.Empty);

                    cmd.Parameters.AddWithValue("@CoverEmbedded", CurrentMTD.Cover);
                    cmd.Parameters.AddWithValue("@Season", CurrentMTD.Season);
                    //cmd.Parameters.AddWithValue("@Quality", CurrentMTD.ParentId > 0 ? GetQualityStrFromSize(CurrentMTD) : string.Empty);
                    cmd.Parameters.AddWithValue("@Quality", (int)CurrentMTD.ParentId == 0 ? GetQualityStrFromSize(CurrentMTD) : string.Empty);

                    CurrentMTD.AudioLanguages = string.Join(", ", CurrentMTD.AudioStreams.Select(a => a.Language == "" ? "?" : a.Language).Distinct());
                    cmd.Parameters.AddWithValue("@AudioLanguages", CurrentMTD.AudioLanguages);
                    CurrentMTD.SubtitleLanguages = string.Join(", ", CurrentMTD.SubtitleStreams.Select(a => a.Language == "" ? "?" : a.Language).Distinct());
                    cmd.Parameters.AddWithValue("@SubtitleLanguages", CurrentMTD.SubtitleLanguages);

                    //cmd.Parameters.AddWithValue("@AudioLanguages", CurrentMTD.AudioLanguages);
                    //cmd.Parameters.AddWithValue("@SubtitleLanguages", CurrentMTD.SubtitleLanguages);

                    cmd.Parameters.AddWithValue("@LastChangeDate", DateTime.Now);

                    //NOT here ... separate save method for base movie info
                    cmd.Parameters.AddWithValue("@Poster", CurrentMTD.Poster ?? (object)DBNull.Value);

                    cmd.Parameters.AddWithValue("@Notes", CurrentMTD.Notes ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Trailer", CurrentMTD.Trailer ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DescriptionLink", CurrentMTD.DescriptionLink ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Recommended", CurrentMTD.Recommended ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@RecommendedLink", CurrentMTD.RecommendedLink ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Theme", CurrentMTD.Theme);

                    cmd.Parameters.AddWithValue("@Id", CurrentMTD.Id);
                    cmd.ExecuteNonQuery();

                    #endregion

                    if (fullSave)
                    {
                        #region VideoStream

                        updateString = @"
                            UPDATE VideoStream
                               SET Format = @Format
                                   ,Format_Profile = @Format_Profile
                                   ,BitRateMode = @BitRateMode
                                   ,BitRate = @BitRate
                                   ,Width = @Width
                                   ,Height = @Height
                                   ,FrameRate_Mode = @FrameRate_Mode
                                   ,FrameRate = @FrameRate
                                   ,Delay = @Delay
                                   ,StreamSize = @StreamSize
                                   ,TitleEmbedded = @TitleEmbedded
                                   ,Language = @Language
                             WHERE Id = @Id";

                        foreach (var videoStream in CurrentMTD.VideoStreams)
                        {
                            cmd = new SqlCeCommand(updateString, conn);
                            cmd.Parameters.AddWithValue("@Format", videoStream.Format);
                            cmd.Parameters.AddWithValue("@Format_Profile", videoStream.Format_Profile);
                            cmd.Parameters.AddWithValue("@BitRateMode", videoStream.BitRateMode);
                            cmd.Parameters.AddWithValue("@BitRate", videoStream.BitRate);
                            cmd.Parameters.AddWithValue("@Width", videoStream.Width);
                            cmd.Parameters.AddWithValue("@Height", videoStream.Height);
                            cmd.Parameters.AddWithValue("@FrameRate_Mode", videoStream.FrameRate_Mode);
                            cmd.Parameters.AddWithValue("@FrameRate", videoStream.FrameRate);
                            cmd.Parameters.AddWithValue("@Delay", videoStream.Delay);
                            cmd.Parameters.AddWithValue("@StreamSize", videoStream.StreamSize);
                            cmd.Parameters.AddWithValue("@TitleEmbedded", videoStream.HasTitle ? videoStream.Title : string.Empty);
                            cmd.Parameters.AddWithValue("@Language", videoStream.Language);
                            cmd.Parameters.AddWithValue("@Id", videoStream.Id);
                            cmd.ExecuteNonQuery();
                        }

                        #endregion

                        #region AudioStreams

                        updateString = @"
                            UPDATE AudioStream
                               SET Format = @Format
                                   ,BitRate = @BitRate
                                   ,Channel = @Channel
                                   ,ChannelPosition = @ChannelPosition
                                   ,SamplingRate = @SamplingRate
                                   ,Resolution = @Resolution
                                   ,Delay = @Delay
                                   ,Video_Delay = @Video_Delay
                                   ,StreamSize = @StreamSize
                                   ,TitleEmbedded = @TitleEmbedded
                                   ,Language = @Language
                                   ,AudioSource = @AudioSource
                             WHERE Id = @Id";

                        foreach (var audioStream in CurrentMTD.AudioStreams)
                        {
                            cmd = new SqlCeCommand(updateString, conn);
                            cmd.Parameters.AddWithValue("@Format", audioStream.Format);
                            cmd.Parameters.AddWithValue("@BitRate", audioStream.BitRate);
                            cmd.Parameters.AddWithValue("@Channel", audioStream.Channel);
                            cmd.Parameters.AddWithValue("@ChannelPosition", audioStream.ChannelPosition);
                            cmd.Parameters.AddWithValue("@SamplingRate", audioStream.SamplingRate);
                            cmd.Parameters.AddWithValue("@Resolution", audioStream.Resolution);
                            cmd.Parameters.AddWithValue("@Delay", audioStream.Delay);
                            cmd.Parameters.AddWithValue("@Video_Delay", audioStream.Video_Delay);
                            cmd.Parameters.AddWithValue("@StreamSize", audioStream.StreamSize);
                            cmd.Parameters.AddWithValue("@TitleEmbedded", audioStream.HasTitle ? audioStream.Title : string.Empty);
                            cmd.Parameters.AddWithValue("@Language", audioStream.Language);
                            cmd.Parameters.AddWithValue("@AudioSource", audioStream.AudioSource);
                            cmd.Parameters.AddWithValue("@Id", audioStream.Id);
                            cmd.ExecuteNonQuery();
                        }

                        #endregion

                        #region TextStream

                        updateString = @"
                            UPDATE SubtitleStream
                               SET Format = @Format
                                   ,StreamSize = @StreamSize
                                   ,TitleEmbedded = @TitleEmbedded
                                   ,Language = @Language
                             WHERE Id = @Id";

                        foreach (var subtitleStream in CurrentMTD.SubtitleStreams)
                        {
                            cmd = new SqlCeCommand(updateString, conn);
                            cmd.Parameters.AddWithValue("@Format", subtitleStream.Format);
                            cmd.Parameters.AddWithValue("@StreamSize", subtitleStream.StreamSize);
                            cmd.Parameters.AddWithValue("@TitleEmbedded", subtitleStream.HasTitle ? subtitleStream.Title : string.Empty);
                            cmd.Parameters.AddWithValue("@Language", subtitleStream.Language);
                            cmd.Parameters.AddWithValue("@Id", subtitleStream.Id);
                            cmd.ExecuteNonQuery();
                        }

                        #endregion
                    }
                }

                if (!string.IsNullOrEmpty(CurrentMTD.Theme) && MovieThemes.IndexOf(CurrentMTD.Theme) == -1)
                {
                    MovieThemes.Add(CurrentMTD.Theme);
                    MovieThemes = MovieThemes.OrderBy(o => o).ToList(); //in case of changing a Theme, the old one and new one will both be in the list
                }
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }

            Helpers.UnsavedChanges = false; //this setter takes care of the buttons states!
            return result;
        }

        public static OperationResult LoadMTD(int fileDetailId, bool fullLoad)
        {
            var result = new OperationResult();
            var mtd = new MovieTechnicalDetails();

            try
            {
                using (var conn = new SqlCeConnection(Constants.ConnectionString))
                {
                    conn.Open();

                    var cmd = new SqlCeCommand(string.Format("SELECT * FROM FileDetail WHERE Id = {0}", fileDetailId), conn);

                    using (var reader = cmd.ExecuteResultSet(ResultSetOptions.Scrollable))
                    {
                        while (reader.Read())
                        {
                            mtd.Id = fileDetailId;
                            mtd.ParentId = reader["ParentId"] == DBNull.Value ? (int?)null : (int)reader["ParentId"];
                            mtd.FileName = reader["FileName"].ToString();
                            mtd.Year = reader["Year"].ToString();
                            mtd.Format = reader["Format"].ToString();
                            mtd.Encoded_Application = reader["Encoded_Application"].ToString();
                            mtd.FileSize = reader["FileSize"].ToString();
                            mtd.FileSize2 = reader["FileSize2"].ToString();
                            mtd.Title = reader["TitleEmbedded"].ToString();
                            mtd.Cover = reader["CoverEmbedded"].ToString();
                            mtd.Season = reader["Season"].ToString();
                            mtd.Theme = reader["Theme"].ToString();
                            mtd.StreamLink = reader["StreamLink"].ToString();
                            mtd.InsertedDate = reader["InsertedDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["InsertedDate"];
                            mtd.LastChangeDate = reader["LastChangeDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["LastChangeDate"];
                            mtd.Quality = reader["Quality"].ToString();
                            mtd.Recommended = reader["Recommended"].ToString();
                            mtd.RecommendedLink = reader["RecommendedLink"].ToString();
                            mtd.DescriptionLink = reader["DescriptionLink"].ToString();
                            mtd.Poster = reader["Poster"] == DBNull.Value ? null : (byte[])reader["Poster"];
                            mtd.Trailer = reader["Trailer"].ToString();
                            mtd.AudioLanguages = reader["AudioLanguages"].ToString();
                            mtd.SubtitleLanguages = reader["SubtitleLanguages"].ToString();
                            mtd.DurationFormatted = reader["Duration"] == DBNull.Value
                                                        ? "<not set>"
                                                        : ((DateTime)reader["Duration"]).ToString("HH:mm:ss");
                            mtd.Notes = reader["Notes"].ToString();

                            break;
                        }
                    }

                    if (fullLoad)
                    {
                        cmd = new SqlCeCommand(string.Format("SELECT * FROM VideoStream WHERE FileDetailId = {0} ORDER BY [Index]", fileDetailId), conn);

                        using (var reader = cmd.ExecuteResultSet(ResultSetOptions.Scrollable))
                        {
                            //if (reader.HasRows) return null;

                            while (reader.Read())
                            {
                                mtd.VideoStreams.Add(
                                    new VideoStreamInfo
                                    {
                                        Id = (int)reader["Id"],
                                        Index = (int)reader["Index"],
                                        Format = reader["Format"].ToString(),
                                        Format_Profile = reader["Format_Profile"].ToString(),
                                        BitRateMode = reader["BitRateMode"].ToString(),
                                        BitRate = reader["BitRate"].ToString(),
                                        Width = reader["Width"].ToString(),
                                        Height = reader["Height"].ToString(),
                                        FrameRate_Mode = reader["FrameRate_Mode"].ToString(),
                                        FrameRate = reader["FrameRate"].ToString(),
                                        Delay = reader["Delay"].ToString(),
                                        StreamSize = reader["StreamSize"].ToString(),
                                        Title = reader["TitleEmbedded"].ToString(),
                                        Language = reader["Language"].ToString()
                                    });
                            }
                        }

                        cmd = new SqlCeCommand(string.Format("SELECT * FROM AudioStream WHERE FileDetailId = {0} ORDER BY [Index]", fileDetailId), conn);

                        using (var reader = cmd.ExecuteResultSet(ResultSetOptions.Scrollable))
                        {
                            //if (!reader.HasRows) return null;

                            while (reader.Read())
                            {
                                mtd.AudioStreams.Add(
                                    new AudioStreamInfo
                                    {
                                        Id = (int)reader["Id"],
                                        Index = (int)reader["Index"],
                                        Format = reader["Format"].ToString(),
                                        BitRate = reader["BitRate"].ToString(),
                                        Channel = reader["Channel"].ToString(),
                                        ChannelPosition = reader["ChannelPosition"].ToString(),
                                        SamplingRate = reader["SamplingRate"].ToString(),
                                        Resolution = reader["Resolution"].ToString(),
                                        Delay = reader["Delay"].ToString(),
                                        Video_Delay = reader["Video_Delay"].ToString(),
                                        StreamSize = reader["StreamSize"].ToString(),
                                        Title = reader["TitleEmbedded"].ToString(),
                                        Language = reader["Language"].ToString(),
                                        AudioSource = (int)reader["AudioSource"]
                                    });
                            }
                        }

                        cmd = new SqlCeCommand(string.Format("SELECT * FROM SubtitleStream WHERE FileDetailId = {0} ORDER BY [Index]", fileDetailId), conn);

                        using (var reader = cmd.ExecuteResultSet(ResultSetOptions.Scrollable))
                        {
                            //if (!reader.HasRows) return null;

                            while (reader.Read())
                            {
                                mtd.SubtitleStreams.Add(
                                    new SubtitleStreamInfo
                                    {
                                        Id = (int)reader["Id"],
                                        Index = (int)reader["Index"],
                                        Format = reader["Format"].ToString(),
                                        StreamSize = reader["StreamSize"].ToString(),
                                        Title = reader["TitleEmbedded"].ToString(),
                                        Language = reader["Language"].ToString()
                                    });
                            }
                        }
                    }
                }

                CurrentMTD = mtd;
                result.AdditionalDataReturn = mtd;
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }

            return result;
        }

        public static CachedMovieStills LoadMovieStills(int fileDetailId)
        {
            var result = new CachedMovieStills { FileDetailId = fileDetailId };

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                var cmd = new SqlCeCommand(string.Format("SELECT * FROM Thumbnails WHERE FileDetailId = {0}", fileDetailId), conn);

                using (var reader = cmd.ExecuteResultSet(ResultSetOptions.Scrollable))
                {
                    if (!reader.HasRows) return result;

                    while (reader.Read())
                    {
                        result.MovieStills.Add((byte[])reader["MovieStill"]);
                    }
                }
            }

            return result;
        }

        public static string GetQualityStrFromSize(MovieTechnicalDetails mtd)
        {
            if (mtd == null || mtd.VideoStreams == null) return "Error!";

            var firstVideoStream = mtd.VideoStreams.FirstOrDefault();
            if (firstVideoStream == null)
                throw new Exception("Unknown!");

            int.TryParse(firstVideoStream.Height, out var videoHeight);
            int.TryParse(firstVideoStream.Width, out var videoWidth);

            if (videoHeight == 0 || videoWidth == 0)
                return "NotSet";

            if (videoWidth > 1300)
                return "FullHD";

            return
                videoWidth == 1280 || videoHeight > 710
                    ? "HD"
                    : "SD";
        }

        public static OperationResult RemoveSeason(int seriesId, int seasonNo)
        {
            var result = new OperationResult();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                try
                {
                    conn.Open();

                    var cmd = new SqlCeCommand(string.Format("SELECT Id FROM FileDetail WHERE ParentId = {0} AND Season = {1}", seriesId, seasonNo), conn);

                    var episodeIds = new List<int>();

                    using (var reader = cmd.ExecuteResultSet(ResultSetOptions.Scrollable))
                    {
                        while (reader.Read())
                        {
                            episodeIds.Add((int)reader["Id"]);
                        }
                    }

                    result = RemoveData(string.Join(",", episodeIds), conn);
                }
                catch (Exception ex)
                {
                    return result.FailWithMessage(ex);
                }
            }

            return result;
        }

        public static OperationResult RemoveSeries(int seriesId)
        {
            var result = new OperationResult();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                try
                {
                    conn.Open();

                    var cmd = new SqlCeCommand(string.Format("SELECT Id FROM FileDetail WHERE ParentId = {0}", seriesId), conn);

                    var episodeIds = new List<int> { seriesId };

                    using (var reader = cmd.ExecuteResultSet(ResultSetOptions.Scrollable))
                    {
                        while (reader.Read())
                        {
                            episodeIds.Add((int)reader["Id"]);
                        }
                    }

                    result = RemoveData(string.Join(",", episodeIds), conn);
                }
                catch (Exception ex)
                {
                    return result.FailWithMessage(ex);
                }
            }

            return result;
        }

        public static OperationResult RemoveMovieOrEpisode(int fileDetailId)
        {
            var result = new OperationResult();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                try
                {
                    conn.Open();

                    result = RemoveData(fileDetailId.ToString(), conn);
                }
                catch (Exception ex)
                {
                    return result.FailWithMessage(ex);
                }
            }

            return result;
        }

        private static OperationResult RemoveData(string episodeIds, SqlCeConnection conn, bool removeFromFileDetail = true)
        {
            var result = new OperationResult();

            try
            {
                var tableNames =
                    removeFromFileDetail
                        ? new[] { "VideoStream", "Thumbnails", "AudioStream", "SubtitleStream", "FileDetail" }
                        : new[] { "VideoStream", "Thumbnails", "AudioStream", "SubtitleStream" };

                foreach (var tableName in tableNames)
                {
                    var cmd =
                        new SqlCeCommand(
                            string.Format("DELETE FROM {0} WHERE {1} IN ({2})",
                                tableName,
                                tableName == "FileDetail" ? "Id" : "FileDetailId",
                                episodeIds),
                            conn);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }

            return result;
        }

        public static OperationResult GetStatistics(bool forSeries)
        {
            var result = new OperationResult();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                try
                {
                    conn.Open();

                    var sqlString = string.Empty;
                    var resultStr = "There are no movies in the list ...";

                    if (forSeries)
                    {
                        sqlString = @"
                            SELECT
	                            0 AS Pos,
	                            '0' AS Size,

	                            COUNT(*) AS CountOf
                            FROM FileDetail
                            WHERE ParentId = -1
                            HAVING COUNT(*) > 0

                            UNION

                            SELECT
	                            1 AS Pos,
	                            CASE
		                            WHEN (SUM(CONVERT(bigint, FileSize)) / 1024 / 1024) > 1024
		                            THEN CONVERT(NVARCHAR(10), CONVERT(numeric(18,2), SUM(CONVERT(numeric(18,2), FileSize)) / CONVERT(numeric(18,2), 1024 * 1024 * 1024))) + ' Gb'
		                            ELSE CONVERT(NVARCHAR(10), SUM(CONVERT(bigint, FileSize)) / 1024 / 1024) + ' Mb'
	                            END AS Size,

	                            COUNT(*) AS CountOf
                            FROM FileDetail
                            WHERE ParentId > 0
                            HAVING COUNT(*) > 0

                            ORDER BY Pos";

                        var cmd = new SqlCeCommand(sqlString, conn);
                        SectionDetails = string.Empty;

                        var seriesCount = 0;
                        var episodesCount = 0;
                        var episodesSize = "0";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if ((int)reader["Pos"] == 0)
                                {
                                    seriesCount = (int)reader["CountOf"];
                                }
                                else
                                {
                                    episodesCount = (int)reader["CountOf"];
                                    episodesSize = (string)reader["Size"];
                                }
                            }
                        }

                        if (seriesCount > 0)
                        {
                            resultStr =
                                string.Format("There are currently {0} Series in list, summing {1} episodes, having a total size of {2}.",
                                    seriesCount,
                                    episodesCount,
                                    episodesSize);
                        }

                        result.AdditionalDataReturn = resultStr;
                    }
                    else
                    {
                        sqlString = @"
                            SELECT
                                1 AS Pos,
	                            CASE
		                            WHEN (SUM(CONVERT(bigint, FileSize)) / 1024 / 1024) > 1024
		                            THEN CONVERT(NVARCHAR(10), CONVERT(numeric(18,2), SUM(CONVERT(numeric(18,2), FileSize)) / CONVERT(numeric(18,2), 1024 * 1024 * 1024))) + ' Gb'
		                            ELSE CONVERT(NVARCHAR(10), SUM(CONVERT(bigint, FileSize)) / 1024 / 1024) + ' Mb'
	                            END AS Size,

	                            COUNT(*) AS MoviesCount,

	                            Quality
                            FROM FileDetail
                            WHERE ParentId IS NULL
                            GROUP BY Quality
							HAVING COUNT(*) > 0

                            UNION

                            SELECT
                                0 AS Pos,
	                            CASE
		                            WHEN (SUM(CONVERT(bigint, FileSize)) / 1024 / 1024) > 1024
		                            THEN CONVERT(NVARCHAR(10), CONVERT(numeric(18,2), SUM(CONVERT(numeric(18,2), FileSize)) / CONVERT(numeric(18,2), 1024 * 1024 * 1024))) + ' Gb'
		                            ELSE CONVERT(NVARCHAR(10), SUM(CONVERT(bigint, FileSize)) / 1024 / 1024) + ' Mb'
	                            END AS Size,

	                            COUNT(*) AS MoviesCount,

	                            'ALL' AS Quality
                            FROM FileDetail
                            WHERE ParentId IS NULL
							HAVING COUNT(*) > 0

                            ORDER BY Pos, Quality";

                        var cmd = new SqlCeCommand(sqlString, conn);
                        SectionDetails = string.Empty;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if ((int)reader["Pos"] == 0)
                                {
                                    resultStr =
                                        string.Format("There are currently {0} movies in list, having a total size of {1}.",
                                            reader["MoviesCount"],
                                            reader["Size"]);
                                }
                                else
                                {
                                    SectionDetails +=
                                        string.Format("{0}{1} @ {2} ~ {3}",
                                            Environment.NewLine,
                                            reader["MoviesCount"],
                                            reader["Quality"],
                                            reader["Size"]);


                                }
                            }
                        }

                        result.AdditionalDataReturn = resultStr;
                    }
                }
                catch (Exception ex)
                {
                    return result.FailWithMessage(ex);
                }
            }

            return result;
        }

        public static List<MovieForWeb> GetMoviesForWeb()
        {
            var result = new List<MovieForWeb>();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource = new SqlCeCommand(@"
                    SELECT
                        vs.BitRate,
	                    CASE
                            WHEN Poster IS NULL THEN CONVERT(BIT, 0)
                            ELSE CONVERT(BIT, 1)
	                    END AS HasPoster,
                        fd.*
                    FROM FileDetail fd
	                    LEFT OUTER JOIN VideoStream vs ON fd.Id = vs.FileDetailId
                    WHERE ParentId IS NULL ORDER BY FileName", conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var duration =
                            reader["Duration"] != DBNull.Value
                                ? (DateTime?)reader["Duration"]
                                : null;

                        var mfw = new MovieForWeb();
                        mfw.Id = (int)reader["Id"];
                        mfw.FN = reader["FileName"].ToString();
                        mfw.R = reader["Recommended"].ToString();
                        mfw.RL = reader["RecommendedLink"].ToString();
                        mfw.Y = reader["Year"].ToString();
                        mfw.Q = reader["Quality"].ToString();
                        mfw.S = reader["FileSize2"].ToString();
                        mfw.B = reader["BitRate"].ToString();
                        mfw.L = duration == null ? "?" : ((DateTime)duration).ToString("HH:mm:ss");
                        mfw.A = reader["AudioLanguages"].ToString();
                        mfw.SU = reader["SubtitleLanguages"].ToString();

                        mfw.DL = reader["DescriptionLink"].ToString();
                        mfw.T = reader["Theme"].ToString();
                        mfw.N = reader["Notes"].ToString();
                        mfw.Nl = string.Empty;
                        mfw.Tr = GetTrailerId(reader["Trailer"].ToString());

                        //mfw.Cover = reader["Poster"] == DBNull.Value ? null : (byte[])reader["Poster"];
                        mfw.HasPoster = (bool)reader["HasPoster"];

                        var insertedDate =
                            reader["InsertedDate"] == DBNull.Value //SD or bad imports
                                ? new DateTime(1970, 1, 1)
                                : (DateTime)reader["InsertedDate"];

                        mfw.InsertedDate = insertedDate;

                        mfw.LastChangeDate =
                            reader["LastChangeDate"] == DBNull.Value
                                ? insertedDate
                                : (DateTime)reader["LastChangeDate"];

                        result.Add(mfw);
                    }
                }
            }

            return result;
        }

        public static byte[] GetPoster(int id)
        {
            byte[] result = null;

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                const string sqlString = @"
                    SELECT Poster FROM FileDetail WHERE Id = @id";

                var cmd = new SqlCeCommand(sqlString, conn);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader["Poster"] == DBNull.Value ? null : (byte[])reader["Poster"];
                        break;
                    }
                }
            }

            return result;
        }

        private static string GetTrailerId(string trailerLink)
        {
            if (string.IsNullOrEmpty(trailerLink))
                return string.Empty;

            var s = trailerLink.IndexOf("youtu.be") >= 0
                ? "https://www.youtube.com/watch?v=" + trailerLink.Substring(trailerLink.LastIndexOf("/") + 1) //no param => takes all from LastIndexOf !
                : trailerLink;

            return s.Replace("https://www.youtube.com/watch?v=", "");
        }

        public static List<SeriesForWeb> GetSeriesForWeb(SeriesType st)
        {
            var result = new List<SeriesForWeb>();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                var cmd = new SqlCeCommand(@"
                    SELECT
                        fd.Id,
                        fd.FileName,
                        fd.Recommended,
                        fd.RecommendedLink,
                        fd.DescriptionLink,
                        fd.Notes,
	                    CASE
                            WHEN fd.Poster IS NULL THEN CONVERT(BIT, 0)
                            ELSE CONVERT(BIT, 1)
	                    END AS HasPoster
                    FROM FileDetail fd
                    WHERE fd.ParentId = @seriesType
                    ORDER BY fd.FileName", conn);

                cmd.Parameters.AddWithValue("@seriesType", (int)st);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sfw = new SeriesForWeb();
                        sfw.Id = (int)reader["Id"];
                        sfw.FN = reader["FileName"].ToString();
                        sfw.R = reader["Recommended"].ToString();
                        sfw.RL = reader["RecommendedLink"].ToString();
                        sfw.DL = reader["DescriptionLink"].ToString();
                        sfw.N = reader["Notes"].ToString();
                        //sfw.Cover = reader["Poster"] == DBNull.Value ? null : (byte[])reader["Poster"];
                        sfw.HasPoster = (bool)reader["HasPoster"];

                        result.Add(sfw);
                    }
                }
            }

            return result;
        }

        public static List<EpisodesForWeb> GetEpisodesForWeb(bool preserveMarkesForExistingThumbnails, SeriesType st)
        {
            var result = new List<EpisodesForWeb>();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                var cmd = new SqlCeCommand(@"
                        SELECT
                            fd.Id,
                            fd.ParentId,
                            fd.FileName,
                            fd.Season,
                            fd.Year,
                            fd.Quality,
                            fd.Duration,
                            fd.FileSize2,
                            fd.FileSize,
                            fd.AudioLanguages,
                            fd.SubtitleLanguages,
                            fd.Theme,
                            fd.Notes,
                            fd.InsertedDate,
                            fd.LastChangeDate
                        FROM FileDetail fd
                            LEFT OUTER JOIN FileDetail p ON p.Id = fd.ParentId
                        WHERE fd.ParentId > 0 AND p.ParentId = @seriesType
                        ORDER BY ParentId, Season, FileName", conn);

                /*
                LEFT JOIN (
                    SELECT FileDetailId, MIN(Id) AS MinId
                    FROM Thumbnails
                    GROUP BY FileDetailId
                ) t on t.FileDetailId = fd.Id

                +

                IsNUll -> Has/Not Thumbnails
             */
                cmd.Parameters.AddWithValue("@seriesType", (int)st);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var duration =
                            reader["Duration"] != DBNull.Value
                                ? (DateTime?)reader["Duration"]
                                : null;

                        var seasonNo = 0;
                        if (reader["Season"].ToString() != string.Empty)
                            int.TryParse(reader["Season"].ToString(), out seasonNo);

                        var efw = new EpisodesForWeb();
                        efw.Id = (int)reader["Id"];
                        efw.SId = (int)reader["ParentId"];
                        efw.FN = reader["FileName"].ToString();
                        efw.SZ = seasonNo;
                        efw.Y = reader["Year"].ToString();
                        efw.Q = reader["Quality"].ToString();
                        efw.L = duration == null ? "?" : ((DateTime)duration).ToString("HH:mm:ss");
                        efw.S = reader["FileSize2"].ToString();

                        if (reader["FileSize"] == DBNull.Value)
                            efw.Si = 0;
                        else
                        {
                            long siLong = 0;

                            if (long.TryParse(reader["FileSize"].ToString(), out siLong))
                                efw.Si = siLong;
                            else
                                efw.Si = 0;
                        }

                        //efw.Si = reader["FileSize"] != DBNull.Value ? (long)reader["FileSize"] : -1;
                        efw.A = reader["AudioLanguages"].ToString();
                        efw.SU = reader["SubtitleLanguages"].ToString();
                        efw.T = reader["Theme"].ToString();
                        efw.N = reader["Notes"].ToString();

                        var insertedDate =
                            reader["InsertedDate"] == DBNull.Value //SD or bad imports
                                ? new DateTime(1970, 1, 1)
                                : (DateTime)reader["InsertedDate"];

                        efw.InsertedDate = insertedDate;

                        efw.LastChangeDate =
                            reader["LastChangeDate"] == DBNull.Value
                                ? insertedDate
                                : (DateTime)reader["LastChangeDate"];

                        //if (loadThumbnails)
                        //{
                        //    efw.MovieStills = LoadMovieStills(efw.Id).MovieStills;
                        //}

                        result.Add(efw);
                    }
                }
            }

            return result;
        }

        public static void FillSeriesDataFromEpisodes(ref List<SeriesForWeb> series,
            List<EpisodesForWeb> episodes)
        {
            foreach (var s in series)
            {
                var episodesForSeries = episodes.Where(e => e.SId == s.Id).OrderBy(o => o.SZ).ToList();

                var minYear = episodesForSeries.Min(e => e.Y);
                var maxYear = episodesForSeries.Max(e => e.Y);

                s.Y = minYear != maxYear
                    ? string.Format("{0} - {1}", minYear, maxYear)
                    : string.IsNullOrEmpty(minYear) && string.IsNullOrEmpty(maxYear)
                        ? "?"
                        : string.IsNullOrEmpty(minYear)
                            ? maxYear
                            : minYear;

                s.S = Math.Round((decimal)episodesForSeries.Sum(e => e.Si) / 1024 / 1024 / 1024, 2).ToString();

                s.Q = string.Join(", ", episodesForSeries.DistinctBy(e => e.Q).Select(e => e.Q).ToList());

                //set the distinct audios in one single line
                //ro, en        |
                //en, nl        | => ro, en, en, nl, ro, en, nl
                //ro, en, nl    |
                var audios = string.Join(",", episodesForSeries.Select(d => d.A).Distinct()).Replace(", ", ",");
                s.A = string.Join(", ", audios.Split(',').Distinct());

                s.Ec = episodesForSeries.Count.ToString();
                /*
                var episodesQuality = string.Join(", ", episodesForSeries.DistinctBy(e => e.Q).Select(e => e.Q).ToList());
                var x = 1;
                                /*
                if (episodesQuality.Any())
                {
                    if (episodesQuality.Count == 1)
                    {
                        s.Q = (Quality)episodesQuality.FirstOrDefault().Q;
                    }
                    else
                    {
                        if (calitateEpisoade.Max(c => c) == 1)
                            serial.Calitate = Calitate.HD_up;

                        if (calitateEpisoade.All(c => c == 2))
                            serial.Calitate = Calitate.SD;
                        else
                        {
                            if (calitateEpisoade.Max(c => c) == 2)
                            {
                                serial.Calitate = Calitate.Mix;
                            }
                        }
                    }
                }

                if (episoadeSerial.Count > 0)
                {
                    var audios = episoadeSerial.Select(d => d.Audio).Distinct();

                    if (audios.Count() == 1)
                    {
                        serial.DifferentAudio = false;
                        serial.Audio = audios.FirstOrDefault();
                    }
                    else
                    {
                        var distinctCount
                            = episoadeSerial.GroupBy(l => l.Audio)
                                            //.OrderByDescending(g => g.Count()) // cele mai multe
                                            .OrderByDescending(g => g.Key.Length)
                                            .Select(g => new { Audio = g.Key, Count = g.Count() });

                        serial.DifferentAudio = true;
                        serial.Audio = distinctCount.FirstOrDefault().Audio;
                    }
                }*/
            }
        }

        public static List<SeriesEpisodesShortInfo> GetCollectionsInfo()
        {
            var result = new List<SeriesEpisodesShortInfo>();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                /*
                 *SELECT fd1.Id, fd1.FileName, sum()
                    FROM FileDetail fd1
                        LEFT OUTER JOIN FileDetail fd2 ON fd1.Id = fd2.ParentId
                    WHERE fd1.ParentId = -1
                    ORDER BY fd1.FileName
                 */
                var commandSource = new SqlCeCommand("SELECT * FROM FileDetail WHERE ParentId = -10 ORDER BY FileName", conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SeriesEpisodesShortInfo
                        {
                            Id = (int)reader["Id"],
                            FileName = reader["FileName"].ToString(),
                            Season = -1,
                            SeriesId = (int)reader["Id"],
                            IsSeries = true
                        });
                    }
                }
            }

            return result;
        }

        public static OperationResult InsertCollection(MovieTechnicalDetails mtd)
        {
            var result = new OperationResult();

            try
            {
                using (var conn = new SqlCeConnection(Constants.ConnectionString))
                {
                    conn.Open();
                    SqlCeCommand cmd;

                    #region Checks

                    var checks = "SELECT COUNT(*) FROM FileDetail WHERE ParentId = @ParentId AND FileName = @FileName";

                    cmd = new SqlCeCommand(checks, conn);
                    cmd.Parameters.AddWithValue("@ParentId", mtd.ParentId);
                    cmd.Parameters.AddWithValue("@FileName", mtd.FileName);
                    cmd.Parameters.AddWithValue("@FileSize", mtd.FileSize);

                    var count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                        return result.FailWithMessage("A Collection with exactly the same name already exists!");


                    #endregion

                    #region FileDetail

                    var insertString = @"
                        INSERT INTO FileDetail (
                            FileName,
                            ParentId,
                            Notes
                        )
                        VALUES (
                            @FileName,
                            @ParentId,
                            @Notes
                        )";

                    cmd = new SqlCeCommand(insertString, conn);
                    cmd.Parameters.AddWithValue("@FileName", mtd.FileName);
                    cmd.Parameters.AddWithValue("@ParentId", mtd.ParentId);
                    cmd.Parameters.AddWithValue("@Notes", mtd.Notes ?? (object)DBNull.Value);

                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Select @@Identity";

                    var newFileDetailId = (int)(decimal)cmd.ExecuteScalar();
                    result.AdditionalDataReturn = newFileDetailId;

                    #endregion
                }
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }

            return result;
        }
        public static List<SeriesEpisodesShortInfo> GetElementsInCollection(int collectionId)
        {
            var result = new List<SeriesEpisodesShortInfo>();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource = new SqlCeCommand(string.Format("SELECT * FROM FileDetail WHERE ParentId = {0} ORDER BY FileName", collectionId), conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SeriesEpisodesShortInfo
                        {
                            Id = (int)reader["Id"],
                            FileName = reader["FileName"].ToString(),
                            Theme = reader["Theme"].ToString(),
                            Quality = reader["Quality"].ToString(),
                            Season = 0,
                            SeriesId = (int)reader["ParentId"],
                            IsEpisode = true
                        });
                    }
                }
            }

            return result;
        }

        public static OperationResult SaveBulkChanges(List<int> selectedEpisodes, List<BulkEditField> fieldValuesControls)
        {
            var result = new OperationResult();
            var episodes = string.Join(",", selectedEpisodes);

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();
                var tx = conn.BeginTransaction();

                try
                {
                    try
                    {
                        foreach (var fvc in fieldValuesControls)
                        {
                            var cmd = new SqlCeCommand(string.Format(@"
                                UPDATE FileDetail
                                   SET {0} = {1}
                                WHERE Id IN ({2})",
                                fvc.FieldName, fvc.Value, episodes), conn);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        return result.FailWithMessage(ex);
                    }
                }
                finally
                {
                    tx.Commit();
                }
            }

            return result;
        }

        //public static string GetMoviesDetails2()
        //{
        //    var result = new List<MovieForWeb>();

        //    using (var conn = new SqlCeConnection(Constants.ConnectionString))
        //    {
        //        conn.Open();

        //        var commandSource = new SqlCeCommand(@"
        //            SELECT
        //                vs.BitRate,
        //             CASE
        //                    WHEN Poster IS NULL THEN CONVERT(BIT, 0)
        //                    ELSE CONVERT(BIT, 1)
        //             END AS HasPoster,
        //                fd.*
        //            FROM FileDetail fd
        //             LEFT OUTER JOIN VideoStream vs ON fd.Id = vs.FileDetailId
        //            WHERE ParentId IS NULL ORDER BY FileName", conn);

        //        using (var reader = commandSource.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                var duration =
        //                    reader["Duration"] != DBNull.Value
        //                        ? (DateTime?)reader["Duration"]
        //                        : null;

        //                var mfw = new MovieForWeb();
        //            }
        //        }
        //    }

        //    return "";
        //}

        public static List<MovieForWeb> GetMoviesForPDF(PdfGenParams pdfGenParams)
        {
            var result = new List<MovieForWeb>();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource = new SqlCeCommand(string.Format(@"
                        SELECT
	                        CASE
                                WHEN Poster IS NULL THEN CONVERT(BIT, 0)
                                ELSE CONVERT(BIT, 1)
	                        END AS HasPoster,
                            fd.*
                        FROM FileDetail fd
                        WHERE {0} {1}
                        ORDER BY FileName",
                        pdfGenParams.ForMovies
                            ? "ParentId IS NULL"
                            : "ParentId = -1",
                        pdfGenParams.PDFGenType == PDFGenType.Christmas
                            ? "AND Theme = 'Craciun'"
                            : pdfGenParams.PDFGenType == PDFGenType.Helloween
                                ? "AND Theme = 'Helloween'"
                                : string.Empty
                        ), conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var duration =
                            reader["Duration"] != DBNull.Value
                                ? (DateTime?)reader["Duration"]
                                : null;

                        var mfw = new MovieForWeb();
                        mfw.Id = (int)reader["Id"];
                        mfw.FN = reader["FileName"].ToString();
                        mfw.R = reader["Recommended"].ToString();
                        mfw.RL = reader["RecommendedLink"].ToString();
                        mfw.Y = reader["Year"].ToString();
                        mfw.Q = reader["Quality"].ToString();
                        mfw.S = reader["FileSize2"].ToString();
                        //mfw.B = reader["BitRate"].ToString();
                        mfw.L = duration == null ? "?" : ((DateTime)duration).ToString("HH:mm:ss");
                        mfw.A = reader["AudioLanguages"].ToString();
                        mfw.SU = reader["SubtitleLanguages"].ToString();

                        mfw.DL = reader["DescriptionLink"].ToString();
                        mfw.T = reader["Theme"].ToString();
                        mfw.N = reader["Notes"].ToString();
                        mfw.Nl = string.Empty;
                        mfw.Tr = GetTrailerId(reader["Trailer"].ToString());

                        //mfw.Cover = reader["Poster"] == DBNull.Value ? null : (byte[])reader["Poster"];
                        mfw.HasPoster = (bool)reader["HasPoster"];

                        if (mfw.HasPoster)
                            mfw.Cover = (byte[])reader["Poster"];

                        var insertedDate =
                            reader["InsertedDate"] == DBNull.Value //SD or bad imports
                                ? new DateTime(1970, 1, 1)
                                : (DateTime)reader["InsertedDate"];

                        mfw.InsertedDate = insertedDate;

                        mfw.LastChangeDate =
                            reader["LastChangeDate"] == DBNull.Value
                                ? insertedDate
                                : (DateTime)reader["LastChangeDate"];

                        result.Add(mfw);
                    }
                }

                if (!pdfGenParams.ForMovies)
                {
                    var episodes = GetEpisodesForWeb(true, SeriesType.Final);

                    foreach (var s in result)
                    {
                        var episodesForSeries = episodes.Where(e => e.SId == s.Id).OrderBy(o => o.SZ).ToList();

                        var audios = string.Join(",", episodesForSeries.Select(d => d.A).Distinct()).Replace(", ", ",");
                        s.A = string.Join(", ", audios.Split(',').Distinct());

                        s.B = episodesForSeries.Count().ToString();
                    }
                }
            }

            return result;
        }

        public static int GetCount(bool forMovies, PDFGenType pdfGenType = PDFGenType.All)
        {
            var result = -1;

            try
            {
                using (var conn = new SqlCeConnection(Constants.ConnectionString))
                {
                    conn.Open();

                    var sqlStrig =
                        forMovies
                            ? string.Format(@"SELECT
                                    COUNT(1) AS CNT
                                FROM FileDetail fd
                                WHERE ParentId IS NULL {0}",
                                pdfGenType == PDFGenType.Christmas
                                    ? "AND Theme = 'Craciun'"
                                    : pdfGenType == PDFGenType.Helloween
                                        ? "AND Theme = 'Helloween'"
                                        : string.Empty
                                )
                            : @"SELECT
                                    COUNT(1) AS CNT
                                FROM FileDetail fd
                                WHERE ParentId = -1";

                    var commandSource = new SqlCeCommand(sqlStrig, conn);

                    using (var reader = commandSource.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return (int)reader["CNT"];
                        }
                    }
                }
            }
            catch (Exception)
            {
                result = -2;
            }

            return result;
        }
    }
}
