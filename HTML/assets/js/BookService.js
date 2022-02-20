$(window).scroll(function () {
  var sticky = $("#header"),
    scroll = $(window).scrollTop();

  if (scroll > 64) sticky.addClass("fixed");
  else sticky.removeClass("fixed");
});

$(".nav-tabs .nav-item").click(function () {
  var link = $(this).children().eq(0);
  if (link.hasClass("active-tab")) {
    link.removeClass("active-tab");
    var img = $(this).children().children().eq(0);
    var imgPath = img.attr("src").split("-white");
    img.attr("src", imgPath[0] + ".png");
  } else {
    link.addClass("active-tab");
    var img = $(this).children().children().eq(0);
    var imgPath = img.attr("src").split(".");
    img.attr("src", imgPath[0] + "-white.png");
  }
});

//
$(".paymentbutton").click(function () {
  $("#paymentsummary").toggle();
});
