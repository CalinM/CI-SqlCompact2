using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Common;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace Utils
{
    public class WebScraping
    {
        /*
        public static OperationResult ImportSynopsis()
        {
            var result = new OperationResult();

            var dlgResult =
                MsgBox.Show("Do you want to preserve existing data?", "Confirmation", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

            if (dlgResult == DialogResult.Cancel)
            {
                result.AdditionalDataReturn = -1;
                return result;
            }

            var opRes = Desene.DAL.GetMoviesForSynopsisImport(dlgResult == DialogResult.Yes);
            if (!opRes.Success)
                return opRes;

            var moviesData = (List<SynopsisImportMovieData>)opRes.AdditionalDataReturn;
            if (moviesData == null || !moviesData.Any())
                return result.FailWithMessage("Existing movies data incorrectly determined!");

            var importResult = new List<SynopsisImportMovieData>();

            foreach (var movieData in moviesData)
            {
                opRes = GetSynopsis(movieData.DescriptionLink);
                if (!opRes.Success)
                {
                    movieData.SkipReason = opRes.CustomErrorMessage;
                    importResult.Add(movieData);
                }
                else
                {
                    movieData.Synopsis = (string)opRes.AdditionalDataReturn;
                    importResult.Add(movieData);
                }
            }

            return result;
        }
        */

        public static OperationResult ImportSynopsis(bool preserveExisting)
        {
            var result = new OperationResult();

            try
            {
                var opRes = Desene.DAL.GetMoviesForSynopsisImport(preserveExisting);
                if (!opRes.Success)
                    return opRes;

                var moviesData = (List<SynopsisImportMovieData>)opRes.AdditionalDataReturn;

                var formProgressIndicator = new FrmProgressIndicator("Movies Catalog generator", "Loading, please wait ...", moviesData.Count);
                formProgressIndicator.Argument = moviesData;
                formProgressIndicator.DoWork += formPI_DoWork_ImportSynopsis;

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
            catch (Exception ex)
            {
                return result.FailWithMessage(ex);
            }

            return result;
        }

        private static void formPI_DoWork_ImportSynopsis(FrmProgressIndicator sender, DoWorkEventArgs e)
        {
            var moviesData = (List<SynopsisImportMovieData>)e.Argument;

            var technicalDetailsImportErrorBgw = new List<TechnicalDetailsImportError>();

            var i = 0;
            foreach (var movieData in moviesData)
            {
                var opRes = GetSynopsis(movieData.DescriptionLink);

                if (!opRes.Success)
                {
                    technicalDetailsImportErrorBgw.Add(
                        new TechnicalDetailsImportError
                        {
                            FilePath = movieData.DescriptionLink,
                            ErrorMesage = opRes.CustomErrorMessage
                        });
                }
                else
                {
                    //immediate save
                    opRes = Desene.DAL.SaveSynopsis(movieData.MovieId, (string)opRes.AdditionalDataReturn);

                    if (!opRes.Success)
                    {
                        technicalDetailsImportErrorBgw.Add(
                            new TechnicalDetailsImportError
                            {
                                FilePath = movieData.DescriptionLink,
                                ErrorMesage = opRes.CustomErrorMessage
                            });
                    }
                }

                i++;

                sender.SetProgress(i, Path.GetFileName(movieData.FileName));
            }

            e.Result = technicalDetailsImportErrorBgw;
        }

        public static OperationResult GetSynopsis(string descriptionLink)
        {
            var result = new OperationResult();

            using (var client = new HttpClient())
            {
                try
                {
                    //the implementation if forced to run synchronous to avoid ip banning from scrapped sites
                    var request = client.GetAsync(descriptionLink).Result;
                    request.EnsureSuccessStatusCode();

                    if (request.Content == null)
                        result.FailWithMessage("No content");

                    var response = request.Content.ReadAsStreamAsync().Result;

                    if (response == null || response.Length == 0)
                        return result.FailWithMessage("Invalid response!");

                    result.AdditionalDataReturn = ExtractSynopsisFromDocument(descriptionLink, response);
                }
                catch (Exception ex)
                {
                    return result.FailWithMessage(ex);
                }
            }

            return result;
        }

        private static string ExtractSynopsisFromDocument(string descriptionLink, Stream source)
        {
            try
            {
                var parser = new HtmlParser();
                var document = parser.ParseDocument(source);

                if (descriptionLink.ToLower().Contains("cinemagia"))
                    return ParseSiteContent_Cinemagia(document);
                else
                if (descriptionLink.ToLower().Contains("cinemarx"))
                    return ParseSiteContent_Cinemarx(document);
                else
                if (descriptionLink.ToLower().Contains("imdb"))
                    return ParseSiteContent_Imdb(document);
                else
                    throw new Exception("Parser not implemented!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string ParseSiteContent_Cinemagia(IHtmlDocument document)
        {
            var expandableSinopsis = document.GetElementsByClassName("expand_sinopsis").Count() > 0;

            var synopsisWrapper = expandableSinopsis ? "body_sinopsis" : "short_body_sinopsis";

            var synopsisParagraph = document.QuerySelectorAll("p").FirstOrDefault(x => x.ParentElement.Id == synopsisWrapper);
            if (synopsisParagraph == null)
            {
                synopsisParagraph = document.GetElementById(synopsisWrapper);

                if (synopsisParagraph == null)
                    throw new Exception("Element not found on page!");
            }

            var splitString = synopsisParagraph.InnerHtml.Split(new string[] { "<br>" }, StringSplitOptions.None);
            var processedList = new List<string>();

            foreach (var parag in splitString)
            {
                processedList.Add(parag.StripHtml().Trim());
            }

            return string.Join(Environment.NewLine, processedList);
        }

        private static string ParseSiteContent_Cinemarx(IHtmlDocument document)
        {
            var paragraphsWithSinopsisClass = document.GetElementsByClassName("info__paragraph");

            if (paragraphsWithSinopsisClass.Count() == 0)
                throw new Exception("Element not found on page!");

            var sinopsisParagraph = paragraphsWithSinopsisClass.FirstOrDefault();
            if (sinopsisParagraph == null)
                throw new Exception("Element not found on page!");

            return
                 sinopsisParagraph.InnerHtml
                    .Replace("<br>", Environment.NewLine)
                    .Replace("&lt;", "<")
                    .Replace("&gt;", ">")
                    .StripHtml()
                    .Trim();
        }

        private static string ParseSiteContent_Imdb(IHtmlDocument document)
        {
            var summaryDiv = document.GetElementsByClassName("summary_text").FirstOrDefault();

            if (summaryDiv == null)
                throw new Exception("Element not found on page!");

            return
                 summaryDiv.InnerHtml
                    .Replace("<br>", Environment.NewLine)
                    .Replace("&lt;", "<")
                    .Replace("&gt;", ">")
                    .StripHtml()
                    .Trim();
        }
    }
}
