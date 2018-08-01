using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;

using DAL;

namespace Desene
{
    public static class DAL
    {
        public static List<CachedMovieStills> CachedMoviesStills = new List<CachedMovieStills>();
        public static MovieTechnicalDetails CurrentMTD;
        public static List<string> MovieThemes = new List<string>();

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

                var commandSource = new SqlCeCommand("SELECT * FROM FileDetail WHERE ParentId IS NULL ORDER BY FileName", conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new MovieShortInfo
                                    {
                                        Id = (int)reader["Id"],
                                        FileName = reader["FileName"].ToString(),
                                        HasPoster = reader["Poster"].GetType() != typeof(DBNull)
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
                 *
                 */
                var commandSource = new SqlCeCommand("SELECT * FROM FileDetail WHERE ParentId = -1 ORDER BY FileName", conn);

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
                                        FileName = string.Format("Season {0}", seasonVal),
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

        public static DataTable GetEpisodesInSeries(int seriesId)
        {
            var result = new DataTable();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource = new SqlCeCommand(string.Format("SELECT * FROM FileDetail WHERE ParentId = {0} ORDER BY Season, FileName", seriesId), conn);

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
            string recommendedLink, string notes, byte[] poster)
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
                            ParentId)
                        VALUES (
                            @FileName,
                            @InsertedDate,
                            @Recommended,
                            @RecommendedLink,
                            @DescriptionLink,
                            @Poster,
                            @Notes,
                            -1)";

                    var cmd = new SqlCeCommand(insertString, conn);
                    cmd.Parameters.AddWithValue("@FileName", title);
                    cmd.Parameters.AddWithValue("@InsertedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Recommended", recommended);
                    cmd.Parameters.AddWithValue("@RecommendedLink", recommendedLink);
                    cmd.Parameters.AddWithValue("@DescriptionLink", descriptionLink);
                    cmd.Parameters.AddWithValue("@Poster", poster ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Notes", notes);

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
                        var checks = "select count(*) from FileDetail where ParentId = @ParentId AND FileName = @FileName AND FileSize = @FileSize";

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
                        var checks = "select count(*) from FileDetail where FileName = @FileName";

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
                            Quality,
                            ParentId,
                            AudioLanguages,
                            SubtitleLanguages,

                            DescriptionLink,
                            Recommended,
                            RecommendedLink,
                            Theme,
                            Notes,
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
                            @Quality,
                            @ParentId,
                            @AudioLanguages,
                            @SubtitleLanguages,

                            @DescriptionLink,
                            @Recommended,
                            @RecommendedLink,
                            @Theme,
                            @Notes,
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
                    cmd.Parameters.AddWithValue("@InsertedDate", DateTime.Now);



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
                    cmd.Parameters.AddWithValue("@StreamLink", mtd.StreamLink ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Poster", mtd.Poster ?? (object)DBNull.Value);

                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Select @@Identity";

                    var newFileDetailId = (int)(decimal)cmd.ExecuteScalar();
                    result.AdditionalDataReturn = newFileDetailId;

                    #endregion

                    #region Thumbnails

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
                        cmd.Parameters.AddWithValue("@TitleEmbedded", audioStream.Title);;
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
                        cmd.Parameters.AddWithValue("@TitleEmbedded", subtitleStream.Title);;
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
            var fileSize = 0;

            try
            {
                if (fileSize2.Trim() != string.Empty && fileSize2.ToUpper().EndsWith("GB") || fileSize2.ToUpper().EndsWith("MB") )
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
            catch (Exception ex)
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

                    var updateString = @"
                        UPDATE FileDetail
                           SET FileName = @FileName
                               ,Year = @Year
                               ,Format = @Format
                               ,Encoded_Application = @Encoded_Application
                               ,FileSize = @FileSize
                               ,FileSize2 = @FileSize2
                               ,Duration = @Duration
                               ,TitleEmbedded = @TitleEmbedded
                               ,Season = @Season
                               ,Quality = @Quality
                               ,AudioLanguages = @AudioLanguages
                               ,SubtitleLanguages = @SubtitleLanguages
                               ,LastChangeDate = @LastChangeDate
                               ,Poster = @Poster
                               ,Notes = @Notes
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
                    cmd.Parameters.AddWithValue("@FileSize", GetFileSize(CurrentMTD.FileSize2));     //as int value, for totals calculations
                    cmd.Parameters.AddWithValue("@FileSize2", CurrentMTD.FileSize2);                 //GUI

                    //todo: save the value from DurationAsInt if the TryParse fails
                    cmd.Parameters.AddWithValue("@Duration", new DateTime(1970, 1, 1).Add(TimeSpan.TryParse(CurrentMTD.DurationFormatted, out var durationAsTime) ? durationAsTime : new TimeSpan()));
                    cmd.Parameters.AddWithValue("@TitleEmbedded", CurrentMTD.HasTitle ? CurrentMTD.Title : string.Empty);
                    cmd.Parameters.AddWithValue("@CoverEmbedded", CurrentMTD.Cover);
                    cmd.Parameters.AddWithValue("@Season", CurrentMTD.Season);
                    cmd.Parameters.AddWithValue("@Quality", CurrentMTD.Quality);

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
                            cmd.Parameters.AddWithValue("@TitleEmbedded", videoStream.HasTitle ?  videoStream.Title : string.Empty);
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
                            cmd.Parameters.AddWithValue("@TitleEmbedded", audioStream.HasTitle ?  audioStream.Title : string.Empty);
                            cmd.Parameters.AddWithValue("@Language", audioStream.Language);
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
                            cmd.Parameters.AddWithValue("@TitleEmbedded", subtitleStream.HasTitle ?  subtitleStream.Title : string.Empty);
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
                            mtd.InsertedDate = reader["LastChangeDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["LastChangeDate"];
                            mtd.Quality = reader["Quality"].ToString();
                            mtd.Recommended = reader["Recommended"].ToString();
                            mtd.RecommendedLink = reader["RecommendedLink"].ToString();
                            mtd.DescriptionLink = reader["DescriptionLink"].ToString();
                            mtd.Poster = reader["Poster"] == DBNull.Value ? null : (byte[])reader["Poster"];
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
                                            Language = reader["Language"].ToString()
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

            return
                videoHeight == 0
                    ? "NotSet"
                    : videoHeight > 900
                        ? "FullHD"
                        : videoHeight < 710
                            ? "SD"
                            : "HD";
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

        private static OperationResult RemoveData(string episodeIds, SqlCeConnection conn)
        {
            var result = new OperationResult();

            try
            {
                var tableNames = new[] { "VideoStream", "Thumbnails", "AudioStream", "SubtitleStream", "FileDetail" };

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









        //public static void InitConnection(string connectionString)
        //{
        //    dbConnection = new SqlCeConnection(ConnectionString);
        //    dbConnection.Open();
        //}


        //public static BindingList<Film> Load_FilmeHD()
        //{
        //    var result = new BindingList<Film>();

        //    var query = "SELECT * FROM Filme ORDER BY Titlu";
        //    var command = new SqlCeCommand(query, dbConnection);

        //    using (var reader = command.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            if (string.IsNullOrEmpty(reader["Titlu"].ToString())) continue;

        //            var hdm = new Film();
        //            hdm.Id = (int)reader["Id"];
        //            hdm.Titlu = reader["Titlu"].ToString();
        //            hdm.Recomandat = reader["Recomandat"].ToString();
        //            hdm.RecomandatLink = reader["RecomandatLink"].ToString();
        //            hdm.An = reader["An"].ToString();

        //            hdm.Calitate = (Calitate)int.Parse(reader["Calitate"].ToString());
        //            hdm.Format = reader["Format"].ToString();
        //            hdm.Rezolutie = reader["Rezolutie"].ToString();

        //            hdm.Dimensiune = reader["Dimensiune"].ToString();
        //            hdm.Bitrate = reader["Bitrate"].ToString();

        //            hdm.Durata = reader["Durata"] is DBNull
        //                ? new DateTime(2010, 8, 10, 0, 0, 0)
        //                : (DateTime)reader["Durata"];

        //            hdm.Audio = reader["Audio"].ToString();
        //            hdm.Subtitrari = reader["Subtitrari"].ToString();
        //            hdm.MoreInfo = reader["MoreInfo"].ToString();
        //            hdm.Tematica = reader["Tematica"].ToString();
        //            hdm.Obs = reader["Obs"].ToString();
        //            hdm.Obs2 = reader["Obs2"].ToString();
        //            hdm.Obs3 = reader["Obs3"].ToString();
        //            hdm.NLSource = reader["NLSource"].ToString();
        //            hdm.Md5 = reader["Md5"].ToString();

        //            //lazy loading the covers to avoid huge memory consumption
        //            //hdm.Cover = reader[18].GetType() == typeof(DBNull) ? null : (byte[])reader[18];

        //            hdm.HasCover = reader["Cover"].GetType() != typeof(DBNull);
        //            hdm.Trailer = reader["Trailer"].ToString();

        //            result.Add(hdm);
        //        }
        //    }

        //    return result;
        //}

        //public static byte[] LoadPoster(string tableName, int id)
        //{
        //    var query = string.Format("SELECT Cover FROM {0} WHERE Id = {1}", tableName, id);
        //    var command = new SqlCeCommand(query, dbConnection);

        //    object result = command.ExecuteScalar();

        //    return (byte[])result;
        //}

        //public static BindingList<Serial> Load_Seriale()
        //{
        //    var result = new BindingList<Serial>();

        //    var query = "SELECT * FROM Seriale ORDER BY Titlu";
        //    var command = new SqlCeCommand(query, dbConnection);

        //    using (var reader = command.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            if (string.IsNullOrEmpty(reader["Titlu"].ToString())) continue;

        //            var s = new Serial();
        //            s.Id = (int)reader["Id"];
        //            s.Titlu = reader["Titlu"].ToString();
        //            s.Recomandat = reader["Recomandat"].ToString();
        //            s.RecomandatLink = reader["RecomandatLink"].ToString();
        //            s.MoreInfo = reader["MoreInfo"].ToString();
        //            s.Obs = reader["Obs"].ToString();

        //            s.HasCover = reader["Cover"].GetType() != typeof(DBNull);
        //            s.Trailer = reader["Trailer"].ToString();

        //            result.Add(s);
        //        }
        //    }

        //    return result;
        //}


        //public static BindingList<Episod> Load_Episoade()
        //{
        //    var result = new BindingList<Episod>();

        //    var query = "SELECT * FROM Episoade ORDER BY Sezon, Titlu";
        //    var command = new SqlCeCommand(query, dbConnection);

        //    using (var reader = command.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            if (string.IsNullOrEmpty(reader["Titlu"].ToString())) continue;

        //            var e = new Episod();
        //            e.Id = (int)reader["Id"];
        //            e.SerialId = (int)reader["SerialId"];
        //            e.Titlu = reader["Titlu"].ToString();
        //            e.Sezon = reader["Sezon"].ToString();
        //            e.An = reader["An"].ToString();
        //            e.Calitate = (Calitate)(int)(reader["Calitate"]);

        //            e.Durata = reader["Durata"].GetType() == typeof(DBNull)
        //                ? new DateTime(2010, 8, 10, 0, 0, 0)
        //                : (DateTime)reader["Durata"];

        //            e.Rezolutie = reader["Rezolutie"].ToString();
        //            e.Format = reader["Format"].ToString();

        //            e.Dimensiune = reader["Dimensiune"].ToString();
        //            e.Audio = reader["Audio"].ToString();
        //            e.Subtitrari = reader["Subtitrari"].ToString();
        //            e.Obs = reader["Obs"].ToString();
        //            e.Tematica = reader["Tematica"].ToString();

        //            result.Add(e);
        //        }
        //    }

        //    return result;
        //}

        //public static string BytesToString(long byteCount)
        //{
        //    string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB

        //    if (byteCount == 0)
        //        return "0" + suf[0];

        //    var bytes = Math.Abs(byteCount);
        //    var place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
        //    var num = Math.Round(bytes / Math.Pow(1024, place), place <= 2 ? 0 : 2);

        //    return (Math.Sign(byteCount) * num) + suf[place];
        //}

        //public static void FillSerialeDataFromEpisodes(ref BindingList<Serial> seriale , BindingList<Episod> episoade)
        //{
        //    foreach (var serial in seriale)
        //    {
        //        var episoadeSerial = episoade.Where(e => e.SerialId == serial.Id).OrderBy(o => o.Sezon).ToList();

        //        var minAn = episoadeSerial.Min(e => e.An);
        //        var maxAn = episoadeSerial.Max(e => e.An);

        //        serial.An = minAn != maxAn
        //            ? string.Format("{0} - {1}", minAn, maxAn)
        //            : string.IsNullOrEmpty(minAn) && string.IsNullOrEmpty(maxAn)
        //                ? "?"
        //                : string.IsNullOrEmpty(minAn)
        //                    ? maxAn
        //                    : minAn;


        //        serial.DimensiuneInt = episoadeSerial.Sum(s => s.DimensiuneInt) / 1024;
        //        serial.Calitate = Calitate.NotSet;

        //        var calitateEpisoade = episoadeSerial.DistinctBy(e => e.Calitate).Select(e => (int)e.Calitate).ToList();

        //        if (calitateEpisoade.Any())
        //        {
        //            if (calitateEpisoade.Count == 1)
        //            {
        //                serial.Calitate = (Calitate)calitateEpisoade.FirstOrDefault();
        //            }
        //            else
        //            {
        //                if (calitateEpisoade.Max(c => c) == 1)
        //                    serial.Calitate = Calitate.HD_up;

        //                if (calitateEpisoade.All(c => c == 2))
        //                    serial.Calitate = Calitate.SD;
        //                else
        //                {
        //                    if (calitateEpisoade.Max(c => c) == 2)
        //                    {
        //                        serial.Calitate = Calitate.Mix;
        //                    }
        //                }
        //            }
        //        }

        //        if (episoadeSerial.Count > 0)
        //        {
        //            var audios = episoadeSerial.Select(d => d.Audio).Distinct();

        //            if (audios.Count() == 1)
        //            {
        //                serial.DifferentAudio = false;
        //                serial.Audio = audios.FirstOrDefault();
        //            }
        //            else
        //            {
        //                var distinctCount
        //                    = episoadeSerial.GroupBy(l => l.Audio)
        //                                    //.OrderByDescending(g => g.Count()) // cele mai multe
        //                                    .OrderByDescending(g => g.Key.Length)
        //                                    .Select(g => new { Audio = g.Key, Count = g.Count() });

        //                serial.DifferentAudio = true;
        //                serial.Audio = distinctCount.FirstOrDefault().Audio;
        //            }
        //        }
        //    }
        //}

        //public static string CalcBitRate(string inputText)
        //{
        //    if (string.IsNullOrEmpty(inputText) || inputText.Trim() == string.Empty)
        //        return string.Empty;

        //    var regex = new Regex("[0-9]*.+//-");

        //    if (regex.IsMatch(inputText))
        //        return inputText;

        //    var mp = new MathParser();

        //    try
        //    {
        //        return Math.Round(mp.Parse(inputText)).ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        return inputText;
        //    }
        //}
    }
}
