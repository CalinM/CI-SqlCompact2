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
using NUglify;

namespace Utils
{
    public class SiteGenerator
    {
        #region Minifycation methods

        private static string MinifyFile(string str)
        {
            try
            {
                return Uglify.Js(str).Code;
            }
            catch (Exception ex)
            {
                //var error = OperationResult.GetErrorMessage(ex);
                return str;
            }
        }

        private static string MinifyData(SiteGenParams siteGenParams, string str)
        {
            return
                siteGenParams.MinifyDataFiles
                    ? MinifyFile(str)
                    : str;
        }

        public static string MinifyScript(SiteGenParams siteGenParams, string str)
        {
            return
                siteGenParams.MinifyScriptFiles
                    ? MinifyFile(str)
                    : str;
        }

        #endregion

        public static OperationResult GenerateSiteFiles(SiteGenParams siteGenParams, IntPtr handle)
        {
            var result = new OperationResult();
            var jsS = new JavaScriptSerializer();
            var genDetails = DateTime.Now.ToString("yyyyMMdd");

            List<MovieShortInfo> msi = null;
            OperationResult opRes = null;

            var genImages = siteGenParams.SavePosters || siteGenParams.SaveEpisodesThumbnals || siteGenParams.PreserveMarkesForExistingThumbnails;

            #region ****** Movies ******

            var moviesData = Desene.DAL.GetMoviesForWeb();

            if (genImages)
            {
                msi = moviesData
                        .Select(x => new MovieShortInfo
                            {
                                Id = x.Id,
                                FileName = x.FN,
                                HasPoster = x.HasPoster
                            })
                        .ToList();


                #region Movies posters

                if (siteGenParams.SavePosters)
                {
                    result = SavePosters(siteGenParams.Location, msi, "Movies");

                    if (!result.Success)
                        return result;
                }

                #endregion


                #region Movies thumbnails

                result = SaveThumbnails(siteGenParams, msi, "Movies");
                if (!result.Success)
                    return result;

                #endregion
            }

            #region Extract Details

            opRes = Desene.DAL.GetStatistics(Sections.Movies);
            if (!opRes.Success)
                return opRes;

            var movieListDetails = opRes.AdditionalDataReturn.ToString();

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
                    MinifyData(siteGenParams, jsS.Serialize(moviesData)),
                    genDetails,                     //no minify ~ text to be displayed!
                    movieListDetails,               //no minify ~ text to be displayed!
                    jsS.Serialize(newMovies),       //no minify ~ only ints
                    jsS.Serialize(updatedMovies)    //no minify ~ only ints
                );

            var moviesDetails2 =
                string.Format("var moviesData2 = {0};",
                    jsS.Serialize(Desene.DAL.GetMoviesDetails2ForWeb(false)
                ));

            #endregion

            #endregion


            #region ****** Series ******

            var seriesData = Desene.DAL.GetSeriesForWeb(SeriesType.Final);
            var episodesData = Desene.DAL.GetEpisodesForWeb(siteGenParams.PreserveMarkesForExistingThumbnails, SeriesType.Final);

            if (genImages)
            {
                msi =
                    seriesData
                        .Select(x => new MovieShortInfo
                            {
                                Id = x.Id,
                                FileName = x.FN,
                                HasPoster = x.HasPoster
                            })
                        .ToList();


                #region Series posters

                if (siteGenParams.SavePosters)
                {
                    result = SavePosters(siteGenParams.Location, msi, "Series");

                    if (!result.Success)
                        return result;
                }

                #endregion


                #region Series Episodes thumbnails

                msi =
                    episodesData
                        .Select(x => new MovieShortInfo
                            {
                                Id = x.Id,
                                FileName = x.FN
                            })
                        .ToList();

                result = SaveThumbnails(siteGenParams, msi, "Series");

                if (!result.Success)
                    return result;

                foreach (var updatedObj in msi)
                {
                    var obj = episodesData.FirstOrDefault(x => x.Id == updatedObj.Id);
                    if (obj != null)
                        obj.Th = updatedObj.ThumbnailGenerated ? 1 : 0;
                }

                #endregion
            }

            #region Extract Series Details - leave it after Thumbnail generation

            Desene.DAL.FillSeriesDataFromEpisodes(ref seriesData, episodesData);

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

            opRes = Desene.DAL.GetStatistics(Sections.Series);
            if (!opRes.Success)
                return opRes;

            var seriesListDetails = opRes.AdditionalDataReturn.ToString();

