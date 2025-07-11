﻿using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Data.SqlServerCe;
using System.Linq;

using DAL;
using Common.ExtensionMethods;
using System.Data.SQLite;
using System.Collections;
using System.Dynamic;

namespace Desene
{
    public static class DAL
    {
        public static BindingList<MovieShortInfo> MoviesData;
        public static List<CachedMovieStills> CachedMoviesStills = new List<CachedMovieStills>();
        public static byte[] TmpPoster;
        public static MovieTechnicalDetails CurrentMTD;
        public static MovieTechnicalDetails NewMTD;
        public static List<string> MovieThemes = new List<string>();
        public static SeriesType SeriesType = SeriesType.Final;
        public static Dictionary<int, MovieTechnicalDetails> CachedMTDs = new Dictionary<int, MovieTechnicalDetails>();
        public static EpisodeParentType EpisodeParentType;

        public static OperationResult LoadBaseDbValues()
        {
            var result = new OperationResult();

            try
            {
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
                {
                    conn.Open();

                    var commandSource = new SQLiteCommand("SELECT DISTINCT Theme FROM FileDetail WHERE Theme IS NOT NULL AND Theme <> '' ORDER BY Theme COLLATE NOCASE ASC", conn);

                    using (var reader = commandSource.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MovieThemes.Add(reader["Theme"].ToString());
                        }
                    }

                    conn.Close();
                }

                return result;
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }
        }

        public static OperationResult LoadAllIds()
        {
            var result = new OperationResult();
            var ids = new List<int>();

            try
            {
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
                {
                    conn.Open();

                    var commandSource = new SQLiteCommand("SELECT Id FROM FileDetail", conn);

                    using (var reader = commandSource.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ids.Add((int)(long)reader["Id"]);
                        }
                    }

                    conn.Close();
                }

                result.AdditionalDataReturn = ids;
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }

