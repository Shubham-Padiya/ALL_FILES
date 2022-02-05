$(document).ready(function () {
    $(".lgn").click(function () {
        var url = $(this).data('url');
        openLoginPopUp(url);
    });
});


function openLoginPopUp(url) {
    var url = "/ForUser/Login";
    $.get(url, function (data) {
        $("#PopUp").html(data);
        $("#PopUp").modal("show");
    });
}

function openForgetPasswordPopUp() {
    var url = "/ForUser/ForgetPassword";
    $.get(url, function (data) {
        $("#PopUp").html(data);
        $("#PopUp").modal("show");
    });
}


function PostRequestsend() {
    var url = "/foruser/login";
    var valdata = $("#loginform").serialize();
    $.post(url, valdata, function (data) {
        $("#PopUp").html(data);
        $("#PopUp").modal("show");
    });
}