$(window).scroll(function () {
  var sticky = $("#header"),
    scroll = $(window).scrollTop();

  if (scroll > 64) {
  sticky.addClass("fixed");
    $(".navbar-brand img").height(54).width(73);
    $(".navbar").css("background", "rgba(82, 82, 82, 0.8)");
    $(".bl").css("background","#26626e");
  ;}
  else {
    sticky.removeClass("fixed");
    $(".navbar-brand img").height(120).width(156);
    $(".navbar").css("background", "transparent");
    $(".bl").css("background","transparent");
  }
});