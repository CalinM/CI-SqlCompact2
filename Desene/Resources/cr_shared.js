var trailerPlaying = false;
var searchResultTimer = null;

var waitForFinalEvent = (function () {
    var timers = {};
    return function (callback, ms, uniqueId) {
        if (!uniqueId) {
            uniqueId = "Don't call this twice without a uniqueId";
        }
        if (timers[uniqueId]) {
            clearTimeout(timers[uniqueId]);
        }
        timers[uniqueId] = setTimeout(callback, ms);
    };
})();

function DisplayHome() {
	SoftCloseSearch();

	//$("#moviesSections").css("display", "none");
	$("#sections-wrapper").html(
		"<div class='aboutPage-warning-title'>" +
        	"Warning!" +
    	"</div>" +
    	"<div class='aboutPage-warning-text'>" +
        	"None of these movies are for sale or hosted on this site!" +
        	"</br>" +
        	"This is a personal index used exclusivelly by me, my family and my friends." +
        	"</br>" +
        	"Feel free to contact me at c****.********u@gmail.com" +
    	"</div>"
	);

	$("#moviesSections span").removeClass("selected-subSection");
	$("#rootNav").css("display", "block");


	//$("#sections-wrapper").html($("#homePageContent").html());
	$("#moviesSections span").removeClass("selected-subSection");
	$(".about-message-img").css("display", "");

	$("#genVersion").html("v" + genDetails);

	//$("#mobileWarning").css("display", isMobile() ? "block" : "none");
	$("#snapshotStat").html("");

	$(".closeNewsSectionWrapper").css("display", "none");
	$(".about-message-img").css("display", "block");
	
	RenderWhatsNew();
}

