

function RenderCollections(){
    CloseSideNav();

    var sectionHtml =
        "<table id=\"seriesHeaderTable\" class=\"tableWrapper\">" +
        "<tr class=\"headerRow\">" +
        "<td style=\"width: 30px;\">" +
        "<div class='c-settings' title='Options'></div>" +
        "</td>" +
        "<td>" +
        "Collection name</br>/ Element title" +
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
        "No. of elements / Theme" +
        "</td>" +
        "</tr>" +
        "</table>" +

        "<div class=\"detailsTableWrapper\">" +
            "<table id=\"seriesMainTable\" class=\"tableWrapper\">";

    var colIndex = 0;

    collectionsData.forEach(function (col) {
        colIndex++;
        var alternateRowClass = colIndex % 2 == 0 ? " alternateRow" : "";

        var tooltip = col.N != "" ? col.N + "\n" : "";

        sectionHtml +=
            "<tr class=\"seriesLine noselect lineWithDetails" + alternateRowClass + "\">" +
            "<td class=\"markerCol\">" +
            "<div class=\"markerSymbol serialExpander collapsed\" data-colId=\"" + col.Id + "\" data-viewType=\"" + col.T + "\" alt=\">\"></div>" +
            "</td>" +
            "<td>" +
            col.FN +
            "</td>" +
            "<td class=\"detailCell w25\">" +
            //"<a href=\"" + link + "\" target=\"_blank\" title=\"" + tooltip + "\">" +
            //"<img src=\"Images\\info.png\" class=\"infoSign\" alt=\"i\">" +
            //"</a>" +
            "</td>" +
            "<td class=\"detailCell w100\">" +
            (col.R != ""
                ? "<div class='recommended' style='float: unset; margin: 0px;'>" + col.R + "</div>"
                : "<div class='recommended' style='float: unset; margin: 0px;' title='Recomandare necunoscuta'>?</div>") +
            "</td>" +
            "<td class=\"detailCell w80\">" +
            col.Q +
            "</td>" +
            "<td class=\"detailCell w100\">" +
            col.S + " GB" +
            "</td>" +
            "<td class=\"detailCell w100\">" +
            col.A +
            "</td>" +
            "<td class=\"detailCell w125\">" +
            col.Y +
            "</td>" +
            "<td class=\"detailCell w125\">" +
            col.Ec +
            "</td>" +
            "</tr>" +

            "<tr class=\"detailSerieLine " + alternateRowClass + "\" data-colId=\"" + col.Id + "\" style=\"display: none;\">" +
            "<td style=\"width: 30px;\">" +
            "</td>" +
            "<td id=\"detailSerie-inner" + col.Id + "\" colspan=\"8\">" +
            "</td>" +
            "</tr>";
    });


    sectionHtml +=
        "</table>" +
        "</div>";


    $(".about-message-img").css("display", "none");
    $("#sections-wrapper").html(sectionHtml);

    setTimeout(function () {
        RebindCollectionsEvents();

        var h = window.innerHeight - $(".master-toolbar").outerHeight() - $("footer").height() - $("#seriesHeaderTable").height();
        $(".detailsTableWrapper").height(h);

        $(".detailsTableWrapper").slimScroll({
            height: h
        });

        $.contextMenu({
            selector: '.c-settings',
            trigger: 'left',
            build: function($trigger, e) {
                // this callback is executed every time the menu is to be shown
                // its results are destroyed every time the menu is hidden
                // e is the original contextmenu event, containing e.pageX and e.pageY (amongst other data)
                return {
                    callback: function(key, options) {
                        if (localStorage.getItem(key) == null) // ||
                            localStorage.setItem(key, "true");
                        else
                            localStorage.removeItem(key);

                        if ($(".expanded").length > 0) {
                            RenderCollections();
                            
                            setTimeout(function() {
                                $(".detailsTableWrapper").mouseover();
                            }, 100);
                            
                        }                            

                        $('[id^="detailSerie-inner"]').empty();
                    },
                    items: {
                        "MoviesAsSeries": {name: "Show movie-type Collections as series", icon: GetCurrentCfg3("MoviesAsSeries") },
                        //"sep1": "---------",
                    }
                };
            }
        });
    }, 100);
}

