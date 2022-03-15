$(document).ready(function () {
    $("#blockcustomer").DataTable({
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
    $(".blockbtn").click(function () {
        $.ajax({
            url: "/serviceprovider/blockcustomer",
            type: "POST",
            data: {
                customerId: $(this).attr("data-id"),
            },
            success: function (result) {
                window.location.href = "/serviceprovider/blockcustomer";
            },
            error: function () {
                alert("error");
            },
        });
    });

    $(".unblockbtn").click(function () {
        $.ajax({
            url: "/serviceprovider/unblockcustomer",
            type: "POST",
            data: {
                customerId: $(this).attr("data-id"),
            },
            success: function (result) {
                window.location.href = "/serviceprovider/blockcustomer";
            },
            error: function () {
                alert("error");
            },
        });
    });
}