function RenderWhatsNew() {
	if (!isMobile()) {
		if ($("#newWrapper").length == 0) {
			var whatsNewSection =
				"<div id='newWrapper' style='position: absolute; bottom: 0; width: 100%;'>" + 
					"<div class='tabbed round'>" + 
						"<ul>" + 
							"<li data-type='0'>New movies</li>" + 
							"<li data-type='1'>Updated movies</li>" + 
							"<li data-type='2'>New series/episodes</li>" + 
							//"<li data-type='3'>New recordings/episodes</li>" + 
							"<li data-type='4'>New collections/elements</li>" + 
						"</ul>" + 

						"<div class='closeNewsSectionWrapper'>" + 
							"<button id='closeNewsSection' class='circle' data-animation='xMarks' title='Close the News sections'></button>" + 
						"</div>" + 
					"</div>" + 

					"<div id='newInnerWrapper'>" + 
					"</div>" + 
				"</div>";

			$("#sections-wrapper").append(whatsNewSection);			
		}

		setTimeout(function () {
			$(".tabbed li").off("click").on("click", function () {
				if (!$(this).hasClass("active")) {
					$(this).parent().find("li").removeClass("active");
					$(this).addClass("active");

					if ($("#newInnerWrapper").height() < 1) //can be 0.5 on different zoom levels
						$("#newInnerWrapper").height(280);

					var sectionRenderer = function (sectionIds, sectionType, masterList) {
						if (sectionIds.length > 0) {
							var newMoviesDet =
								masterList
									.filter(
										function (e) {
											return sectionIds.indexOf(e.Id) >= 0;
										})
									.sort(function (a, b) {
										return sectionIds.indexOf(a.Id) - sectionIds.indexOf(b.Id);
									});

							var sectionHtml = "<div id=\"newMovies\" class=\"owl-carousel owl-theme\">";
							var extraPath = "";

							switch (sectionType) {
								case 0:
									extraPath = "Movies";
									break;
								case 1:
									extraPath = "Series";
									break;
								case 2:
									extraPath = "Recordings";
									break;
								// case 3:
								// 	extraPath = "Collections";
								// 	break;
							}

							newMoviesDet.forEach(function (el) {
								var tooltip =
									"Title: " + el.FN + "\n" +
									"Quality: " + el.Q + "\n" +
									"Audio: " + el.A + "\n" +
									"Subtitle: " + el.SU + "\n" +
									"Recommended: " + el.R + "\n";
									
								if (sectionType == 0)
									tooltip += "\n" + "Double click to jump to movie info ...";

								// if (sectionType == 3 && el.T == 0) //collection of type "movies" ~ no poster at collection level
								// {
								// 	sectionHtml +=
								// 		"<div class=\"collectionT0-new-cover\" title=\"" + tooltip + "\">" +
								// 			el.FN +
								// 		"</div>";
								// }
								// else {
									sectionHtml +=
										"<div data-movieId='" + el.Id + "'>" +
											"<img src=\"Imgs/" + extraPath + "/poster-" + el.Id + ".jpg\" class=\"movie-cover-new\" alt=\"Loading poster ...\" title=\"" + tooltip + "\">" +
										"</div>";
								//}
							});

							sectionHtml += "</div>";

							finishRenderSection(sectionHtml);

							if (sectionType == 0)
							{
								setTimeout(function() {
									$(".movie-cover-new").off("dblclick").on("dblclick", function() {
										$("#snapshotStat").html(moviesStat);

										BuildMoviesSection(moviesData, null);

										setTimeout(() => {
											var movieId = $(this).parent().data("movieid");

											$("#sections-wrapper").slimScroll({
												scrollToAnimationDuration: 500,
												scrollTo: parseInt($("div[data-movieid='" + movieId +"']").offset().top) - 100
											});

											setTimeout(() => {
												var movieEl = $(".movie-detail-wrapper[data-movieid=" + movieId + "]").parent();
												movieEl.addClass("newItemClicked-highlight");

												setTimeout(() => {
													movieEl.removeClass("newItemClicked-highlight");
												}, 1500);

											}, 1000);
										}, 200);

										$("#moviesSections span").removeClass("selected-subSection");
										$("#moviesSections span[data-categ=17]").addClass("selected-subSection");
									})
								}, 100);
							}
						}
						else {
							$("#newMovies").html("No data available!");
						}
					}

					var collectionSectionRenderer = function () {
						var sectionHtml = "<div id=\"newMovies\" class=\"owl-carousel owl-theme\">";

						Object.keys(newElementsInCol).sort((a, b) => b - a).forEach(function (elId) {
							var colId = newElementsInCol[elId];
							var elData;

							if (elId == colId) {
								//console.log("series-type");
								elData = $.grep(collectionsData, function (x) { return x.Id == colId; });
							}
							else {
								//console.log("movie-type");
								elData = $.grep(collectionsElements, function (x) { return x.Id == elId; });
							}

							var tooltip =
								"Title: " + elData[0].FN + "\n" +
								"Quality: " + elData[0].Q + "\n" +
								"Audio: " + elData[0].A + "\n" +
								"Subtitle: " + elData[0].SU + "\n" +
								"Recommended: " + elData[0].R;

							sectionHtml +=
								"<div>" +
								"<img src=\"Imgs/Collections/poster-" + elData[0].Id + ".jpg\" class=\"movie-cover-new\" title=\"" + tooltip + "\" alt=\"" + elData[0].FN + "\">" +
								"</div>";
						});

						sectionHtml += "</div>";

						finishRenderSection(sectionHtml);

					}

					var finishRenderSection = function (sectionContent) {
						$("#newInnerWrapper").html(sectionContent);

						//issue with items cloning when items count < displayed; possible fix:
						//loop: ( $('.owl-carousel .items').length > 5 )
						//https://stackoverflow.com/questions/33119078/cloned-items-in-owl-carousel
						//https://github.com/OwlCarousel2/OwlCarousel2/issues/2091
						setTimeout(function () {
							$("#newMovies").owlCarousel({
								autoplay: true,
								autoplayTimeout: 3000,
								autoplayHoverPause: true,
								loop: true,
								margin: 10,
								nav: false,
								dots: true,

								responsive: {
									0: {
										items: 2,
										//loop:( $('.item').length > 2 )
									},
									600: {
										items: 3,
										//loop:( $('.item').length > 3 )
									},
									1000: {
										items: 6,
										//margin: 20,
										//loop:( $('.item').length > 6 )
									},
									2000: {
										items: 8,
										//loop:( $('.item').length > 8 )
									}
								}
							});
						}, 0);
					}

					switch ($(this).data("type")) {
						case 0:
							sectionRenderer(newMovies, 0, moviesData);
							break;

						case 1:
							sectionRenderer(updatedMovies, 0, moviesData);
							break;

						case 2:
							sectionRenderer(newSeriesEpisodes, 1, seriesData);
							break;

						case 3:
							sectionRenderer(newRecordingsEpisodes, 2, recordingsData);
							break;

						case 4:
							if (Object.keys(newElementsInCol).length > 0)
								collectionSectionRenderer();
							break;
					}

					$(".closeNewsSectionWrapper").css("display", "block");
					$(".about-message-img").css("display", "none");
				}
			});

			$("#closeNewsSection").off("click").on("click", function () {
				$(".closeNewsSectionWrapper").css("display", "none");
				$(".tabbed li").removeClass("active");
				$("#newInnerWrapper").height(0);
				$(".about-message-img").css("display", "block");
		
				setTimeout(function () {
					$("#newMovies").empty();
		
				}, 1100);
			});	
		}, 0);	
	}
	else //IsMobile
	{
		var sectionRendererM =  function (sectionIds, sectionType, subSectionType, masterList, parentCell) {
			if (sectionIds.length > 0) {
				var newMoviesDet =
					masterList
						.filter(
							function (e) {
								return sectionIds.indexOf(e.Id) >= 0;
							})
						.sort(function (a, b) {
							return sectionIds.indexOf(a.Id) - sectionIds.indexOf(b.Id);
						});
				
				var elId = "newItems" +  "_" + subSectionType;

				var sectionHtml = "<div id=\""+ elId + "\" class=\"owl-carousel owl-theme\">";
				var extraPath = "";

				switch (sectionType) {
					case 0:
						extraPath = "Movies";
						break;
					case 1:
						extraPath = "Series";
						break;
				}

				newMoviesDet.forEach(function (el) {
					var tooltip =
						"Title: " + el.FN + "\n" +
						"Quality: " + el.Q + "\n" +
						"Audio: " + el.A + "\n" +
						"Subtitle: " + el.SU + "\n" +
						"Recommended: " + el.R + "\n" + "\n" +
						"Double click to jump to movie info ...";

					sectionHtml +=
						"<div data-movieId='" + el.Id + "'>" +
							"<img src=\"Imgs/" + extraPath + "/poster-" + el.Id + ".jpg\" class=\"movie-cover-new\" alt=\"Loading poster ...\" title=\"" + tooltip + "\">" +
						"</div>";
				});

				sectionHtml += "</div>";

				//finishRenderSection(sectionHtml);

				$("#" + parentCell).html(sectionHtml);

				setTimeout(function () {
					$("#" + elId).owlCarousel({
						autoplay: false,
						autoplayTimeout: 3000,
						autoplayHoverPause: false,
						loop: true,
						margin: 10,
						nav: false,
						dots: true,
					});
				}, 0);
			}
			else {
				$("#newMovies").html("No data available!");
			}			
		}
		

		var whatsNewSection =
			"<table id='newWrapper' style='width: 100%; table-layout: fixed; padding-left: 75px; padding-right: 75px; margin-top: 100px;'>" + 
				"<tr>" +
					"<td class='newSectionTitleM'>" +
						"New movies" +
					"</td>" +										
				"</tr>" +	
				"<tr>" +
					"<td class='sectionTitleSeparator'>" +
					"</td>" +										
				"</tr>" +									
				"<tr>" +				
					"<td id='newMoviesM' class='newMoviesM'>" +
					"</td>" +
				"</tr>" +
				"<tr>" +
					"<td class='newSectionTitleM'>" +
						"Updated movies" +
					"</td>" +
				"</tr>" +
				"<tr>" +
					"<td class='sectionTitleSeparator'>" +
					"</td>" +										
				"</tr>" +										
				"<tr>" +
					"<td id='updatedMoviesM' class='newMoviesM'>" +
					"</td>" +
				"</tr>" +
				"<tr>" +
					"<td class='newSectionTitleM'>" +
						"New series/episodes" +
					"</td>" +
				"</tr>" +
				"<tr>" +
					"<td class='sectionTitleSeparator'>" +
					"</td>" +										
				"</tr>" +										
				"<tr>" +
					"<td id='newUpdatedSeriesM' class='newMoviesM'>" +
					"</td>" +
				"</tr>" +				
			"</table>";

		$("#sections-wrapper").append(whatsNewSection);	

		setTimeout(function() {
			sectionRendererM(newMovies, 0, 0, moviesData, "newMoviesM");
			sectionRendererM(updatedMovies, 0, 1, moviesData, "updatedMoviesM");
			sectionRendererM(newSeriesEpisodes, 1, 2, seriesData, "newUpdatedSeriesM");
		}, 0);
	}
}

