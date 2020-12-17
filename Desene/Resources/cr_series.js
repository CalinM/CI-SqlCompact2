var currentSeriesTypeViewDataM;
var currentSeriesTypeViewDataD;
var showingSeries;

function RenderSeriesTypeView() {
    var sectionHtml;

    if (isMobile())
    {
        sectionHtml =
            "<div class=\"container\">" +
                "<div class=\"cards\">";

        currentSeriesTypeViewDataM.forEach(function (el) {
            sectionHtml +=
                    "<div class=\"cardM\">" +
                        "<div class=\"movie-detail-wrapper\" data-movieId=\"" + el.Id + "\">" +
                            "<div class=\"movie-detail\">" +

                                //the movieId is also placed on the Poster to be visible in the lazy loading process
                                "<img data-src=\"Imgs/Series/poster-" + el.Id + ".jpg\" data-movieId=\"" + el.Id + "\" class=\"movie-cover lazy\" alt=\"Loading poster ...\" title=\"" + el.FN + "\">" +
                            "</div>" +
                        "</div>" +
                    "</div>";
        }, this);

        sectionHtml +=
                "</div>" +
            "</div>";
    }
    else
    {
        $(this).addClass("selected-subSection");

        sectionHtml =
            "<table id=\"seriesHeaderTable\" class=\"tableWrapper\">" +
            "<tr class=\"headerRow\">" +
            "<td style=\"width: 30px;\">" +
            "</td>" +
            "<td>" +
            "Series name</br>/ Episode title" +
            "</td>" +
            "<td class=\"markerCol\">" +
            "</td>" +
            "<td class=\"detailCell w100\">" +
            "Recommended" +
            "</td>" +
            "<td class=\"detailCell w80\">" +
            "Quality" +
            "</td>" +
            "<td class=\"detailCell w100\">" +
            "Size" +
            "</td>" +
            "<td class=\"detailCell w100\">" +
            "Audio" +
            "</td>" +
            "<td class=\"detailCell w125\">" +
            "Year" +
            "</td>" +
            "<td class=\"detailCell w125\">" +
            "No. of episodes / Theme" +
            "</td>" +
            "</tr>" +
            "</table>" +

            "<div class=\"detailsTableWrapper\">" +
            "<table id=\"seriesMainTable\" class=\"tableWrapper\">";

        var serialIndex = 0;

        currentSeriesTypeViewDataM.forEach(function (serial) {
            serialIndex++;

            var link = serial.DL != null ? serial.DL : "www.imdb.com";
            var tooltip = serial.N != "" ? serial.N + "\n" : "";
            tooltip += "Click for details ... (external link!)";

            //var episoadeSerial = $.grep(currentSeriesTypeViewDataD, function (el) { return el.SId == serial.Id; });
            var alternateRowClass = serialIndex % 2 == 0 ? " alternateRow" : "";

            var differentAudioStyle = serial.DifferentAudio
                ? "style=\"color: red; cursor: help;\" title=\"Exista episoade cu diferente in track-urile audio (ex. nu sunt dublate Ro)\""
                : "";

            sectionHtml +=
                "<tr class=\"seriesLine noselect lineWithDetails" + alternateRowClass + "\">" +
                    "<td class=\"markerCol\">" +
                        "<div class=\"markerSymbol serialExpander collapsed\" data-serialId=\"" + serial.Id + "\" alt=\">\"></div>" +
                    "</td>" +
                    "<td>" +
                        serial.FN +
                    "</td>" +
                    "<td class=\"detailCell w25\">" +
                        "<a href=\"" + link + "\" target=\"_blank\" title=\"" + tooltip + "\">" +
                            "<img src=\"Images\\info.png\" class=\"infoSign\" alt=\"i\">" +
                        "</a>" +
                    "</td>" +
                    "<td class=\"detailCell w100\">" +
                        (
                            serial.R != ""
                            ? (
                                serial.RL != ""
                                    ? "<a class='recommended recommendedWithLink' title='Recomandat: " + serial.R + "\nClick for details ... (external link!)' href='" + serial.RL + "' target='_blank'>" + getRecommendedVal(serial.R) + "</a>"
                                    : "<div class='recommended'title='Recomandat: " + serial.R +"'>" + getRecommendedVal(serial.R) + "</div>"
                            )
                            : "<div class='recommended' title='Recomandare necunoscuta'>?</div>"
                        ) +
                    "</td>" +
                    "<td class=\"detailCell w80\">" +
                        serial.Q +
                    "</td>" +
                    "<td class=\"detailCell w100\">" +
                        serial.S + " GB" +
                    "</td>" +
                    "<td class=\"detailCell w100\" " + differentAudioStyle + ">" +
                        serial.A +
                    "</td>" +
                    "<td class=\"detailCell w125\">" +
                        serial.Y +
                    "</td>" +
                    "<td class=\"detailCell w125\">" +
                        serial.Ec +
                    "</td>" +
                "</tr>" +

                "<tr class=\"detailSerieLine " + alternateRowClass + "\" data-serialId=\"" + serial.Id + "\" style=\"display: none;\">" +
                    "<td style=\"width: 30px;\">" +
                    "</td>" +
                    "<td id=\"detailSerie-inner" + serial.Id + "\" colspan=\"8\">" +
                    "</td>" +
                "</tr>";
        });

        sectionHtml +=
                "</table>" +
            "</div>";
    }

    $(".about-message-img").css("display", "none");
    $("#sections-wrapper").html(sectionHtml);

    setTimeout(function () {
        if (!isMobile()) {
            RebindSeriesEvents();

            var h = window.innerHeight - $(".master-toolbar").outerHeight() - $("footer").height() - $("#seriesHeaderTable").height();
            $(".detailsTableWrapper").height(h);

            $(".detailsTableWrapper").slimScroll({
                height: h
            });
            $("#sections-wrapper").slimScroll({
                height: $("#sections-wrapper").height()
            });
        }
        else {
            RebindSeriesEventsM();

            $("#sections-wrapper .lazy").lazy({
                appendScroll: $("#sections-wrapper"),
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
        }
    }, 100);

    CloseSideNav();
}

function RebindSeriesEvents() {
    $(".serialExpander").off("click").on("click", function (evt) {
        evt.stopPropagation();
        ToggleExpandSeries($(this));
    });

    $(".sezonExpander").off("click").on("click", function (evt) {
        evt.stopPropagation();
        ToggleExpandSezon($(this));
    });


    $(".seriesLine.lineWithDetails").off("click").on("click", function (e) {
        ToggleExpandSeries($(this).find(".markerSymbol"));
    });

    $(".seasonLine.lineWithDetails").off("click").on("click", function (e) {
        ToggleExpandSezon($(this).find(".markerSymbol"));
    });

    $(".movieStillDisplay").off("click").on("click", function (evt) {
        evt.stopPropagation();

        if (evt.ctrlKey) {
            $(this).closest("table").find(".thRow").each(function (el) {
                $(this).css("display", "none");
            })
        }
        else
        {
            var currentRow = $(this).closest("tr");
            var episodeId = currentRow.data("episodeid");
            var thumbnailRow = $("#th-" + episodeId);

            if (thumbnailRow.length > 0) //it was previously generated
            {
                if ($(thumbnailRow).css("display") == "none")
                    $(thumbnailRow).css("display", "table-row");
                else
                    $(thumbnailRow).css("display", "none");
            }
            else
            {
                var thumbnailRowStr =
                    "<tr id=\"th-" +episodeId + "\" class=\"thRow\" style=\"display: table-row;\" data-serialId=\"" + currentRow.data("serialid") + "\" data-sezon=\"" + currentRow.data("sezon") + "\" >" +
                        "<td style=\"width: 30px;\">" +
                        "</td>" +
                        "<td colspan=\"8\">" +
                            "<table class=\"thumbnails-wrapper\">" +
                                "<tr>" +
                                    "<td class=\"thumbnailStillCell\">" +
                                        "<img src=\"Imgs\\Series\\Thumbnails\\thumb-" + episodeId + "-0.jpg\" alt=\"?\">" +
                                    "</td>" +
                                    "<td class=\"thumbnailStillCell\">" +
                                        "<img src=\"Imgs\\Series\\Thumbnails\\thumb-" + episodeId + "-1.jpg\" alt=\"?\">" +
                                    "</td>" +
                                    "<td class=\"thumbnailStillCell\">" +
                                        "<img src=\"Imgs\\Series\\Thumbnails\\thumb-" + episodeId + "-2.jpg\" alt=\"?\">" +
                                    "</td>" +
                                "</tr>" +
                            "</table>" +
                            "</td>" +
                    "</tr>";

                $(currentRow).after(thumbnailRowStr);
            }
        }
    });
}

function RebindSeriesEventsM() {

    var recalcDetailsHeight = function () {
        $(".detailLine").height($(".detailLine-wrapper").height());
    };

    $(".cardM").off("click").on("click", function () {
        var seriesId = $(this).children().data("movieid");

        $(".cardM").removeClass("selectedCard");
		if ($(".detailLine[data-movieid='" + seriesId + "']").length > 0) {
			$('.detailLine').remove(); //only remove, nothing more
		}
		else {
            $('.detailLine').remove();
            $(this).addClass("selectedCard");

			var clickedTop = $(this).offset().top;
			var visibleElements = $(this).parent().find(".cardM:visible");
			var elementsOnLine =
				$.grep(visibleElements, function (el) { return $(el).offset().top == clickedTop; });

			var lastElementOnLine = elementsOnLine[elementsOnLine.length - 1];
            var widthInnerTable = elementsOnLine.length * $(".cardM").outerWidth(true);

            var movieData = $.grep(currentSeriesTypeViewDataM, function (el) { return el.Id == seriesId });
			var baseData = movieData[0];

            var seriesSeasons = new Array();

            unique(
                $.grep(currentSeriesTypeViewDataD,
                    function (el) {
                        return el.SId == seriesId;
                    })
                    .map(function (el) {
                        return el.SZ;
                    })
            ).forEach(function (seasonNo) {
                seriesSeasons.push({ SeasonId: seasonNo, SeasonName: isNumeric(seasonNo) ? "Season " + seasonNo : seasonNo })
            });

            var sortedSeasons =
                seriesSeasons.sort((a, b) => a.SeasonName.localeCompare(b.SeasonName, undefined, { numeric: true, sensitivity: 'base' }));

            var detailLine =
                "<div class='detailLine' data-movieId='" + seriesId + "' style='height: 0;'>" +
                    "<table class='detailLine-wrapper' style='min-width: 0px; width: " + widthInnerTable + "px; border: none; border-collapse: collapse;'>" +
                        "<tr>" +
                            "<td colspan=2>" +
                                "<table style='width: 100%; border-collapse: collapse;'>"+
                                    "<tr class='series-title-row-m'>" +
                                        "<td class='title' style='width: 100%;'>" +
                                            baseData.FN +
                                        "</td>" +
                                        "<td class='series-quality-m'>" +
                                            baseData.Q +
                                        "</td>" +
                                        "<td class='recommended-cell'>" +
                                            "<div class='recommended-m' style='font-size: 16px'>" +
                                                getRecommendedVal(baseData.R) +
                                            "</div>" +
                                        "</td>"   +
                                    "</tr> " +
                                "</table>" +
                            "</td>"   +
                        "</tr>";

            sortedSeasons.forEach(function (seasonObj) {
                detailLine +=
                        "<tr>" +
                            "<td class='season-cell-m' colspan=2>" +
                                seasonObj.SeasonName +
                            "</td>" +
                        "</tr>";

                var episodesInSeason = $.grep(currentSeriesTypeViewDataD, function (el) { return el.SId == seriesId && el.SZ == seasonObj.SeasonId; });

                episodesInSeason.forEach(function (episode) {
                    detailLine +=
                        "<tr>" +
                            "<td class='episode-title-cell-m' data-episodeId='" + episode.Id + "'>" + //only on episode name, to avoid triggering th efunctionality while scrolling
                               episode.FN + (episode.T == "Christmas"
                               ? " 🎅" //🎄
                               : episode.T == "Helloween"
                                   ? " 🎃"
                                   : "") +
                            "</td>" +
                            "<td class='episodes-audio-m'>" +
                               episode.A +
                            "</td>" +
                        "</tr>";
                });
            });

            detailLine +=
                    "</table>" +
                "</div>"
                ;

            $(lastElementOnLine).after(detailLine);


            setTimeout(function() {
                recalcDetailsHeight();

                $(".episode-title-cell-m").on("click", function() {
                    var episodeId = $(this).data("episodeid");
                    var parentRow = $(this).parent();

                    $(".detailLine-wrapper tr").removeClass("episodeDetailExpanded");
                    $(".episode-details-open").removeClass("episode-details-open");

                    if ($(".episodeDetails[data-episodeid='" + episodeId + "']").length > 0) {
                        $('.episodeDetails').remove(); //only remove, nothing more
                        recalcDetailsHeight();
                    }
                    else {
                        $('.episodeDetails').remove();
                        var episodeData = $.grep(currentSeriesTypeViewDataD, function (el) { return el.Id == episodeId; })[0];

                        var episodeDetails =
                                    "<tr class='episodeDetails' data-episodeid='" + episodeId + "'>" +
                                        "<td colspan=2>" +
                                            "<table style='width: 100%;'>"+
                                                "<tr>" +
                                                    "<td>" +
                                                        "Year: " + episodeData.Y + "</br>" +
                                                        "Quality: " + episodeData.Q + "</br>" +
                                                        "Duration: " + episodeData.L + "</br>" +
                                                        "Size: " + episodeData.S + "</br>" +
                                                    "</td>" +

                                                    "<td style='width: 500px;'>" +
                                                        "<div class='screenshots-wrapper owl-carousel owl-theme' style=''>" +

                                                        "<div class='parentVertAlign'>" +
                                                        "<img class='forceVertAlign' src=\"Imgs\\Series\\Thumbnails\\thumb-" + episodeId + "-0.jpg\" alt=\"?\">" +
                                                        "</div>" +
                                                        "<div class='parentVertAlign'>" +
                                                        "<img class='forceVertAlign' src=\"Imgs\\Series\\Thumbnails\\thumb-" + episodeId + "-1.jpg\" alt=\"?\">" +
                                                        "</div>" +
                                                        "<div class='parentVertAlign'>" +
                                                        "<img class='forceVertAlign' src=\"Imgs\\Series\\Thumbnails\\thumb-" + episodeId + "-2.jpg\" alt=\"?\">" +
                                                        "</div>" +
                                                    "</td>" +

                                                "</tr> " +
                                            "</table>" +
                                        "</td>"   +
                                    "</tr>";

                        $(parentRow).after(episodeDetails);
                        $(parentRow).addClass("episodeDetailExpanded");
                        $(this).addClass("episode-details-open");

                        setTimeout(function () {
                            recalcDetailsHeight();
                        }, 50);

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
                    }


                });
            }, 100);
        }
    });
}

function ToggleExpandSeries(s) {
    var seriesId = $(s).data("serialid");

    var toggleExpand = function () {
        var detailsRow = $(".detailSerieLine[data-serialId='" + seriesId + "']");
        ToggleDetailVisibility(detailsRow, s);
    }

    var seriesDetailsEl = $("#detailSerie-inner" + seriesId);
    if (seriesDetailsEl.text() == '') {

        var seriesSeasons = new Array();

        unique(
            $.grep(currentSeriesTypeViewDataD,
                function (el) {
                    return el.SId == seriesId;
                })
                .map(function (el) {
                    return el.SZ;
                })
        ).forEach(function (seasonNo) {
            seriesSeasons.push({ SeasonId: seasonNo, SeasonName: isNumeric(seasonNo) ? "Season " + seasonNo : seasonNo })
        });

        var sortedSeasons =
            seriesSeasons.sort((a, b) => a.SeasonName.localeCompare(b.SeasonName, undefined, { numeric: true, sensitivity: 'base' }));


        var serialDetails = $.grep(currentSeriesTypeViewDataM, function (el) { return el.Id == seriesId });

        if (sortedSeasons.length == 0 || serialDetails.length == 0) {
            console.warn("invalid data");
            return;
        }

        var serial = serialDetails[0];

        var seriesDetailsHtml =
            "<table class=\"tableWrapper\">" +
            "<tr>" +
            "<td style=\"width:250px; vertical-align: top;\">" +

            (
                serial.Tr == null || serial.Tr == ""
                    ? "<img src=\"Imgs/" + (showingSeries ? "Series" : "Recordings") + "/poster-" + serial.Id + ".jpg\" data-movieId=\"" + serial.Id + "\">"
                    : "<a class='movieTrailerLink' href='https://www.youtube.com/watch?v=" + serial.Tr + "'>" +
                    "<img src=\"Imgs/Series/poster-" + serial.Id + ".jpg\" data-movieId=\"" + serial.Id + "\">" +
                    "</a>"
            ) +

            "</td>" +
            "<td style=\"vertical-align: top;\">";

        var firstSeason = true;

        var addSeason = function (seasonObj) {
            var seasonSection =
                "<table class=\"tableWrapper\">" +
                "<tr class=\"seasonLine noselect lineWithDetails\">" +
                "<td class=\"markerCol\">" +
                "<div class=\"markerSymbol sezonExpander " + (firstSeason ? "expanded" : "collapsed") + "\" data-serialId=\"" + seriesId + "\" data-sezon=\"" + seasonObj.SeasonId + "\">" +
                "</div>" +
                "</td>" +
                "<td colspan='6'>" +
                seasonObj.SeasonName +
                "</td>" +
                "</tr>";

            var episodesInSeason = $.grep(currentSeriesTypeViewDataD, function (el) { return el.SId == seriesId && el.SZ == seasonObj.SeasonId; });

            episodesInSeason.forEach(function (episode) {
                seasonSection +=
                    "<tr class=\"episoadeLine\" data-serialId=\"" + seriesId + "\" data-sezon=\"" + seasonObj.SeasonId + "\" data-episodeId=\"" + episode.Id +
                    "\" style=\"" + (firstSeason ? "display: table-row;" : "display: none;") + "\">" +

                    "<td style=\"width: 30px;\">" +
                    "</td>" +
                    "<td>" +
                    episode.FN +
                    "</td>" +
                    "<td class=\"detailCell w25\">" +
                    (episode.Th == 0
                        ? ""
                        : "<img src=\"Images\\thumbnail.png\" class=\"infoSign movieStillDisplay\" style=\"cursor: pointer;\" title=\"Click to expand/collapse the thumbnails section for this file.\nPress CTRLCTRL while clicking to collapse all thumbnails sections in the current season.\" alt=\"^\">"
                    ) +
                    "</td>" +
                    "<td class=\"detailCell w100\">" +
                    "</td>" +
                    "<td class=\"detailCell w80\">" +
                    episode.Q +
                    "</td>" +
                    "<td class=\"detailCell w100\">" +
                    episode.S +
                    "</td>" +
                    "<td class=\"detailCell w100\">" +
                    episode.A +
                    "</td>" +
                    "<td class=\"detailCell w125\">" +
                    episode.Y +
                    "</td>" +
                    "<td class=\"detailCell w123\">" +
                    episode.T +
                    "</td>" +
                    "</tr>";
            });

            seasonSection +=
                "</table>";

            return seasonSection;
        }

        sortedSeasons.forEach(function (seasonObj) {
            seriesDetailsHtml += addSeason(seasonObj);
            firstSeason = false;
        });

        seriesDetailsHtml +=
            "</td>" +
            "</tr>" +
            "</table>";

        seriesDetailsEl.html(seriesDetailsHtml);
        RebindSeriesEvents();

        toggleExpand();
    }
    else
        toggleExpand();
};

function ToggleExpandSezon(s) {
    var detailsRow = $("tr[data-serialId='" + $(s).data("serialid") + "'][data-sezon='" + $(s).data("sezon") + "']");
    ToggleDetailVisibility(detailsRow, s);
};

function ToggleDetailVisibility(detailsRow, s) {
    if ($(detailsRow).css("display") == "none") {
        $(s).removeClass("collapsed");
        $(s).addClass("expanded");
        $(detailsRow).css("display", "table-row");
    }
    else {
        $(s).removeClass("expanded");
        $(s).addClass("collapsed");
        $(detailsRow).css("display", "none");
    }
}