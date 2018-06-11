using Common;
using System;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;

namespace Utils
{
    public static class OldDataMigration
    {
        /*
        public static OperationResult ImportFilmeHD()
        {
            var result = new OperationResult();

            try
            {
                var connDest = new SqlCeConnection(Constants.ConnectionString);
                var connSource = new SqlCeConnection("Data Source = Desene.sdf;Persist Security Info=False");
                connDest.Open();
                connSource.Open();

                const string insertString =
                    @"
                    INSERT INTO MovieOrSeries (
                        Recommended,
                        RecommendedLink,
                        DescriptionLink,
                        Poster,
                        IsSerie,
                        Notes)
                    VALUES (
                        @Recommended,
                        @RecommendedLink,
                        @DescriptionLink,
                        @Poster,
                        @IsSerie,
                        @Notes)";

                const string insertString2 =
                    @"
                    INSERT INTO FileDetail (
                        MovieOrSeriesId,
                        FileName,
                        Year,
                        Theme,
                        InsertedDate,
                        LastChangeDate)
                    VALUES (
                        @MovieOrSeriesId,
                        @FileName,
                        @Year,
                        @Theme,
                        @InsertedDate,
                        @LastChangeDate)";

                //var sr =  File.CreateText("googllinks.txt");
                var googleUrls = File.ReadAllLines(@"goo.gl to long url.csv")
                                     .Select(ShortToLongUrl.FromCsv)
                                     .ToList();

                var urlSLErrors = "";

                try
                {
                    var commandSource = new SqlCeCommand("select * from Filme", connSource);

                    using (var reader = commandSource.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (string.IsNullOrEmpty(reader["Titlu"].ToString())) continue;

                            var cmd = new SqlCeCommand(insertString, connDest) { CommandType = CommandType.Text };

                            cmd.Parameters.AddWithValue("@Recommended", reader["Recomandat"].ToString());
                            cmd.Parameters.AddWithValue("@RecommendedLink", reader["RecomandatLink"].ToString() != "0" ?  reader["RecomandatLink"].ToString() : string.Empty);
                            cmd.Parameters.AddWithValue("@IsSerie", false);
                            cmd.Parameters.AddWithValue("@Notes", reader["Obs"].ToString());
                            //cmd.Parameters.AddWithValue("@DescriptionLink", reader["MoreInfo"].ToString() != "0" ?  reader["MoreInfo"].ToString() : string.Empty);


                            if (reader["MoreInfo"].ToString() == "0" || string.IsNullOrEmpty(reader["MoreInfo"].ToString()))
                            {
                                cmd.Parameters.AddWithValue("@DescriptionLink", string.Empty);
                            }
                            else
                            {
                                if (reader["MoreInfo"].ToString().Contains("goo.gl"))
                                {
                                    var shortUrl = reader["MoreInfo"].ToString().Replace("#", "");
                                    var longUrlObj = googleUrls.FirstOrDefault(s => s.ShortUrl == shortUrl);

                                    if (longUrlObj == null)
                                    {
                                        cmd.Parameters.AddWithValue("@DescriptionLink", reader["MoreInfo"].ToString());
                                        urlSLErrors += reader["Id"].ToString() + ", ";
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@DescriptionLink", longUrlObj.LongUrl);
                                    }

                                    //sr.WriteLine(reader["MoreInfo"].ToString().Replace("#", ""));
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@DescriptionLink", reader["MoreInfo"].ToString());
                                }
                            }

                            if (!(reader["Cover"] is DBNull))
                                cmd.Parameters.AddWithValue("@Poster", (byte[])reader["Cover"]);
                            else
                                cmd.Parameters.AddWithValue("@Poster", DBNull.Value);


                            cmd.ExecuteNonQuery();


                            cmd.CommandText = "Select @@Identity";
                            var newId = (int)(decimal)cmd.ExecuteScalar();

                            cmd = new SqlCeCommand(insertString2, connDest) { CommandType = CommandType.Text };
                            cmd.Parameters.AddWithValue("@MovieOrSeriesId", newId);
                            cmd.Parameters.AddWithValue("@FileName", reader["Titlu"].ToString());
                            cmd.Parameters.AddWithValue("@Year", reader["An"].ToString());
                            cmd.Parameters.AddWithValue("@Theme", reader["Tematica"].ToString());

                            if (!(reader["DataAdaugare"] is DBNull))
                                cmd.Parameters.AddWithValue("@InsertedDate", (DateTime)reader["DataAdaugare"]);
                            else
                                cmd.Parameters.AddWithValue("@InsertedDate", DBNull.Value);

                            if (!(reader["DataUM"] is DBNull))
                                cmd.Parameters.AddWithValue("@LastChangeDate", (DateTime)reader["DataUM"]);
                            else
                                cmd.Parameters.AddWithValue("@LastChangeDate", DBNull.Value);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                finally
                {
                    connDest.Close();
                    connSource.Close();

                    //sr.Close();
                }
            }
            catch (Exception e)
            {
                result.FailWithMessage(e);
            }

            return result;
        }

        public static OperationResult ImportSeriale()
        {
            var result = new OperationResult();

            try
                {
                var connDest = new SqlCeConnection(Constants.ConnectionString);
                var connSource = new SqlCeConnection("Data Source = Desene.sdf;Persist Security Info=False");

                connDest.Open();
                connSource.Open();

                const string insertString =
                    @"
                    INSERT INTO MovieOrSeries (
                        Recommended,
                        RecommendedLink,
                        DescriptionLink,
                        Poster,
                        IsSerie,
                        Notes)
                    VALUES (
                        @Recommended,
                        @RecommendedLink,
                        @DescriptionLink,
                        @Poster,
                        @IsSerie,
                        @Notes)";

                const string insertString2 =
                    @"
                    INSERT INTO FileDetail (
                        MovieOrSeriesId,
                        ParentId,
                        FileName,
                        Year,
                        Theme,
                        Season,
                        InsertedDate,
                        LastChangeDate)
                    VALUES (
                        @MovieOrSeriesId,
                        @ParentId,
                        @FileName,
                        @Year,
                        @Theme,
                        @Season,
                        @InsertedDate,
                        @LastChangeDate)";

                try
                {
                    var commandSource = new SqlCeCommand("select * from Seriale", connSource);

                    using (var reader = commandSource.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (string.IsNullOrEmpty(reader["Titlu"].ToString())) continue;

                            var cmd = new SqlCeCommand(insertString, connDest) { CommandType = CommandType.Text };

                            cmd.Parameters.AddWithValue("@Recommended", reader["Recomandat"].ToString());
                            cmd.Parameters.AddWithValue("@RecommendedLink", reader["RecomandatLink"].ToString() != "0" ?  reader["RecomandatLink"].ToString() : string.Empty);
                            cmd.Parameters.AddWithValue("@DescriptionLink", reader["MoreInfo"].ToString());
                            cmd.Parameters.AddWithValue("@IsSerie", true);
                            cmd.Parameters.AddWithValue("@Notes", reader["Obs"].ToString());

                            if (!(reader["Cover"] is DBNull))
                                cmd.Parameters.AddWithValue("@Poster", (byte[])reader["Cover"]);
                            else
                                cmd.Parameters.AddWithValue("@Poster", DBNull.Value);


                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "Select @@Identity";
                            var newId = (int)(decimal)cmd.ExecuteScalar();

                            cmd = new SqlCeCommand(insertString2, connDest) { CommandType = CommandType.Text };
                            cmd.Parameters.AddWithValue("@MovieOrSeriesId", newId);
                            cmd.Parameters.AddWithValue("@ParentId", -1);
                            cmd.Parameters.AddWithValue("@FileName", reader["Titlu"].ToString());
                            cmd.Parameters.AddWithValue("@Theme", string.Empty);
                            cmd.Parameters.AddWithValue("@Year", string.Empty);
                            cmd.Parameters.AddWithValue("@Season", string.Empty);

                            if (!(reader["DataAdaugare"] is DBNull))
                                cmd.Parameters.AddWithValue("@InsertedDate", (DateTime)reader["DataAdaugare"]);
                            else
                                cmd.Parameters.AddWithValue("@InsertedDate", DBNull.Value);

                            if (!(reader["DataUM"] is DBNull))
                                cmd.Parameters.AddWithValue("@LastChangeDate", (DateTime)reader["DataUM"]);
                            else
                                cmd.Parameters.AddWithValue("@LastChangeDate", DBNull.Value);


                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "Select @@Identity";
                            var newId2 = (int)(decimal)cmd.ExecuteScalar();

                            var commandSource2 = new SqlCeCommand("select * from Episoade where SerialId = " + reader["Id"], connSource);

                            using (var readerEp = commandSource2.ExecuteReader())
                            {
                                while (readerEp.Read())
                                {
                                    var cmdEp = new SqlCeCommand(insertString2, connDest) { CommandType = CommandType.Text };
                                    cmdEp.Parameters.AddWithValue("@MovieOrSeriesId", newId);
                                    cmdEp.Parameters.AddWithValue("@ParentId", newId2);
                                    cmdEp.Parameters.AddWithValue("@FileName", readerEp["Titlu"].ToString());
                                    cmdEp.Parameters.AddWithValue("@Year", readerEp["An"].ToString());
                                    cmdEp.Parameters.AddWithValue("@Theme", readerEp["Tematica"].ToString());
                                    cmdEp.Parameters.AddWithValue("@Season", readerEp["Sezon"].ToString());

                                    if (!(readerEp["DataAdaugare"] is DBNull))
                                        cmdEp.Parameters.AddWithValue("@InsertedDate", (DateTime)readerEp["DataAdaugare"]);
                                    else
                                        cmdEp.Parameters.AddWithValue("@InsertedDate", DBNull.Value);

                                    if (!(readerEp["DataUM"] is DBNull))
                                        cmdEp.Parameters.AddWithValue("@LastChangeDate", (DateTime)readerEp["DataUM"]);
                                    else
                                        cmdEp.Parameters.AddWithValue("@LastChangeDate", DBNull.Value);


                                    cmdEp.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                finally
                {
                    connDest.Close();
                    connSource.Close();
                }
            }
            catch (Exception e)
            {
                result.FailWithMessage(e);
            }

            return result;
        }
        */