function SoftCloseSearch() {
	if ($("#searchCtrl").hasClass("focus")) {
		// if (searchResultTimer != null) {
		//     clearInterval(searchResultTimer);
		// }
		$("#searchCtrl").removeClass("focus");
		$("#searchCtrl").val("");
	}
}

function DisplaySearchResult(s) {
	if (searchResultTimer != null) {
		clearInterval(searchResultTimer);
	}

	$(s).attr("updated", "false");

	$("#sections-wrapper").empty();
	$("#snapshotStat").html("Search result");

	var searchedString = $(s).val().toLowerCase();
	var titlesMatchingSearchCriteria = $.grep(moviesData, function (el) { return el.FN.toLowerCase().indexOf(searchedString) >= 0; });

	if (titlesMatchingSearchCriteria.length > 0)
		BuildMoviesSection(titlesMatchingSearchCriteria, null, true, false); //no sort

	var seriesIds = $.grep(seriesData, function (el) { return el.FN.toLowerCase().indexOf(searchedString) >= 0; }).map(function (x) { return x.Id });	
	var seriesIdsFromEpisodes = $.grep(episodesDataS, function (el) { return el.FN.toLowerCase().indexOf(searchedString) >= 0; }).map(function (x) { return x.SId });
	
	var seriesIdMatchingSearchCriteria = [...new Set([...seriesIds,...seriesIdsFromEpisodes])];
	var seriesMatchingSearchCriteria = seriesData.filter(item => seriesIdMatchingSearchCriteria.includes(item.Id));

	if (seriesMatchingSearchCriteria.length > 0)
		BuildMoviesSection(seriesMatchingSearchCriteria, null, true, true); //no sort

	if (titlesMatchingSearchCriteria.length == 0 && seriesMatchingSearchCriteria.length == 0)
		$("#sections-wrapper").html("<div class='searchResultHeader'>Nothing found!</div>");
	//var [...new Set($.grep(episodesDataS, function (el) { return el.FN.toLowerCase().indexOf(searchedString) >= 0; }).map(function (x) { return x.SId }))]
}

