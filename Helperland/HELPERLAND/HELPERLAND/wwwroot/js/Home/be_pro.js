$(window).scroll(function () {
  var sticky = $("#header"),
    scroll = $(window).scrollTop();

  if (scroll > 64) {
  sticky.addClass("fixed");
    $(".navbar-brand img").height(54).width(73);
    $(".navbar").css("background", "rgba(82, 82, 82, 0.8)");
    $(".bl").css("background","#29626d");
  ;}
  else {
    sticky.removeClass("fixed");
    $(".navbar-brand img").height(102).width(138);
    $(".navbar").css("background", "transparent");
    $(".bl").css("background","transparent");
  }
});


$(".dropdown-item").click(function () {
    var src = $(this).children().eq(0).attr('src');
    $('#flagImage').attr('src', src);
});

$('document').ready(function () {
    $('#check').change(function () {
        $('#gs').prop("disabled", !this.checked);
    }).change()
});