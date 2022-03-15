$("#go-up").hide();
$("#okbtn > a").click(function () {
    $("#okbtn").fadeOut(500);
});
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
$("#btn-back-to-top").hide();
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




$(".dropdown-item").click(function () {
    var src = $(this).children().eq(0).attr('src');
    $('#flagImage').attr('src', src);
});



function openForgetPasswordPopUp() {
    var url = "/ForUser/ForgetPassword";
    $.get(url, function (data) {
        $("#PopUp").html(data);
        $("#PopUp").modal("show");
    });
}



function ResetPassRequestsend() {
    var url = "/foruser/forgetpassword";
    var valdata = $("#forgetPasswordForm").serialize();
    $.post(url, valdata, function (data) {
        $("#PopUp").html(data);
        $("#PopUp").modal("show");
    });
}



var currentlyOpen;
$(document).ready(function () {

    $(".navbar-brand img").height(102).width(138);
    $(".navbar").css("background", "transparent");
    $(".bl").css("background", "transparent");

    loadLoginPopUp(loginPopUp);

    $(".lgn").click(function () {
        $(window).scrollTop(0);
        if (currentlyOpen != "Login") {
            loadLoginPopUp('True');
        }
        else {
            showLoginPopUp();
        }
    });
});

function loadLoginPopUp(openPopUp) {
    var url = "/foruser/login";
    $.get(url, function (data) {
        $("#PopUp").html(data);
        if (openPopUp == 'True') {
            showLoginPopUp();
        }
    });
}

function showLoginPopUp() {
    $("#PopUp").modal("show");
    currentlyOpen = "Login";
}

function PostRequestsend() {
    var url = "/foruser/login";
    var valdata = $("#loginForm").serialize();  
    $.post(url, valdata, function (data) {
        var url = data.split("returnUrl=");
        if (url[1] != null) {
            window.location.href = url[1];
        }
        else {
            $("#PopUp").html(data);
            $("#PopUp").modal("show");
        }
    });
}