function HandleCancelSearch(s) {
	$(s).removeClass("focus");

	BuildMoviesSection(moviesData, null);
}

function getRecommendedVal(movieVal) {
	var result = movieVal.replace("+", "");

	if (result != "?")
		result = result.replace("?", "");

	return result;
}

function ShowOptionsButton(){
	$("#moviesSort").css("display", "table-cell");

	$(function() {
		$.contextMenu({
			selector: "#sortButton",
			trigger: 'left',
			build: function($trigger, e) {
				return {
					callback: function(key, options) {
						switch (key)
						{
							case "MovieAudioFilter_Any":
								localStorage.setItem("MovieAudioFilter", "*");
								break;
							case "MovieAudioFilter_RO":
								localStorage.setItem("MovieAudioFilter", "RO");
								break;
							case "MovieAudioFilter_NL":
								localStorage.setItem("MovieAudioFilter", "NL");
								break;
							case "MovieTheme_Any":
								localStorage.setItem("MovieTheme", "*");
								break;
							case "MovieTheme_Christmas":
								localStorage.setItem("MovieTheme", "Christmas");
								break;
							case "MovieTheme_Easter":
								localStorage.setItem("MovieTheme", "Easter");
								break;
							case "MovieTheme_Helloween":
								localStorage.setItem("MovieTheme", "Helloween");
								break;
							case "MovieTheme_Sinterklass":
								localStorage.setItem("MovieTheme", "Sinterklass");
								break;
							case "MovieTheme_Winter":
								localStorage.setItem("MovieTheme", "Winter");
								break;

							//Valentine's day ?
							default:
								localStorage.setItem("MoviesSort", key);
								break;
						}

						if (key == "gridview")
							BuildMoviesGridSection()
						else
							BuildMoviesSection(moviesData, null);
					},
					items: {
						"fold1a": {
							"name": "Sort by",
							"items": {
								"FN": { name: "Name", icon: GetCurrentCfg2("MoviesSort", "FN", "FN") },
								"Y": { name: "Year ▲", icon: GetCurrentCfg2("MoviesSort", "Y", "FN") },
								"-Y": { name: "Year ▼", icon: GetCurrentCfg2("MoviesSort", "-Y", "FN") },
								"sep1": "---------",
								"ISP": { name: "Date added ▲", icon: GetCurrentCfg2("MoviesSort", "ISP", "FN") },
								"-ISP": { name: "Date added ▼", icon: GetCurrentCfg2("MoviesSort", "-ISP", "FN") },
								//"sep2": "---------",
								//"USP": { name: "Date updated ▲", icon: GetCurrentCfg2("MoviesSort", "USP", "FN") },
								//"-USP": { name: "Date updated ▼", icon: GetCurrentCfg2("MoviesSort", "-USP", "FN") }

							}
						},
						"sep2": "---------",
						"fold2a": {
							"name": "Contain audio",
							"items": {
								"MovieAudioFilter_Any": { name: "Any", icon: GetCurrentCfg2("MovieAudioFilter", "*", "*") },
								"sep3": "---------",
								"MovieAudioFilter_RO": { name: "Romanian", icon: GetCurrentCfg2("MovieAudioFilter", "RO", "*") },
								"MovieAudioFilter_NL": { name: "Dutch", icon: GetCurrentCfg2("MovieAudioFilter", "NL", "*") },
							}
						},
						"fold2b": {
							"name": "Theme",
							"items": {
								"MovieTheme_Any": { name: "Any", icon: GetCurrentCfg2("MovieTheme", "*", "*") },
								"sep3": "---------",
								"MovieTheme_Christmas": { name: "Christmas", icon: GetCurrentCfg2("MovieTheme", "Christmas", "*") },
								"MovieTheme_Easter": { name: "Easter", icon: GetCurrentCfg2("MovieTheme", "Easter", "*") },
								"MovieTheme_Helloween": { name: "Helloween", icon: GetCurrentCfg2("MovieTheme", "Helloween", "*") },
								"MovieTheme_Sinterklass": { name: "Sinterklass", icon: GetCurrentCfg2("MovieTheme", "Sinterklass", "*") },
								"MovieTheme_Winter": { name: "Winter", icon: GetCurrentCfg2("MovieTheme", "Winter", "*") },
							}
						},
						"sep4": "---------",
						"gridview": { "name": "Advanced view (grid)"}
					}
				};
			}
		});
	});
}

