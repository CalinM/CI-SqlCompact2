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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Utils
{
    public class SiteGenerator
    {
        public static OperationResult GenerateSiteFiles(SiteGenParams siteGenParams)
        {
            var result = new OperationResult();
            var jsS = new JavaScriptSerializer();
            var genDetails = DateTime.Now.ToString("yyyyMMdd");


            #region Movies

            var moviesData = Desene.DAL.GetMoviesForWeb();

            if (siteGenParams.SavePosters)
            {
                var imgsPath = Path.Combine(siteGenParams.Location, "Imgs");
                if (!Directory.Exists(imgsPath))
                    Directory.CreateDirectory(imgsPath);

                var fromProgressIndicator = new FrmProgressIndicator("Site generation - Movies Posters", "-", moviesData.Count);
                fromProgressIndicator.Argument = new KeyValuePair<SiteGenParams, List<MovieForWeb>>(siteGenParams, moviesData);
                fromProgressIndicator.DoWork += formPI_DoWork_GenerateSitePosters_Movies;

                switch (fromProgressIndicator.ShowDialog())
                {
                    case DialogResult.Cancel:
                        result.Success = false;
                        result.CustomErrorMessage = "Operation has been canceled";

                        return result;

                    case DialogResult.Abort:
                        result.Success = false;
                        result.CustomErrorMessage = fromProgressIndicator.Result.Error.Message;

                        return result;

                    case DialogResult.OK:
                        result.AdditionalDataReturn = fromProgressIndicator.Result.Result;
                        break;
                }
            }

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
                string.Format("var detaliiFilme = {0}; var detaliiGenerare = '{1}'; var detaliiListaF = '{2}'; var newMovies = {3}; var updatedMovies = {4};",
                    jsS.Serialize(moviesData),
                    genDetails,
                    movieListDetails,
                    jsS.Serialize(newMovies),
                    jsS.Serialize(updatedMovies));

            #endregion

            #endregion


            #region Series

            var seriesData = Desene.DAL.GetSeriesForWeb();
            var episodesData = Desene.DAL.GetEpisodesForWeb(siteGenParams.PreserveMarkesForExistingThumbnails);


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

            if (siteGenParams.SavePosters)
            {
                var imgsPath = Path.Combine(siteGenParams.Location, "Imgs\\Seriale");
                if (!Directory.Exists(imgsPath))
                    Directory.CreateDirectory(imgsPath);

                var fromProgressIndicator = new FrmProgressIndicator("Site generation - Series posters", "-", seriesData.Count);
                fromProgressIndicator.Argument = new KeyValuePair<SiteGenParams, List<SeriesForWeb>>(siteGenParams, seriesData);
                fromProgressIndicator.DoWork += formPI_DoWork_GenerateSitePosters_Series;

                switch (fromProgressIndicator.ShowDialog())
                {
                    case DialogResult.Cancel:
                        result.Success = false;
                        result.CustomErrorMessage = "Operation has been canceled";

                        return result;

                    case DialogResult.Abort:
                        result.Success = false;
                        result.CustomErrorMessage = fromProgressIndicator.Result.Error.Message;

                        return result;

                    case DialogResult.OK:
                        result.AdditionalDataReturn = fromProgressIndicator.Result.Result;
                        break;
                }
            }

            #endregion

            #region Episodes thumbnails

            if (siteGenParams.SaveEpisodesThumbnals)
            {
                var imgsPath = Path.Combine(siteGenParams.Location, "Imgs\\Seriale\\Thumbnails");
                if (!Directory.Exists(imgsPath))
                    Directory.CreateDirectory(imgsPath);

                var fromProgressIndicator = new FrmProgressIndicator("Site generation - Episodes thumbnails", "-", episodesData.Count);
                fromProgressIndicator.Argument = new KeyValuePair<SiteGenParams, List<EpisodesForWeb>>(siteGenParams, episodesData);
                fromProgressIndicator.DoWork += formPI_DoWork_GenerateSiteEpisodes_Thumbnails;

                switch (fromProgressIndicator.ShowDialog())
                {
                    case DialogResult.Cancel:
                        result.Success = false;
                        result.CustomErrorMessage = "Operation has been canceled";

                        return result;

                    case DialogResult.Abort:
                        result.Success = false;
                        result.CustomErrorMessage = fromProgressIndicator.Result.Error.Message;

                        return result;

                    case DialogResult.OK:
                        result.AdditionalDataReturn = fromProgressIndicator.Result.Result;
                        break;
                }
            }
            else
            if (siteGenParams.PreserveMarkesForExistingThumbnails && Directory.Exists(Path.Combine(siteGenParams.Location, "Imgs\\Seriale\\Thumbnails\\")))
            {
                var fromProgressIndicator = new FrmProgressIndicator("Setting the episodes thumbnails marker from existing files", "-", episodesData.Count);
                fromProgressIndicator.Argument = new KeyValuePair<SiteGenParams, List<EpisodesForWeb>>(siteGenParams, episodesData);
                fromProgressIndicator.DoWork += formPI_DoWork_GenerateSiteEpisodes_Thumbnails2;

                switch (fromProgressIndicator.ShowDialog())
                {
                    case DialogResult.Cancel:
                        result.Success = false;
                        result.CustomErrorMessage = "Operation has been canceled";

                        return result;

                    case DialogResult.Abort:
                        result.Success = false;
                        result.CustomErrorMessage = fromProgressIndicator.Result.Error.Message;

                        return result;

                    case DialogResult.OK:
                        result.AdditionalDataReturn = fromProgressIndicator.Result.Result;
                        break;
                }
            }

            #endregion

            #region Extract Details - leave it after Thumbnail generation

            Desene.DAL.FillSeriesDataFromEpisodes(ref seriesData, episodesData);

            var seriesListDetails =
                string.Format("The current list contains: {0} Series, combined having {1} episodes, (summing aprox. {2} GB)",
                    seriesData.Count,
                    episodesData.Count,
                    //episodes.Sum(s => s.DimensiuneInt) / 1024
                    "?");

            var detSerialeInfo =
                string.Format("var detaliiSeriale = {0}; var detaliiEpisoade = {1}; var detaliiListaS = '{2}'; var newSeriesEpisodes = {3}",
                    jsS.Serialize(seriesData),
                    jsS.Serialize(episodesData),
                    seriesListDetails,
                    jsS.Serialize(seriesWithInsertedEp));

            #endregion

            #endregion

            result.AdditionalDataReturn = new KeyValuePair<string, string>(detMovieInfo, detSerialeInfo);
            return result;
        }

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

                var fileName = Path.Combine(siteGenDetails.Key.Location, string.Format("Imgs\\poster-{0}.jpg", movieForWebDet.Id));
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

                var fileName = Path.Combine(siteGenDetails.Key.Location, string.Format("Imgs\\Seriale\\poster-{0}.jpg", seriesForWebDet.Id));
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

        public static Image CreateStillThumbnail(int newHeight, Image source)
        {
            // Prevent using images internal thumbnail
            source.RotateFlip(RotateFlipType.Rotate180FlipNone);
            source.RotateFlip(RotateFlipType.Rotate180FlipNone);

            var newWidth = source.Width * newHeight / source.Height;

            return source.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);
        }

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
                            string.Format("Imgs\\Seriale\\Thumbnails\\thumb-{0}-{1}.jpg", episodeDet.Id, j));

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

        private static void formPI_DoWork_GenerateSiteEpisodes_Thumbnails2(FrmProgressIndicator sender, DoWorkEventArgs e)
        {
            var episodeDetails = (KeyValuePair<SiteGenParams, List<EpisodesForWeb>>)e.Argument;
            var errorsWhileSaving = new List<TechnicalDetailsImportError>();

            var path = Path.Combine(episodeDetails.Key.Location, "Imgs\\Seriale\\Thumbnails\\");
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
    }
}