            var detSerialeInfo =
                string.Format("var seriesData = {0}; var episodesDataS = {1}; var seriesStat = '{2}'; var newSeriesEpisodes = {3}",
                    MinifyData(siteGenParams, jsS.Serialize(seriesData)),
                    MinifyData(siteGenParams, jsS.Serialize(episodesData)),
                    seriesListDetails,                      //no minify ~ text to be displayed!
                    jsS.Serialize(seriesWithInsertedEp)     //no minify ~ only ints
                );

            #endregion

            #endregion


            #region ****** Collections ******

            var collectionsData = Desene.DAL.GetSeriesForWeb(SeriesType.Collection);
            var elementsData = Desene.DAL.GetCollectionElementsForWeb();

            if (genImages)
            {
                #region Collections posters

                if (siteGenParams.SavePosters)
                {
                    var elementsForPosterSave =
                        collectionsData
                            //.Where(x => x.T == 1) //series-type
                            .Select(x => new MovieShortInfo
                                {
                                    Id = x.Id,
                                    FileName = x.FN,
                                    HasPoster = x.HasPoster
                                })
                            .ToList();

                    result = SavePosters(siteGenParams.Location, elementsForPosterSave, "Collections");

                    if (!result.Success)
                        return result;

                    var movieTypesCollectionsIds = collectionsData.Where(x => x.T == 0).Select(x => x.Id).ToList();

                    if (movieTypesCollectionsIds.Any())
                    {
                        var movieTypeElements = elementsData.Where(x => movieTypesCollectionsIds.Contains(x.CId));

                        if (movieTypeElements.Any())
                        {
                            elementsForPosterSave =
                                movieTypeElements
                                    .Select(x => new MovieShortInfo
                                        {
                                            Id = x.Id,
                                            FileName = x.FN,
                                            HasPoster = x.HasPoster
                                        })
                                    .ToList();

                            result = SavePosters(siteGenParams.Location, elementsForPosterSave, "Collections");

                            if (!result.Success)
                                return result;
                        }
                    }
                }

                #endregion


                #region Collections elements thumbnails

                msi =
                    elementsData
                        .Select(x => new MovieShortInfo
                            {
                                Id = x.Id,
                                FileName = x.FN,
                                HasPoster = x.HasPoster
                            })
                        .ToList();

                result = SaveThumbnails(siteGenParams, msi, "Collections");

                if (!result.Success)
                    return result;

                foreach (var updatedObj in msi)
                {
                    var obj = elementsData.FirstOrDefault(x => x.Id == updatedObj.Id);
                    if (obj != null)
                        obj.Th = updatedObj.ThumbnailGenerated ? 1 : 0;
                }

                #endregion
            }

            #region Extract Collections Details - leave it after Thumbnail generation

            var collectionsAndInsertedElements = new Dictionary<string, string>(); //elementId, collectionId, if equal => this is a new Series-type collection

            foreach (var elData in elementsData.OrderByDescending(o => o.InsertedDate))
            {
                var collectionObj = collectionsData.FirstOrDefault(x => x.Id == elData.CId);

                if (collectionObj.T == (int)CollectionsSiteSectionType.SeriesType)
                {
                    if (!collectionsAndInsertedElements.ContainsKey(elData.CId.ToString()))
                    {
                        collectionsAndInsertedElements.Add(elData.CId.ToString(), elData.CId.ToString());
                        continue;
                    }
                }
                else
                {
                    collectionsAndInsertedElements.Add(elData.Id.ToString(), elData.CId.ToString());
                }

                if (collectionsAndInsertedElements.Count() >= 25)
                    break;
            }



            Desene.DAL.FillCollectionDataFromEpisodes(ref collectionsData, elementsData);

            opRes = Desene.DAL.GetStatistics(Sections.Collections);
            if (!opRes.Success)
                return opRes;

            var collectionsListDetails = opRes.AdditionalDataReturn.ToString();

            var detCollectionsInfo =
                string.Format("var collectionsData = {0}; var collectionsElements = {1}; var colStat = '{2}'; var newElementsInCol = {3}",
                    MinifyData(siteGenParams, jsS.Serialize(collectionsData)),
                    MinifyData(siteGenParams, jsS.Serialize(elementsData)),
                    collectionsListDetails,         //no minify ~ text to be displayed!
                    jsS.Serialize(collectionsAndInsertedElements)
                    //it needs cleanup, it's Dictionary, but Uglify seems to have an issue with a serialized dictionary, it returns the Value of the last Key in the dictionary
                    //MinifyData(siteGenParams, jsS.Serialize(collectionsAndInsertedElements))
                );