function BuildMoviesSection(moviesInSection_, outputToElement, isSearchResult, isSearchResultSeries) {
	if (outputToElement == null)
		ShowOptionsButton();

	var partialPath = outputToElement == null ? "Movies" : "Collections";

	if (isSearchResultSeries)
		partialPath = "Series";

	var sectionHtml =
		"<div class=\"container\">" +
		"<div class=\"cards\">";

	var moviesInSection;
	var sortCriteria;

	if (isSearchResult)
	{
		sortCriteria = ["fn"];
		moviesInSection = moviesInSection_;
	}
	else
	{
		var sortCriteria = outputToElement == null
			? GetSortFields("MoviesSort", "fn")
			: ["fn"];

		var filterCriteria = outputToElement == null
			? GetCurrentCfg("MovieAudioFilter", "*")
			: null;

		var isLanguageFilter = filterCriteria != null && filterCriteria != "*";
		moviesInSection = isLanguageFilter
			? $.grep(moviesInSection_, function (el) { return el.A.toUpperCase().includes(filterCriteria); })
			: moviesInSection_;

		var filterCriteria2 = outputToElement == null
			? GetCurrentCfg("MovieTheme", "*")
			: null;

		var isLanguageFilter2 = filterCriteria2 != null && filterCriteria2 != "*";
		if (isLanguageFilter2)
			moviesInSection = $.grep(moviesInSection, function (el) { return el.T == filterCriteria2; })

		if (isLanguageFilter || isLanguageFilter2)
		{
			$("#snapshotStat").html("The current filtered view contains " + moviesInSection.length + " movies");
			$("footer").addClass("footer-filtered");
		}
		else
		{
			if ($("footer").hasClass("footer-filtered"))
			{
				$("footer").removeClass("footer-filtered");
				$("#snapshotStat").html(moviesStat);
			}
		}
	}

	moviesInSection.sort(fieldSorter(sortCriteria)).forEach(function (el) {
		sectionHtml +=
			"<div class=\"card\">" +
			"<div class=\"movie-detail-wrapper\" data-movieId=\"" + el.Id + "\">" +
			"<div class=\"movie-detail\">" +

			//the movieId is also placed on the Poster to be visible in the lazy loading process
			"<img data-src=\"Imgs/" + partialPath + "/poster-" + el.Id + ".jpg\" data-movieId=\"" + el.Id + "\" class=\"movie-cover lazy\" alt=\"Loading poster ...\" title=\"" + el.FN + "\">" +
			"</div>" +

			"<div class=\"movie-detail\">" +
			(
				el.R != ""
					? (
						el.RL != ""
							? "<a class='recommended recommendedWithLink' title='Recommended: " + el.R + "\nClick for details ... (external link!)' href='" + el.RL + "' target='_blank'>" + getRecommendedVal(el.R) + "</a>"
							: "<div class='recommended'title='Recommended: " + el.R +"'>" + getRecommendedVal(el.R) + "</div>"
					  )
					: "<div class='recommended' title='Recomandare necunoscuta'>?</div>"
			) +

			"<a class='recommended info recommendedWithLink' title='Theme: " + (el.T == "" ? "-" : el.T) + "\nYear: " + el.Y + "\nDuration: " + (el.L == "" || el.L == "00:00:00" ? "?" : el.L) + "\nClick for details ... (external link!)' href='" + el.DL + "' target='_blank'>i</a>" +
			"<div class='quality' title='Size: " + el.S + "\nBitrate: " + el.B + "'>" + (el.Q == "" ? "SD?" : el.Q) + "</div>" +
			"<div class='audio' title='Subtitles: " + (el.SU == "" ? "-" : el.SU) + "\nAudio Nl source: " + (el.Nl == "" ? "-" : el.Nl) + "'>" + el.A + "</div>" +
			"</div>" +
			"</div>" +
			"</div>";
	}, this);

	sectionHtml +=
		"</div>" +
		"</div>";

	if (outputToElement == null) {//only for Movies
		$("#sections-wrapper").scrollTop(0);

		if (isSearchResult)
		{
			if ($("div[id*='searchResult_movies']").length == 0)
				$("#sections-wrapper").empty();

			var wrapperId = "searchResult_movies" + (isSearchResultSeries ? "S" : "M")
			
			$("#sections-wrapper").append(
				"<div class='searchResultHeader'>Search results - " + (isSearchResultSeries ? "Series (including episodes names)" : "Movies") + "</div><div id='" + wrapperId + "'></div>");
			outputToElement = "#" + wrapperId;
		}
	}

	$(outputToElement == null ? "#sections-wrapper" : outputToElement).html(sectionHtml);

	setTimeout(function () {
		$("#sections-wrapper .lazy").lazy({
			appendScroll: outputToElement == null || isSearchResult ? $("#sections-wrapper") : $(".detailsTableWrapper"),
			onError: function (element) {
				var movieId = $(element).data("movieid");
				var movieCard = $(".movie-detail-wrapper[data-movieid=\"" + movieId + "\"] .movie-detail:first");

				movieCard.html($("#posterNotFound").html());

				var movieWithoutPoster = $.grep(moviesInSection, function (el) { return el.Id == movieId });
				movieCard.find(".movieTitle-posterNotFound:first").text(movieWithoutPoster.length != 1
					? "Error retrieving movie title!"
					: movieWithoutPoster[0].FN);
			},

			throttle: 250
		});

		if (!isMobile()) {
			$("#sections-wrapper").slimScroll({
				height: $("#sections-wrapper").height()
			});
		}
	}, 100);

	$(".about-message-img").css("display", "none");

	//if (outputToElement != "#searchResult_movies")	
	if (!isSearchResultSeries)
		$(".card").off("click").on("click", function () {
			var movieId = $(this).children().data("movieid");

			$(".card").removeClass("selectedCard");
			if ($(".detailLine[data-movieid='" + movieId + "']").length > 0) {
				$('.detailLine').remove(); //only remove, nothing more
			}
			else {
				$('.detailLine').remove();
				$(this).addClass("selectedCard");

				var clickedTop = $(this).offset().top;
				var visibleElements = $(this).parent().find(".card:visible");
				var elementsOnLine =
					$.grep(visibleElements, function (el) { return $(el).offset().top == clickedTop; });

				var lastElementOnLine = elementsOnLine[elementsOnLine.length - 1];

				var bgImageDetailLine = "url(\"" + $("img[data-movieid='" + movieId + "']").attr("src") + "\")";
				var widthInnerTable = (elementsOnLine.length - 1) * $(".selectedCard").outerWidth(true) + $(".selectedCard").width();

				var middleColWidth = elementsOnLine.length > 3 ? "150" : "125";

				var movieData = $.grep(moviesInSection, function (el) { return el.Id == movieId });
				var baseData = movieData[0];

				var movieData2 = $.grep(outputToElement == null || outputToElement == "#searchResult_moviesM" ? moviesData2 : collectionsData2, function (el) { return el.Id == movieId });
				var detailData = movieData2[0];
				
				var detailLine =
					"<div class='detailLine' data-movieId='" + movieId + "' style='background-image: " + bgImageDetailLine + "'>" +
					"<table class='detailLine-wrapper' style='width: " + widthInnerTable + "px'>" +
					"<tr>" +
					"<td colspan='3' class='title'>" +
					baseData.FN +
					"</td>" +
					"</tr>" +
					"<tr style='height: 100%;'>" +
					"<td style='width: 100%; vertical-align: top;'>" +
					"<div class='synopsis'>" +
					detailData.Syn +
					"</div>" +
					"</td>" +
					"<td class='technicalDetailsCell' style='min-width: " + middleColWidth + "px; max-width: " + middleColWidth + "px;'>" +
					"<div class='technicalDetails-wrapper'>" +
					"<div class='tdTitle'>Video tracks</div>";

				for (var i = 0; i < detailData.Vtd.length; i++) {
					detailLine += "<div>" + detailData.Vtd[i] + "</div>";
				}

				detailLine += "<div class='tdTitle'>Audio tracks</div>";

				for (var i = 0; i < detailData.Ats.length; i++) {
					detailLine += "<div>" + detailData.Ats[i] + "</div>";
				}

				detailLine += "<div class='tdTitle'>Subtitle tracks</div>";

				for (var i = 0; i < detailData.Sts.length; i++) {
					detailLine += "<div>" + detailData.Sts[i] + "</div>";
				}

				detailLine += "<div class='tdTitle'>File details</div>";

				for (var i = 0; i < detailData.Fd.length; i++) {
					detailLine += "<div>" + detailData.Fd[i] + "</div>";
				}



				detailLine +=
					"</div>" +
					"</td>" +
					"<td style='min-width: 500px; border-left: solid thin silver;'>" +
					"<div class='screenshots-wrapper owl-carousel owl-theme' style=''>";

				if (baseData.Tr != null && baseData.Tr != "") {
					detailLine +=
						"<div>" +
						"<iframe" +
						" id='trailerFrm'" +
						" frameborder='0'" +
						" scrolling='no'" +
						" marginheight='0'" +
						" marginwidth='0'" +
						" width='400.5'" +
						" height='225'" +
						" type='text/html'" +
						//" allowfullscreen" +
						" src='https://www.youtube.com/embed/" + baseData.Tr + "?autoplay=0&fs=0&iv_load_policy=3&showinfo=0&rel=0&cc_load_policy=0&start=0&end=0&enablejsapi=1' > " +
						"</iframe>" +
						"</div>";
				}

				detailLine +=
					"<div class='parentVertAlign'>" +
					"<img class='forceVertAlign' src=\"Imgs\\" + partialPath + "\\Thumbnails\\thumb-" + movieId + "-0.jpg\" alt=\"?\">" +
					"</div>" +
					"<div class='parentVertAlign'>" +
					"<img class='forceVertAlign' src=\"Imgs\\" + partialPath + "\\Thumbnails\\thumb-" + movieId + "-1.jpg\" alt=\"?\">" +
					"</div>" +
					"<div class='parentVertAlign'>" +
					"<img class='forceVertAlign' src=\"Imgs\\" + partialPath + "\\Thumbnails\\thumb-" + movieId + "-2.jpg\" alt=\"?\">" +
					"</div>" +
					"</div>" +
					"</td>" +
					"</tr>" +
					"</table>" +
					"</div>";

				$(lastElementOnLine).after(detailLine);


				$(".synopsis, .technicalDetails-wrapper").slimScroll({
					height: $(".synopsis").height()
				});

				//hideBarY
				$(".detailLine .slimScrollBar").fadeOut('slow');
				$(".detailLine .slimScrollRail").fadeOut('slow');


				var player = new YT.Player('trailerFrm', {
					events: { 'onStateChange': onPlayerStateChange }
				});

				$('.screenshots-wrapper').owlCarousel({
					margin: 10,
					nav: true,
					navText: ["<div class='nav-btn prev-slide'></div>", "<div class='nav-btn next-slide'></div>"],
					responsive: {
						0: {
							items: 1
						},
						600: {
							items: 1
						},
						1000: {
							items: 1
						}
					}
				});

				$(".screenshots-wrapper").on("changed.owl.carousel",
					function (event) {
						if (event.type == "changed" && trailerPlaying) {
							//full refresh of the iframe
							//var leg=$('#trailerFrm').attr("src");
							//$('#trailerFrm').attr("src", leg);

							trailerPlaying = false;
							player.pauseVideo();
							//player.stopVideo();
						}
					});

				//$("#sections-wrapper").animate({scrollTop: $("#sections-wrapper").scrollTop() + $(".detailLine").prop("scrollHeight")+50}, 500);
				var scrollTop =
					isMobile() && $(document).height() < $(document).width() //mobile on landscape
						? $(".detailLine").offset().top - $("#sections-wrapper").offset().top
						//: $(".selectedCard").offset().top - $("#sections-wrapper").offset().top;
						: outputToElement == null
							? $(".selectedCard").offset().top - $("#sections-wrapper").offset().top
							: $(".selectedCard").offset().top - $(".detailsTableWrapper").offset().top;

				// Position of selected element relative to container top
				var targetTop =
					outputToElement == null
						? $("#sections-wrapper > *").offset().top - $("#sections-wrapper").offset().top
						: $(".detailsTableWrapper > *").offset().top - $(".detailsTableWrapper").offset().top;

				// The offset of a scrollable container
				var scrollOffset = scrollTop - targetTop;

				// Scroll untill target element is at the top of its container
				//$("#sections-wrapper").scrollTop(scrollOffset);

				if (outputToElement == null)
					$("#sections-wrapper").animate({ scrollTop: scrollOffset }, 500);
				else
					$(".detailsTableWrapper").animate({ scrollTop: scrollOffset }, 500);
			}
		});
}

