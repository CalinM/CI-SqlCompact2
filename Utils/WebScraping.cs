using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Common;
using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace Utils
{
    public class WebScraping
    {
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

            var sinopsisWrapper = expandableSinopsis ? "body_sinopsis" : "short_body_sinopsis";

            var sinopsisParagraph = document.QuerySelectorAll("p").FirstOrDefault(x => x.ParentElement.Id == sinopsisWrapper);
            if (sinopsisParagraph == null)
                throw new Exception("Element not found on page!");

            return
                sinopsisParagraph
                    .InnerHtml
                    .Replace("<br>", Environment.NewLine)
                    .StripHtml();
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