            var collectionsDetails2 =
                string.Format("var collectionsData2 = {0};",
                    jsS.Serialize(Desene.DAL.GetMoviesDetails2ForWeb(true)
                ));

            #endregion

            #endregion


            #region ****** Recordings ******

            var recordingsData = Desene.DAL.GetSeriesForWeb(SeriesType.Recordings);
            var recordingsEpisodesData = Desene.DAL.GetEpisodesForWeb(siteGenParams.PreserveMarkesForExistingThumbnails, SeriesType.Recordings);

            var detRecordingsInfo = "var recordingsData = []; var episodesDataR = []; var recordingsStat = '-'; var newRecordingsEpisodes = []"; //fallback in case there are no recordings

            if (recordingsData.Any())
            {

                if (genImages)
                {
                    msi =
                        recordingsData
                            .Select(x => new MovieShortInfo
                                {
                                    Id = x.Id,
                                    FileName = x.FN,
                                    HasPoster = x.HasPoster
                                })
                            .ToList();


                    #region Recordings posters

                    if (siteGenParams.SavePosters)
                    {
                        result = SavePosters(siteGenParams.Location, msi, "Recordings");

                        if (!result.Success)
                            return result;
                        }

                    #endregion


                    #region Recordings Episodes thumbnails

                    msi =
                        recordingsEpisodesData
                            .Select(x => new MovieShortInfo
                                {
                                    Id = x.Id,
                                    FileName = x.FN
                                })
                            .ToList();

                    result = SaveThumbnails(siteGenParams, msi, "Recordings");

                    if (!result.Success)
                        return result;

                    foreach (var updatedObj in msi)
                    {
                        var obj = recordingsEpisodesData.FirstOrDefault(x => x.Id == updatedObj.Id);
                        if (obj != null)
                            obj.Th = updatedObj.ThumbnailGenerated ? 1 : 0;
                    }

                    #endregion
                }


                #region Extract Recordings Details - leave it after Thumbnail generation

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


                Desene.DAL.FillSeriesDataFromEpisodes(ref recordingsData, recordingsEpisodesData);


                opRes = Desene.DAL.GetStatistics(Sections.Collections);
                if (!opRes.Success)
                    return opRes;

                var recordingsListDetails = opRes.AdditionalDataReturn.ToString();

                detRecordingsInfo =
                    string.Format("var recordingsData = {0}; var episodesDataR = {1}; var recordingsStat = '{2}'; var newRecordingsEpisodes = {3}",
                        MinifyData(siteGenParams, jsS.Serialize(recordingsData)),
                        MinifyData(siteGenParams, jsS.Serialize(recordingsEpisodesData)),
                        recordingsListDetails,                      //no minify ~ text to be displayed!
                        jsS.Serialize(recordingsWithInsertedEp)     //no minify ~ only ints
                    );

                #endregion
            }

            #endregion


            result.AdditionalDataReturn = new GeneratedJSData(detMovieInfo, detSerialeInfo, detRecordingsInfo, moviesDetails2, detCollectionsInfo, collectionsDetails2);
            return result;
        }

        #region POSTERS saving methods

        private static OperationResult SavePosters(string siteGenLocation, List<MovieShortInfo> data, string subFolder)
        {
            var result = new OperationResult();

            var imgsPath = Path.Combine(siteGenLocation, "Imgs", subFolder);
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

            var postersToSave = new List<MovieShortInfo>(data.Where(x => !existingPostersForIds.Contains(x.Id) && x.HasPoster));

            if (postersToSave.Any())
            {
                var formProgressIndicator = new FrmProgressIndicator(string.Format("Site generation - {0} posters", subFolder), "-", postersToSave.Count);
                formProgressIndicator.Argument =
                    new BgwArgument_Work
                    {
                        SiteGenLocation = siteGenLocation,
                        SubFolder = subFolder,
                        MSI = postersToSave
                    };

                formProgressIndicator.DoWork += formPI_DoWork_GenerateSitePosters;

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
        /// Reads the saved posters from the database, convert them to smaller size and save them on disk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void formPI_DoWork_GenerateSitePosters(FrmProgressIndicator sender, DoWorkEventArgs e)
        {
            var args = (BgwArgument_Work)e.Argument;
            var errorsWhileSaving = new List<TechnicalDetailsImportError>();

            var i = 0;
            foreach (var movieShortInfo in args.MSI)
            {
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                i++;
                sender.SetProgress(i, Path.GetFileName(movieShortInfo.FileName));

                if (!movieShortInfo.HasPoster) continue;

                var fileName = Path.Combine(args.SiteGenLocation, string.Format("Imgs\\{0}\\poster-{1}.jpg", args.SubFolder, movieShortInfo.Id));
                if (File.Exists(fileName)) continue; //?? wasn't it excluded before this ?

                try
                {
                    var cover = Desene.DAL.GetPoster(movieShortInfo.Id);

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
                            FilePath = movieShortInfo.FileName,
                            ErrorMesage = OperationResult.GetErrorMessage(ex)
                        });
                }
            }

            e.Result = errorsWhileSaving;
        }