function CloseSideNav() {
	$("#sideNav").css("width", "0");
	$(".sideNav-overlay").css("display", "none");
	$("#sections-wrapper").removeClass("sideNav-overlay-content-transform")
};

function unique(array) {
	return $.grep(array, function (el, index) {
		return index == $.inArray(el, array);
	});
}

function isNumeric(val) {
	return /^\d+$/.test(val);
}

function isMobile() {
	var a = navigator.userAgent || navigator.vendor || window.opera;
	return /(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4));
}

function onPlayerStateChange(event) {
	switch (event.data) {
		case 0:
			//video ended
			trailerPlaying = false;
			break;
		case 1:
			//video playing from + player.getCurrentTime())
			trailerPlaying = true;
			break;
		case 2:
			//video paused at '+ player.getCurrentTime())
			trailerPlaying = false;
			break;
	}
}

function GetCurrentCfg3(itemName) {
	return localStorage.getItem(itemName) == null ? "" : "customicon";
}

function GetCurrentCfg2(itemName, itemValue, defaultValue) {
	var itemCfg = localStorage.getItem(itemName);

	if (itemCfg == null && itemValue == defaultValue)
		return "customicon";

	return itemCfg != itemValue
		? ""
		: "customicon";
}

function GetCurrentCfg(itemName, defaultvalue, secondSort) {
	var cfgValue = localStorage.getItem(itemName);

	return cfgValue != null
		? cfgValue
		: defaultvalue;
}

