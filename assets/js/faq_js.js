
$("#de2").hide();
$(".cust").addClass("active-btn");
$(".prov").addClass("inactive-btn");

$(".cust a").click(function () {
  custClicked();
});
$(".prov a").click(function () {
  provClicked();
});

function custClicked() {
  if (!$(".cust").hasClass("active-btn")) {
    $(".cust").addClass("active-btn");
    $(".cust").removeClass("inactive-btn");
    $(".prov").removeClass("active-btn");
    $(".prov").addClass("inactive-btn");
    $("#de").show();
    $("#de2").hide();
  }
}

function provClicked() {
  if (!$(".prov").hasClass("active-btn")) {
    $(".prov").addClass("active-btn");
    $(".prov").removeClass("inactive-btn");
    $(".cust").removeClass("active-btn");
    $(".cust").addClass("inactive-btn");
    $("#de2").show();
    $("#de").hide();
  }
}





let rotation = 0;

function rotateimg_d2() {
  rotation += 90;
  if (rotation === 180) {
    rotation = 0;
  }
      document.querySelector("#im_d2").style.transform = `rotate(${rotation}deg)`;
      }

  function rotateimg1_d2() {
  rotation += 90;
  if (rotation === 180) {
    rotation = 0;
  }
      document.querySelector("#im1_d2").style.transform = `rotate(${rotation}deg)`;
      }

function rotateimg1() {
  rotation += 90;
  if (rotation === 180) {
    rotation = 0;
  }
      document.querySelector("#img1").style.transform = `rotate(${rotation}deg)`;
      }


function rotateimg2() {
  rotation += 90; 
  if (rotation === 180) {
    rotation = 0;
  }
      document.querySelector("#img2").style.transform = `rotate(${rotation}deg)`;
      }


function rotateimg3() {
  rotation += 90; 
  if (rotation === 180) {
    rotation = 0;
}
    document.querySelector("#img3").style.transform = `rotate(${rotation}deg)`;
}

function rotateimg4() {
    rotation += 90; 
    if (rotation === 180) {
      rotation = 0;
  }
      document.querySelector("#img4").style.transform = `rotate(${rotation}deg)`;
}

function rotateimg5() {
    rotation += 90; 
    if (rotation === 180) {
      rotation = 0;
  }
      document.querySelector("#img5").style.transform = `rotate(${rotation}deg)`;
}

function rotateimg6() {
    rotation += 90; 
    if (rotation === 180) {
      rotation = 0;
  }
      document.querySelector("#img6").style.transform = `rotate(${rotation}deg)`;
}

function rotateimg7() {
    rotation += 90; 
    if (rotation === 180) {
      rotation = 0;
  }
      document.querySelector("#img7").style.transform = `rotate(${rotation}deg)`;
}

function rotateimg8() {
    rotation += 90; 
    if (rotation === 180) {
      rotation = 0;
  }
      document.querySelector("#img8").style.transform = `rotate(${rotation}deg)`;
}

function rotateimg9() {
    rotation += 90; 
    if (rotation === 180) {
      rotation = 0;
  }
      document.querySelector("#img9").style.transform = `rotate(${rotation}deg)`;
}

$(window).scroll(function () {
  var sticky = $("#header"),
    scroll = $(window).scrollTop();

  if (scroll > 64) sticky.addClass("fixed");
  else sticky.removeClass("fixed");
});