        public static OperationResult ImportFilmeHD()
        {
            var result = new OperationResult();

            try
            {
                var connDest = new SqlCeConnection(Constants.ConnectionString);
                var connSource = new SqlCeConnection("Data Source = Desene.sdf;Persist Security Info=False");
                connDest.Open();
                connSource.Open();

                const string insertString =
                    @"
                    INSERT INTO FileDetail (
                        FileName,
                        Year,
                        Theme,
                        InsertedDate,
                        LastChangeDate,
                        Recommended,
                        RecommendedLink,
                        DescriptionLink,
                        Poster,
                        Notes)
                    VALUES (
                        @FileName,
                        @Year,
                        @Theme,
                        @InsertedDate,
                        @LastChangeDate,
                        @Recommended,
                        @RecommendedLink,
                        @DescriptionLink,
                        @Poster,
                        @Notes)";

                //var sr =  File.CreateText("googllinks.txt");
                var googleUrls = File.ReadAllLines(@"goo.gl to long url.csv")
                                     .Select(ShortToLongUrl.FromCsv)
                                     .ToList();

                var urlSLErrors = "";

                try
                {
                    var commandSource = new SqlCeCommand("select * from Filme", connSource);

                    using (var reader = commandSource.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (string.IsNullOrEmpty(reader["Titlu"].ToString())) continue;

                            var cmd = new SqlCeCommand(insertString, connDest) { CommandType = CommandType.Text };
                            cmd.Parameters.AddWithValue("@FileName", reader["Titlu"].ToString());
                            cmd.Parameters.AddWithValue("@Year", reader["An"].ToString());
                            cmd.Parameters.AddWithValue("@Theme", reader["Tematica"].ToString());

                            if (!(reader["DataAdaugare"] is DBNull))
                                cmd.Parameters.AddWithValue("@InsertedDate", (DateTime)reader["DataAdaugare"]);
                            else
                                cmd.Parameters.AddWithValue("@InsertedDate", DBNull.Value);

                            if (!(reader["DataUM"] is DBNull))
                                cmd.Parameters.AddWithValue("@LastChangeDate", (DateTime)reader["DataUM"]);
                            else
                                cmd.Parameters.AddWithValue("@LastChangeDate", DBNull.Value);

                            cmd.Parameters.AddWithValue("@Recommended", reader["Recomandat"].ToString());
                            cmd.Parameters.AddWithValue("@RecommendedLink", reader["RecomandatLink"].ToString() != "0" ?  reader["RecomandatLink"].ToString() : string.Empty);
                            cmd.Parameters.AddWithValue("@Notes", reader["Obs"].ToString());
                            //cmd.Parameters.AddWithValue("@DescriptionLink", reader["MoreInfo"].ToString() != "0" ?  reader["MoreInfo"].ToString() : string.Empty);


                            if (reader["MoreInfo"].ToString() == "0" || string.IsNullOrEmpty(reader["MoreInfo"].ToString()))
                            {
                                cmd.Parameters.AddWithValue("@DescriptionLink", string.Empty);
                            }
                            else
                            {
                                if (reader["MoreInfo"].ToString().Contains("goo.gl"))
                                {
                                    var shortUrl = reader["MoreInfo"].ToString().Replace("#", "");
                                    var longUrlObj = googleUrls.FirstOrDefault(s => s.ShortUrl == shortUrl);

                                    if (longUrlObj == null)
                                    {
                                        cmd.Parameters.AddWithValue("@DescriptionLink", reader["MoreInfo"].ToString());
                                        urlSLErrors += reader["Id"].ToString() + ", ";
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@DescriptionLink", longUrlObj.LongUrl);
                                    }

                                    //sr.WriteLine(reader["MoreInfo"].ToString().Replace("#", ""));
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@DescriptionLink", reader["MoreInfo"].ToString());
                                }
                            }

                            if (!(reader["Cover"] is DBNull))
                                cmd.Parameters.AddWithValue("@Poster", (byte[])reader["Cover"]);
                            else
                                cmd.Parameters.AddWithValue("@Poster", DBNull.Value);



                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                finally
                {
                    connDest.Close();
                    connSource.Close();

                    //sr.Close();
                }
            }
            catch (Exception e)
            {
                result.FailWithMessage(e);
            }

            return result;
        }

        public static OperationResult ImportSeriale()
        {
            var result = new OperationResult();

            try
                {
                var connDest = new SqlCeConnection(Constants.ConnectionString);
                var connSource = new SqlCeConnection("Data Source = Desene.sdf;Persist Security Info=False");

                connDest.Open();
                connSource.Open();

                const string insertString =
                    @"
                    INSERT INTO FileDetail (
                        ParentId,
                        FileName,
                        Year,
                        Theme,
                        Season,
                        InsertedDate,
                        LastChangeDate,
                        Recommended,
                        RecommendedLink,
                        DescriptionLink,
                        Poster,
                        Notes)
                    VALUES (
                        @ParentId,
                        @FileName,
                        @Year,
                        @Theme,
                        @Season,
                        @InsertedDate,
                        @LastChangeDate,
                        @Recommended,
                        @RecommendedLink,
                        @DescriptionLink,
                        @Poster,
                        @Notes)";

                try
                {
                    var commandSource = new SqlCeCommand("select * from Seriale", connSource);

                    using (var reader = commandSource.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (string.IsNullOrEmpty(reader["Titlu"].ToString())) continue;

                            var cmd = new SqlCeCommand(insertString, connDest) { CommandType = CommandType.Text };
                            cmd.Parameters.AddWithValue("@ParentId", -1);
                            cmd.Parameters.AddWithValue("@FileName", reader["Titlu"].ToString());
                            cmd.Parameters.AddWithValue("@Theme", string.Empty);
                            cmd.Parameters.AddWithValue("@Year", string.Empty);
                            cmd.Parameters.AddWithValue("@Season", string.Empty);

                            if (!(reader["DataAdaugare"] is DBNull))
                                cmd.Parameters.AddWithValue("@InsertedDate", (DateTime)reader["DataAdaugare"]);
                            else
                                cmd.Parameters.AddWithValue("@InsertedDate", DBNull.Value);

                            if (!(reader["DataUM"] is DBNull))
                                cmd.Parameters.AddWithValue("@LastChangeDate", (DateTime)reader["DataUM"]);
                            else
                                cmd.Parameters.AddWithValue("@LastChangeDate", DBNull.Value);

                            cmd.Parameters.AddWithValue("@Recommended", reader["Recomandat"].ToString());
                            cmd.Parameters.AddWithValue("@RecommendedLink", reader["RecomandatLink"].ToString() != "0" ?  reader["RecomandatLink"].ToString() : string.Empty);
                            cmd.Parameters.AddWithValue("@DescriptionLink", reader["MoreInfo"].ToString());
                            cmd.Parameters.AddWithValue("@Notes", reader["Obs"].ToString());

                            if (!(reader["Cover"] is DBNull))
                                cmd.Parameters.AddWithValue("@Poster", (byte[])reader["Cover"]);
                            else
                                cmd.Parameters.AddWithValue("@Poster", DBNull.Value);


                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "Select @@Identity";
                            var seriesId = (int)(decimal)cmd.ExecuteScalar();

                            var commandSource2 = new SqlCeCommand("select * from Episoade where SerialId = " + reader["Id"], connSource);

                            using (var readerEp = commandSource2.ExecuteReader())
                            {
                                while (readerEp.Read())
                                {
                                    var cmdEp = new SqlCeCommand(insertString, connDest) { CommandType = CommandType.Text };
                                    cmdEp.Parameters.AddWithValue("@ParentId", seriesId);
                                    cmdEp.Parameters.AddWithValue("@FileName", readerEp["Titlu"].ToString());
                                    cmdEp.Parameters.AddWithValue("@Year", readerEp["An"].ToString());
                                    cmdEp.Parameters.AddWithValue("@Theme", readerEp["Tematica"].ToString());
                                    cmdEp.Parameters.AddWithValue("@Season", readerEp["Sezon"].ToString());

                                    if (!(readerEp["DataAdaugare"] is DBNull))
                                        cmdEp.Parameters.AddWithValue("@InsertedDate", (DateTime)readerEp["DataAdaugare"]);
                                    else
                                        cmdEp.Parameters.AddWithValue("@InsertedDate", DBNull.Value);

                                    if (!(readerEp["DataUM"] is DBNull))
                                        cmdEp.Parameters.AddWithValue("@LastChangeDate", (DateTime)readerEp["DataUM"]);
                                    else
                                        cmdEp.Parameters.AddWithValue("@LastChangeDate", DBNull.Value);

                                    cmdEp.Parameters.AddWithValue("@Recommended", DBNull.Value);
                                    cmdEp.Parameters.AddWithValue("@RecommendedLink", DBNull.Value);
                                    cmdEp.Parameters.AddWithValue("@DescriptionLink", DBNull.Value);
                                    cmdEp.Parameters.AddWithValue("@Notes", DBNull.Value);
                                    cmdEp.Parameters.AddWithValue("@Poster", DBNull.Value);

                                    cmdEp.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                finally
                {
                    connDest.Close();
                    connSource.Close();
                }
            }
            catch (Exception e)
            {
                result.FailWithMessage(e);
            }

            return result;
        }
    }

    class ShortToLongUrl
    {
        public string ShortUrl { get; set; }
        public string LongUrl {get; set;}

        public static ShortToLongUrl FromCsv(string csvLine)
        {
            var values = csvLine.Split(',');

            return new ShortToLongUrl { ShortUrl = values[0], LongUrl = values[1] };
        }
    }
}