function GetSortFields(itemName, secondSort) {
	var defaultValue = "FN";
	var cfgValue = localStorage.getItem(itemName);


	if (cfgValue == null)
	{
		return [defaultValue]
	}
	else
	{
		var result = [cfgValue.toUpperCase()];
		if (cfgValue.toUpperCase() != secondSort.toUpperCase())
			result.push(secondSort.toUpperCase());

		return result;
	}
}

function fieldSorter(fields) {
    return function (a, b) {
        return fields
            .map(function (o) {
                var dir = 1;
                if (o[0] === '-') {
                   dir = -1;
                   o=o.substring(1);
				}

				var varA =
				(typeof a[o] === 'string')
					? a[o].toUpperCase()
					: a[o];

				var varB =
					(typeof b[o] === 'string')
						? b[o].toUpperCase()
						: b[o];

				if (o == "Y")
				{
					if (varA == "?") varA = "";
					if (varB == "?") varB = "";
				}

                if (varA > varB) return dir;
                if (varA < varB) return -(dir);
                return 0;
            })
            .reduce(function firstNonZeroValue (p,n) {
                return p ? p : n;
            }, 0);
    };
}


/*
function checkImage(src, good, bad, colId) {
    var img = new Image();
	img.onload = good(colId);
	img.onerror = bad(colId);
    img.src = src;
}
*/
/*
const getImageOrFallback = (path, good, bad) => {
	return new Promise(resolve => {
	  const img = new Image();
	  img.src = path;
	  img.onload = () => resolve(good);
	  img.onerror = () => resolve(bad);
	});
  };

const link = getImageOrFallback(
	"Imgs/Collections/poster-" + colId + ".jpg",
	'exista',
	'nu exista'
	).then(result => collectionDetailsS += result)
*/