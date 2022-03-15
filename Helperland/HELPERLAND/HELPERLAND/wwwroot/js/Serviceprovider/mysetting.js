$(document).ready(function () {
    $("#mydetails").load("/serviceprovider/mydetails", function () {
        myDetailsEvents();
    });
})

function myDetailsEvents() {
    $(".avtar-list").each(function () {
        if ($(this).attr("src") == $("#mainProfileImg").attr("src")) {
            $(this).addClass("active-avtar");
        }
    });

    $(".avtar-list").click(function () {
        $(".avtar-list").removeClass("active-avtar");
        $(this).addClass("active-avtar");
        $("#mainProfileImg").attr("src", $(this).attr("src"));
        $("#mainProfileImgValue").val($(this).attr("src"));
    });
}