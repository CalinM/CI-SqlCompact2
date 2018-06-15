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
        public static DataTable Series { get; set; }
        public static List<CachedMovieStills> CachedMoviesStills = new List<CachedMovieStills>();
        public static MovieTechnicalDetails CurrentMTD;
        public static BindingList<MovieShortInfo> GetMoviesGridData()
        {
            var result = new BindingList<MovieShortInfo>();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                var commandSource = new SqlCeCommand("select * from FileDetail where ParentId is null order by FileName", conn);

                using (var reader = commandSource.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new MovieShortInfo
                                    {
                                        Id = (int)reader["Id"],
                                        FileName = reader["FileName"].ToString(),
                                        HasPoster = true//reader["Poster"].GetType() != typeof(DBNull)
                                    });
                    }
                }
            }

            return result;
        }

        public static void LoadSeriesData()
        {
            Series = new DataTable();

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                //loading Series AND Episodes!
                var cmd = new SqlCeCommand("select * from FileDetail where ParentId is not null", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    Series.Load(reader);
                }
            }
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
                            SubtitleLanguages)
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
                            @SubtitleLanguages)";

                    var cmd = new SqlCeCommand(insertString, conn);
                    cmd.Parameters.AddWithValue("@FileName", mtd.FileName);
                    cmd.Parameters.AddWithValue("@Year", eip.Year);
                    cmd.Parameters.AddWithValue("@Format", mtd.Format);
                    cmd.Parameters.AddWithValue("@Encoded_Application", mtd.Encoded_Application);
                    cmd.Parameters.AddWithValue("@FileSize", mtd.FileSize);
                    cmd.Parameters.AddWithValue("@FileSize2", mtd.FileSize2);
                    cmd.Parameters.AddWithValue("@Duration", mtd.DurationAsDateTime);
                    cmd.Parameters.AddWithValue("@TitleEmbedded", mtd.Title);
                    cmd.Parameters.AddWithValue("@CoverEmbedded", mtd.Cover);
                    cmd.Parameters.AddWithValue("@Season", eip.Season);
                    cmd.Parameters.AddWithValue("@InsertedDate", DateTime.Now);



                    cmd.Parameters.AddWithValue("@Quality", GetQualityStrFromSize(mtd));
                    cmd.Parameters.AddWithValue("@ParentId", eip.ParentId);
                    //cmd.Parameters.AddWithValue("@Poster", mtd.Thumbnail ?? (object)DBNull.Value);

                    var audioLanguages = string.Join(", ", mtd.AudioStreams.Select(a => a.Language == "" ? "?" : a.Language).Distinct());
                    cmd.Parameters.AddWithValue("@AudioLanguages", audioLanguages);
                    var subtitleLanguages = string.Join(", ", mtd.SubtitleStreams.Select(a => a.Language == "" ? "?" : a.Language).Distinct());
                    cmd.Parameters.AddWithValue("@SubtitleLanguages", subtitleLanguages);

                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Select @@Identity";

                    var newFileDetailId = (int)(decimal)cmd.ExecuteScalar();

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

        public static OperationResult SaveMTD()
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
                               //,Poster = @Poster
                         WHERE Id = @Id";

                    var cmd = new SqlCeCommand(updateString, conn);
                    cmd.Parameters.AddWithValue("@FileName", CurrentMTD.FileName);
                    cmd.Parameters.AddWithValue("@Year", CurrentMTD.Year);
                    cmd.Parameters.AddWithValue("@Format", CurrentMTD.Format);
                    cmd.Parameters.AddWithValue("@Encoded_Application", CurrentMTD.Encoded_Application);
                    cmd.Parameters.AddWithValue("@FileSize", CurrentMTD.FileSize);              //-----
                    cmd.Parameters.AddWithValue("@FileSize2", CurrentMTD.FileSize2);            //-----
                    cmd.Parameters.AddWithValue("@Duration", CurrentMTD.DurationAsDateTime);    //-----
                    cmd.Parameters.AddWithValue("@TitleEmbedded", CurrentMTD.Title);
                    cmd.Parameters.AddWithValue("@CoverEmbedded", CurrentMTD.Cover);
                    cmd.Parameters.AddWithValue("@Season", CurrentMTD.Season);
                    cmd.Parameters.AddWithValue("@Quality", CurrentMTD.Quality);
                    cmd.Parameters.AddWithValue("@AudioLanguages", CurrentMTD.AudioLanguages);
                    cmd.Parameters.AddWithValue("@SubtitleLanguages", CurrentMTD.SubtitleLanguages);
                    cmd.Parameters.AddWithValue("@LastChangeDate", DateTime.Now);

                    //NOT here ... separate save method for base movie info
                    //cmd.Parameters.AddWithValue("@Poster", CurrentMTD.Poster ?? (object)DBNull.Value);

                    cmd.Parameters.AddWithValue("@Id", CurrentMTD.Id);
                    cmd.ExecuteNonQuery();

                    #endregion

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
                        cmd.Parameters.AddWithValue("@TitleEmbedded", videoStream.Title);
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
                        cmd.Parameters.AddWithValue("@TitleEmbedded", audioStream.Title);;
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
                        cmd.Parameters.AddWithValue("@TitleEmbedded", subtitleStream.Title);;
                        cmd.Parameters.AddWithValue("@Language", subtitleStream.Language);
                        cmd.Parameters.AddWithValue("@Id", subtitleStream.Id);
                        cmd.ExecuteNonQuery();
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

        public static OperationResult LoadMTD(int fileDetailId)
        {
            var result = new OperationResult();
            var mtd = new MovieTechnicalDetails();

            try
            {
                var fileInfoRow = Series.Select("Id = " + fileDetailId)[0];
                mtd.Id = fileDetailId;
                mtd.ParentId = fileInfoRow["ParentId"] == DBNull.Value ? (int?)null : (int)fileInfoRow["ParentId"];
                mtd.FileName = fileInfoRow["FileName"].ToString();
                mtd.Year = fileInfoRow["Year"].ToString();
                mtd.Format = fileInfoRow["Format"].ToString();
                mtd.Encoded_Application = fileInfoRow["Encoded_Application"].ToString();
                mtd.FileSize = fileInfoRow["FileSize"].ToString();
                mtd.FileSize2 = fileInfoRow["FileSize2"].ToString();
                mtd.Title = fileInfoRow["TitleEmbedded"].ToString();
                mtd.Cover = fileInfoRow["CoverEmbedded"].ToString();
                mtd.Season = fileInfoRow["Season"].ToString();
                mtd.Theme = fileInfoRow["Theme"].ToString();
                mtd.StreamLink = fileInfoRow["StreamLink"].ToString();
                mtd.InsertedDate = fileInfoRow["InsertedDate"] == DBNull.Value ? (DateTime?)null : (DateTime)fileInfoRow["InsertedDate"];
                mtd.InsertedDate = fileInfoRow["LastChangeDate"] == DBNull.Value ? (DateTime?)null : (DateTime)fileInfoRow["LastChangeDate"];
                mtd.Quality = fileInfoRow["Quality"].ToString();
                mtd.Recommended = fileInfoRow["Recommended"].ToString();
                mtd.RecommendedLink = fileInfoRow["RecommendedLink"].ToString();
                mtd.DescriptionLink = fileInfoRow["DescriptionLink"].ToString();
                mtd.Poster = fileInfoRow["Poster"] == DBNull.Value ? null : (byte[])fileInfoRow["Poster"];
                mtd.AudioLanguages = fileInfoRow["AudioLanguages"].ToString();
                mtd.SubtitleLanguages = fileInfoRow["SubtitleLanguages"].ToString();
                mtd.DurationFormatted = ((DateTime)fileInfoRow["Duration"]).ToString("HH:mm:ss");

                using (var conn = new SqlCeConnection(Constants.ConnectionString))
                {
                    conn.Open();

                    var cmd = new SqlCeCommand(string.Format("SELECT * FROM VideoStream WHERE FileDetailId = {0} ORDER BY [Index]", fileDetailId), conn);

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
                    if (!reader.HasRows) return null;

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