function RebindCollectionsEvents() {
    $(".serialExpander").off("click").on("click", function (evt) {
        evt.stopPropagation();
        ToggleExpandCollections($(this));
    });

    $(".seriesLine.lineWithDetails").off("click").on("click", function (e) {
        ToggleExpandCollections($(this).find(".markerSymbol"));
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
                    "<tr id=\"th-" +episodeId + "\" class=\"thRow\" style=\"display: table-row;\">" + //data-serialId=\"" + serial.Id + "\" data-sezon=\"" + episod.SZ + "\"
                        "<td colspan=\"8\">" +
                            "<table class=\"thumbnails-wrapper\">" +
                                "<tr>" +
                                    "<td class=\"thumbnailStillCell\">" +
                                        "<img src=\"Imgs\\Collections\\Thumbnails\\thumb-" + episodeId + "-0.jpg\" alt=\"?\">" +
                                    "</td>" +
                                    "<td class=\"thumbnailStillCell\">" +
                                        "<img src=\"Imgs\\Collections\\Thumbnails\\thumb-" + episodeId + "-1.jpg\" alt=\"?\">" +
                                    "</td>" +
                                    "<td class=\"thumbnailStillCell\">" +
                                        "<img src=\"Imgs\\Collections\\Thumbnails\\thumb-" + episodeId + "-2.jpg\" alt=\"?\">" +
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

function ToggleExpandCollections(s) {
    var colId = $(s).data("colid");

    var toggleExpand = function () {
        var detailsRow = $(".detailSerieLine[data-colId='" + colId + "']");
        ToggleDetailVisibility(detailsRow, s);
    }

    var collectionDetailsEl = $("#detailSerie-inner" + colId);
    if (collectionDetailsEl.text() == "") {

        var collectionDetails = $.grep(collectionsElements, function (el) { return el.CId == colId });

        if ($(s).data("viewtype") == 1 || localStorage.getItem("MoviesAsSeries") != null) //series-type OR movies-type configured to run as series-type
        {
            var collectionDetailsS =
                "<table id='x1' class=\"tableWrapper\">" +
                    "<tr>" +
                        "<td style=\"width:250px; vertical-align: top;\">";

            /*
            this happends before "<table id='x1' class=\"tableWrapper\">" +
            checkImage(
                "Imgs/Collections/poster-" + colId + ".jpg",
                function(id) {
                    debugger;
                    collectionDetailsS += "<img src=\"Imgs/Collections/poster-" + id + ".jpg\" data-movieId=\"" + id + "\">";
                },
                function(id) {
                    var collectionData = $.grep(collectionsData, function (el) { return el.Id == id });
                    debugger;
                    collectionDetailsS += "<div class='movieTypeAsSeriesTypePoster'>" +
                                "<div class='collectionName'>" +
                                    collectionData[0].FN +
                                "</div>" +
                            "</div>";
                },
                colId);
            */

            /*
            if ($(s).data("viewtype") == 1)
            {
                collectionDetailsS +=
                            "<img src=\"Imgs/Collections/poster-" + colId + ".jpg\" data-movieId=\"" + colId + "\">";
            }
            else
            {
                var collectionData = $.grep(collectionsData, function (el) { return el.Id == colId });

                collectionDetailsS +=
                            "<div class='movieTypeAsSeriesTypePoster'>" +
                                "<div class='collectionName'>" +
                                    collectionData[0].FN +
                                "</div>" +
                            "</div>";
            }
            */

           collectionDetailsS +=
                            "<img src=\"Imgs/Collections/poster-" + colId + ".jpg\" data-movieId=\"" + colId + "\">";            

            collectionDetailsS +=
                        "</td>" +
                        "<td style=\"vertical-align: top;\">" +
                            "<table id='x2' class=\"tableWrapper\">";


            collectionDetails.forEach(function (episode) {
                collectionDetailsS +=
                    "<tr class=\"episoadeLine\" data-colId=\"" + colId + "\" data-episodeId=\"" + episode.Id + "\">" +
                        "<td>" +
                            episode.FN +
                        "</td>" +
                        "<td class=\"detailCell w25\">" +
                            (episode.Th == 0
                                ? ""
                                : "<img src=\"Images\\thumbnail.png\" class=\"infoSign movieStillDisplay\" style=\"cursor: pointer;\" title=\"Click to expand/collapse the thumbnails section for this file.\nPress CTRL while clicking to expand/collapse all sections in the current season.\" alt=\"^\">"
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

            collectionDetailsS +=
                "</table>" +
                "</td>" +
                "</tr>" +
                "</table>";

            $(collectionDetailsEl).html(collectionDetailsS);

            //to bind the thumbnail img click events
            setTimeout(function () {
                RebindCollectionsEvents();
            }, 100);
        }
        else
        {
            BuildMoviesSection(collectionDetails, collectionDetailsEl); //no sort
        }

        toggleExpand();
    }
    else
        toggleExpand();
};