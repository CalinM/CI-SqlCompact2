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

        #region Synopsis

        public static OperationResult ImportSynopsis(bool preserveExisting)
        {
            var result = new OperationResult();

            try
            {
                var opRes = Desene.DAL.GetMoviesForSynopsisImport(preserveExisting);
                if (!opRes.Success)
                    return opRes;

                var moviesData = (List<SynopsisImportMovieData>)opRes.AdditionalDataReturn;

                var formProgressIndicator = new FrmProgressIndicator("Synopsis Import", "Loading, please wait ...", moviesData.Count);
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
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

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
                if (descriptionLink.ToLower().Contains("moviemeter"))
                    return ParseSiteContent_Moviemeter(document);
                //else
                //if (descriptionLink.ToLower().Contains("bol.com"))
                //    return ParseSiteContent_Bol(document);
                else
                if (descriptionLink.ToLower().Contains("filmvandaag"))
                    return ParseSiteContent_Filmvandaag(document);
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
            var summaryDiv = document.QuerySelectorAll("*[data-testid='plot-xl']").FirstOrDefault();

            //var summaryDiv = document.GetElementsByClassName("summary_text").FirstOrDefault();

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

        private static string ParseSiteContent_Moviemeter(IHtmlDocument document)
        {
            var detailsDiv = document.GetElementsByClassName("details").FirstOrDefault();
            if (detailsDiv == null)
                throw new Exception("Element not found on page!");

            var synopsisParagraph = detailsDiv.Children[5];
            if (synopsisParagraph == null)
                throw new Exception("Element not found on page (2)!");

            return
                 synopsisParagraph.InnerHtml
                    .Replace("<br>", Environment.NewLine)
                    .Replace("&lt;", "<")
                    .Replace("&gt;", ">")
                    .StripHtml()
                    .Trim();
        }

        private static string ParseSiteContent_Bol(IHtmlDocument document)
        {
            var descriptionDiv = document.GetElementsByClassName("product-description").FirstOrDefault();
            if (descriptionDiv == null)
                throw new Exception("Element not found on page!");

            return
                 descriptionDiv.InnerHtml
                    .Replace("<br>", Environment.NewLine)
                    .Replace("&lt;", "<")
                    .Replace("&gt;", ">")
                    .StripHtml()
                    .Trim();
        }

        private static string ParseSiteContent_Filmvandaag(IHtmlDocument document)
        {
            var descriptionDiv = document.GetElementsByClassName("synopsis").FirstOrDefault();
            if (descriptionDiv == null)
                throw new Exception("Element not found on page!");

            return
                 descriptionDiv.InnerHtml
                    .Replace("<br>", Environment.NewLine)
                    .Replace("&lt;", "<")
                    .Replace("&gt;", ">")
                    .StripHtml()
                    .Trim();
        }

        #endregion

        #region CommonSenseMedia data

        public static OperationResult ImportCommonSenseMediaData(bool forMovies, bool preserveExisting)
        {
            var result = new OperationResult();

            try
            {
                var opRes = Desene.DAL.GetMoviesForCommonSenseMediaDataImport(forMovies, preserveExisting);
                if (!opRes.Success)
                    return opRes;

                var moviesData = (List<SynopsisImportMovieData>)opRes.AdditionalDataReturn;

                var formProgressIndicator = new FrmProgressIndicator("CommonSenseMedia data import", "Loading, please wait ...", moviesData.Count);
                formProgressIndicator.Argument = moviesData;
                formProgressIndicator.DoWork += formPI_DoWork_ImportCommonSenseMedia;

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

        private static void formPI_DoWork_ImportCommonSenseMedia(FrmProgressIndicator sender, DoWorkEventArgs e)
        {
            var moviesData = (List<SynopsisImportMovieData>)e.Argument;

            var technicalDetailsImportErrorBgw = new List<TechnicalDetailsImportError>();

            var i = 0;
            foreach (var movieData in moviesData)
            {
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                var opRes = GetCommonSenseMediaData(movieData.DescriptionLink);

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
                    opRes = Desene.DAL.SaveCommonSenseMediaData(movieData.MovieId, (CSMScrapeResult)opRes.AdditionalDataReturn);

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

        public static OperationResult GetCommonSenseMediaData(string recommendedLink)
        {
            var result = new OperationResult();
            CSMScrapeResult csmData = null;

            using (var client = new HttpClient())
            {
                try
                {
                    //the implementation if forced to run synchronous to avoid ip banning from scrapped sites
                    var request = client.GetAsync(recommendedLink).Result;
                    request.EnsureSuccessStatusCode();

                    if (request.Content == null)
                        result.FailWithMessage("No content");

                    var response = request.Content.ReadAsStreamAsync().Result;

                    if (response == null || response.Length == 0)
                        return result.FailWithMessage("Invalid response!");

                    csmData = new CSMScrapeResult();
                    var parser = new HtmlParser();
                    var document = parser.ParseDocument(response);

                    var expandableSinopsis = document.GetElementsByClassName("field-name-field-review-recommended-age");

                    var greenAge = document.GetElementsByClassName("csm-green-age").FirstOrDefault();
                    csmData.GreenAge = greenAge == null ? null : greenAge.TextContent;

                    var csmRatingL0 = document.GetElementsByClassName("field-name-field-stars-rating").FirstOrDefault();

                    if (csmRatingL0 != null)
                    {
                        var csmRatingLx = csmRatingL0.QuerySelectorAll("div.field_stars_rating").FirstOrDefault();

                        if (csmRatingLx != null)
                        {
                            var ratingClass = csmRatingLx.ClassList.FirstOrDefault(_ => _.StartsWith("rating-"));

                            if (ratingClass != null)
                            {
                                if (int.TryParse(ratingClass.Replace("rating-", ""), out int ratingInt))
                                    csmData.Rating = ratingInt;
                            }

                            var shortDescriptionObj = csmRatingL0.QuerySelectorAll("meta[property=\"description\"]").FirstOrDefault();
                            if (shortDescriptionObj != null)
                            {
                                var metaElement = (AngleSharp.Html.Dom.IHtmlMetaElement)shortDescriptionObj;
                                csmData.ShortDescription = metaElement.Content;
                            }

                            var reviewObj = csmRatingL0.QuerySelectorAll("meta[property=\"reviewBody\"]").FirstOrDefault();
                            if (reviewObj != null)
                            {
                                var metaElement = (AngleSharp.Html.Dom.IHtmlMetaElement)reviewObj;

                                var rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
                                csmData.Review = rx.Replace(metaElement.Content, "");
                            }
                        }
                    }




                    var statisticsAdult = document.GetElementsByClassName("user-review-statistics adult").FirstOrDefault();
                    if (statisticsAdult != null)
                    {
                        var adultAge = statisticsAdult.GetElementsByClassName("stat-wrapper age").FirstOrDefault();
                        csmData.AdultRecomendedAge = adultAge == null ? null : adultAge.TextContent;


                        var adultRating = statisticsAdult.GetElementsByClassName("field-stars-rating").FirstOrDefault();
                        if (adultRating != null)
                        {
                            var ratingClass = adultRating.ClassList.FirstOrDefault(_ => _.StartsWith("rating-"));
                            //result.AdultRating = ratingClass.Replace("rating-", "");
                            if (int.TryParse(ratingClass.Replace("rating-", ""), out int ratingInt))
                                csmData.AdultRating = ratingInt;
                        }
                    }

                    var statisticsChild = document.GetElementsByClassName("user-review-statistics child").FirstOrDefault();
                    if (statisticsChild != null)
                    {
                        var childAge = statisticsChild.GetElementsByClassName("stat-wrapper age").FirstOrDefault();
                        csmData.ChildRecomendedAge = childAge == null ? null : childAge.TextContent;

                        var childRating = statisticsChild.GetElementsByClassName("field-stars-rating").FirstOrDefault();
                        if (childRating != null)
                        {
                            var ratingClass = childRating.ClassList.FirstOrDefault(_ => _.StartsWith("rating-"));
                            //result.ChildRating = ratingClass.Replace("rating-", "");
                            if (int.TryParse(ratingClass.Replace("rating-", ""), out int ratingInt))
                                csmData.ChildRating = ratingInt;
                        }
                    }

                    var storyParent = document.GetElementsByClassName("pane-node-field-what-is-story").FirstOrDefault();
                    if (storyParent != null)
                    {
                        var storyEl = storyParent.QuerySelectorAll("p").FirstOrDefault();
                        csmData.Story = storyEl == null ? null : storyEl.TextContent;
                    }

                    var isItAnyGoodParent = document.GetElementsByClassName("pane-node-field-any-good").FirstOrDefault();
                    if (isItAnyGoodParent != null)
                    {
                        var isItAnyGoodEl = isItAnyGoodParent.QuerySelectorAll("p").FirstOrDefault();
                        csmData.IsItAnyGood = isItAnyGoodEl == null ? null : isItAnyGoodEl.TextContent;
                    }

                    var talkWithKidsAboutParent = document.GetElementsByClassName("pane-node-field-family-topics").FirstOrDefault();
                    if (talkWithKidsAboutParent != null)
                    {
                        var talkWithKidsAboutEl = talkWithKidsAboutParent.QuerySelectorAll("p");

                        if (talkWithKidsAboutEl.Any())
                        {
                            csmData.TalkWithKidsAbout = new List<string>();

                            foreach (var item in talkWithKidsAboutEl)
                            {
                                csmData.TalkWithKidsAbout.Add(item.TextContent);
                            }
                        }
                    }

                    ALotOrALittle tmp = null;

                    tmp = Get_ALotOrALittle(document, "content-grid-item-educational", ALotOrAlittleElements.EducationalValue);
                    if (tmp != null) csmData.ALotOrALittle.Add(tmp);

                    tmp = Get_ALotOrALittle(document, "content-grid-item-message", ALotOrAlittleElements.PositiveMessages);
                    if (tmp != null) csmData.ALotOrALittle.Add(tmp);

                    tmp = Get_ALotOrALittle(document, "content-grid-item-role_model", ALotOrAlittleElements.PositiveRoleModelsAndRepresentations);
                    if (tmp != null) csmData.ALotOrALittle.Add(tmp);

                    tmp = Get_ALotOrALittle(document, "content-grid-item-violence", ALotOrAlittleElements.ViolenceAndScariness);
                    if (tmp != null) csmData.ALotOrALittle.Add(tmp);

                    tmp = Get_ALotOrALittle(document, "content-grid-item-sex", ALotOrAlittleElements.SexyStuff);
                    if (tmp != null) csmData.ALotOrALittle.Add(tmp);

                    tmp = Get_ALotOrALittle(document, "content-grid-item-language", ALotOrAlittleElements.Language);
                    if (tmp != null) csmData.ALotOrALittle.Add(tmp);

                    tmp = Get_ALotOrALittle(document, "content-grid-item-consumerism", ALotOrAlittleElements.Consumerism);
                    if (tmp != null) csmData.ALotOrALittle.Add(tmp);

                    tmp = Get_ALotOrALittle(document, "content-grid-item-drugs", ALotOrAlittleElements.DrinkingDrugsAndSmoking);
                    if (tmp != null) csmData.ALotOrALittle.Add(tmp);
                }
                catch (Exception ex)
                {
                    return result.FailWithMessage(ex);
                }
            }

            result.AdditionalDataReturn = csmData;
            return result;

            ALotOrALittle Get_ALotOrALittle(IHtmlDocument document, string elementId, ALotOrAlittleElements category)
            {
                ALotOrALittle result2 = null;

                var parentElement = document.GetElementById(elementId);
                if (parentElement != null)
                {
                    var stars = 0;
                    var description = string.Empty;

                    var ratingEl = parentElement.QuerySelectorAll("div.field_content_grid_rating").FirstOrDefault();
                    if (ratingEl != null)
                    {
                        var ratingClass = ratingEl.ClassList.FirstOrDefault(_ => _.StartsWith("content-grid-") && char.IsDigit(_[_.Length - 1]));
                        //stars = ratingClass.Replace("content-grid-", "");

                        if (int.TryParse(ratingClass.Replace("content-grid-", ""), out int ratingInt))
                            stars = ratingInt;
                    }

                    var ratingDescEl = parentElement.QuerySelectorAll("div.field-name-field-content-grid-rating-text").FirstOrDefault();
                    if (ratingDescEl != null)
                    {
                        var ratingDescEl2 = ratingDescEl.QuerySelectorAll("p").FirstOrDefault();
                        if (ratingDescEl2 != null)
                        {
                            description = ratingDescEl2.TextContent;
                        }
                    }

                    result2 = new ALotOrALittle() { Rating = stars, Description = description, Category = category};
                }

                return result2;
            }
        }

        #endregion
    }
}
