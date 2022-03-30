var totalServieTime = 3;
var perCleaning = 60;
var totalPayment = 60;

$(document).ready(function () {
    totalServiceTime = 3;
    findServiceScheduleElements();
    var url = "/bookservice/yourdetail";
    $.get(url, function (data) {
        $("#urdetail").html(data);
    });

    $("#date").change(function () {
        $("#service-date").html($("#date").val());
        $("#perCleaning").html(perCleaning + ",00&euro;");
        $("#totalpayment").html(totalPayment + ",00&euro;");
    });

    $("#ServiceTime").change(function () {
        $("#service-time").html($("#ServiceTime").val());
    });
});
$(window).scroll(function () {
  var sticky = $("#header"),
    scroll = $(window).scrollTop();

  if (scroll > 64) sticky.addClass("fixed");
  else sticky.removeClass("fixed");
});

function makeActive(link) {
    link.addClass("active-tab");
    var img = link.children().eq(0);
    var imgPath = img.attr("src").split(".");
    img.attr("src", imgPath[0] + "-white.png");
}


function findServiceScheduleElements() {
    $(".bordercolor").click(function () {
        var service = $(this).parent().children().eq(1).text();
        var children;
        switch (service)
        {
            case "Inside cabinets": children = 0;
                break;
            case "Inside fridge": children = 1;
                break;
            case "Inside oven": children = 2;
                break;
            case "Laundry wash & dry": children = 3;
                break;
            case "Interior windows": children = 4;
                break;
        }

        var img = $(this).children().eq(0);
        if ($(this).hasClass("active-service")) {
            $(this).removeClass("active-service");
            var imgPath = img.attr("src").split("-green.png");
            img.attr("src", imgPath[0] + ".png");
            RemoveService(children);
        }
        else {
            $(this).addClass("active-service");
            var imgPath = img.attr("src").split(".");
            img.attr("src", imgPath[0] + "-green.png");
            AddService(service, children);
        }

    });
}

function AddService(service, children) {
    var html = '<span></span>' + service + '<span class="ms-auto">30 Min.</span>';
    $("#extra-services").children().eq(children).html(html);
    totalServieTime = totalServieTime + 0.5;
    totalPayment += 10;
    $("#totalpayment").html(totalPayment + ".00&euro;");
    $("#totaltime").html(totalServieTime);
}

function RemoveService(children) {
    $("#extra-services").children().eq(children).empty();
    totalServieTime -= 0.5;
    totalPayment -= 10;
    $("#totalpayment").html(totalPayment + ".00&euro;");
    $("#totaltime").html(totalServieTime);
}





function SubmitPayment() {
    var id = 0;
    var list = [];
    $('#extra-services > div').each(function (i) {
        id += 1;
        if ($(this).children().length > 0) {
            list.push({ "ServiceExtraId": id });
        }
    });

    var data = {
        ServiceHourlyRate: 20,
        ExtraHours: totalServieTime - 3,
        SubTotal: totalPayment,
        TotalCost: totalPayment,
        PaymentDue: false,
        PaymentDone: true,
        ServiceRequestExtras: list
    }

    $.ajax({
        type: 'POST',
        url: '/bookservice/makepayment',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data),
        success: function (result) {
            $("#payment").html(result);
            ServiceRequestResult(isError, resultMessage, serviceRequestId);
        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');
        }
    })
}

function hideModal() {
    $("#resultModal").hide();
    window.location.href = "/customer/servicerequest";
}

function ServiceRequestResult(isError, Message, ServiceRequestId) {
    if (isError == "True") {
        $("#resultImage").attr("src", "/imgs/rederror.png");
    }
    else {
        $("#resultImage").attr("src", "/imgs/greenright.png");
        $("#serviceRequestId").html("Service Request Id: " + ServiceRequestId);
    }
    $("#resultMessage").html(Message);
    $(window).scrollTop(0);
    $("#resultModal").modal("show");
}


function editAddress() {
    $("#resultModal").modal("hide");
}

function newAddress() {
    var url = "/bookservice/editaddress";
    $.get(url, function (data) {
        $("#resultModal").html(data);
        $("#resultModal").modal("show");
    });
}