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
                    // Set user agent to avoid blocking
                    client.DefaultRequestHeaders.Add("User-Agent",
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

                    var rx = new System.Text.RegularExpressions.Regex("<[^>]*>");

                    // The implementation is forced to run synchronous to avoid IP banning from scraped sites
                    var request = client.GetAsync(recommendedLink).Result;
                    request.EnsureSuccessStatusCode();

                    if (request.Content == null)
                        return result.FailWithMessage("No content");

                    var response = request.Content.ReadAsStreamAsync().Result;

                    if (response == null || response.Length == 0)
                        return result.FailWithMessage("Invalid response!");

                    csmData = new CSMScrapeResult();
                    var parser = new HtmlParser();
                    var document = parser.ParseDocument(response);

                    // Extract age rating - look for csm-green-age or rating__age elements first
                    var ageElement = document.QuerySelector(".csm-green-age") ??
                                    document.QuerySelector(".rating__age") ??
                                    document.QuerySelector("div[class*='age-rating']") ??
                                    document.QuerySelector("span[class*='age-rating']") ??
                                    document.QuerySelector("div[data-testid='age-rating']");

                    if (ageElement != null)
                    {
                        csmData.GreenAge = SanitizeText(ageElement.TextContent);
                    }
                    else
                    {
                        // Fallback: look for "Age X+" pattern in text
                        var ageElements = document.QuerySelectorAll("*");
                        foreach (var element in ageElements)
                        {
                            var text = element.TextContent?.Trim();
                            if (!string.IsNullOrEmpty(text))
                            {
                                var ageMatch = System.Text.RegularExpressions.Regex.Match(text, @"Age (\d+)\+");
                                if (ageMatch.Success)
                                {
                                    csmData.GreenAge = ageMatch.Groups[0].Value; // "Age 5+"
                                    break;
                                }

                                // Alternative pattern: "Why Age X+?"
                                var whyAgeMatch = System.Text.RegularExpressions.Regex.Match(text, @"Why Age (\d+)\+");
                                if (whyAgeMatch.Success)
                                {
                                    csmData.GreenAge = $"Age {whyAgeMatch.Groups[1].Value}+";
                                    break;
                                }
                            }
                        }
                    }

                    // Extract overall rating - look for star ratings or numeric ratings
                    var ratingElement = document.QuerySelector("div[class*='star-rating']") ??
                                       document.QuerySelector("div[data-testid='star-rating']") ??
                                       document.QuerySelector(".rating__score");

                    if (ratingElement != null)
                    {
                        // Try to count filled stars
                        var filledStars = ratingElement.QuerySelectorAll("i[class*='star'][class*='filled'], i[class*='star'][class*='active'], .icon-star-rating.active");
                        if (filledStars.Any())
                        {
                            csmData.Rating = filledStars.Count();
                        }
                        else
                        {
                            // Try to extract rating from class names or text
                            var ratingText = ratingElement.TextContent?.Trim();
                            if (!string.IsNullOrEmpty(ratingText))
                            {
                                var ratingMatch = System.Text.RegularExpressions.Regex.Match(ratingText, @"(\d+)\s*out\s*of\s*5");
                                if (ratingMatch.Success && int.TryParse(ratingMatch.Groups[1].Value, out int rating))
                                {
                                    csmData.Rating = rating;
                                }
                            }
                        }
                    }

                    // Extract short description - look for meta description or summary
                    var shortDescElement = document.QuerySelector("meta[property='og:description']") ??
                                          document.QuerySelector("meta[name='description']") ??
                                          document.QuerySelector("div[class*='summary'] p") ??
                                          document.QuerySelector("div[data-testid='summary'] p");

                    if (shortDescElement != null)
                    {
                        if (shortDescElement.TagName.ToLower() == "meta")
                        {
                            var metaElement = shortDescElement as IHtmlMetaElement;
                            csmData.ShortDescription = metaElement?.Content;
                        }
                        else
                        {
                            csmData.ShortDescription = SanitizeText(shortDescElement.TextContent);
                        }
                    }

                    // Extract "Parents Need to Know" section from modal - only the first paragraph
                    var parentsNeedToKnowElement = document.QuerySelector(".modal__body .slider__slides .paragraph-inline") ??
                                                  document.QuerySelector(".modal__body .paragraph-inline") ??
                                                  document.QuerySelector(".slider__slides .paragraph-inline");

                    if (parentsNeedToKnowElement != null)
                    {
                        var fullText = SanitizeText(parentsNeedToKnowElement.TextContent);
                        // Split by sentence endings and take only the first paragraph
                        var sentences = fullText.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
                        if (sentences.Length > 0)
                        {
                            // Take sentences until we hit a natural paragraph break (look for common paragraph starters)
                            var firstParagraph = new List<string>();
                            foreach (var sentence in sentences)
                            {
                                var trimmedSentence = sentence.Trim();
                                if (!string.IsNullOrEmpty(trimmedSentence))
                                {
                                    firstParagraph.Add(trimmedSentence);
                                    // Stop if we hit a sentence that looks like it starts a new paragraph
                                    if (trimmedSentence.StartsWith("Also") ||
                                        trimmedSentence.StartsWith("Additionally") ||
                                        trimmedSentence.StartsWith("Furthermore") ||
                                        trimmedSentence.StartsWith("Moreover") ||
                                        trimmedSentence.StartsWith("In addition") ||
                                        (firstParagraph.Count > 3 && trimmedSentence.Length > 50)) // Heuristic for paragraph break
                                    {
                                        break;
                                    }
                                }
                            }
                            csmData.Review = string.Join(". ", firstParagraph) + ".";
                        }
                        else
                        {
                            csmData.Review = fullText;
                        }
                    }
                    else
                    {
                        // Fallback: look for any paragraph-inline elements and take the first one
                        var paragraphInlineElements = document.QuerySelectorAll(".paragraph-inline");
                        if (paragraphInlineElements.Any())
                        {
                            var fullText = SanitizeText(paragraphInlineElements.First().TextContent);
                            // Apply the same paragraph splitting logic
                            var sentences = fullText.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
                            if (sentences.Length > 0)
                            {
                                var firstParagraph = new List<string>();
                                foreach (var sentence in sentences)
                                {
                                    var trimmedSentence = sentence.Trim();
                                    if (!string.IsNullOrEmpty(trimmedSentence))
                                    {
                                        firstParagraph.Add(trimmedSentence);
                                        if (trimmedSentence.StartsWith("Also") ||
                                            trimmedSentence.StartsWith("Additionally") ||
                                            trimmedSentence.StartsWith("Furthermore") ||
                                            trimmedSentence.StartsWith("Moreover") ||
                                            trimmedSentence.StartsWith("In addition") ||
                                            (firstParagraph.Count > 3 && trimmedSentence.Length > 50))
                                        {
                                            break;
                                        }
                                    }
                                }
                                csmData.Review = string.Join(". ", firstParagraph) + ".";
                            }
                            else
                            {
                                csmData.Review = fullText;
                            }
                        }
                    }

                    var allText = document.Body?.TextContent ?? "";

                    // Extract main review content - look for "Is It Any Good?" section
                    var isItGoodElement = document.QuerySelector(".review-view-is-any-good-content.formatted-text");

                    if (isItGoodElement != null)
                    {
                        // Remove all elements with "ratings" class before extracting text
                        var ratingsElements = isItGoodElement.QuerySelectorAll(".ratings");
                        foreach (var ratingElementX in ratingsElements)
                        {
                            ratingElement.Remove();
                        }

                        csmData.IsItAnyGood = SanitizeText(isItGoodElement.TextContent);
                    }
                    else
                    {
                        // Fallback: use regex extraction but clean up ratings content
                        var isItGoodMatch = System.Text.RegularExpressions.Regex.Match(allText,
                            @"Is It Any Good\?\s*(.*?)(?=Talk to Your Kids About|Movie Details|$)",
                            System.Text.RegularExpressions.RegexOptions.Singleline);

                        if (isItGoodMatch.Success)
                        {
                            var content = isItGoodMatch.Groups[1].Value.Trim();

                            // Remove the specific rating format "**Our review Parents say **(207 ) **Kids say **(475 )"
                            content = System.Text.RegularExpressions.Regex.Replace(content,
                                @"\*\*Our review\s+Parents say\s+\*\*\s*\(\s*\d+\s*\)\s*\*\*Kids say\s+\*\*\s*\(\s*\d+\s*\)",
                                "",
                                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                            // Remove any remaining markdown-style bold markers
                            content = System.Text.RegularExpressions.Regex.Replace(content, @"\*\*", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                            // Clean up extra whitespace
                            content = System.Text.RegularExpressions.Regex.Replace(content, @"\s+", " ");

                            csmData.IsItAnyGood = content.Trim();
                        }
                    }

                    // Extract "What's the Story?" section - look for full content in modal/expandable section
                    var storyElement = document.QuerySelector(".modal__body .slider__slides .paragraph-inline") ??
                                      document.QuerySelector(".story-modal .paragraph-inline") ??
                                      document.QuerySelector(".expandable-content .paragraph-inline") ??
                                      document.QuerySelector("[data-testid='story-content'] .paragraph-inline");

                    if (storyElement == null)
                    {
                        // Fallback: try to find story content by looking for elements that might contain the full story
                        var storyElements = document.QuerySelectorAll(".paragraph-inline");
                        foreach (var element in storyElements)
                        {
                            var text = element.TextContent?.Trim();
                            if (!string.IsNullOrEmpty(text) && text.Length > 100 &&
                                (text.Contains("story") || text.Contains("plot") || text.Contains("movie") || text.Contains("film")))
                            {
                                storyElement = element;
                                break;
                            }
                        }
                    }

                    if (storyElement != null)
                    {
                        csmData.Story = SanitizeText(storyElement.TextContent);
                    }
                    else
                    {
                        // Final fallback: use regex to extract from full text
                        var storyMatch = System.Text.RegularExpressions.Regex.Match(allText,
                            @"What's the Story\?\s*(.*?)(?=Is It Any Good|Talk to Your Kids About|Movie Details|$)",
                            System.Text.RegularExpressions.RegexOptions.Singleline);

                        if (storyMatch.Success)
                        {
                            var storyText = storyMatch.Groups[1].Value.Trim();
                            // Remove "Show more" or similar text
                            storyText = System.Text.RegularExpressions.Regex.Replace(storyText, @"Show more.*$", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                            csmData.Story = storyText.Trim();
                        }
                    }

                    // Extract "Talk to Your Kids About" section
                    var talkSectionElement = document.QuerySelector("#family-talk");
                    if (talkSectionElement != null)
                    {
                        // Find the parent container and look for the reveal content
                        var parentContainer = talkSectionElement.ParentElement;
                        if (parentContainer != null)
                        {
                            var revealContent = parentContainer.QuerySelector(".reveal__content ul");
                            if (revealContent != null)
                            {
                                var listItems = revealContent.QuerySelectorAll("li");
                                if (listItems.Any())
                                {
                                    var talkTopics = new List<string>();
                                    foreach (var listItem in listItems)
                                    {
                                        var paragraph = listItem.QuerySelector("p");
                                        if (paragraph != null)
                                        {
                                            var text = SanitizeText(paragraph.TextContent);
                                            if (!string.IsNullOrEmpty(text) && text.Length > 10)
                                            {
                                                talkTopics.Add(text);
                                            }
                                        }
                                    }

                                    if (talkTopics.Any())
                                    {
                                        csmData.TalkWithKidsAbout = talkTopics;
                                    }
                                }
                            }
                        }
                    }

                    // Extract Adult Reviews data
                    var adultReviewsElement = document.QuerySelector("#review-view-user-reviews-tab-item-adult");
                    if (adultReviewsElement != null)
                    {
                        // Extract Adult Rating from rating__score
                        var adultRatingElement = adultReviewsElement.QuerySelector(".rating__score");
                        if (adultRatingElement != null)
                        {
                            var activeStars = adultRatingElement.QuerySelectorAll(".icon-star-solid.active");
                            if (activeStars.Any())
                            {
                                csmData.AdultRating = activeStars.Count();
                            }
                        }

                        // Extract Adult Recommended Age from rating__age
                        var adultAgeElement = adultReviewsElement.QuerySelector(".rating__age");
                        if (adultAgeElement != null)
                        {
                            csmData.AdultRecomendedAge = SanitizeText(adultAgeElement.TextContent);
                        }
                    }

                    // Extract Child Reviews data
                    var childReviewsElement = document.QuerySelector("#review-view-user-reviews-tab-item-child");
                    if (childReviewsElement != null)
                    {
                        // Extract Child Rating from rating__score
                        var childRatingElement = childReviewsElement.QuerySelector(".rating__score");
                        if (childRatingElement != null)
                        {
                            var activeStars = childRatingElement.QuerySelectorAll(".icon-star-solid.active");
                            if (activeStars.Any())
                            {
                                csmData.ChildRating = activeStars.Count();
                            }
                        }

                        // Extract Child Recommended Age from rating__age
                        var childAgeElement = childReviewsElement.QuerySelector(".rating__age");
                        if (childAgeElement != null)
                        {
                            csmData.ChildRecomendedAge = SanitizeText(childAgeElement.TextContent);
                        }
                    }




                    // Extract "A lot or a little" sections - this would need specific implementation
                    // based on the actual page structure for these sections
                    foreach (ALotOrAlittleElements category in (ALotOrAlittleElements[])Enum.GetValues(typeof(ALotOrAlittleElements)))
                    {
                        var categoryData = ExtractALotOrALittleData(document, category);
                        if (categoryData != null)
                        {
                            csmData.ALotOrALittle.Add(categoryData);
                        }
                    }

                    result.AdditionalDataReturn = csmData;
                }
                catch (Exception ex)
                {
                    return result.FailWithMessage(ex);
                }
            }

            return result;
        }

        private static ALotOrALittle ExtractALotOrALittleData(IHtmlDocument document, ALotOrAlittleElements category)
        {
            try
            {
                var categoryDescription = EnumHelpers.GetEnumDescription(category);

                // Look for content-grid-content divs
                var contentGridElements = document.QuerySelectorAll(".content-grid-content");

                foreach (var gridElement in contentGridElements)
                {
                    // Check if this grid element contains the category we're looking for
                    var labelElement = gridElement.QuerySelector(".rating__label");
                    if (labelElement != null && labelElement.TextContent?.Trim() == categoryDescription)
                    {
                        var result = new ALotOrALittle { Category = category };

                        // Get the full text from data-text attribute
                        var dataText = gridElement.GetAttribute("data-text");
                        if (!string.IsNullOrEmpty(dataText))
                        {
                            // Remove HTML tags and clean up the text
                            var rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
                            result.Description = rx.Replace(dataText, "").Trim();

                            // Clean up extra whitespace
                            result.Description = System.Text.RegularExpressions.Regex.Replace(result.Description, @"\s+", " ");
                        }

                        // Extract rating (look for rating__score element and count active i elements)
                        var ratingElement = gridElement.QuerySelector(".rating__score");
                        if (ratingElement != null)
                        {
                            // Count i elements with "active" class
                            var activeElements = ratingElement.QuerySelectorAll("i.active");
                            if (activeElements.Any())
                            {
                                result.Rating = activeElements.Count();
                            }
                        }

                        return result;
                    }
                }
            }
            catch (Exception)
            {
                // Ignore errors for individual categories
            }
            return null;
        }

        private static string SanitizeText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            // Remove HTML tags
            var rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
            text = rx.Replace(text, "");

            // Clean up whitespace
            text = System.Text.RegularExpressions.Regex.Replace(text, @"\s+", " ");

            return text.Trim();
        }

        #endregion
    }
}