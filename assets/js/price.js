let fbtn=document.getElementById("btn-back-to-top");

window.onscroll=function () {
  scrollFunction();
};
function scrollFunction() {
  if(
    document.body.scrollTop > 20 || document.documentElement.scrollTop>20
  ){
  fbtn.style.display = "block";
  }
  else
  {
    fbtn.style.display = "none";
  }
}

// onclick we reach to top
fbtn.addEventListener("click", backToTop);

function backToTop() {
  document.body.scrollTop = 0;
  document.documentElement.scrollTop = 0;
}

$(window).scroll(function () {
    var sticky = $("#header"),
      scroll = $(window).scrollTop();
  
    if (scroll > 64) sticky.addClass("fixed");
    else sticky.removeClass("fixed");
  });