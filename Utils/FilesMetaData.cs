using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

using Common;
using DAL;
using MediaInfoLib;

using NReco.VideoConverter;

namespace Utils
{
    public static class FilesMetaData
    {
        //MOVIES & EPISODES -> main import method (primary entry point)
        //using FilesImportParams as a DTO to pass GenerateThumbnail and ThumbnailImageSecond values
        public static OperationResult GetFilesTechnicalDetails(string[] files, FilesImportParams fileImportParams)
        {
            var result = new OperationResult();

            var fromProgressIndicator = new FrmProgressIndicator("Retrieving files/movies technical details", "Get files technical details", files.Length);
            fromProgressIndicator.Argument = new KeyValuePair<FilesImportParams, string[]>(fileImportParams, files);

            if (fileImportParams.ParentId != null)
            {
                fromProgressIndicator.DoWork += formPI_DoWork_RetrieveFilesInfo;        //episodes
            }
            else
            {
                fromProgressIndicator.DoWork += formPI_DoWork_RetrieveFilesInfo2;       //movies
            }


            switch (fromProgressIndicator.ShowDialog())
            {
                case DialogResult.Cancel:
                    result.Success = false;
                    result.CustomErrorMessage = "Operation has been canceled";
                    break;

                case DialogResult.Abort:
                    result.Success = false;
                    result.CustomErrorMessage = fromProgressIndicator.Result.Error.Message;
                    break;

                case DialogResult.OK:
                    result.AdditionalDataReturn = fromProgressIndicator.Result.Result;
                    break;
            }

            return result;
        }


        //MOVIES & EPISODES -> Method used to retrieve the details from a file
        public static OperationResult GetFileTechnicalDetails(string filePath, MediaInfo mi = null)
        {
            var result = new OperationResult();

            try
            {
                if (mi is null) mi = new MediaInfo();
                mi.Open(filePath);
                var x = mi.Option("Info_Parameters");
                //https://sourceforge.net/p/mediainfo/discussion/297610/thread/d51f253c/?limit=25

                var mtd = new MovieTechnicalDetails
                                {
                                    FileName = mi.Get(StreamKind.General, 0, "FileName"),
                                    InitialPath = filePath,
                                    Format = mi.Get(StreamKind.General, 0, "Format"),
                                    FileSize = mi.Get(StreamKind.General, 0, "FileSize"), 			    // to calculate totals
                                    FileSize2 = mi.Get(StreamKind.General, 0, "FileSize/String"),	    // for display
                                    Title = mi.Get(StreamKind.General, 0, "Title"),                     // to detect texts
                                    Encoded_Application = mi.Get(StreamKind.General, 0, "Encoded_Application"),
                                    Cover = mi.Get(StreamKind.General, 0, "Cover"),                     // to detect texts
                                    Duration = mi.Get(StreamKind.General, 0, "Duration"),               // recalculated in Duration2

                                    //no, not here, to it will look nicer (shorter)  as summary: ro, dut, eng
                                    //AudioLanguages = mi.Get(StreamKind.General, 0, "Audio_Language_List").Replace(" / ", ", "),
                                    //SubtitleLanguages = mi.Get(StreamKind.General, 0, "Text_Language_List").Replace(" / ", ", "),
                                };

                var vcStr = mi.Get(StreamKind.General, 0, "VideoCount");
                var vc = int.TryParse(vcStr, out var tmpInt) ? tmpInt : 0;

                for (var i = 0; i < vc; i++)
                {
                    mtd.VideoStreams.Add(
                        new VideoStreamInfo
                            {
                                Index = i + 1,
                                Format = mi.Get(StreamKind.Video, i, "Format"),
                                Format_Profile = mi.Get(StreamKind.Video, i, "Format_Profile"),
                                BitRateMode = mi.Get(StreamKind.Video, i, "BitRate_Mode/String"),
                                BitRate = mi.Get(StreamKind.Video, i, "BitRate/String"),
                                Width = mi.Get(StreamKind.Video, i, "Width"),
                                Height = mi.Get(StreamKind.Video, i, "Height"),
                                FrameRate_Mode = mi.Get(StreamKind.Video, i, "FrameRate_Mode/String"),
                                FrameRate = mi.Get(StreamKind.Video, i, "FrameRate/String"),
                                Delay = mi.Get(StreamKind.Video, i, "Delay/String"),
                                StreamSize = mi.Get(StreamKind.Video, i, "StreamSize/String"),
                                Title = mi.Get(StreamKind.Video, i, "Title"),				        // to detect texts
                                Language = mi.Get(StreamKind.Video, i, "Language/String")
                            });
                }

                var acStr = mi.Get(StreamKind.General, 0, "AudioCount");
                var ac = int.TryParse(acStr, out tmpInt) ? tmpInt : 0;

                for (var i = 0; i < ac; i++)
                {
                    mtd.AudioStreams.Add(
                        new AudioStreamInfo
                            {
                                Index = i + 1,
                                Format = mi.Get(StreamKind.Audio, i, "Format"),
                                BitRate = mi.Get(StreamKind.Audio, i, "BitRate/String"),
                                Channel = mi.Get(StreamKind.Audio, i, "Channel(s)/String"),
                                ChannelPosition = mi.Get(StreamKind.Audio, i, "ChannelPositions"),
                                SamplingRate = mi.Get(StreamKind.Audio, i, "SamplingRate/String"),
                                Resolution = mi.Get(StreamKind.Audio, i, "Resolution/String"),
                                Delay = mi.Get(StreamKind.Audio, i, "Delay/String"),
                                Video_Delay = mi.Get(StreamKind.Audio, i, "Video_Delay/String"),
                                StreamSize = mi.Get(StreamKind.Audio, i, "StreamSize/String"),
                                Title = mi.Get(StreamKind.Audio, i, "Title"),				        // to detect texts
                                Language = mi.Get(StreamKind.Audio, i, "Language"), //Language/String
                                Default = mi.Get(StreamKind.Audio, i, "Default").ToLower() == "yes",
                                Forced = mi.Get(StreamKind.Audio, i, "Forced").ToLower() == "yes"
                            });
                }

                var tcStr = mi.Get(StreamKind.General, 0, "TextCount");
                var tc = int.TryParse(tcStr, out tmpInt) ? tmpInt : 0;

                for (var i = 0; i < tc; i++)
                {
                    mtd.SubtitleStreams.Add(
                        new SubtitleStreamInfo
                            {
                                Index = i + 1,
                                Format = mi.Get(StreamKind.Text, i, "Format"),
                                StreamSize = mi.Get(StreamKind.Text, i, "StreamSize/String"),
                                Title = mi.Get(StreamKind.Text, i, "Title"),    			        // to detect texts
                                Language = mi.Get(StreamKind.Text, i, "Language"),
                                Default = mi.Get(StreamKind.Audio, i, "Default").ToLower() == "yes",
                                Forced = mi.Get(StreamKind.Audio, i, "Forced").ToLower() == "yes"
                            });
                }

                result.AdditionalDataReturn = mtd;
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }

            return result;
        }

