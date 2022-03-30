$(document).ready(function () {
  $("#servicehistory").DataTable({
    searching: false,
    info: false,
    dom: '<"float-left"B><"float-right"f>rt<"row"<"col-sm-4"l><"col-sm-4"i><"col-sm-4"p>>',
    sPaginationType: "full_numbers",
    language: {
      paginate: {
        first: '<img src="../../imgs/left_play.png">',
        next: '<img src="../../imgs/keyboard-right-arrow-buttonn.png">',
        previous: '<img src="../../imgs/keyboard-left-arrow-button.png">',
        last: '<img src="../../imgs/right_play.png">',
      },
    },
    responsive: {
      details: {
        display: $.fn.dataTable.Responsive.display.modal({
          header: function (row) {
            var data = row.data();
            return "Details for " + data[0] + " " + data[1];
          },
        }),
        renderer: $.fn.dataTable.Responsive.renderer.tableAll({
          tableClass: "table",
        }),
      },
    },
  });
  $(".buttons-pdf").hide();
  $(".buttons-copy").hide();
    $(".buttons-csv").hide();

    showdetail();


    $(".rateit-average").rateit({ readonly: true });
    $("#onTimeArrival").rateit();
    $("#friendly").rateit();
    $("#quality").rateit();
    $(".rateit-reset").hide();
});

$("#export").on("click", function () {
  $(".buttons-csv").trigger("click");
});

$("#addservicerequest").on("click", function () {
    window.location.href="/bookservice/Bookservice";
});

function showdetail() {
    $("tbody .servicerequestid").click(function () {
        $.ajax({
            url: "/customer/servicedetail",
            type: "GET",
            data: {
                id: $(this).text(),
            },
            success: function (showresult) {
                $("#customerpopup").html(showresult);
                $("#customerpopup").modal("show");
            },
            error: function () {
                alert("We can't find any details");
            },
        });
    });
}

$(".ratesp").click(function () {
    var srid = $(this).attr("data-id");
    $.ajax({
        url: "/customer/ratings",
        type: "GET",
        data: { 'id': srid },
        success: function (result) {
            $(".rateit-reset").hide();
            $(".ratingspimg").attr("src", result.ratingToNavigation.userProfileImage);
            $(".spname").html(result.ratingToNavigation.firstName + " " + result.ratingToNavigation.lastName);
            $(".rateitAverage").rateit("value", result.ratings);
            $(".averageRatingInWord").html(result.ratings);
            $("#onTimeArrival").rateit('value', result.onTimeArrival);
            $("#friendly").rateit("value", result.friendly);
            $("#quality").rateit("value", result.qualityOfService);
            $("#ratingPopup").modal("show");
            $(".submitRating").attr("data-id", result.ratingId);

            $(".submitRating").click(function () {
                var data = {
                    RatingId: $(this).attr("data-id"),
                    OnTimeArrival: $("#onTimeArrival").rateit("value"),
                    Friendly: $("#friendly").rateit("value"),
                    QualityOfService: $("#quality").rateit("value"),
                    Ratings: ($("#onTimeArrival").rateit("value") + $("#friendly").rateit("value") + $("#quality").rateit("value")) / 3,
                    Comments: $(".feedback").val(),
                };

                $.ajax({
                    url: "/customer/ratings",
                    type: "POST",
                    contentType:"application/json",
                    data: JSON.stringify(data),
                    success: function (result) {
                        window.location.href = "/customer/servicehistory";
                    },
                    error: function () {
                        alert("error");
                    },
                });
            });
        },
        error: function () {
            alert("error1");
        },
    });
});