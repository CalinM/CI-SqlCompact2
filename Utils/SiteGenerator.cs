using Common;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Utils
{
    public class SiteGenerator
    {
        public static OperationResult GenerateSiteFiles(SiteGenParams siteGenParams, IntPtr handle)
        {
            var result = new OperationResult();
            var jsS = new JavaScriptSerializer();
            var genDetails = DateTime.Now.ToString("yyyyMMdd");


            #region Movies

            var moviesData = Desene.DAL.GetMoviesForWeb();

            if (siteGenParams.SavePosters)
            {
                var imgsPath = Path.Combine(siteGenParams.Location, "Imgs\\Movies");
                if (!Directory.Exists(imgsPath))
                    Directory.CreateDirectory(imgsPath);

                var frmProgressIndicator = new FrmProgressIndicator("Site generation - Movies Posters", "-", moviesData.Count);
                frmProgressIndicator.Argument = new KeyValuePair<SiteGenParams, List<MovieForWeb>>(siteGenParams, moviesData);
                frmProgressIndicator.DoWork += formPI_DoWork_GenerateSitePosters_Movies;

                switch (frmProgressIndicator.ShowDialog())
                {
                    case DialogResult.Cancel:
                        result.Success = false;
                        result.CustomErrorMessage = "Operation has been canceled";

                        return result;

                    case DialogResult.Abort:
                        result.Success = false;
                        result.CustomErrorMessage = frmProgressIndicator.Result.Error.Message;

                        return result;

                    case DialogResult.OK:
                        result.AdditionalDataReturn = frmProgressIndicator.Result.Result;
                        break;
                }
            }

            #region Movies thumbnails

            result = SaveMoviesThumbnails(siteGenParams, moviesData, "Movies");
            if (!result.Success)
                return result;

            #endregion

            #region Extract Details

            var movieListDetails =
                string.Format("The current list contains: {0} FullHD, {1} HD, {2} SD and {3} movies of unknown quality (summing aprox. {4} GB)",
                    moviesData.Count(f => f.Q == "FullHD"),
                    moviesData.Count(f => f.Q == "HD"),
                    moviesData.Count(f => f.Q == "SD"),
                    moviesData.Count(f => f.Q == "NotSet"),
                    //movies.Sum(s => s.DimensiuneInt) / 1024
                    "?");

            var newMovies =
                moviesData
                    .OrderByDescending(o => o.InsertedDate)
                    .Select(md => md.Id)
                    .Take(25)
                    .ToList();

            var updatedMovies =
                moviesData
                    .Where(md => md.LastChangeDate.Subtract(md.InsertedDate).Days > 1)
                    .OrderByDescending(o => o.LastChangeDate)
                    .Select(md => md.Id)
                    .Take(25)
                    .ToList();

            var detMovieInfo =
                string.Format("var moviesData = {0}; var genDetails = '{1}'; var moviesStat = '{2}'; var newMovies = {3}; var updatedMovies = {4};",
                    jsS.Serialize(moviesData),
                    genDetails,
                    movieListDetails,
                    jsS.Serialize(newMovies),
                    jsS.Serialize(updatedMovies));

            var moviesDetails2 =
                string.Format("var moviesData2 = {0};",
                    jsS.Serialize(Desene.DAL.GetMoviesDetails2ForWeb()
                ));

            #endregion

            #endregion


            #region Series

            var seriesData = Desene.DAL.GetSeriesForWeb(SeriesType.Final);
            var episodesData = Desene.DAL.GetEpisodesForWeb(siteGenParams.PreserveMarkesForExistingThumbnails, SeriesType.Final);


            var seriesWithInsertedEp = new List<int>();

            foreach (var epData in episodesData.OrderByDescending(o => o.InsertedDate))
            {
                if (seriesWithInsertedEp.IndexOf(epData.SId) == -1)
                {
                    seriesWithInsertedEp.Add(epData.SId);

                    if (seriesWithInsertedEp.Count() >= 25)
                        break;
                }
            }

            #region Series posters

            result = SavePosters(siteGenParams, seriesData, "Series");
            if (!result.Success)
                return result;

            #endregion

            #region Series Episodes thumbnails

            result = SaveEpisodesThumbnails(siteGenParams, episodesData, "Series");
            if (!result.Success)
                return result;

            #endregion

            #region Extract Series Details - leave it after Thumbnail generation

            Desene.DAL.FillSeriesDataFromEpisodes(ref seriesData, episodesData);

            var seriesListDetails =
                string.Format("The current list contains: {0} Series, combined having {1} episodes, (summing aprox. {2} GB)",
                    seriesData.Count,
                    episodesData.Count,
                    //episodes.Sum(s => s.DimensiuneInt) / 1024
                    "?");

            var detSerialeInfo =
                string.Format("var seriesData = {0}; var episodesDataS = {1}; var seriesStat = '{2}'; var newSeriesEpisodes = {3}",
                    jsS.Serialize(seriesData),
                    jsS.Serialize(episodesData),
                    seriesListDetails,
                    jsS.Serialize(seriesWithInsertedEp));

            #endregion

            #endregion


            #region Recordings

            var recordingsData = Desene.DAL.GetSeriesForWeb(SeriesType.Recordings);
            var recordingsEpisodesData = Desene.DAL.GetEpisodesForWeb(siteGenParams.PreserveMarkesForExistingThumbnails, SeriesType.Recordings);
            var detRecordingsInfo = "var recordingsData = []; var episodesDataR = []; var recordingsStat = '-'; var newRecordingsEpisodes = []"; //fallback

            if (recordingsData.Any())
            {
                var recordingsWithInsertedEp = new List<int>();

                foreach (var epData in recordingsEpisodesData.OrderByDescending(o => o.InsertedDate))
                {
                    if (recordingsWithInsertedEp.IndexOf(epData.SId) == -1)
                    {
                        recordingsWithInsertedEp.Add(epData.SId);

                        if (recordingsWithInsertedEp.Count() >= 25)
                            break;
                    }
                }


                #region Recordings posters

                result = SavePosters(siteGenParams, recordingsData, "Recordings");
                if (!result.Success)
                    return result;

                #endregion


                #region Recordings Episodes thumbnails

                result = SaveEpisodesThumbnails(siteGenParams, recordingsEpisodesData, "Recordings");
                if (!result.Success)
                    return result;

                #endregion


                #region Extract Recordings Details - leave it after Thumbnail generation

                Desene.DAL.FillSeriesDataFromEpisodes(ref recordingsData, recordingsEpisodesData);

                var recordingsListDetails =
                    string.Format("The current list contains: {0} Series, combined having {1} episodes, (summing aprox. {2} GB)",
                        recordingsData.Count,
                        recordingsEpisodesData.Count,
                        //episodes.Sum(s => s.DimensiuneInt) / 1024
                        "?");

                detRecordingsInfo =
                    string.Format("var recordingsData = {0}; var episodesDataR = {1}; var recordingsStat = '{2}'; var newRecordingsEpisodes = {3}",
                        jsS.Serialize(recordingsData),
                        jsS.Serialize(recordingsEpisodesData),
                        recordingsListDetails,
                        jsS.Serialize(recordingsWithInsertedEp));

                #endregion
            }

            #endregion


            result.AdditionalDataReturn = new GeneratedJSData(detMovieInfo, detSerialeInfo, detRecordingsInfo, moviesDetails2);
            return result;
        }

        private static OperationResult SavePosters(SiteGenParams siteGenParams, List<SeriesForWeb> data, string caption)
        {
            var result = new OperationResult();

            if (siteGenParams.SavePosters)
            {
                var imgsPath = Path.Combine(siteGenParams.Location, "Imgs\\Series");
                var existingPostersForIds = new List<int>();

                if (!Directory.Exists(imgsPath))
                    Directory.CreateDirectory(imgsPath);
                else
                {
                    var d = new DirectoryInfo(imgsPath);
                    var Files = d.GetFiles("*.jpg");

                    foreach (FileInfo file in Files)
                    {
                        var id = int.Parse(file.Name.Split('-', '.')[1]);
                        if (!existingPostersForIds.Contains(id))
                            existingPostersForIds.Add(id);
                    }
                }

                var idsToSavePosters = new List<SeriesForWeb>(data.Where(x => !existingPostersForIds.Contains(x.Id)));

                var formProgressIndicator = new FrmProgressIndicator(string.Format("Site generation - {0} posters", caption), "-", idsToSavePosters.Count);
                formProgressIndicator.Argument = new KeyValuePair<SiteGenParams, List<SeriesForWeb>>(siteGenParams, idsToSavePosters);
                formProgressIndicator.DoWork += formPI_DoWork_GenerateSitePosters_Series;

                switch (formProgressIndicator.ShowDialog())
                {
                    case DialogResult.Cancel:
                        result.Success = false;
                        result.CustomErrorMessage = "Operation has been canceled";

                        return result;

                    case DialogResult.Abort:
                        result.Success = false;
                        result.CustomErrorMessage = formProgressIndicator.Result.Error.Message;

                        return result;

                    case DialogResult.OK:
                        result.AdditionalDataReturn = formProgressIndicator.Result.Result;
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Reads the saved MOVIE poster (from the database) and, convert them to smaller size and save them on disk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void formPI_DoWork_GenerateSitePosters_Movies(FrmProgressIndicator sender, DoWorkEventArgs e)
        {
            var siteGenDetails = (KeyValuePair<SiteGenParams, List<MovieForWeb>>)e.Argument;
            var errorsWhileSaving = new List<TechnicalDetailsImportError>();

            var i = 0;
            foreach (var movieForWebDet in siteGenDetails.Value)
            {
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                i++;
                sender.SetProgress(i, Path.GetFileName(movieForWebDet.FN));

                if (!movieForWebDet.HasPoster) continue;

                var fileName = Path.Combine(siteGenDetails.Key.Location, string.Format("Imgs\\Movies\\poster-{0}.jpg", movieForWebDet.Id));
                if (File.Exists(fileName)) continue;

                try
                {
                    var cover = Desene.DAL.GetPoster(movieForWebDet.Id);

                    using (var ms = new MemoryStream(cover))
                    {
                        var imgOgj = GraphicsHelpers.CreatePosterThumbnail(250, 388, Image.FromStream(ms));

                        imgOgj.Save(fileName, ImageFormat.Jpeg);
                    }
                }
                catch (Exception ex)
                {
                    errorsWhileSaving.Add(
                        new TechnicalDetailsImportError
                        {
                            FilePath = movieForWebDet.FN,
                            ErrorMesage = OperationResult.GetErrorMessage(ex)
                        });
                }
            }

            e.Result = errorsWhileSaving;
        }

        /// <summary>
        /// Reads the saved SERIES poster (from the database) and, convert them to smaller size and save them on disk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void formPI_DoWork_GenerateSitePosters_Series(FrmProgressIndicator sender, DoWorkEventArgs e)
        {
            var siteGenDetails = (KeyValuePair<SiteGenParams, List<SeriesForWeb>>)e.Argument;
            var errorsWhileSaving = new List<TechnicalDetailsImportError>();

            var i = 0;
            foreach (var seriesForWebDet in siteGenDetails.Value)
            {
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                i++;
                sender.SetProgress(i, Path.GetFileName(seriesForWebDet.FN));

                if (!seriesForWebDet.HasPoster) continue;

                var fileName = Path.Combine(siteGenDetails.Key.Location, string.Format("Imgs\\Series\\poster-{0}.jpg", seriesForWebDet.Id));
                if (File.Exists(fileName)) continue;

                try
                {
                    var cover = Desene.DAL.GetPoster(seriesForWebDet.Id);

                    using (var ms = new MemoryStream(cover))
                    {
                        var imgOgj = GraphicsHelpers.CreatePosterThumbnail(250, 388, Image.FromStream(ms));

                        imgOgj.Save(fileName, ImageFormat.Jpeg);
                    }
                }
                catch (Exception ex)
                {
                    errorsWhileSaving.Add(
                        new TechnicalDetailsImportError
                        {
                            FilePath = seriesForWebDet.FN,
                            ErrorMesage = OperationResult.GetErrorMessage(ex)
                        });
                }
            }

            e.Result = errorsWhileSaving;
        }


        #region Thumbnails

        private static Image CreateStillThumbnail(int newHeight, Image source)
        {
            // Prevent using images internal thumbnail
            source.RotateFlip(RotateFlipType.Rotate180FlipNone);
            source.RotateFlip(RotateFlipType.Rotate180FlipNone);

            var newWidth = source.Width * newHeight / source.Height;

            return source.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);
        }

        #region MOVIES

        private static OperationResult SaveMoviesThumbnails(SiteGenParams siteGenParams, List<MovieForWeb> data, string caption)
        {
            var result = new OperationResult();

            if (siteGenParams.SaveEpisodesThumbnals)
            {
                var imgsPath = Path.Combine(siteGenParams.Location, "Imgs\\Movies\\Thumbnails");
                var existingThumbnailsForIds = new List<int>();

                if (!Directory.Exists(imgsPath))
                    Directory.CreateDirectory(imgsPath);
                else
                {
                    var d = new DirectoryInfo(imgsPath);
                    var Files = d.GetFiles("*.jpg");

                    foreach (FileInfo file in Files)
                    {
                        var id = int.Parse(file.Name.Split('-')[1]);
                        if (!existingThumbnailsForIds.Contains(id))
                            existingThumbnailsForIds.Add(id);
                    }
                }

                var idsToSaveThumbnails = new List<MovieForWeb>(data.Where(x => !existingThumbnailsForIds.Contains(x.Id)));

                var formProgressIndicator = new FrmProgressIndicator(string.Format("Site generation - Movies ({0}) thumbnails", caption), "-", idsToSaveThumbnails.Count);
                formProgressIndicator.Argument = new KeyValuePair<SiteGenParams, List<MovieForWeb>>(siteGenParams, idsToSaveThumbnails);
                formProgressIndicator.DoWork += formPI_DoWork_GenerateSiteMovies_Thumbnails;

                switch (formProgressIndicator.ShowDialog())
                {
                    case DialogResult.Cancel:
                        result.Success = false;
                        result.CustomErrorMessage = "Operation has been canceled";

                        return result;

                    case DialogResult.Abort:
                        result.Success = false;
                        result.CustomErrorMessage = formProgressIndicator.Result.Error.Message;

                        return result;

                    case DialogResult.OK:
                        result.AdditionalDataReturn = formProgressIndicator.Result.Result;
                        break;
                }

                foreach (var episodeForWeb in data.Where(x => existingThumbnailsForIds.Contains(x.Id)))
                {
                    episodeForWeb.Th = 1;
                }
            }
            else
            if (siteGenParams.PreserveMarkesForExistingThumbnails && Directory.Exists(Path.Combine(siteGenParams.Location, "Imgs\\Movies\\Thumbnails\\")))
            {
                var formProgressIndicator = new FrmProgressIndicator("Setting the movies thumbnails marker from existing files", "-", data.Count);
                formProgressIndicator.Argument = new KeyValuePair<SiteGenParams, List<MovieForWeb>>(siteGenParams, data);
                formProgressIndicator.DoWork += formPI_DoWork_GenerateSiteMovies_Thumbnails2;

                switch (formProgressIndicator.ShowDialog())
                {
                    case DialogResult.Cancel:
                        result.Success = false;
                        result.CustomErrorMessage = "Operation has been canceled";

                        return result;

                    case DialogResult.Abort:
                        result.Success = false;
                        result.CustomErrorMessage = formProgressIndicator.Result.Error.Message;

                        return result;

                    case DialogResult.OK:
                        result.AdditionalDataReturn = formProgressIndicator.Result.Result;
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Reads the saved thumbnails (from the database) and, convert them to smaller size and save them on disk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void formPI_DoWork_GenerateSiteMovies_Thumbnails(FrmProgressIndicator sender, DoWorkEventArgs e)
        {
            var moviesDetails = (KeyValuePair<SiteGenParams, List<MovieForWeb>>)e.Argument;
            var errorsWhileSaving = new List<TechnicalDetailsImportError>();

            var i = 0;
            foreach (var movieDet in moviesDetails.Value)
            {
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                i++;
                sender.SetProgress(i, Path.GetFileName(movieDet.FN));

                try
                {
                    var epStills = Desene.DAL.LoadMovieStills(movieDet.Id).MovieStills;

                    if (epStills == null || epStills.Count == 0) continue;
                    movieDet.Th = 1;

                    for (var j = 0; j < epStills.Count; j++)
                    {
                        var fileName = Path.Combine(moviesDetails.Key.Location,
                            string.Format("Imgs\\Movies\\Thumbnails\\thumb-{0}-{1}.jpg", movieDet.Id, j));

                        if (File.Exists(fileName)) continue;

                        using (var ms = new MemoryStream(epStills[j]))
                        {
                            var imgOgj = CreateStillThumbnail(250, Image.FromStream(ms));

                            imgOgj.Save(fileName, ImageFormat.Jpeg);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorsWhileSaving.Add(
                        new TechnicalDetailsImportError
                        {
                            FilePath = movieDet.FN,
                            ErrorMesage = OperationResult.GetErrorMessage(ex)
                        });
                }
            }

            e.Result = errorsWhileSaving;
        }

        /// <summary>
        /// Checks the thumbnail existence on disk and if found, it set the thumbnail presence marker (into the current loaded lists)
        /// Fires when no thumbnail generation is made, but the user opt in for existing thumbnail preservation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void formPI_DoWork_GenerateSiteMovies_Thumbnails2(FrmProgressIndicator sender, DoWorkEventArgs e)
        {
            var moviesDetails = (KeyValuePair<SiteGenParams, List<MovieForWeb>>)e.Argument;
            var errorsWhileSaving = new List<TechnicalDetailsImportError>();

            var path = Path.Combine(moviesDetails.Key.Location, "Imgs\\Movies\\Thumbnails\\");
            var i = 0;

            foreach (var movieDet in moviesDetails.Value)
            {
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                i++;
                sender.SetProgress(i, Path.GetFileName(movieDet.FN));

                try
                {
                    var fnPattern = string.Format("thumb-{0}-*.jpg", movieDet.Id);

                    if (Directory.EnumerateFiles(path, fnPattern).Any())
                        movieDet.Th = 1;
                }
                catch (Exception ex)
                {
                    errorsWhileSaving.Add(
                        new TechnicalDetailsImportError
                        {
                            FilePath = movieDet.FN,
                            ErrorMesage = OperationResult.GetErrorMessage(ex)
                        });
                }
            }

            e.Result = errorsWhileSaving;
        }

        #endregion


        #region SERIES/episodes

        private static OperationResult SaveEpisodesThumbnails(SiteGenParams siteGenParams, List<EpisodesForWeb> data, string caption)
        {
            var result = new OperationResult();

            if (siteGenParams.SaveEpisodesThumbnals)
            {
                var imgsPath = Path.Combine(siteGenParams.Location, "Imgs\\Series\\Thumbnails");
                var existingThumbnailsForIds = new List<int>();

                if (!Directory.Exists(imgsPath))
                    Directory.CreateDirectory(imgsPath);
                else
                {
                    var d = new DirectoryInfo(imgsPath);
                    var Files = d.GetFiles("*.jpg");

                    foreach (FileInfo file in Files)
                    {
                        var id = int.Parse(file.Name.Split('-')[1]);
                        if (!existingThumbnailsForIds.Contains(id))
                            existingThumbnailsForIds.Add(id);
                    }
                }

                var idsToSaveThumbnails = new List<EpisodesForWeb>(data.Where(x => !existingThumbnailsForIds.Contains(x.Id)));

                var formProgressIndicator = new FrmProgressIndicator(string.Format("Site generation - Episodes ({0}) thumbnails", caption), "-", idsToSaveThumbnails.Count);
                formProgressIndicator.Argument = new KeyValuePair<SiteGenParams, List<EpisodesForWeb>>(siteGenParams, idsToSaveThumbnails);
                formProgressIndicator.DoWork += formPI_DoWork_GenerateSiteEpisodes_Thumbnails;

                switch (formProgressIndicator.ShowDialog())
                {
                    case DialogResult.Cancel:
                        result.Success = false;
                        result.CustomErrorMessage = "Operation has been canceled";

                        return result;

                    case DialogResult.Abort:
                        result.Success = false;
                        result.CustomErrorMessage = formProgressIndicator.Result.Error.Message;

                        return result;

                    case DialogResult.OK:
                        result.AdditionalDataReturn = formProgressIndicator.Result.Result;
                        break;
                }

                foreach (var episodeForWeb in data.Where(x => existingThumbnailsForIds.Contains(x.Id)))
                {
                    episodeForWeb.Th = 1;
                }
            }
            else
            if (siteGenParams.PreserveMarkesForExistingThumbnails && Directory.Exists(Path.Combine(siteGenParams.Location, "Imgs\\Series\\Thumbnails\\")))
            {
                var formProgressIndicator = new FrmProgressIndicator("Setting the episodes thumbnails marker from existing files", "-", data.Count);
                formProgressIndicator.Argument = new KeyValuePair<SiteGenParams, List<EpisodesForWeb>>(siteGenParams, data);
                formProgressIndicator.DoWork += formPI_DoWork_GenerateSiteEpisodes_Thumbnails2;

                switch (formProgressIndicator.ShowDialog())
                {
                    case DialogResult.Cancel:
                        result.Success = false;
                        result.CustomErrorMessage = "Operation has been canceled";

                        return result;

                    case DialogResult.Abort:
                        result.Success = false;
                        result.CustomErrorMessage = formProgressIndicator.Result.Error.Message;

                        return result;

                    case DialogResult.OK:
                        result.AdditionalDataReturn = formProgressIndicator.Result.Result;
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Reads the saved thumbnails (from the database) and, convert them to smaller size and save them on disk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void formPI_DoWork_GenerateSiteEpisodes_Thumbnails(FrmProgressIndicator sender, DoWorkEventArgs e)
        {
            var episodeDetails = (KeyValuePair<SiteGenParams, List<EpisodesForWeb>>)e.Argument;
            var errorsWhileSaving = new List<TechnicalDetailsImportError>();

            var i = 0;
            foreach (var episodeDet in episodeDetails.Value)
            {
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                i++;
                sender.SetProgress(i, Path.GetFileName(episodeDet.FN));

                try
                {
                    var epStills = Desene.DAL.LoadMovieStills(episodeDet.Id).MovieStills;

                    if (epStills == null || epStills.Count == 0) continue;
                    episodeDet.Th = 1;

                    for (var j = 0; j < epStills.Count; j++)
                    {
                        var fileName = Path.Combine(episodeDetails.Key.Location,
                            string.Format("Imgs\\Series\\Thumbnails\\thumb-{0}-{1}.jpg", episodeDet.Id, j));

                        if (File.Exists(fileName)) continue;

                        using (var ms = new MemoryStream(epStills[j]))
                        {
                            var imgOgj = CreateStillThumbnail(250, Image.FromStream(ms));

                            imgOgj.Save(fileName, ImageFormat.Jpeg);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorsWhileSaving.Add(
                        new TechnicalDetailsImportError
                        {
                            FilePath = episodeDet.FN,
                            ErrorMesage = OperationResult.GetErrorMessage(ex)
                        });
                }
            }

            e.Result = errorsWhileSaving;
        }

        /// <summary>
        /// Checks the thumbnail existence on disk and if found, it set the thumbnail presence marker (into the current loaded lists)
        /// Fires when no thumbnail generation is made, but the user opt in for existing thumbnail preservation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void formPI_DoWork_GenerateSiteEpisodes_Thumbnails2(FrmProgressIndicator sender, DoWorkEventArgs e)
        {
            var episodeDetails = (KeyValuePair<SiteGenParams, List<EpisodesForWeb>>)e.Argument;
            var errorsWhileSaving = new List<TechnicalDetailsImportError>();

            var path = Path.Combine(episodeDetails.Key.Location, "Imgs\\Series\\Thumbnails\\");
            var i = 0;

            foreach (var episodeDet in episodeDetails.Value)
            {
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                i++;
                sender.SetProgress(i, Path.GetFileName(episodeDet.FN));

                try
                {
                    var fnPattern = string.Format("thumb-{0}-*.jpg", episodeDet.Id);

                    if (Directory.EnumerateFiles(path, fnPattern).Any())
                        episodeDet.Th = 1;
                }
                catch (Exception ex)
                {
                    errorsWhileSaving.Add(
                        new TechnicalDetailsImportError
                        {
                            FilePath = episodeDet.FN,
                            ErrorMesage = OperationResult.GetErrorMessage(ex)
                        });
                }
            }

            e.Result = errorsWhileSaving;
        }

        #endregion



        #endregion
    }
}
