$(document).ready(function () {
  $("#servicerequest").DataTable({
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

                $(".nestedpopup .reschedule").click(function () {
                    loadSchedulePopUp($(this).attr("data-id"))
                });
                $(".nestedpopup .cancle").click(function () {
                    loadCanclePopUp($(this).attr("data-id"))
                });
            },
            error: function () {
                alert("We can't find any details");
            },
        });
    });
}

$("td .reschedule").click(function () {
    loadSchedulePopUp($(this).attr("data-id"))
});
$("td .cancle").click(function () {
    loadCanclePopUp($(this).attr("data-id"))
});


function loadSchedulePopUp(servicerequestid) {
    $.ajax({
        url: "/customer/reschedule",
        type: "GET",
        data: {
            id: servicerequestid,
        },
        success: function (showpopup) {
            $("#customerpopup").html(showpopup);
            $("#customerpopup").modal("show");
        },
        error: function () {
            alert("can't get id");
        },
    });
}

function loadCanclePopUp(servicerequestid) {
    $.ajax({
        url: "/customer/cancel",
        type: "GET",
        data: {
            id: servicerequestid,
        },
        success: function (showpopup) {
            $("#customerpopup").html(showpopup);
            $("#customerpopup").modal("show");

            $("#cancelRequestBtn").click(function () {
                requestsend(servicerequestid);
            });
        },
        error: function () {
            alert("can't get id");
        },
    });
}

function requestsend(servicerequestid) {
    $.ajax({
        url: "/customer/cancel",
        type: "POST",
        data: {
            id: servicerequestid,
            comment: $(".cancel-modal-dialog > textarea").val(),
        },
        success: function (result) {
            $("#customerpopup").html(result);
            $("#customerpopup").modal("show");
        },
        error: function () {
            alert("sorry");
        },
    });
}