        #endregion


        #region Thumbnails

        private static Image CreateStillThumbnail(int newHeight, Image source)
        {
            // Prevent using images internal thumbnail
            source.RotateFlip(RotateFlipType.Rotate180FlipNone);
            source.RotateFlip(RotateFlipType.Rotate180FlipNone);

            var newWidth = source.Width * newHeight / source.Height;

            return source.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);
        }

        private static OperationResult SaveThumbnails(SiteGenParams siteGenParams, List<MovieShortInfo> data, string subFolder)
        {
            var result = new OperationResult();
            var imgsPath = Path.Combine(siteGenParams.Location, "Imgs", subFolder, "Thumbnails");

            if (siteGenParams.SaveEpisodesThumbnals)
            {
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

                var thumbnailsToSave = new List<MovieShortInfo>(data.Where(x => !existingThumbnailsForIds.Contains(x.Id) && x.ThumbnailGenerated));

                if (thumbnailsToSave.Any())
                {
                    var formProgressIndicator = new FrmProgressIndicator(string.Format("Site generation - {0} thumbnails", subFolder), "-", thumbnailsToSave.Count);
                    formProgressIndicator.Argument =
                        new BgwArgument_Work
                        {
                            SiteGenLocation = siteGenParams.Location,
                            SubFolder = subFolder,
                            MSI = thumbnailsToSave
                        };

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
                }

                //setting the marker for all previous existing thumbnails
                foreach (var msi in data.Where(x => existingThumbnailsForIds.Contains(x.Id)))
                {
                    msi.ThumbnailGenerated = true;
                }
            }
            else
            if (siteGenParams.PreserveMarkesForExistingThumbnails && Directory.Exists(imgsPath))
            {
                var formProgressIndicator = new FrmProgressIndicator("Setting the movies thumbnails marker from existing files", "-", data.Count);
                formProgressIndicator.Argument =
                    new BgwArgument_Work
                    {
                        SiteGenLocation = siteGenParams.Location,
                        SubFolder = subFolder,
                        MSI = data
                    };

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
            var args = (BgwArgument_Work)e.Argument;
            var errorsWhileSaving = new List<TechnicalDetailsImportError>();

            var i = 0;
            foreach (var movieShortInfo in args.MSI)
            {
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                i++;
                sender.SetProgress(i, Path.GetFileName(movieShortInfo.FileName));

                try
                {
                    var stills = Desene.DAL.LoadMovieStills(movieShortInfo.Id).MovieStills;

                    if (stills == null || stills.Count == 0) continue;

                    movieShortInfo.ThumbnailGenerated = true;

                    for (var j = 0; j < stills.Count; j++)
                    {
                        var fileName = Path.Combine(args.SiteGenLocation,
                            string.Format("Imgs\\{0}\\Thumbnails\\thumb-{1}-{2}.jpg", args.SubFolder, movieShortInfo.Id, j));

                        if (File.Exists(fileName)) continue;

                        using (var ms = new MemoryStream(stills[j]))
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
                            FilePath = movieShortInfo.FileName,
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
            var args = (BgwArgument_Work)e.Argument;
            var errorsWhileSaving = new List<TechnicalDetailsImportError>();

            var path = Path.Combine(args.SiteGenLocation, string.Format("Imgs\\{0}\\Thumbnails\\", args.SubFolder));

            var i = 0;
            foreach (var movieShortInfo in args.MSI)
            {
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                i++;
                sender.SetProgress(i, Path.GetFileName(movieShortInfo.FileName));

                try
                {
                    var fnPattern = string.Format("thumb-{0}-*.jpg", movieShortInfo.Id);

                    if (Directory.EnumerateFiles(path, fnPattern).Any())
                        movieShortInfo.ThumbnailGenerated = true;
                }
                catch (Exception ex)
                {
                    errorsWhileSaving.Add(
                        new TechnicalDetailsImportError
                        {
                            FilePath = movieShortInfo.FileName,
                            ErrorMesage = OperationResult.GetErrorMessage(ex)
                        });
                }
            }

            e.Result = errorsWhileSaving;
        }

        #endregion
    }
}
