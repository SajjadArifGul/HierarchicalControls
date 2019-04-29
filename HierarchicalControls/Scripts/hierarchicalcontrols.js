$(".hierarchicalDropDown li span").click(function (event) {
    if ($(this).hasClass("hasChilds")) {
        showHideElements($(this));
    }

    $(this).toggleClass("selected");
    event.stopPropagation();
});

function showHideElements(elements) {
    $.each(elements, function (key, element) {
        if ((!$(element).hasClass("hOpen") && !$(element).hasClass("hClose")) || $(element).hasClass("hClose")) {
            $(element).addClass("hOpen");
            $(element).addClass("fa-minus-square");
            $(element).removeClass("hClose");
            $(element).removeClass("fa-plus-square");

            $(element).parent().siblings("ul").show();
        }
        else if ($(element).hasClass("hOpen")) {
            $(element).addClass("hClose");
            $(element).addClass("fa-plus-square");
            $(element).removeClass("hOpen");
            $(element).removeClass("fa-minus-square");
            $(element).parent().siblings("ul").hide();

            $(element).parent().siblings("ul").find("span.hasChilds").addClass("hClose");
            $(element).parent().siblings("ul").find("span.hasChilds").addClass("fa-plus-square");
            $(element).parent().siblings("ul").find("span.hasChilds").removeClass("hOpen");
            $(element).parent().siblings("ul").find("span.hasChilds").removeClass("fa-minus-square");
            $(element).parent().siblings("ul").find("ul").hide();
        }
    });
}

$(".hierarchicalDropDown .hOption:not(.manual)").click(function (event) {
    var filtersDiv = $(this).parents(".content").children(".applied-filters");

    var dataid = $(this).attr("data-id");

    if (filtersDiv.children(".filters[dataid=" + dataid + "]").length <= 0) {
        var $filterElem = $('<div>', { class: 'filters', dataid: $(this).attr("data-id"), html: $(this).attr("data-text") + "<i class='fas fa-times'></i>", onclick: 'removeThis(this)' });
        filtersDiv.append($filterElem);
    }

    $(this).parents(".dropdown-options").hide();

    addValue(filtersDiv.siblings("input.searchElement"), $(this).attr("data-id"));

    displaySearchElement();
    event.stopPropagation();
});


$(".hierarchicalDropDown .hOption.manual").click(function (event) {
    debugger;
    var filtersDiv = $(this).parents(".content").children(".applied-filters");

    var dataid = $(this).attr("data-id");

    if (filtersDiv.children(".filters[dataid=" + dataid + "]").length <= 0) {
        var $filterElem = $('<div>', { class: 'filters', dataid: $(this).attr("data-id"), html: $(this).attr("data-text") + "<i class='fas fa-times'></i>", onclick: 'removeThis(this)' });
        filtersDiv.append($filterElem);
    }

    $(this).parents(".dropdown-options").hide();

    addValueWithoutSearch(filtersDiv.siblings("input.searchElement"), $(this).attr("data-id"));

    event.stopPropagation();
});

$(".hierarchicalTreeView .hOption").click(function (event) {
    $(this).children(".item-holder").toggleClass("checked");

    var filtersDiv = $(this).parents(".content");
    if ($(this).children(".item-holder").hasClass("checked")) {
        addValue(filtersDiv.siblings("input.searchElement"), $(this).attr("data-id"));
    }
    else {
        removeValue(filtersDiv.siblings("input.searchElement"), $(this).attr("data-id"));
    }

    displaySearchElement();

    event.stopPropagation();
});

$(".hierarchicalAccordions .accordion").click(function () {
    var $card = $(this).children("div.card");

    $card.toggleClass("open");
    $card.children(".data-holder").toggle();
});

$(".hierarchicalAccordions .accordion").find(".accordionList").click(function (event) {
    event.stopPropagation();
});

$(document).ready(function () {
    $(".hexpander").click(function () {
        $(this).parents(".content-container-main").toggleClass("opened");
    });

    $(".vexpender").click(function () {
        $(this).parents(".holder").toggleClass("closed");
    });

    $(".displayList").click(function (event) {
        $(this).siblings(".dropdown-options").toggle();
        event.stopPropagation();
    });

    $(".resetHierarchicalDropDownFilters").click(function (event) {
        $(this).parents(".holder").children(".content").children(".applied-filters").html("");

        displaySearchElement();
        doSearch();
    });

    $(".typeselection li").click(function (event) {
        $(this).toggleClass("active");
        $(this).children("i.far").toggleClass("fa-check");

        if ($(this).hasClass("active")) {
            addValue($(this).parents(".content").siblings("input.searchElement"), $(this).attr("data-id"));
        }
        else {
            removeValue($(this).parents(".content").siblings("input.searchElement"), $(this).attr("data-id"));
        }

        displaySearchElement();
    });

    $("#clearSearchFilters").click(function (event) {
        $("input.searchElement").val("");
        removeSearchElements();

        $(this).parents(".filters-applied").hide();

        doSearch();
    });
});

function removeThis(elem) {
    removeValue($(elem).parents(".applied-filters").siblings("input.searchElement"), $(elem).attr("dataid"));

    $(elem).remove();

    displaySearchElement();
}

function removeSearchElements() {
    $(".dpicker").val("");
    $("input[name=Keywords]").val("");

    $(".applied-filters .filters").remove();
    $(".hierarchicalTreeView .hOption .item-holder.checked").removeClass("checked");
    $(".typeselection li.active").children("i.far").removeClass("fa-check");
    $(".typeselection li.active").removeClass("active");
}

function displaySearchElement() {
    if ($("input.searchElement[value!='']").length > 0) {
        $("#clearSearchFilters").parents(".filters-applied").show();
    }
}

function removeValue(element, value) {
    var existingValue = $(element).val();
    var valArray = [];

    if (existingValue) {
        valArray = existingValue.split(",");
    }

    var pos = jQuery.inArray(value, valArray);
    if (pos > -1) {
        valArray.splice(pos, 1);

        $(element).val(valArray.join());
        doSearch();
    }
}

function addValue(element, value) {
    var existingValue = $(element).val();
    var valArray = [];

    if (existingValue) {
        valArray = existingValue.split(",");
    }

    if (jQuery.inArray(value, valArray) == -1) {
        valArray.push(value);

        $(element).val(valArray.join());
        doSearch();
    }
}

function addValueWithoutSearch(element, value) {
    var existingValue = $(element).val();
    var valArray = [];

    if (existingValue) {
        valArray = existingValue.split(",");
    }

    if (jQuery.inArray(value, valArray) == -1) {
        valArray.push(value);

        $(element).val(valArray.join());
    }
}

var searchTimeout;
function doSearch() {
    var url = $("#searchURL").val();

    var searchValues = new Object();

    $(".searchElement").each(function (index) {
        var searchElementName = $(this).attr('name');
        var searchElementValue = $(this).val();

        if (searchElementValue) {
            searchValues[searchElementName] = searchElementValue;
        }
    });

    console.log(searchValues);

    $("#pageLoader").show();
    clearTimeout(searchTimeout);
    searchTimeout = setTimeout(function () {
        $.ajax({
            type: "POST",
            async: true,
            url: url,
            data: {
                searchValues: searchValues,
            },
            beforeSend: function () { }
        })
        .done(function (response) {
            $("#content-container").html(response);
            $("#pageLoader").hide();
        })
        .fail(function (XMLHttpRequest, textStatus, errorThrown) {
            $("#pageLoader").hide();
            alert(errorThrown.Message);
        })
        .always(function () { });
    }, 100);
}
