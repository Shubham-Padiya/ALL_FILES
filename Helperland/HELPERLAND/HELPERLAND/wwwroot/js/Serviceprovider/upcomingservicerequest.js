$(document).ready(function () {
    $("#upcomingrequest").DataTable({
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

function showdetail() {
    $("tbody .servicerequestid").click(function () {
        $.ajax({
            url: "/serviceprovider/servicedetails",
            type: "GET",
            data: {
                id: $(this).text(),
            },
            success: function (showresult) {
                $("#serviceproviderpopup").html(showresult);
                $("#serviceproviderpopup").modal("show");

                $(".nestedpopup .cancelRequest").click(function () {
                    openCancelDailog($(this).attr("data-id"));
                });
                $(".nestedpopup .completeRequest").click(function () {
                    openCompleteDailog($(this).attr("data-id"));
                });
            },
            error: function () {
                alert("We can't find any details");
            },
        });
    });
}

$("td .cancelRequest").click(function () {
    openCancelDailog($(this).attr("data-id"));
});
$("td .completeRequest").click(function () {
    openCompleteDailog($(this).attr("data-id"));
});


function openCancelDailog(servicerequestid) {
    $.ajax({
        url: "/serviceprovider/cancelrequest",
        type: "GET",
        data: {
            id: servicerequestid,
        },
        success: function (result) {
            $("#serviceproviderpopup").html(result);
            $("#serviceproviderpopup").modal("show");
            $("#cancelRequestbtn").click(function () {
                Requestsend(servicerequestid);
            });
        },
        error: function () {
            alert("Invalid");
        },
    });
}

function Requestsend(servicerequestid) {
    $.ajax({
        url: "/serviceprovider/cancelrequest",
        type: "POST",
        data: {
            id: servicerequestid,
            comment: $("#reason").val(),
        },
        success: function (result) {
            $("#serviceproviderpopup").html(result);
            $("#serviceproviderpopup").modal("show");
        },
        error: function () {
            alert("Invalid");
        },
    });
}


function openCompleteDailog(servicerequestid) {
    $.ajax({
        url: "/serviceprovider/completeservice",
        type: "GET",
        data: {
            id: servicerequestid,
        },
        success: function (result) {
            $("#serviceproviderpopup").html(result);
            $("#serviceproviderpopup").modal("show");
            $("#completeRequestbtn").click(function () {
                completeRequestsend(servicerequestid);
            });
        },
        error: function () {
            alert("Invalid");
        },
    });
}

function completeRequestsend(servicerequestid) {
    $.ajax({
        url: "/serviceprovider/completeservice",
        type: "POST",
        data: {
            id: servicerequestid,
        },
        success: function (result) {
            $("#serviceproviderpopup").html(result);
            $("#serviceproviderpopup").modal("show");
        },
        error: function () {
            alert("Invalid");
        },
    });
}