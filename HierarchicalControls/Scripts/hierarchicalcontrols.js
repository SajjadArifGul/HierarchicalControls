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

            $(element).siblings("ul").show();
        }
        else if ($(element).hasClass("hOpen")) {
            $(element).addClass("hClose");
            $(element).addClass("fa-plus-square");
            $(element).removeClass("hOpen");
            $(element).removeClass("fa-minus-square");
            $(element).siblings("ul").hide();

            $(element).siblings("ul").find("span.hasChilds").addClass("hClose");
            $(element).siblings("ul").find("span.hasChilds").addClass("fa-plus-square");
            $(element).siblings("ul").find("span.hasChilds").removeClass("hOpen");
            $(element).siblings("ul").find("span.hasChilds").removeClass("fa-minus-square");
            $(element).siblings("ul").find("ul").hide();
        }
    });
}