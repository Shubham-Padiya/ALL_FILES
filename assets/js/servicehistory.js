$(window).scroll(function () {
    var sticky = $("#header"),
    scroll = $(window).scrollTop()

    if (scroll > 64) sticky.addClass("fixed");
    else sticky.removeClass("fixed"); 
});
$(".pagging > .number").click(function () {
$(".pagging > .number").removeClass("active");
$(".pagging > .number").css("color", "#777777");
$(this).addClass("active");
$(this).css("color", "white");
});