        //MOVIES & EPISODES -> Method used to extract the image frames from the processed/specified file
        public static OperationResult GetMovieStills(MovieTechnicalDetails mtd, FFMpegConverter ffMpegConverter)
        {
            var result = new OperationResult();

            try
            {
                if (ffMpegConverter == null)
                    ffMpegConverter = new FFMpegConverter();

                if (mtd.DurationAsInt > 0)
                {
                    var durationInSec =  mtd.DurationAsInt / 1000;

                    for (var j = 1; j <= 3; j++)
                    {
                        var position = (durationInSec * (25 * j)) / 100; //%

                        using (var ms = new MemoryStream())
                        {
                            ffMpegConverter.GetVideoThumbnail(mtd.InitialPath, ms, position);
                            mtd.MovieStills.Add(ms.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }

            return result;
        }


        //MOVIES -> using a BGW, the files details are determined (files obtained using the user's parameters ~ path, extension) using the "GetFileTechnicalDetails"
        //method above. Due to possible large amount of data, the files are IMMEDIATELY saved into the database to avoid processed data loss
        private static void formPI_DoWork_RetrieveFilesInfo2(FrmProgressIndicator sender, DoWorkEventArgs e)
        {
            var arguments = (KeyValuePair<FilesImportParams, string[]>)e.Argument;
            var displayInfoResult = new List<MovieTechnicalDetails>();

            var mi = new MediaInfo();
            FFMpegConverter ffMpegConverter = null;

            if (arguments.Key.GenerateThumbnail)
            {
                ffMpegConverter = new FFMpegConverter();
            }

            var technicalDetailsImportErrorBgw = new List<TechnicalDetailsImportError>();

            var i = 0;
            foreach (var filePath in arguments.Value)
            {
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                var opRes = GetFileTechnicalDetails(filePath, mi);

                if (opRes.Success)
                {
                    var mtdObj = (MovieTechnicalDetails)opRes.AdditionalDataReturn;

                    if (!arguments.Key.DisplayInfoOnly)
                    {
                        var opRes2 = GetMovieStills(mtdObj, ffMpegConverter);

                        if (!opRes2.Success)
                        {
                            technicalDetailsImportErrorBgw.Add(
                                new TechnicalDetailsImportError
                                    {
                                        FilePath = filePath,
                                        ErrorMesage = opRes2.CustomErrorMessage
                                    });
                        }

                        opRes2 = Desene.DAL.InsertMTD2(mtdObj, arguments.Key);

                        if (!opRes2.Success)
                        {
                            technicalDetailsImportErrorBgw.Add(
                                new TechnicalDetailsImportError
                                {
                                    FilePath = filePath,
                                    ErrorMesage = opRes2.CustomErrorMessage
                                });
                        }
                    }
                    else
                    {
                        displayInfoResult.Add(mtdObj);
                    }
                }
                else
                    technicalDetailsImportErrorBgw.Add(
                        new TechnicalDetailsImportError
                            {
                                FilePath = filePath,
                                ErrorMesage = opRes.CustomErrorMessage
                            });

                i++;

                sender.SetProgress(i, Path.GetFileName(filePath));
            }

            if (!arguments.Key.DisplayInfoOnly)
                e.Result = technicalDetailsImportErrorBgw;
            else
                e.Result = new KeyValuePair<List<MovieTechnicalDetails>, List<TechnicalDetailsImportError>>(displayInfoResult, technicalDetailsImportErrorBgw);
        }


        //EPISODES -> using a BGW, the files details are determined (files obtained using the user's parameters ~ path, extension) using the "GetFileTechnicalDetails"
        //method above and stored into a list. The actual save is done using another BGW, inside the "SaveImportedEpisodes" method
        private static void formPI_DoWork_RetrieveFilesInfo(FrmProgressIndicator sender, DoWorkEventArgs e)
        {
            var arguments = (KeyValuePair<FilesImportParams, string[]>)e.Argument;

            var mi = new MediaInfo();
            FFMpegConverter ffMpegConverter = null;

            if (arguments.Key.GenerateThumbnail)
            {
                ffMpegConverter = new FFMpegConverter();
            }


            var movieTechnicalDetailsBgw = new List<MovieTechnicalDetails>();
            var technicalDetailsImportErrorBgw = new List<TechnicalDetailsImportError>();

            var i = 0;
            foreach (var filePath in arguments.Value)
            {
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                var opRes = GetFileTechnicalDetails(filePath, mi);

                if (opRes.Success)
                {
                    var mtdObj = (MovieTechnicalDetails)opRes.AdditionalDataReturn;

                    GetMovieStills(mtdObj, ffMpegConverter);

                    movieTechnicalDetailsBgw.Add(mtdObj);
                }
                else
                    technicalDetailsImportErrorBgw.Add(
                        new TechnicalDetailsImportError
                            {
                                FilePath = filePath,
                                ErrorMesage = opRes.CustomErrorMessage
                            });

                i++;

                sender.SetProgress(i, Path.GetFileName(filePath));
            }

            e.Result = new KeyValuePair<List<TechnicalDetailsImportError>, List<MovieTechnicalDetails>>(
                technicalDetailsImportErrorBgw, movieTechnicalDetailsBgw);
        }

        //EPISODES -> Called after the FilesTechnicalDetails determination, it initialize the BGW used to SAVE the cached data
        public static OperationResult SaveImportedEpisodes(KeyValuePair<FilesImportParams, List<MovieTechnicalDetails>> importParamsAndDetails)
        {
            var result = new OperationResult();

            var fromProgressIndicator = new FrmProgressIndicator("Saving technical details in the database", "-", importParamsAndDetails.Value.Count);
            fromProgressIndicator.Argument = importParamsAndDetails;
            fromProgressIndicator.DoWork += formPI_DoWork_SaveTechnicalDetails;

            switch (fromProgressIndicator.ShowDialog())
            {
                case DialogResult.Cancel:
                    result.Success = false;
                    result.CustomErrorMessage = "Operation has been canceled";
                    break;

                case DialogResult.Abort:
                    result.Success = false;
                    result.CustomErrorMessage = fromProgressIndicator.Result.Error.Message;
                    break;

                case DialogResult.OK:
                    result.AdditionalDataReturn = fromProgressIndicator.Result.Result;
                    break;
            }

            return result;
        }

        //EPISODES -> Saving the cached/determined files details
        private static void formPI_DoWork_SaveTechnicalDetails(FrmProgressIndicator sender, DoWorkEventArgs e)
        {
            var importParamsAndDetails = (KeyValuePair<FilesImportParams, List<MovieTechnicalDetails>>)e.Argument;
            var errorsWhileSaving = new List<TechnicalDetailsImportError>();

            var i = 0;
            foreach (var movieTechnicalDetails in importParamsAndDetails.Value)
            {
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                var opRes = Desene.DAL.InsertMTD(movieTechnicalDetails, importParamsAndDetails.Key);

                if (!opRes.Success)
                {
                    errorsWhileSaving.Add(
                        new TechnicalDetailsImportError
                        {
                            FilePath = movieTechnicalDetails.FileName,
                            ErrorMesage = opRes.CustomErrorMessage
                        });
                }

                i++;
                sender.SetProgress(i, Path.GetFileName(movieTechnicalDetails.FileName));
            }

            e.Result = errorsWhileSaving;
        }
    }
}