            return result;
        }

        public static BindingList<MovieShortInfo> GetMoviesGridData2(string sortField, string simpleFilter)
        {
            var result = new BindingList<MovieShortInfo>();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource = new SQLiteCommand(string.Format(@"
                    SELECT
	                    Id,
	                    FileName,
                        CASE
                            WHEN length(Poster) IS NULL THEN 0
                            ELSE 1
	                    END AS HasPoster,

	                    CASE
	                        WHEN length(Quality) IS NULL THEN 'sd?'
	                        ELSE Quality
	                    END AS Quality,

	                    CASE
	                        WHEN length(Synopsis) IS NULL THEN 0
	                        ELSE 1
	                    END AS HasSynopsis

                    FROM FileDetail
                    WHERE
                        ParentId IS NULL

                        AND
                        (UPPER(FileName) LIKE '%{0}%' OR UPPER(Notes) LIKE '%{0}%')

                    ORDER BY {1}",
                    simpleFilter.ToUpper(),
                    sortField), conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new MovieShortInfo
                        {
                            Id = (int)(long)reader["Id"],
                            FileName = reader["FileName"].ToString(),
                            HasPoster = (long)reader["HasPoster"] == 1,
                            Quality = reader["Quality"].ToString(),
                            HasSynopsis = (long)reader["HasSynopsis"] == 1
                        });
                    }
                }

                conn.Close();
            }

            return result;
        }

        public static BindingList<MovieShortInfo> GetMoviesGridData(string sortField, string advFilter)
        {
            var result = new BindingList<MovieShortInfo>();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource = new SQLiteCommand(string.Format(@"
                    SELECT
	                    fd.Id,
	                    fd.FileName,
                        CASE
                            WHEN length(fd.Poster) IS NULL THEN 0
                            ELSE 1
	                    END AS HasPoster,

	                    CASE
	                        WHEN length(fd.Quality) IS NULL THEN 'sd?'
	                        ELSE fd.Quality
	                    END AS Quality,

	                    CASE
	                        WHEN length(fd.Synopsis) IS NULL THEN 0
	                        ELSE 1
	                    END AS HasSynopsis,

	                    CASE
	                        WHEN csm.Id IS NULL THEN 0
	                        ELSE 1
	                    END AS HasCsmData,

                        fd.InsertedDate,
                        fd.LastChangeDate

                    FROM FileDetail fd
                        LEFT OUTER JOIN CommonSenseMediaDetail csm ON csm.FileDetailId = fd.Id
                    WHERE ParentId IS NULL
                        {0}
                    ORDER BY {1}",
                    advFilter,
                    sortField), conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var msi =
                            new MovieShortInfo
                            {
                                Id = (int)(long)reader["Id"],
                                FileName = reader["FileName"].ToString(),
                                HasPoster = (long)reader["HasPoster"] == 1,
                                Quality = reader["Quality"].ToString(),
                                HasSynopsis = (long)reader["HasSynopsis"] == 1,
                                HasCsmData = (long)reader["HasCsmData"] == 1,
                            };

                        //todo: fix the dates for SD movies!!!
                        var insertedDate =
                            reader["InsertedDate"] == DBNull.Value
                                ? new DateTime(1910, 1, 1)
                                : (DateTime)reader["InsertedDate"];

                        var lastChangedDate =
                            reader["LastChangeDate"] == DBNull.Value
                                ? insertedDate
                                : (DateTime)reader["LastChangeDate"];

                        msi.InsertedDate = insertedDate;
                        msi.LastChangedDate = lastChangedDate;

                        result.Add(msi);
                    }
                }

                conn.Close();
            }

            return result;
        }

        public static List<SeriesEpisodesShortInfo> GetSeriesInfo()
        {
            var result = new List<SeriesEpisodesShortInfo>();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var cmd = new SQLiteCommand("SELECT Id, FileName, AudioLanguages FROM FileDetail WHERE ParentId = @displayType ORDER BY FileName COLLATE NOCASE ASC", conn);
                cmd.Parameters.AddWithValue("@displayType", (int)SeriesType);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SeriesEpisodesShortInfo
                        {
                            Id = (int)(long)reader["Id"],
                            FileName = reader["FileName"].ToString(),
                            AudioLanguages = reader["AudioLanguages"].ToString(),
                            Season = string.Empty,
                            SeriesId = (int)(long)reader["Id"],
                            IsSeries = true
                        });
                    }
                }

                conn.Close();
            }

            return result;
        }

        public static string GetSeriesTitleFromId(int seriesId)
        {
            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource = new SQLiteCommand(string.Format("SELECT FileName FROM FileDetail WHERE Id = {0}", seriesId), conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader["FileName"].ToString();
                    }
                }

                conn.Close();
            }

            return "unknown!";
        }

        public static List<SeriesEpisodesShortInfo> GetSeasonsForSeries(int seriesId)
        {
            var result = new List<SeriesEpisodesShortInfo>();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource = new SQLiteCommand(string.Format("SELECT DISTINCT Season FROM FileDetail WHERE ParentId = {0} ORDER BY Season", seriesId), conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    int i;

                    while (reader.Read())
                    {
                        //var seasonVal = int.Parse(reader["Season"].ToString());

                        result.Add(new SeriesEpisodesShortInfo
                        {
                            Id = seriesId,
                            //FileName = seasonVal > 0 ? string.Format("Season {0}", seasonVal) : "Specials",
                            FileName = int.TryParse(reader["Season"].ToString(), out i) ? string.Format("Season {0}", i) : reader["Season"].ToString(),
                            Theme = string.Empty,
                            Quality = string.Empty,
                            Season = reader["Season"].ToString(),
                            SeriesId = seriesId,
                            IsSeason = true
                        });
                    }
                }

                conn.Close();
            }

            return result.OrderBy(o => o.FileName, new NaturalSortComparer<string>()).ToList();
        }

        /// <summary>
        /// Called when the Sesson treeview item is expanded
        /// </summary>
        /// <param name="seriesId"></param>
        /// <param name="seasonVal"></param>
        /// <returns></returns>
        public static List<SeriesEpisodesShortInfo> GetEpisodesInSeason(int seriesId, string seasonVal)
        {
            var result = new List<SeriesEpisodesShortInfo>();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource = new SQLiteCommand(string.Format("SELECT * FROM FileDetail WHERE ParentId = {0} AND Season = '{1}' ORDER BY FileName COLLATE NOCASE ASC", seriesId, seasonVal), conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SeriesEpisodesShortInfo
                        {
                            Id = (int)(long)reader["Id"],
                            FileName = reader["FileName"].ToString(),
                            Theme = reader["Theme"].ToString(),
                            Quality = reader["Quality"].ToString(),
                            Season = seasonVal,
                            SeriesId = (int)reader["ParentId"],
                            IsEpisode = true
                        });
                    }
                }

                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Provide data to the SeriesEpisodes (summary) gridview
        /// </summary>
        /// <param name="seriesId"></param>
        /// <returns></returns>
        public static List<EpisodeTechnicalDetails> GetEpisodesInSeries(SeriesEpisodesShortInfo sesInfo)
        {
            var result = new List<EpisodeTechnicalDetails>();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource =
                    new SQLiteCommand(
                        string.Format(@"
                            SELECT
                                fd.Id,
                                fd.Season,
                                fd.FileName,
                                fd.Year,
                                fd.FileSize,
                                fd.FileSize2,
                                fd.Duration,
                                fd.Quality,
                                fd.AudioLanguages,
                                vs.BitRate,
                                vs.FrameRate

                            FROM FileDetail fd
                                LEFT OUTER JOIN VideoStream AS vs ON vs.FileDetailId = fd.Id
                            WHERE ParentId = {0} {1}
                            ORDER BY Season, FileName COLLATE NOCASE ASC",
                            sesInfo.SeriesId,
                            sesInfo.IsSeason
                                ? string.Format("AND Season = '{0}'", sesInfo.Season)
                                : string.Empty
                            ), conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int i;

                        result.Add(new EpisodeTechnicalDetails()
                        {
                                Id = (int)(long)reader["Id"],
                                Season = reader["Season"].ToString(),
                                Season2 = int.TryParse(reader["Season"].ToString(), out i) ? string.Format("Season {0}", i) : reader["Season"].ToString(),
                                FileName = reader["FileName"].ToString(),
                                Year = reader["Year"].ToString(),
                                FileSize = reader["FileSize"].ToString(),
                                FileSize2 = reader["FileSize2"].ToString(),
                                Duration = reader["Duration"] == DBNull.Value
                                                        ? "<not set>"
                                                        : Convert.ToDateTime(reader["Duration"]).ToString("HH:mm:ss"),
                                Quality = reader["Quality"].ToString(),
                                AudioLanguages =  reader["AudioLanguages"].ToString(),
                                BitRate = reader["BitRate"].ToString(),
                                FrameRate = reader["FrameRate"].ToString()

                        });
                    }
                }

                conn.Close();
            }

            result =
                result
                    .OrderBy(o => o.Season2, new NaturalSortComparer<string>())
                    .ThenBy(o => o.FileName)
                    .ToList();

            if (result.Any())
            {
                foreach (var sumSeason in
                         result
                             .GroupBy(x => x.Season2)
                             .Select(grp =>
                                 new KeyValuePair<string, decimal>(grp.Key, grp.Sum(y => decimal.TryParse(y.FileSize, out var val) ? val : 0)))
                             .OrderByDescending(_ => _.Key)
                        )
                {
                    var idx = result.FindLastIndex(_ => _.Season2 == sumSeason.Key);
                    result.Insert(
                        idx + 1,//sadadadsa
                        new EpisodeTechnicalDetails()
                        {
                            Id = -1,
                            FileName = "Total " + sumSeason.Key,
                            Season2 = sumSeason.Key,
                            Quality = string.Empty,
                            FileSize2 =
                                sumSeason.Value / 1024 / 1024 > 1024
                                    ? (sumSeason.Value / 1024 / 1024 / 1024).ToString("0.00") + " Gb"
                                    : (sumSeason.Value / 1024 / 1024).ToString("0.00") + " Mb"
                        });
                }

                if (sesInfo.IsSeries && result.DistinctBy(_ => _.Season2).Count() > 1)
                {
                    var grandTotal = result.Sum(_ => decimal.TryParse(_.FileSize, out var val) ? val : 0);

                    result.Insert(
                        result.Count(),
                        new EpisodeTechnicalDetails()
                        {
                            Id = -2,
                            FileName = "Total",
                            Season2 = sesInfo.FileName,
                            Quality = string.Empty,
                            FileSize2 =
                                grandTotal / 1024 / 1024 > 1024
                                    ? (grandTotal / 1024 / 1024 / 1024).ToString("0.00") + " Gb"
                                    : (grandTotal / 1024 / 1024).ToString("0.00") + " Mb"
                        });
                }
            }

            return result;
        }

        public static List<MovieTechnicalDetails> GetCollectionElements(SeriesEpisodesShortInfo sesInfo)
        {
            var result = new List<MovieTechnicalDetails>();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource =
                    new SQLiteCommand(
                        string.Format(@"
                            SELECT
                                fd.Id,
                                fd.FileName,
                                fd.Year,
                                fd.FileSize2,
                                fd.Duration,
                                fd.Quality,
                                fd.AudioLanguages
                            FROM FileDetail fd
                            WHERE ParentId = {0}
                            ORDER BY FileName COLLATE NOCASE ASC",
                            sesInfo.SeriesId
                            ), conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new MovieTechnicalDetails()
                        {
                            Id = (int)(long)reader["Id"],
                            FileName = reader["FileName"].ToString(),
                            Year = reader["Year"].ToString(),
                            FileSize2 = reader["FileSize2"].ToString(),
                            Duration = reader["Duration"] == DBNull.Value
                                                    ? "<not set>"
                                                    : Convert.ToDateTime(reader["Duration"]).ToString("HH:mm:ss"),
                            Quality = reader["Quality"].ToString(),
                            AudioLanguages =  reader["AudioLanguages"].ToString(),
                        });
                    }
                }

                conn.Close();
            }

            return result;
        }

        public static List<SeriesEpisodesShortInfo> GetFilteredFileNames(string filterBy, bool showingCollections)
        {
            var result = new List<SeriesEpisodesShortInfo>();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var sqlString =
                    string.Format(@"
                        SELECT
                            fd2.FileName AS SeriesName,
                            fd.*
                        FROM FileDetail fd
                            LEFT OUTER JOIN FileDetail fd2 ON fd.ParentId = fd2.id
                        WHERE fd2.ParentId = {0} AND fd.FileName LIKE '%{1}%'
                        ORDER BY ParentId DESC",
                        showingCollections
                            ? "-10"
                            : "-1",
                        filterBy);

                var commandSource = new SQLiteCommand(sqlString, conn);

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
                                    Season = string.Empty,
                                    SeriesId = parentId,
                                    IsSeries = true
                                });
                            }

                            result.Add(new SeriesEpisodesShortInfo
                            {
                                Id = (int)(long)reader["Id"],
                                FileName = reader["FileName"].ToString(),
                                Theme = reader["Theme"].ToString(),
                                Quality = reader["Quality"].ToString(),
                                Season = reader["Season"].ToString(),
                                SeriesId = parentId,
                                IsEpisode = true
                            });
                        }
                        else
                        {
                            var seriesId = (int)(long)reader["Id"];

                            if (!result.Any(r => r.IsSeries && r.Id == seriesId))
                            {
                                result.Add(new SeriesEpisodesShortInfo
                                {
                                    Id = seriesId,
                                    FileName = reader["FileName"].ToString(),
                                    Season = string.Empty,
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

                conn.Close();
            }

            return result.OrderBy(o => o.FileName).ThenBy(o => o.Season).ToList();
        }

        public static OperationResult InsertSeries(string title, string descriptionLink, string recommended,
            string recommendedLink, string notes, byte[] poster, string trailer)
        {
            var result = new OperationResult();

            try
            {
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
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

                    var cmd = new SQLiteCommand(insertString, conn);
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
                    cmd.CommandText = "SELECT last_insert_rowid()";

                    result.AdditionalDataReturn = (int)(long)cmd.ExecuteScalar();

                    conn.Close();
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
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
                {
                    conn.Open();
                    SQLiteCommand cmd;

                    #region Checks

                    if (eip != null)
                    {
                        var checks = "SELECT COUNT(*) FROM FileDetail WHERE ParentId = @ParentId AND FileName = @FileName AND FileSize = @FileSize";

                        cmd = new SQLiteCommand(checks, conn);
                        cmd.Parameters.AddWithValue("@ParentId", eip.ParentId);
                        cmd.Parameters.AddWithValue("@FileName", mtd.FileName);
                        cmd.Parameters.AddWithValue("@FileSize", mtd.FileSize);

                        var count = (int)(long)cmd.ExecuteScalar();

                        if (count > 0)
                            return result.FailWithMessage("A file with exactly the same name and size already exists in the selected series. The file details were not added to the database!");
                    }
                    else
                    {
                        var checks = "SELECT COUNT(*) FROM FileDetail WHERE FileName = @FileName and ParentId IS NULL";

                        cmd = new SQLiteCommand(checks, conn);
                        cmd.Parameters.AddWithValue("@FileName", mtd.FileName);

                        var count = (int)(long)cmd.ExecuteScalar();

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
                            Poster,
                            Synopsis
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
                            @Poster,
                            @Synopsis
                        )";

                    cmd = new SQLiteCommand(insertString, conn);
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
                    cmd.Parameters.AddWithValue("@ParentId",
                        //eip == null ? (object)DBNull.Value : eip.ParentId
                        mtd.Season == null //not an Episode
                            ? mtd.ParentId == null //not a collection element
                                ? eip == null ? (object)DBNull.Value : eip.ParentId
                                : mtd.ParentId
                            : eip == null ? (object)DBNull.Value : eip.ParentId);

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
                    cmd.Parameters.AddWithValue("@Synopsis", mtd.Synopsis ?? (object)DBNull.Value);

                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "SELECT last_insert_rowid()";

                    var newFileDetailId = (int)(long)cmd.ExecuteScalar();
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

                        cmd = new SQLiteCommand(insertString, conn);
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
                        cmd = new SQLiteCommand(insertString, conn);
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
                        cmd = new SQLiteCommand(insertString, conn);
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
                        cmd = new SQLiteCommand(insertString, conn);
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

                    conn.Close();
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
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
                {
                    conn.Open();
                    SQLiteCommand cmd;
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
                            cmd = new SQLiteCommand(string.Format("SELECT * FROM FileDetail WHERE Id = {0}", existingFileInfo.Id), conn);

                            using (var reader = cmd.ExecuteReader()) //cmd.ExecuteReader()
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

                    cmd = new SQLiteCommand(insertString, conn);
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
                    cmd.CommandText = "SELECT last_insert_rowid()";

                    var newFileDetailId = (int)(long)cmd.ExecuteScalar();

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

                        cmd = new SQLiteCommand(insertString, conn);
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
                        cmd = new SQLiteCommand(insertString, conn);
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
                        cmd = new SQLiteCommand(insertString, conn);
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
                        cmd = new SQLiteCommand(insertString, conn);
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

                    conn.Close();
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
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
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
                               ,Synopsis = @Synopsis
                         WHERE Id = @Id";

                    var cmd = new SQLiteCommand(updateString, conn);
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

                    //the series do not have a a quality value
                    cmd.Parameters.AddWithValue("@Quality",
                            CurrentMTD.ParentId == null || CurrentMTD.ParentId.GetValueOrDefault(-1) > 0 //movie or episode
                                ? GetQualityStrFromSize(CurrentMTD)
                                : string.Empty);

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
                    cmd.Parameters.AddWithValue("@Synopsis", CurrentMTD.Synopsis ?? (object)DBNull.Value);

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
                            cmd = new SQLiteCommand(updateString, conn);
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
                            cmd = new SQLiteCommand(updateString, conn);
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
                            cmd = new SQLiteCommand(updateString, conn);
                            cmd.Parameters.AddWithValue("@Format", subtitleStream.Format);
                            cmd.Parameters.AddWithValue("@StreamSize", subtitleStream.StreamSize);
                            cmd.Parameters.AddWithValue("@TitleEmbedded", subtitleStream.HasTitle ? subtitleStream.Title : string.Empty);
                            cmd.Parameters.AddWithValue("@Language", subtitleStream.Language);
                            cmd.Parameters.AddWithValue("@Id", subtitleStream.Id);
                            cmd.ExecuteNonQuery();
                        }

                        #endregion
                    }

                    conn.Close();
                }

                if (!string.IsNullOrEmpty(CurrentMTD.Theme) && MovieThemes.IndexOf(CurrentMTD.Theme) == -1)
                {
                    MovieThemes.Add(CurrentMTD.Theme);
                    MovieThemes = MovieThemes.OrderBy(o => o).ToList(); //in case of changing a Theme, the old one and new one will both be in the list
                }

                if (CachedMTDs.ContainsKey(CurrentMTD.Id))
                {
                    CachedMTDs.Remove(CurrentMTD.Id);
                    CachedMTDs.Add(CurrentMTD.Id, CurrentMTD);
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

            if (CachedMTDs.ContainsKey(fileDetailId))
            {
                CurrentMTD = CachedMTDs[fileDetailId];
                result.AdditionalDataReturn = CurrentMTD;

                return result;
            }

            var mtd = new MovieTechnicalDetails();

            try
            {
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
                {
                    conn.Open();

                    var cmd = new SQLiteCommand(string.Format("SELECT * FROM FileDetail WHERE Id = {0}", fileDetailId), conn);

                    using (var reader = cmd.ExecuteReader())
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
                            mtd.InsertedDate = reader["InsertedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["InsertedDate"]);
                            mtd.LastChangeDate = reader["LastChangeDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LastChangeDate"]);
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
                                                        : Convert.ToDateTime(reader["Duration"]).ToString("HH:mm:ss");
                            mtd.Notes = reader["Notes"].ToString();
                            mtd.Synopsis = reader["Synopsis"].ToString();

                            break;
                        }
                    }

                    if (fullLoad)
                    {
                        cmd = new SQLiteCommand(string.Format("SELECT * FROM VideoStream WHERE FileDetailId = {0} ORDER BY [Index]", fileDetailId), conn);

                        using (var reader = cmd.ExecuteReader())
                        {
                            //if (reader.HasRows) return null;

                            while (reader.Read())
                            {
                                var vsObj = new VideoStreamInfo();
                                vsObj.Id = (int)(long)reader["Id"];
                                vsObj.Index = (int)reader["Index"];
                                vsObj.Format = reader["Format"].ToString();
                                vsObj.Format_Profile = reader["Format_Profile"].ToString();
                                vsObj.BitRateMode = reader["BitRateMode"].ToString();
                                vsObj.BitRate = reader["BitRate"].ToString();
                                vsObj.Width = reader["Width"].ToString();
                                vsObj.Height = reader["Height"].ToString();
                                vsObj.FrameRate_Mode = reader["FrameRate_Mode"].ToString();
                                vsObj.FrameRate = reader["FrameRate"].ToString();
                                vsObj.Delay = reader["Delay"].ToString();
                                vsObj.StreamSize = reader["StreamSize"].ToString();
                                vsObj.Title = reader["TitleEmbedded"].ToString();
                                vsObj.Language = reader["Language"].ToString();

                                mtd.VideoStreams.Add(vsObj);
                            }
                        }

                        cmd = new SQLiteCommand(string.Format("SELECT * FROM AudioStream WHERE FileDetailId = {0} ORDER BY [Index]", fileDetailId), conn);

                        using (var reader = cmd.ExecuteReader())
                        {
                            //if (!reader.HasRows) return null;

                            while (reader.Read())
                            {
                                mtd.AudioStreams.Add(
                                    new AudioStreamInfo
                                    {
                                        Id = (int)(long)reader["Id"],
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

                        cmd = new SQLiteCommand(string.Format("SELECT * FROM SubtitleStream WHERE FileDetailId = {0} ORDER BY [Index]", fileDetailId), conn);

                        using (var reader = cmd.ExecuteReader())
                        {
                            //if (!reader.HasRows) return null;

                            while (reader.Read())
                            {
                                mtd.SubtitleStreams.Add(
                                    new SubtitleStreamInfo
                                    {
                                        Id = (int)(long)reader["Id"],
                                        Index = (int)reader["Index"],
                                        Format = reader["Format"].ToString(),
                                        StreamSize = reader["StreamSize"].ToString(),
                                        Title = reader["TitleEmbedded"].ToString(),
                                        Language = reader["Language"].ToString()
                                    });
                            }
                        }
                    }

                    cmd = new SQLiteCommand(string.Format("SELECT COUNT(1) FROM CommonSenseMediaDetail WHERE FileDetailId = {0}", fileDetailId), conn);
                    cmd.CommandType = CommandType.Text;

                    mtd.HasRecommendedDataSaved = Convert.ToInt32(cmd.ExecuteScalar()) > 0;

                    conn.Close();
                }

                CurrentMTD = mtd;
                result.AdditionalDataReturn = mtd;
                CachedMTDs.Add(fileDetailId, mtd);
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

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var cmd = new SQLiteCommand(string.Format("SELECT * FROM Thumbnails WHERE FileDetailId = {0}", fileDetailId), conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows) return result;

                    while (reader.Read())
                    {
                        result.MovieStills.Add((byte[])reader["MovieStill"]);
                    }
                }

                conn.Close();
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

        public static OperationResult RemoveSeason(int seriesId, string seasonNo)
        {
            var result = new OperationResult();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                try
                {
                    conn.Open();

                    var cmd = new SQLiteCommand(string.Format("SELECT Id FROM FileDetail WHERE ParentId = {0} AND Season = '{1}'", seriesId, seasonNo), conn);

                    var episodeIds = new List<int>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            episodeIds.Add((int)(long)reader["Id"]);
                        }
                    }

                    result = RemoveData(string.Join(",", episodeIds), conn);
                    conn.Close();
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

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                try
                {
                    conn.Open();

                    var cmd = new SQLiteCommand(string.Format("SELECT Id FROM FileDetail WHERE ParentId = {0}", seriesId), conn);

                    var episodeIds = new List<int> { seriesId };

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            episodeIds.Add((int)(long)reader["Id"]);
                        }
                    }

                    result = RemoveData(string.Join(",", episodeIds), conn);
                    conn.Close();
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

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                try
                {
                    conn.Open();

                    result = RemoveData(fileDetailId.ToString(), conn);

                    conn.Close();
                }
                catch (Exception ex)
                {
                    return result.FailWithMessage(ex);
                }
            }

            return result;
        }

        private static OperationResult RemoveCommonSenseMediaData(int fileDetailId, SQLiteConnection conn)
        {
            var result = new OperationResult();

            try
            {
                var cmsTables = new[] { "CommonSenseMediaDetail_ALotOrALittle", "CommonSenseMediaDetail_TalkAbout", "CommonSenseMediaDetail" };

                var cmd =
                    new SQLiteCommand(
                        string.Format("SELECT Id FROM CommonSenseMediaDetail WHERE FileDetailId = {0}", fileDetailId),
                        conn);

                var csmId = cmd.ExecuteScalar();

                if (csmId != null)
                {
                    foreach (var tableName in cmsTables)
                    {
                        cmd =
                            new SQLiteCommand(
                                string.Format("DELETE FROM {0} WHERE {1} IN ({2})",
                                    tableName,
                                    tableName == "CommonSenseMediaDetail" ? "Id" : "CSMDetailId",
                                    csmId),
                                conn);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }

            return result;
        }

        private static OperationResult RemoveData(string episodeIds, SQLiteConnection conn)
        {
            var result = new OperationResult();
            
            try
            {
                SQLiteCommand cmd;

                if (!episodeIds.Contains(","))
                {
                    var fileDetailId = int.Parse(episodeIds);
                    result = RemoveCommonSenseMediaData(fileDetailId, conn);

                    if (!result.Success)
                        return result;
                }

                var tableNames = new[] { "VideoStream", "Thumbnails", "AudioStream", "SubtitleStream", "FileDetail" };

                foreach (var tableName in tableNames)
                {
                    cmd =
                        new SQLiteCommand(
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

        public static OperationResult RemoveData(List<int> episodeIds)
        {
            var result = new OperationResult();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                try
                {
                    conn.Open();

                    result = RemoveData(string.Join(",", episodeIds), conn);

                    conn.Close();
                }
                catch (Exception ex)
                {
                    return result.FailWithMessage(ex);
                }
            }

            return result;
        }

        public static OperationResult GetStatistics(Sections section)
        {
            var result = new OperationResult();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                try
                {
                    conn.Open();

                    var sqlString = string.Empty;
                    var resultStr = "There are no movies in the list ...";
                    SQLiteCommand cmd = null;

                    switch (section)
                    {
                        case Sections.Movies:

                            /*
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
                                GROUP BY Quality            -- grouped per quality
							    HAVING COUNT(*) > 0

                                UNION
                             */

                            sqlString = @"
                                SELECT
                                    0 AS Pos,
	                                CASE
		                                WHEN (SUM(CAST(fd.FileSize AS NUMERIC)) / 1024 / 1024) > 1024
		                                THEN CAST(SUM(CAST(fd.FileSize AS NUMERIC)) / 1024 / 1024 / 1024 AS TEXT) || ' Gb'
		                                ELSE CAST(SUM(CAST(fd.FileSize AS NUMERIC)) / 1024 / 1024 AS TEXT) || ' Mb'
	                                END AS Size,

	                                COUNT(1) AS MoviesCount,

	                                'ALL' AS Quality        -- ALL
                                FROM FileDetail fd
                                WHERE ParentId IS NULL
							    --HAVING COUNT(*) > 0

                                ORDER BY Pos, Quality";

                            cmd = new SQLiteCommand(sqlString, conn);

                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if ((int)(long)reader["Pos"] == 0)
                                    {
                                        resultStr =
                                            string.Format("There are currently {0} movies in list, having a total size of aprox. {1}",
                                                reader["MoviesCount"],
                                                reader["Size"]);
                                    }
                                }
                            }

                            break;

                        default:
                            sqlString = string.Format(@"
                                SELECT
	                                0 AS Pos,
	                                '0' AS Size,

	                                COUNT(1) AS CountOf
                                FROM FileDetail
                                WHERE ParentId = -1
                                --HAVING COUNT(1) > 0

                                UNION

                                SELECT
	                                1 AS Pos,
	                                CASE
		                                WHEN (SUM(CAST(fd.FileSize AS NUMERIC)) / 1024 / 1024) > 1024
		                                THEN CAST(SUM(CAST(fd.FileSize AS NUMERIC)) / 1024 / 1024 / 1024 AS TEXT) || ' Gb'
		                                ELSE CAST(SUM(CAST(fd.FileSize AS NUMERIC)) / 1024 / 1024 AS TEXT) || ' Mb'
	                                END AS Size,

	                                COUNT(1) AS CountOf
                                FROM FileDetail fd
                            	    LEFT OUTER JOIN Filedetail p on p.Id = fd.ParentId
                                WHERE p.ParentId = {0}
                                --HAVING COUNT(1) > 0

                                ORDER BY Pos",
                                section == Sections.Series
                                    ? -1
                                    : section == Sections.Recordings
                                        ? - 100
                                        : -10
                                );

                            cmd = new SQLiteCommand(sqlString, conn);

                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if ((int)(long)reader["Pos"] == 0)
                                    {
                                        resultStr = string.Format("The current list contains {0} series", (int)(long)reader["CountOf"]);
                                    }
                                    if ((int)(long)reader["Pos"] == 1)
                                    {
                                        resultStr += string.Format(", combined having {0} episodes, (summing aprox. {1})",
                                            (int)(long)reader["CountOf"],
                                            reader["Size"].ToString());
                                    }
                                }
                            }

                            break;
                    }

                    result.AdditionalDataReturn = resultStr;
                    conn.Close();
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

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource = new SQLiteCommand(@"
                    SELECT DISTINCT
                        vs.BitRate

	                    ,CASE
                            WHEN Poster IS NULL THEN 0
                            ELSE 1
	                    END AS HasPoster

                        ,fd.Id
                        ,FileName
						,RecommendedLink
						,Recommended
						,Year
						,Quality
						,FileSize2
              			,Duration
						,BitRate
						,AudioLanguages
						,SubtitleLanguages
						,DescriptionLink
				        ,Theme
						,Notes
						,Trailer
						,InsertedDate
						,LastChangeDate

	                    ,CASE
                            WHEN th.Id IS NULL THEN 0
                            ELSE 1
	                    END AS HasThumbnails

                    FROM FileDetail fd
	                    LEFT OUTER JOIN VideoStream vs ON fd.Id = vs.FileDetailId
                        LEFT OUTER JOIN Thumbnails th ON fd.Id = th.FileDetailId
                    WHERE ParentId IS NULL ORDER BY FileName", conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var mfw = new MovieForWeb();
                        mfw.Id = (int)(long)reader["Id"];
                        mfw.FN = reader["FileName"].ToString();
                        mfw.R = reader["Recommended"].ToString();
                        mfw.RL = reader["RecommendedLink"].ToString();
                        mfw.Y = reader["Year"].ToString();
                        mfw.Q = reader["Quality"].ToString();
                        mfw.S = reader["FileSize2"].ToString();
                        mfw.B = reader["BitRate"].ToString();

                        mfw.L =
                            reader["Duration"] == DBNull.Value
                                ? "?"
                                : Convert.ToDateTime(reader["Duration"]).ToString("HH:mm:ss");

                        mfw.A = reader["AudioLanguages"].ToString();
                        mfw.SU = reader["SubtitleLanguages"].ToString();

                        mfw.DL = reader["DescriptionLink"].ToString();
                        mfw.T = reader["Theme"].ToString();
                        mfw.N = reader["Notes"].ToString();
                        mfw.Nl = string.Empty;
                        mfw.Tr = GetTrailerId(reader["Trailer"].ToString());

                        //mfw.Cover = reader["Poster"] == DBNull.Value ? null : (byte[])reader["Poster"];
                        mfw.HasPoster = (long)reader["HasPoster"] == 1;
                        mfw.HasThumbnails = (long)reader["HasThumbnails"] == 1;

                        var insertedDate =
                            reader["InsertedDate"] == DBNull.Value //SD or bad imports
                                ? new DateTime(1970, 1, 1)
                                : Convert.ToDateTime(reader["InsertedDate"]);

                        mfw.InsertedDate = insertedDate;

                        mfw.LastChangeDate =
                            reader["LastChangeDate"] == DBNull.Value
                                ? insertedDate
                                : Convert.ToDateTime(reader["LastChangeDate"]);

                        result.Add(mfw);
                    }
                }

                conn.Close();
            }

            var tempSortList = result.OrderBy(o => o.InsertedDate).ToList();
            var i = 0;

            foreach (var sortedEl in tempSortList)
            {
                var origEl = result.FirstOrDefault(x => x.Id == sortedEl.Id);
                origEl.ISP = i;
                i++;
            }

            tempSortList = result.OrderBy(o => o.LastChangeDate).ToList();
            i = 0;

            foreach (var sortedEl in tempSortList)
            {
                var origEl = result.FirstOrDefault(x => x.Id == sortedEl.Id);
                origEl.USP = i;
                i++;
            }

            return result;
        }

        public static List<MoviesDetails2ForWeb> GetMoviesDetails2ForWeb(bool getMovieTypeCollectionElements)
        {
            var result = new List<MoviesDetails2ForWeb>();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var cmd1 = new SQLiteCommand(
                    !getMovieTypeCollectionElements
                        ? @"
                            SELECT
                                Id,
                                Synopsis,

	                            Format,
	                            FileSize2,
	                            Duration

                            FROM FileDetail fd
                            WHERE ParentId IS NULL
                            ORDER BY Id
                            "
                        : @"
                            SELECT
                                fd.Id,
                                fd.Synopsis,

	                            fd.Format,
	                            fd.FileSize2,
	                            fd.Duration

                            FROM FileDetail fd
                                LEFT OUTER JOIN FileDetail p ON p.Id = fd.ParentId
                            WHERE p.ParentId = -10 AND p.SectionType = 0
                            ORDER BY p.Id, fd.Id
                            ",
                    conn);

                using (var reader1 = cmd1.ExecuteReader())
                {
                    while (reader1.Read())
                    {
                        var mDet2 =
                            new MoviesDetails2ForWeb
                            {
                                Id = (int)(long)reader1["Id"],
                                Syn =
                                    reader1["Synopsis"] == DBNull.Value
                                        ? "?"
                                        : ((string)reader1["Synopsis"]).Replace(Environment.NewLine, "<br>")
                            };

                        //mDet2.FileDetails.Add((string)reader1["Synopsis"]);


                        var cmd2 =
                            new SQLiteCommand(
                                string.Format(@"
                                    SELECT
	                                    Width || ' x ' || Height AS Col1,
	                                    Format || ' / ' || Format_Profile AS Col2,
	                                    Bitrate AS Col3
                                    FROM VideoStream
                                    WHERE FileDetailId = {0}
                                ",
                                mDet2.Id),
                                conn);

                        using (var reader2 = cmd2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                for (var i = 1; i <= 3; i++)
                                    mDet2.Vtd.Add(
                                        reader2[string.Format("Col{0}", i)] == DBNull.Value
                                            ? "?"
                                            : (string)reader2[string.Format("Col{0}", i)]);
                            }
                        }

                        mDet2.Vtd.Add(
                            reader1["Duration"] == DBNull.Value
                                ? "?"
                                : Convert.ToDateTime(reader1["Duration"]).ToString("HH:mm:ss")
                        );



                        var cmd3 =
                            new SQLiteCommand(
                                string.Format(@"
                                    SELECT
	                                    Language,
	                                    TitleEmbedded,
	                                    Channel,
	                                    Format,
	                                    BitRate
                                    FROM AudioStream
                                    WHERE FileDetailId = {0}
                                    ORDER BY Id
                                ",
                                mDet2.Id),
                                conn);

                        using (var reader3 = cmd3.ExecuteReader())
                        {
                            while (reader3.Read())
                            {
                                var languageName =
                                    reader3["Language"] == DBNull.Value || string.IsNullOrEmpty((string)reader3["Language"])
                                        ? "?"
                                        : Languages.GetLanguageFromIdentifier((string)reader3["Language"]).Name.Replace("Romanian; Moldavian; Moldovan", "Romanian");

                                if (reader3["TitleEmbedded"] != DBNull.Value && !string.IsNullOrEmpty((string)reader3["TitleEmbedded"]))
                                    languageName += string.Format(" [{0}]", (string)reader3["TitleEmbedded"]);

                                mDet2.Ats.Add(languageName);

                                mDet2.Ats.Add(
                                    string.Format("{0} / {1} / {2}",
                                        ((string)reader3["Channel"]).Replace(" channels", "ch"),
                                        (string)reader3["Format"],
                                        ((string)reader3["BitRate"]).Replace(" ", "")
                                    )
                                );
                            }
                        }




                        var cmd4 =
                            new SQLiteCommand(
                                string.Format(@"
                                    SELECT
	                                    Language
                                    FROM SubtitleStream
                                    WHERE FileDetailId = {0}
                                    ORDER BY Id
                                ",
                                mDet2.Id),
                                conn);

                        using (var reader4 = cmd4.ExecuteReader())
                        {
                            while (reader4.Read())
                            {
                                var languageName =
                                    reader4["Language"] == DBNull.Value || string.IsNullOrEmpty((string)reader4["Language"])
                                        ? "?"
                                        : Languages.GetLanguageFromIdentifier((string)reader4["Language"]).Name.Replace("Romanian; Moldavian; Moldovan", "Romanian");

                                if (!mDet2.Sts.Any(s => s == languageName))
                                    mDet2.Sts.Add(languageName);
                            }
                        }

                        mDet2.Fd.Add(
                            reader1["Format"] == DBNull.Value
                                ? "?"
                                : (string)reader1["Format"]);

                        mDet2.Fd.Add(
                            reader1["FileSize2"] == DBNull.Value
                                ? "?"
                                : (string)reader1["FileSize2"]);

                        result.Add(mDet2);
                    }
                }

                conn.Close();
            }

            return result;
        }

        public static byte[] GetPoster(int id)
        {
            byte[] result = null;

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                const string sqlString = @"
                    SELECT Poster FROM FileDetail WHERE Id = @id";

                var cmd = new SQLiteCommand(sqlString, conn);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader["Poster"] == DBNull.Value ? null : (byte[])reader["Poster"];
                        break;
                    }
                }

                conn.Close();
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

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var cmd = new SQLiteCommand(@"
                    SELECT
                        fd.Id,
                        fd.FileName,
                        fd.Recommended,
                        fd.RecommendedLink,
                        fd.DescriptionLink,
                        fd.Notes,
	                    CASE
                            WHEN fd.Poster IS NULL THEN 0
                            ELSE 1
	                    END AS HasPoster,
                        fd.SectionType
                    FROM FileDetail fd
                    WHERE fd.ParentId = @seriesType
                    ORDER BY fd.FileName COLLATE NOCASE ASC", conn);

                cmd.Parameters.AddWithValue("@seriesType", (int)st);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sfw = new SeriesForWeb();
                        sfw.Id = (int)(long)reader["Id"];
                        sfw.FN = reader["FileName"].ToString();
                        sfw.R = reader["Recommended"].ToString();
                        sfw.RL = reader["RecommendedLink"].ToString();
                        sfw.DL = reader["DescriptionLink"].ToString();
                        sfw.N = reader["Notes"].ToString();
                        //sfw.Cover = reader["Poster"] == DBNull.Value ? null : (byte[])reader["Poster"];
                        sfw.HasPoster = (int)(long)reader["HasPoster"] == 1;
                        sfw.T = reader["SectionType"] == DBNull.Value ? 0 : (int)reader["SectionType"];

                        result.Add(sfw);
                    }
                }

                conn.Close();
            }

            return result;
        }

        public static List<EpisodesForWeb> GetEpisodesForWeb(bool preserveMarkesForExistingThumbnails, SeriesType st)
        {
            var result = new List<EpisodesForWeb>();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var cmd = new SQLiteCommand(@"
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
                        ORDER BY fd.ParentId, fd.Season, fd.FileName COLLATE NOCASE ASC", conn);

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
                        /*
                        var seasonNo = 0;
                        if (reader["Season"].ToString() != string.Empty)
                            int.TryParse(reader["Season"].ToString(), out seasonNo);
                        */
                        var efw = new EpisodesForWeb();
                        efw.Id = (int)(long)reader["Id"];
                        efw.SId = (int)reader["ParentId"];
                        efw.FN = reader["FileName"].ToString();
                        efw.SZ = reader["Season"].ToString(); //seasonNo;
                        efw.Y = reader["Year"].ToString();
                        efw.Q = reader["Quality"].ToString();

                        efw.L =
                            reader["Duration"] == DBNull.Value
                                ? "?"
                                : Convert.ToDateTime(reader["Duration"]).ToString("HH:mm:ss");

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
                                : Convert.ToDateTime(reader["InsertedDate"]);

                        efw.InsertedDate = insertedDate;

                        efw.LastChangeDate =
                            reader["LastChangeDate"] == DBNull.Value
                                ? insertedDate
                                : Convert.ToDateTime(reader["LastChangeDate"]);

                        //if (loadThumbnails)
                        //{
                        //    efw.MovieStills = LoadMovieStills(efw.Id).MovieStills;
                        //}

                        result.Add(efw);
                    }
                }

                conn.Close();
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

        public static void FillCollectionDataFromEpisodes(ref List<SeriesForWeb> collections,
            List<CollectionElementForWeb> elements)
        {
            foreach (var c in collections)
            {
                var colElements = elements.Where(e => e.CId == c.Id).ToList();

                var minYear = colElements.Min(e => e.Y);
                var maxYear = colElements.Max(e => e.Y);

                c.Y = minYear != maxYear
                    ? string.Format("{0} - {1}", minYear, maxYear)
                    : string.IsNullOrEmpty(minYear) && string.IsNullOrEmpty(maxYear)
                        ? "?"
                        : string.IsNullOrEmpty(minYear)
                            ? maxYear
                            : minYear;

                c.S = Math.Round((decimal)colElements.Sum(e => e.Si) / 1024 / 1024 / 1024, 2).ToString();

                c.Q = string.Join(", ", colElements.DistinctBy(e => e.Q).Select(e => e.Q).ToList());

                //set the distinct audios in one single line
                //ro, en        |
                //en, nl        | => ro, en, en, nl, ro, en, nl
                //ro, en, nl    |
                var audios = string.Join(",", colElements.Select(d => d.A).Distinct()).Replace(", ", ",");
                c.A = string.Join(", ", audios.Split(',').Distinct());

                c.Ec = colElements.Count.ToString();
            }
        }


        public static List<CollectionElementForWeb> GetCollectionElementsForWeb()
        {
            var result = new List<CollectionElementForWeb>();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var cmd = new SQLiteCommand(@"
                        SELECT
                            fd.Id,
                            fd.ParentId,
                            fd.FileName,
                            fd.Recommended,
                            fd.RecommendedLink,
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
                            fd.LastChangeDate,
                            topVS.BitRate,
                            fd.DescriptionLink,
                            fd.Trailer,

	                        CASE
                                WHEN fd.Poster IS NULL THEN 0
                                ELSE 1
	                        END AS HasPoster

                        FROM FileDetail fd
                            LEFT OUTER JOIN FileDetail p ON p.Id = fd.ParentId

                            LEFT OUTER JOIN VideoStream topVS ON topVS.Id =
                                    (
                                        SELECT Id
                                        FROM VideoStream vs2
                                        WHERE vs2.FileDetailId = fd.Id
                                        ORDER BY Id ASC
                                        LIMIT 1
                                    )

                        WHERE p.ParentId = -10
                        ORDER BY fd.ParentId, fd.FileName COLLATE NOCASE ASC", conn);

               // cmd.Parameters.AddWithValue("@seriesType", (int)st);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var efw = new CollectionElementForWeb();
                        efw.Id = (int)(long)reader["Id"];
                        efw.CId = (int)reader["ParentId"];
                        efw.FN = reader["FileName"].ToString();
                        efw.R = reader["Recommended"].ToString();
                        efw.RL = reader["RecommendedLink"].ToString();
                        efw.Y = reader["Year"].ToString();
                        efw.Q = reader["Quality"].ToString();

                        //file size
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

                        //...
                        efw.B = reader["BitRate"].ToString();

                        //duration
                        efw.L =
                            reader["Duration"] == DBNull.Value
                                ? "?"
                                : Convert.ToDateTime(reader["Duration"]).ToString("HH:mm:ss");

                        efw.A = reader["AudioLanguages"].ToString();
                        efw.SU = reader["SubtitleLanguages"].ToString();
                        efw.DL = reader["DescriptionLink"].ToString();
                        efw.T = reader["Theme"].ToString();
                        efw.N = reader["Notes"].ToString();
                        efw.Tr = GetTrailerId(reader["Trailer"].ToString());


                        var insertedDate =
                            reader["InsertedDate"] == DBNull.Value //SD or bad imports
                                ? new DateTime(1970, 1, 1)
                                : Convert.ToDateTime(reader["InsertedDate"]);


                        efw.InsertedDate = insertedDate;

                        efw.LastChangeDate =
                            reader["LastChangeDate"] == DBNull.Value
                                ? insertedDate
                                : Convert.ToDateTime(reader["LastChangeDate"]);

                        efw.HasPoster = (int)(long)reader["HasPoster"] == 1;

                        result.Add(efw);
                    }
                }

                conn.Close();
            }

            return result;
        }


        public static List<SeriesEpisodesShortInfo> GetCollectionsInfo()
        {
            var result = new List<SeriesEpisodesShortInfo>();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                /*
                 *SELECT fd1.Id, fd1.FileName, sum()
                    FROM FileDetail fd1
                        LEFT OUTER JOIN FileDetail fd2 ON fd1.Id = fd2.ParentId
                    WHERE fd1.ParentId = -1
                    ORDER BY fd1.FileName
                 */
                var commandSource = new SQLiteCommand("SELECT Id, FileName, SectionType, Notes FROM FileDetail WHERE ParentId = -10 ORDER BY FileName", conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SeriesEpisodesShortInfo
                        {
                            Id = (int)(long)reader["Id"],
                            FileName = reader["FileName"].ToString(),
                            Season = string.Empty,
                            SeriesId = (int)(long)reader["Id"],
                            IsSeries = true,
                            SectionType = (int)reader["SectionType"],
                            Notes = reader["Notes"].ToString()
                        });
                    }
                }

                conn.Close();
            }

            return result;
        }

        public static OperationResult InsertCollection(string title, string notes, int sectionType,
            byte[] poster)
        {
            var result = new OperationResult();
            const int parentId = -10;

            try
            {
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
                {
                    conn.Open();
                    SQLiteCommand cmd;

                    #region Checks

                    var checks = "SELECT COUNT(*) FROM FileDetail WHERE ParentId = @ParentId AND FileName = @FileName";

                    cmd = new SQLiteCommand(checks, conn);
                    cmd.Parameters.AddWithValue("@ParentId", parentId);
                    cmd.Parameters.AddWithValue("@FileName", title);
                    //sectionType

                    var count = (int)(long)cmd.ExecuteScalar();

                    if (count > 0)
                        return result.FailWithMessage("A Collection with exactly the same name already exists!");


                    #endregion

                    #region FileDetail

                    var insertString = @"
                        INSERT INTO FileDetail (
                            FileName,
                            ParentId,
                            Notes,
                            SectionType,
                            Poster
                        )
                        VALUES (
                            @FileName,
                            @ParentId,
                            @Notes,
                            @SectionType,
                            @Poster
                        )";

                    cmd = new SQLiteCommand(insertString, conn);
                    cmd.Parameters.AddWithValue("@FileName", title);
                    cmd.Parameters.AddWithValue("@ParentId", parentId);
                    cmd.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes);
                    cmd.Parameters.AddWithValue("@SectionType", sectionType);
                    cmd.Parameters.AddWithValue("@Poster", poster == null ? (object)DBNull.Value : poster);

                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "SELECT last_insert_rowid()";

                    var newFileDetailId = (int)(long)cmd.ExecuteScalar();
                    result.AdditionalDataReturn = newFileDetailId;

                    #endregion

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }

            return result;
        }

        public static OperationResult UpdateCollection(int id, string title, string notes, int sectionType,
            byte[] poster)
        {
            var result = new OperationResult();
            //const int parentId = -10;

            try
            {
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
                {
                    conn.Open();
                    SQLiteCommand cmd;

                    var insertString = @"
                        UPDATE FileDetail
                           SET FileName = @FileName,
                               Notes = @Notes,
                               SectionType = @SectionType,
                               Poster = @Poster
                         WHERE Id = @Id";

                    cmd = new SQLiteCommand(insertString, conn);
                    cmd.Parameters.AddWithValue("@FileName", title);
                    cmd.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes);
                    cmd.Parameters.AddWithValue("@SectionType", sectionType);
                    cmd.Parameters.AddWithValue("@Poster", poster == null ? (object)DBNull.Value : poster);
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }

            Helpers.UnsavedChanges = false; //this setter takes care of the buttons states!
            return result;
        }



        public static List<SeriesEpisodesShortInfo> GetElementsInCollection(int collectionId, int parentSectionType)
        {
            var result = new List<SeriesEpisodesShortInfo>();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource = new SQLiteCommand(string.Format("SELECT Id, FileName, Theme, Quality, ParentId FROM FileDetail WHERE ParentId = {0} ORDER BY FileName", collectionId), conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SeriesEpisodesShortInfo
                        {
                            Id = (int)(long)reader["Id"],
                            FileName = reader["FileName"].ToString(),
                            Theme = reader["Theme"].ToString(),
                            Quality = reader["Quality"].ToString(),
                            Season = "0", //???
                            SeriesId = (int)reader["ParentId"],
                            IsEpisode = true,
                            SectionType = parentSectionType
                        });
                    }
                }

                conn.Close();
            }

            return result;
        }

        public static OperationResult SaveBulkChanges(List<int> selectedEpisodes, List<BulkEditField> fieldValuesControls)
        {
            var result = new OperationResult();
            var episodes = string.Join(",", selectedEpisodes);

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();
                var tx = conn.BeginTransaction();

                try
                {
                    try
                    {
                        var sqlString = string.Empty;

                        foreach (var fvc in fieldValuesControls)
                        {
                            if (fvc.FieldName == "Language")
                            {
                                sqlString = string.Format(@"
                                    UPDATE AudioStream
                                       SET Language = '{0}'
                                     WHERE Id IN
                                        (
                                            SELECT
                                                MIN(Id) AS MinId
                                             FROM AudioStream
                                            WHERE FileDetailId IN ({1})
                                            GROUP BY FileDetailId
                                        )",
                                    fvc.Value, episodes);
                            }
                            else
                            if (fvc.FieldName == "Season")
                            {
                                sqlString = string.Format(@"
                                    UPDATE FileDetail
                                       SET Season = '{0}'
                                    WHERE Id IN ({1})",
                                    fvc.Value, episodes);
                            }
                            else
                            {
                                sqlString = string.Format(@"
                                    UPDATE FileDetail
                                       SET {0} = {1}
                                    WHERE Id IN ({2})",
                                    fvc.FieldName, fvc.Value, episodes);
                            }

                            var cmd = new SQLiteCommand(sqlString, conn);
                            cmd.Transaction = tx;
                            cmd.ExecuteNonQuery();

                            //re-building the AudioLanguages in FileDetail
                            if (fvc.FieldName == "Language")
                            {
                                sqlString = string.Format(@"
                                    SELECT
                                        FileDetailId,
                                        Language
                                    FROM AudioStream
                                    WHERE FileDetailId IN ({0})",
                                    episodes);

                                cmd = new SQLiteCommand(sqlString, conn);
                                cmd.Transaction = tx;
                                var newLanguageValues = new List<SelectableElement>();

                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        newLanguageValues.Add(new SelectableElement((int)reader["FileDetailId"], reader["Language"].ToString()));
                                    }
                                }

                                var groupList =
                                    newLanguageValues
                                        .GroupBy(e => e.Value)
                                        .ToList()
                                        // Because the ToList(), this select projection is not done in the DB
                                        .Select(eg => new SelectableElement(eg.Key, string.Join(", ", eg.Select(i => i.Description))))
                                        .ToList();

                                sqlString = @"
                                    UPDATE FileDetail
                                       SET AudioLanguages = '{0}'
                                    WHERE Id = {1}";

                                foreach (var gEl in groupList)
                                {
                                    cmd = new SQLiteCommand(string.Format(sqlString, gEl.Description, gEl.Value), conn);
                                    cmd.Transaction = tx;
                                    cmd.ExecuteNonQuery();
                                }
                            }
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

                    conn.Close();
                }
            }

            return result;
        }

        //public static string GetMoviesDetails2()
        //{
        //    var result = new List<MovieForWeb>();

        //    using (var conn = new SQLiteConnection(Constants.ConnectionString))
        //    {
        //        conn.Open();

        //        var commandSource = new SQLiteCommand(@"
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

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource = new SQLiteCommand(string.Format(@"
                        SELECT
	                        CASE
                                WHEN Poster IS NULL THEN 0
                                ELSE 1
	                        END AS HasPoster,
                            fd.*
                        FROM FileDetail fd
                        WHERE {0} {1}
                        ORDER BY FileName COLLATE NOCASE ASC",
                        pdfGenParams.ForMovies
                            ? "ParentId IS NULL"
                            : "ParentId = -1",
                        pdfGenParams.PDFGenType == PDFGenType.Christmas
                            ? "AND Theme = 'Christmas'"
                            : pdfGenParams.PDFGenType == PDFGenType.Helloween
                                ? "AND Theme = 'Helloween'"
                                : string.Empty
                        ), conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var mfw = new MovieForWeb();
                        mfw.Id = (int)(long)reader["Id"];
                        mfw.FN = reader["FileName"].ToString();
                        mfw.R = reader["Recommended"].ToString();
                        mfw.RL = reader["RecommendedLink"].ToString();
                        mfw.Y = reader["Year"].ToString();
                        mfw.Q = reader["Quality"].ToString();
                        mfw.S = reader["FileSize2"].ToString();
                        //mfw.B = reader["BitRate"].ToString();

                        mfw.L =
                            reader["Duration"] == DBNull.Value
                                ? "?"
                                : Convert.ToDateTime(reader["Duration"]).ToString("HH:mm:ss");

                        mfw.A = reader["AudioLanguages"].ToString();
                        mfw.SU = reader["SubtitleLanguages"].ToString();

                        mfw.DL = reader["DescriptionLink"].ToString();
                        mfw.T = reader["Theme"].ToString();
                        mfw.N = reader["Notes"].ToString();
                        mfw.Nl = string.Empty;
                        mfw.Tr = GetTrailerId(reader["Trailer"].ToString());

                        //mfw.Cover = reader["Poster"] == DBNull.Value ? null : (byte[])reader["Poster"];
                        mfw.HasPoster = (reader["HasPoster"].GetType() == typeof(long) ? (int)(long)reader["HasPoster"] : (int)reader["HasPoster"]) == 1;

                        if (mfw.HasPoster)
                            mfw.Cover = (byte[])reader["Poster"];

                        var insertedDate =
                            reader["InsertedDate"] == DBNull.Value
                                ? new DateTime(1970, 1, 1)
                                : Convert.ToDateTime(reader["InsertedDate"]);

                        mfw.InsertedDate = insertedDate;

                        mfw.LastChangeDate =
                            reader["LastChangeDate"] == DBNull.Value
                                ? insertedDate
                                : Convert.ToDateTime(reader["LastChangeDate"]);

                        result.Add(mfw);
                    }
                }

                ////should already be there
                //if (!pdfGenParams.ForMovies)
                //{
                //    var episodes = GetEpisodesForWeb(true, SeriesType.Final);

                //    foreach (var s in result)
                //    {
                //        var episodesForSeries = episodes.Where(e => e.SId == s.Id).OrderBy(o => o.SZ).ToList();

                //        var audios = string.Join(",", episodesForSeries.Select(d => d.A).Distinct()).Replace(", ", ",");
                //        s.A = string.Join(", ", audios.Split(',').Distinct());

                //        s.B = episodesForSeries.Count().ToString();
                //    }
                //}

                conn.Close();
            }

            return result;
        }

        public static int GetCount(bool forMovies, PDFGenType pdfGenType = PDFGenType.All)
        {
            var result = -1;

            try
            {
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
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

                    var commandSource = new SQLiteCommand(sqlStrig, conn);

                    using (var reader = commandSource.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return (int)(long)reader["CNT"];
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception)
            {
                result = -2;
            }

            return result;
        }

        public static OperationResult GetMoviesForSynopsisImport(bool preserveExisting)
        {
            var result = new OperationResult();
            var returnData = new List<SynopsisImportMovieData>();

            try
            {
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
                {
                    conn.Open();

                    var sqlStrig = string.Format(@"
                        SELECT Id, FileName, DescriptionLink
                        FROM FileDetail
                        WHERE ParentId IS NULL
                            AND DescriptionLink IS NOT NULL AND DescriptionLink <> ''
                            {0}
                        ",
                        preserveExisting
                            ? " AND (Synopsis IS NULL OR Synopsis = '')"
                            : string.Empty);

                    var commandSource = new SQLiteCommand(sqlStrig, conn);

                    using (var reader = commandSource.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnData.Add(
                                new SynopsisImportMovieData
                                {
                                    MovieId = (int)(long)reader["Id"],
                                    FileName = (string)reader["FileName"],
                                    DescriptionLink = (string)reader["DescriptionLink"]
                                });
                        }
                    }

                    conn.Close();
                }

                result.AdditionalDataReturn = returnData;
            }
            catch (Exception ex)
            {
                result.FailWithMessage(ex);
            }

            return result;
        }

        public static OperationResult SaveSynopsis(int movieId, string synopsis)
        {
            var result = new OperationResult();

            try
            {
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
                {
                    conn.Open();

                    var updateString = @"
                        UPDATE FileDetail
                           SET Synopsis = @Synopsis
                         WHERE Id = @Id";

                    var cmd = new SQLiteCommand(updateString, conn);
                    cmd.Parameters.AddWithValue("@Synopsis", synopsis);
                    cmd.Parameters.AddWithValue("@Id", movieId);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                result.FailWithMessage(ex);
            }

            return result;
        }

        public static OperationResult GetMoviesForCommonSenseMediaDataImport(bool forMovies, bool preserveExisting)
        {
            var result = new OperationResult();
            var returnData = new List<SynopsisImportMovieData>();

            try
            {
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
                {
                    conn.Open();

                    var sqlStrig = string.Format(@"
                        SELECT fd.Id, FileName, RecommendedLink
                        FROM FileDetail fd
                            LEFT OUTER JOIN CommonSenseMediaDetail csm ON csm.FileDetailId = fd.Id
                        WHERE
                            fd.RecommendedLink LIKE '%commonsensemedia%'
                            {0}
                            {1}
                        ",
                        preserveExisting
                            ? " AND (csm.Id IS NULL)"
                            : string.Empty,
                        forMovies
                            ? " AND ParentId IS NULL "
                            : " AND ParentId = -1"
                        );

                    var commandSource = new SQLiteCommand(sqlStrig, conn);

                    using (var reader = commandSource.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnData.Add(
                                new SynopsisImportMovieData
                                {
                                    MovieId = (int)(long)reader["Id"],
                                    FileName = (string)reader["FileName"],
                                    DescriptionLink = (string)reader["RecommendedLink"]
                                });
                        }
                    }

                    conn.Close();
                }

                result.AdditionalDataReturn = returnData;
            }
            catch (Exception ex)
            {
                result.FailWithMessage(ex);
            }

            return result;
        }

        public static OperationResult SaveCommonSenseMediaData(int movieId, CSMScrapeResult csmData)
        {
            var result = new OperationResult();

            try
            {
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
                {
                    conn.Open();

                    result = RemoveCommonSenseMediaData(movieId, conn);
                    if (!result.Success)
                        return result;

                    SQLiteCommand cmd;

                    var sqlString = @"
                        INSERT INTO CommonSenseMediaDetail (
                            FileDetailId,
                            DateTimeStamp,
                            GreenAge,
                            Rating,
                            ShortDescription,
                            Review,
                            AdultRecomendedAge,
                            AdultRating,
                            ChildRecomendedAge,
                            ChildRating,
                            Story,
                            IsItAnyGood
                        )
                        VALUES (
                            @FileDetailId,
                            @DateTimeStamp,
                            @GreenAge,
                            @Rating,
                            @ShortDescription,
                            @Review,
                            @AdultRecomendedAge,
                            @AdultRating,
                            @ChildRecomendedAge,
                            @ChildRating,
                            @Story,
                            @IsItAnyGood
                        );";

                    cmd = new SQLiteCommand(sqlString, conn);
                    cmd.Parameters.AddWithValue("@FileDetailId", movieId);
                    cmd.Parameters.AddWithValue("@DateTimeStamp", DateTime.Now);
                    cmd.Parameters.AddWithValue("@GreenAge", csmData.GreenAge);
                    cmd.Parameters.AddWithValue("@Rating", csmData.Rating);
                    cmd.Parameters.AddWithValue("@ShortDescription", csmData.ShortDescription);
                    cmd.Parameters.AddWithValue("@Review", csmData.Review);
                    cmd.Parameters.AddWithValue("@AdultRecomendedAge", csmData.AdultRecomendedAge);
                    cmd.Parameters.AddWithValue("@AdultRating", csmData.AdultRating);
                    cmd.Parameters.AddWithValue("@ChildRecomendedAge", csmData.ChildRecomendedAge);
                    cmd.Parameters.AddWithValue("@ChildRating", csmData.ChildRating);
                    cmd.Parameters.AddWithValue("@Story", csmData.Story);
                    cmd.Parameters.AddWithValue("@IsItAnyGood", csmData.IsItAnyGood);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT last_insert_rowid()";
                    var newId = (int)(long)cmd.ExecuteScalar();


                    if (csmData.TalkWithKidsAbout.Any())
                    {
                        sqlString = @"
                            INSERT INTO CommonSenseMediaDetail_TalkAbout (
                                CSMDetailId,
                                SummaryPoint
                            )
                            VALUES (
                                @CSMDetailId,
                                @SummaryPoint
                            )";

                        foreach (var s in csmData.TalkWithKidsAbout)
                        {
                            if (string.IsNullOrEmpty(s)) continue;

                            cmd = new SQLiteCommand(sqlString, conn);
                            cmd.Parameters.AddWithValue("@CSMDetailId", newId);
                            cmd.Parameters.AddWithValue("@SummaryPoint", s);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (csmData.ALotOrALittle.Any())
                    {
                        sqlString = @"
                            INSERT INTO CommonSenseMediaDetail_ALotOrALittle (
                                CSMDetailId,
                                Category,
                                Rating,
                                Description
                            )
                            VALUES (
                                @CSMDetailId,
                                @Category,
                                @Rating,
                                @Description
                            )";

                        foreach (var aal in csmData.ALotOrALittle)
                        {
                            cmd = new SQLiteCommand(sqlString, conn);
                            cmd.Parameters.AddWithValue("@CSMDetailId", newId);
                            cmd.Parameters.AddWithValue("@Category", (int)aal.Category);
                            cmd.Parameters.AddWithValue("@Rating", aal.Rating);
                            cmd.Parameters.AddWithValue("@Description", aal.Description);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                result.FailWithMessage(ex);
            }

            return result;
        }


        public static OperationResult SetSeriesValuesFromEpisodes(int seriesId)
        {
            var result = new OperationResult();

            try
            {
                var episodesAudioLanguages = new List<string>();
                var audios = "?";

                using (var conn = new SQLiteConnection(Constants.ConnectionString))
                {
                    conn.Open();

                    var cmd = new SQLiteCommand(@"
                        SELECT DISTINCT
                            fd.AudioLanguages
                        FROM FileDetail fd
                        WHERE fd.ParentId = @seriesId",
                        conn);

                    cmd.Parameters.AddWithValue("@seriesId", seriesId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            episodesAudioLanguages.Add(reader["AudioLanguages"].ToString());
                        }
                    }

                    if (episodesAudioLanguages.Count > 0)
                    {
                        var tempStr = string.Join(",", episodesAudioLanguages.Select(d => d).Distinct()).Replace(", ", ",");
                        audios = string.Join(", ", tempStr.Split(',').Distinct());

                        if (string.IsNullOrEmpty(audios))
                            audios = "?";
                    }

                    var updateString = @"
                            UPDATE FileDetail
                               SET AudioLanguages = @audios
                             WHERE Id = @seriesId";

                    cmd = new SQLiteCommand(updateString, conn);
                        cmd.Parameters.AddWithValue("@audios", audios);
                        cmd.Parameters.AddWithValue("@seriesId", seriesId);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }

                result.AdditionalDataReturn = audios;
            }
            catch (Exception ex)
            {
                result.FailWithMessage(ex);
            }

            return result;
        }

        public static OperationResult UpdateSeasonName(int seriesId, string oldName, string newName)
        {
            var result = new OperationResult();

            try
            {
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
                {
                    conn.Open();
                    SQLiteCommand cmd;

                    var insertString = @"
                        UPDATE FileDetail
                           SET Season = @newName
                         WHERE ParentId = @seriesId AND Season = @oldName";

                    cmd = new SQLiteCommand(insertString, conn);
                    cmd.Parameters.AddWithValue("@newName", newName);
                    cmd.Parameters.AddWithValue("@seriesId", seriesId);
                    cmd.Parameters.AddWithValue("@oldName", oldName);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                result.FailWithMessage(ex);
            }

            return result;
        }

        public static OperationResult LoadCSMData(int fileDetailsId)
        {
            var result = new OperationResult();
            var resultObj = new CSMScrapeResult();

            try
            {
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
                {
                    conn.Open();
                    SQLiteCommand cmd;

                    var sqlString = string.Format("select * from CommonSenseMediaDetail where FileDetailId = {0} LIMIT 1", fileDetailsId);
                    cmd = new SQLiteCommand(sqlString, conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultObj.Id = (int)(long)reader["id"];
                            resultObj.MovieId = fileDetailsId;
                            resultObj.GreenAge = reader["GreenAge"].ToString();
                            resultObj.Rating = (int)reader["Rating"];
                            resultObj.ShortDescription = reader["ShortDescription"].ToString();
                            resultObj.Review = reader["Review"].ToString();
                            resultObj.AdultRecomendedAge = reader["AdultRecomendedAge"].ToString();
                            resultObj.AdultRating = reader["AdultRating"] == DBNull.Value ? (int?)null : (int)reader["AdultRating"];
                            resultObj.ChildRecomendedAge = reader["ChildRecomendedAge"].ToString();
                            resultObj.ChildRating = reader["ChildRating"] == DBNull.Value ? (int?)null : (int)reader["ChildRating"];
                            resultObj.Story = reader["Story"].ToString();
                            resultObj.IsItAnyGood = reader["IsItAnyGood"].ToString();
                        }
                    }

                    sqlString = string.Format("select * from CommonSenseMediaDetail_TalkAbout where CSMDetailId = {0}", resultObj.Id);
                    cmd = new SQLiteCommand(sqlString, conn);

                    resultObj.TalkWithKidsAbout = new List<string>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultObj.TalkWithKidsAbout.Add(reader["SummaryPoint"].ToString());
                        }
                    }

                    sqlString = string.Format("select * from CommonSenseMediaDetail_ALotOrALittle where CSMDetailId = {0}", resultObj.Id);
                    cmd = new SQLiteCommand(sqlString, conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultObj.ALotOrALittle.Add(
                                new ALotOrALittle()
                                {
                                    Category = (ALotOrAlittleElements)(int)reader["Category"],
                                    Rating = (int)reader["Rating"],
                                    Description = reader["Description"].ToString()
                                }); ;
                        }
                    }

                    result.AdditionalDataReturn = resultObj;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                result.FailWithMessage(ex);
            }

            return result;
        }
        
        public static ArrayList ListTables()
        {
            var result = new ArrayList();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var sqlString = "SELECT name FROM sqlite_master WHERE type ='table' AND name NOT LIKE 'sqlite_%'";
                var cmd = new SQLiteCommand(sqlString, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            string table_name = reader.GetString(0);
                            result.Add(table_name);
                        }
                    }
                }

                conn.Close();
            }

            return result;
        }

        public static ArrayList ListColumns(string table)
        {
            var result = new ArrayList();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var sqlString = "SELECT * FROM " + table;
                var cmd = new SQLiteCommand(sqlString, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        result.Add(reader.GetName(i) + " : " + reader.GetFieldType(i));
                    }
                }

                conn.Close();
            }

            return result;
        }

        public static List<DbStructureModel> ListTables2()
        {
            var result = new List<DbStructureModel>();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var sqlString = "SELECT name FROM sqlite_master WHERE type ='table' AND name NOT LIKE 'sqlite_%'";
                var cmd = new SQLiteCommand(sqlString, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            result.Add(new DbStructureModel() { Name = reader.GetString(0) });
                        }
                    }
                }

                conn.Close();
            }

            return result;
        }

        public static List<DbStructureModel> ListColumns2(string table)
        {
            var result = new List<DbStructureModel>();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var sqlString = "SELECT * FROM " + table;
                var cmd = new SQLiteCommand(sqlString, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        result.Add(
                            new DbStructureModel()
                            {
                                Name = reader.GetName(i),
                                ColumnType = reader.GetFieldType(i).ToString().Replace("System.", ""),
                            });
                    }
                }

                conn.Close();
            }

            return result;
        }

        public static OperationResult ExecuteSQLcommand(string toExecute)
        {
            var result = new OperationResult();

            try
            {
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
                {
                    conn.Open();

                    var cmd = new SQLiteCommand(toExecute, conn);

                    if (toExecute.ToLower().StartsWith("select"))
                    {
                        var resultData = new List<object>();

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var dynamicObj = new ExpandoObject() as IDictionary<string, object>;
                                    for (var i = 0; i < reader.FieldCount; i++)
                                    {
                                        if (reader[i].GetType() == typeof(byte[])) continue;
                                        dynamicObj.Add(reader.GetName(i), reader[i]);
                                    }

                                    resultData.Add(dynamicObj);
                                }
                            }
                        }

                        result.AdditionalDataReturn = resultData;
                    }
                    else
                    {
                        var affectedEntities = cmd.ExecuteNonQuery();
                        result.AdditionalDataReturn = affectedEntities;
                    }                

                    conn.Close();
                }

                return result;
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex, false);
            }
        }

        public static DataTable ToDataTable(/*this*/ IEnumerable<dynamic> items)
        {
            if (!items.Any()) return null;

            var table = new DataTable { TableName = "FilesDetails" };

            var keyCollection = new List<string>();

            items.Cast<IDictionary<string, object>>().ToList().ForEach(x =>
            {
                if (x.Keys.Count > keyCollection.Count)
                    keyCollection = x.Keys.ToList();
            });

            keyCollection.Select(y => table.Columns.Add(y)).ToList();

            foreach (var kv in items.Cast<IDictionary<string, object>>().ToList())
            {
                var row = table.NewRow();
                foreach (var keyName in keyCollection)
                {
                    if (kv.Keys.Contains(keyName))
                    {
                        row[keyName] = kv[keyName];
                    }
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static List<CSMclientData> GetCSMclient()
        {
            var result = new List<CSMclientData>();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var cmd = new SQLiteCommand(@"
                    SELECT
                        csmd.*
                    FROM FileDetail fd
                        LEFT OUTER JOIN CommonSenseMediaDetail csmd ON fd.Id = csmd.FileDetailId 
                    WHERE csmd.Id IS NOT NULL AND csmd.Rating IS NOT NULL", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var csmClientData = new CSMclientData();
                        csmClientData.id = (int)(long)reader["FileDetailId"];

                        csmClientData.csm = new CSMScrapeResultClient();
                        csmClientData.csm.ga = reader["GreenAge"].ToString();
                        csmClientData.csm.rt = reader["Rating"] == DBNull.Value ? 0 : (int)reader["Rating"];
                        csmClientData.csm.sd = reader["ShortDescription"].ToString();
                        csmClientData.csm.r = reader["Review"].ToString();
                        csmClientData.csm.aa = reader["AdultRecomendedAge"].ToString();
                        csmClientData.csm.rta = reader["AdultRating"] == DBNull.Value ? 0 : (int)reader["AdultRating"];
                        csmClientData.csm.ka = reader["ChildRecomendedAge"].ToString();
                        csmClientData.csm.rtk = reader["ChildRating"] == DBNull.Value ? 0 : (int)reader["ChildRating"];
                        csmClientData.csm.s = reader["Story"].ToString();
                        csmClientData.csm.g = reader["IsItAnyGood"].ToString();

                        var csmId = (long)reader["Id"];

                        var cmd2 = new SQLiteCommand(
                            string.Format("select * from CommonSenseMediaDetail_ALotOrALittle where CSMDetailId = {0}", csmId), conn);

                        using (var reader2 = cmd2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                csmClientData.csm.aloal.Add(
                                    new ALotOrAlittleClient
                                    {
                                        c = (int)reader2["Category"],
                                        r = (int)reader2["Rating"],
                                        d = reader2["Description"].ToString(),
                                    });
                            }
                        }

                        cmd2 = new SQLiteCommand(
                            string.Format("select * from CommonSenseMediaDetail_TalkAbout where CSMDetailId = {0}", csmId), conn);

                        using (var reader2 = cmd2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                csmClientData.csm.ta.Add(reader2["SummaryPoint"].ToString());
                            }
                        }

                        result.Add(csmClientData);
                    }
                }

                conn.Close();
            }

            return result;
        }

        public static void PersistExecution(string toExecute, string errorMessage)
        {
            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                SQLiteCommand cmd;
                var sqlString = @"
                    INSERT INTO SQLExecutionHistory (
                        DateTimeStamp,
                        SQLExecuted,
                        ErrorMessage
                    )
                    VALUES (
                        @DateTimeStamp,
                        @SQLExecuted,
                        @ErrorMessage
                    );";

                cmd = new SQLiteCommand(sqlString, conn);
                cmd.Parameters.AddWithValue("@DateTimeStamp", DateTime.Now);
                cmd.Parameters.AddWithValue("@SQLExecuted", toExecute);
                cmd.Parameters.AddWithValue("@ErrorMessage", errorMessage);
                cmd.ExecuteNonQuery();              

                conn.Close();
            }
        }

        public static List<SQLExecutionHistory> GetSqlHistory()
        {
            var result = new List<SQLExecutionHistory>();

            using (var conn = new SQLiteConnection(Constants.ConnectionString))
            {
                conn.Open();

                var cmd = new SQLiteCommand(@"
                    SELECT
                        *
                    FROM SQLExecutionHistory
                    ORDER BY Id DESC", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sqlHistoryItem = new SQLExecutionHistory();
                        sqlHistoryItem.Id = (int)(long)reader["Id"];
                        sqlHistoryItem.DateTimeStamp = (DateTime)reader["DateTimeStamp"];
                        sqlHistoryItem.SQLExecuted = reader["SQLExecuted"].ToString();
                        sqlHistoryItem.ErrorMessage = reader["ErrorMessage"].ToString();


                        result.Add(sqlHistoryItem);
                    }
                }

                conn.Close();
            }

            return result;    
        }
    }
}