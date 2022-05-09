using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Common;
using DAL;
using MediaInfoLib;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
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

            var formProgressIndicator = new FrmProgressIndicator("Retrieving files/movies technical details", "Get files technical details", files.Length);
            formProgressIndicator.Argument = new KeyValuePair<FilesImportParams, string[]>(fileImportParams, files);

            if (fileImportParams.ParentId != null)
            {
                formProgressIndicator.DoWork += formPI_DoWork_RetrieveFilesInfo;        //episodes
            }
            else
            {
                formProgressIndicator.DoWork += formPI_DoWork_RetrieveFilesInfo2;       //movies
            }


            switch (formProgressIndicator.ShowDialog())
            {
                case DialogResult.Cancel:
                    result.Success = false;
                    result.CustomErrorMessage = "Operation has been canceled";
                    break;

                case DialogResult.Abort:
                    result.Success = false;
                    result.CustomErrorMessage = formProgressIndicator.Result.Error.Message;
                    break;

                case DialogResult.OK:
                    result.AdditionalDataReturn = formProgressIndicator.Result.Result;
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
                    FileSize = mi.Get(StreamKind.General, 0, "FileSize"),               // to calculate totals
                    FileSize2 = mi.Get(StreamKind.General, 0, "FileSize/String"),       // for display
                    Title = mi.Get(StreamKind.General, 0, "Title"),                     // to detect texts
                    Encoded_Application = mi.Get(StreamKind.General, 0, "Encoded_Application"),
                    Cover = mi.Get(StreamKind.General, 0, "Cover"),                     // to detect texts
                    Duration = mi.Get(StreamKind.General, 0, "Duration"),               // recalculated in Duration2

                    //no, not here, to it will look nicer (shorter)  as summary: ro, dut, eng
                    //AudioLanguages = mi.Get(StreamKind.General, 0, "Audio_Language_List").Replace(" / ", ", "),
                    //SubtitleLanguages = mi.Get(StreamKind.General, 0, "Text_Language_List").Replace(" / ", ", "),
                };


                if (mtd.Duration == "0" || mtd.Duration == string.Empty) //CMA: enable this only for TS?
                {
                    var tsDurationFallback = GetVideoDurationAlt(filePath);

                    mtd.Duration = ((int)tsDurationFallback.TotalMilliseconds).ToString();
                }

                if (mtd.Duration.Contains("."))
                {
                    decimal d = 0;
                    if (decimal.TryParse(mtd.Duration, out d))
                    {
                        mtd.Duration = ((int)d).ToString();
                    }
                }


                var vcStr = mi.Get(StreamKind.General, 0, "VideoCount");
                var vc = int.TryParse(vcStr, out var tmpInt) ? tmpInt : 0;

                for (var i = 0; i < vc; i++)
                {
                    //var bitRate = mi.Get(StreamKind.Video, i, "BitRate/String");
                    //if (string.IsNullOrEmpty(bitRate))
                    //    bitRate = mi.Get(StreamKind.Video, i, "BitRate_Maximum/String");
                    //if (string.IsNullOrEmpty(bitRate))
                    //    bitRate = "unknown";

                    var bitRate = mi.Get(StreamKind.Video, i, "BitRate/String");

                    if (!string.IsNullOrEmpty(bitRate))
                    {
                        var maxBitRate = mi.Get(StreamKind.Video, i, "BitRate_Maximum/String");

                        if (!string.IsNullOrEmpty(maxBitRate))
                        {
                            bitRate += " - " + maxBitRate;
                        }
                    }
                    else
                    {
                        bitRate = "unknown";
                    }

                    var delay =
                        Desene.DAL.SeriesType == SeriesType.Recordings
                            ? string.Empty //weird values (20h+, etc)
                            : mi.Get(StreamKind.Video, i, "Delay/String");

                    mtd.VideoStreams.Add(
                        new VideoStreamInfo
                        {
                            Index = i + 1,
                            Format = mi.Get(StreamKind.Video, i, "Format"),
                            Format_Profile = mi.Get(StreamKind.Video, i, "Format_Profile"),
                            BitRateMode = mi.Get(StreamKind.Video, i, "BitRate_Mode/String"),
                            BitRate = bitRate,
                            Width = mi.Get(StreamKind.Video, i, "Width"),
                            Height = mi.Get(StreamKind.Video, i, "Height"),
                            FrameRate_Mode = mi.Get(StreamKind.Video, i, "FrameRate_Mode/String"),
                            FrameRate = mi.Get(StreamKind.Video, i, "FrameRate/String"),
                            Delay = delay,
                            StreamSize = mi.Get(StreamKind.Video, i, "StreamSize/String"),
                            Title = mi.Get(StreamKind.Video, i, "Title"),                       // to detect texts
                            Language = mi.Get(StreamKind.Video, i, "Language/String")
                        });
                }

                var acStr = mi.Get(StreamKind.General, 0, "AudioCount");
                var ac = int.TryParse(acStr, out tmpInt) ? tmpInt : 0;

                for (var i = 0; i < ac; i++)
                {
                    var delay =
                        Desene.DAL.SeriesType == SeriesType.Recordings
                            ? string.Empty //weird values (20h+, etc)
                            : mi.Get(StreamKind.Audio, i, "Delay/String");

                    var bitRate = mi.Get(StreamKind.Audio, i, "BitRate/String");
                    if (string.IsNullOrEmpty(bitRate))
                        bitRate = GetAudioBitrateAlt(filePath);


                    mtd.AudioStreams.Add(
                        new AudioStreamInfo
                        {
                            Index = i + 1,
                            Format = mi.Get(StreamKind.Audio, i, "Format"),
                            BitRate = bitRate,
                            Channel = mi.Get(StreamKind.Audio, i, "Channel(s)/String"),
                            ChannelPosition = mi.Get(StreamKind.Audio, i, "ChannelPositions"),
                            SamplingRate = mi.Get(StreamKind.Audio, i, "SamplingRate/String"),
                            Resolution = mi.Get(StreamKind.Audio, i, "Resolution/String"),
                            Delay = delay,
                            Video_Delay = mi.Get(StreamKind.Audio, i, "Video_Delay/String"),
                            StreamSize = mi.Get(StreamKind.Audio, i, "StreamSize/String"),
                            Title = mi.Get(StreamKind.Audio, i, "Title"),                       // to detect texts
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
                            Title = mi.Get(StreamKind.Text, i, "Title"),                        // to detect texts
                            Language = mi.Get(StreamKind.Text, i, "Language"),
                            Default = mi.Get(StreamKind.Audio, i, "Default").ToLower() == "yes",
                            Forced = mi.Get(StreamKind.Audio, i, "Forced").ToLower() == "yes"
                        });
                }

                result.AdditionalDataReturn = mtd;
                mi.Close();
            }
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }

            return result;
        }

        private static TimeSpan GetVideoDurationAlt(string filePath)
        {
            using (var shell = ShellObject.FromParsingName(filePath))
            {
                IShellProperty prop = shell.Properties.System.Media.Duration;
                if (prop.ValueAsObject == null) return TimeSpan.FromTicks(0);

                var t = (ulong)prop.ValueAsObject;
                return TimeSpan.FromTicks((long)t);
            }
        }

        private static string GetAudioBitrateAlt(string filePath)
        {
            using (var shell = ShellObject.FromParsingName(filePath))
            {
                IShellProperty prop = shell.Properties.System.Audio.EncodingBitrate;
                if (prop.ValueAsObject == null) return "unknown";

                var dRaw = (uint)prop.ValueAsObject;
                return (dRaw / 1000).ToString() + " kb/s";
            }
        }

        //MOVIES & EPISODES -> Method used to extract the image frames from the processed/specified file
        public static OperationResult GetMovieStills(MovieTechnicalDetails mtd, FFMpegConverter ffMpegConverter)
        {
            var result = new OperationResult();

            if (mtd.DurationAsInt <= 0)
                return result;

            var durationInSec = mtd.DurationAsInt / 1000;

            try
            {
                if (Path.GetExtension(mtd.InitialPath) == ".ts")
                {
                    var inputFile = new MediaFile { Filename = mtd.InitialPath };

                    var tempFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "tmpStill.jpg");
                    var outputFile = new MediaFile { Filename = tempFile };

                    using (var engine = new Engine())
                    {
                        engine.GetMetadata(inputFile);

                        for (var j = 1; j <= 3; j++)
                        {
                            var position = (durationInSec * (25 * j)) / 100; //%

                            var options = new ConversionOptions { Seek = TimeSpan.FromSeconds(position) };
                            engine.GetThumbnail(inputFile, outputFile, options);

                            using (var file = new FileStream(tempFile, FileMode.Open, FileAccess.Read))
                            {
                                var bytes = new byte[file.Length];
                                file.Read(bytes, 0, (int)file.Length);

                                mtd.MovieStills.Add(bytes);
                            }
                        }

                        File.Delete(tempFile);
                    }
                }
                else
                {
                    if (ffMpegConverter == null)
                        ffMpegConverter = new FFMpegConverter();

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
                        var opRes2 = arguments.Key.GenerateThumbnail
                            ? GetMovieStills(mtdObj, ffMpegConverter)
                            : new OperationResult();

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
                        //if (!string.IsNullOrEmpty(arguments.Key.MkvValidatorPath) && File.Exists(arguments.Key.MkvValidatorPath))
                        //{
                        //    //ffmpeg
                        //    //ffmpeg -hide_banner -v error -i "tpyd203_t401-402_hd537-538_df104-105   Nickelodeon _ Digi Online 2022-02-28 18_06.mp4" -map 0:1 -f null - 2>out.txt
                        //    var xxx = RunExternalExe(arguments.Key.MkvValidatorPath, "--quiet " + " \"" + filePath + "\"");
                        //    var y = 1;
                        //}
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

                if (arguments.Key.SkipMultiVersion &&
                    (Path.GetFileNameWithoutExtension(filePath).EndsWith(" v2") ||
                     Path.GetFileNameWithoutExtension(filePath).EndsWith(" v3") ||
                     Path.GetFileNameWithoutExtension(filePath).EndsWith(" v4")))
                {
                    continue;
                }

                var opRes = GetFileTechnicalDetails(filePath, mi);

                if (opRes.Success)
                {
                    var mtdObj = (MovieTechnicalDetails)opRes.AdditionalDataReturn;

                    if (Desene.DAL.SeriesType == SeriesType.Recordings)
                        mtdObj.AudioStreams[0].Language = arguments.Key.RecordingAudio;

                    if (arguments.Key.GenerateThumbnail)
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

            var formProgressIndicator = new FrmProgressIndicator("Saving technical details in the database", "-", importParamsAndDetails.Value.Count);
            formProgressIndicator.Argument = importParamsAndDetails;
            formProgressIndicator.DoWork += formPI_DoWork_SaveTechnicalDetails;

            switch (formProgressIndicator.ShowDialog())
            {
                case DialogResult.Cancel:
                    result.Success = false;
                    result.CustomErrorMessage = "Operation has been canceled";
                    break;

                case DialogResult.Abort:
                    result.Success = false;
                    result.CustomErrorMessage = formProgressIndicator.Result.Error.Message;
                    break;

                case DialogResult.OK:
                    result.AdditionalDataReturn = formProgressIndicator.Result.Result;
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



        public static string RunExternalExe(string filename, string arguments = null)
        {
            var process = new Process();

            process.StartInfo.FileName = filename;
            if (!string.IsNullOrEmpty(arguments))
            {
                process.StartInfo.Arguments = arguments;
            }

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;

            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            var stdOutput = new StringBuilder();
            process.OutputDataReceived += (sender, args) => stdOutput.AppendLine(args.Data); // Use AppendLine rather than Append since args.Data is one line of output, not including the newline character.

            string stdError = null;
            try
            {
                process.Start();
                process.BeginOutputReadLine();
                stdError = process.StandardError.ReadToEnd();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                throw new Exception("OS error while executing " + Format(filename, arguments)+ ": " + e.Message, e);
            }

            if (process.ExitCode == 0)
            {
                return stdOutput.ToString();
            }
            else
            {
                var message = new StringBuilder();

                if (!string.IsNullOrEmpty(stdError))
                {
                    message.AppendLine(stdError);
                }

                if (stdOutput.Length != 0)
                {
                    message.AppendLine("Std output:");
                    message.AppendLine(stdOutput.ToString());
                }

                throw new Exception(Format(filename, arguments) + " finished with exit code = " + process.ExitCode + ": " + message);
            }
        }

        private static string Format(string filename, string arguments)
        {
            return "'" + filename +
                ((string.IsNullOrEmpty(arguments)) ? string.Empty : " " + arguments) +
                "'";
        }
    }
}
