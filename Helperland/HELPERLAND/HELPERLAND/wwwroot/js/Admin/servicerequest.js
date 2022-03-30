

$("#open").show();
$("#close").hide();

$("#open").click(function () {
    openNavbar();
});
$("#close").click(function () {
    closeNavbar();
});
function closeNavbar() {
    $(".navigation").animate(
        {
            width: "0px",
        }
    );
    $(".navigation").hide();
    $("#open").show();
    $("#close").hide();
}
function openNavbar() {
    $(".navigation").animate(
        {
            width: "100%",
        }
    );
    $(".navigation").show();
    $("#open").hide();
    $("#close").show();
}

$(document).ready(function () {
    $('#requesttable').DataTable({
        searching: true,
        info: true,
        processing: true,
        responsive: true,

        "serverSide": true,
        ajax: {
            "url": "/Admin/ServiceRequestData",
            "type": "POST",
            "datatype": "json",
        },
        dom: '<"float-left"B><"float-right"f>rt<"row"<"col-sm-4"l><"col-sm-4"i><"col-sm-4"p>>',
        pageLength: 10,
        pagging: "true",
        paggingType: "simple_numbers",
        language: {
            paginate: {
                next: '<img src="/imgs/polygon-1-copy-5.png" style="transform:rotate(180deg);">',
                previous: '<img src="/imgs/polygon-1-copy-5.png">',
            },
        },
        "columnDefs": [
            {
                
                targets: 0,
                name: "ServiceRequestId",
                class: "servicerequestid",
                "render": function (data, type, row, meta) {
                    return row.serviceRequestId;
                    //return "hello";
                }
            },
            {
                targets: 1,
                name: "Servie Start Date",
                "render": function (data, type, row, meta) {
                    return getServiceStartData(row);
                    //return "hello";
                }
            },
            {
                targets: 2,
                name: "Customer Name",
                "render": function (data, type, row, meta) {
                    //return "hello";
                    return getCustomerData(row);
                }
            },
            {
                targets: 3,
                name: "SPName",
                "render": function (data, type, row, meta) {
                    //return "hello";
                    return getSPData(row);
                }
            },
            {
                targets: 4,
                orderable: false,
                "render": function (data, type, row, meta) {
                    //return "hello";
                    return row.totalCost + "&euro;";
                }
            },
            {
                targets: 5,
                orderable: false,
                "render": function (data, type, row, meta) {
                    //return "hello";
                    return row.totalCost + "&euro;";
                }
            },
            {
                targets: 6,
                orderable: false,
                "render": function (data, type, row, meta) {
                    return " ";
                }
            },
            {
                targets: 7,
                orderable: false,
                "render": function (data, type, row, meta) {
                    return getStatusData(row);
                    //return "hello";
                }
            },
            {
                targets: 8,
                orderable: false,
                "render": function () {
                    return '<span class="done">Done</span>';
                }
            },
            {
                targets: 9,
                orderable: false,
                render: function (data, type, row, meta) {
                    //return "hello";
                    return getActionData(row);
                }
            }
        ],
        responsive: {
            details: {
                display: $.fn.dataTable.Responsive.display.modal({
                    header: function (row) {
                        var data = row.data();
                        return 'Details for ' + data[0] + ' ' + data[1];
                    }
                }),
                renderer: $.fn.dataTable.Responsive.renderer.tableAll({
                    tableClass: 'table'
                })
            }
        }
    });

    $("#requesttable_filter").hide();
    $.fn.dataTable.ext.errMode = "none";
    $("#requesttable").on("error.dt", function (e, settings, techNote, message) {
        console.log("An error occurred: ", message);
    });


    oTable = $('#requesttable').DataTable();

    $('#searchbtn').click(function () {
        oTable.search([
            $("#serviceId").val(),
            $("#postalCode").val(),
            $("#email").val(),
            $("#selectCustomer").val(),
            $("#selectSP").val(),
            $("#selectStatus").val(),
            $("#hasIssue").prop("checked"),
            $("#fromDate").val(),
            $("#toDate").val()
        ]);
        oTable.draw();
    });

    $('#clearbtn').click(function () {
        $("input").val("");
        $("#hasIssue").removeProp("checked");
        $("#selectStatus").val("selectStatus");
        $("#searchbtn").trigger("click");
    });

});


function getServiceStartData(row) {
    return (
        '<img src="/imgs/calendar2.png" alt="" /><span>' +
        " " +
        row.serviceDate +
        '</span ><br /><img src="/imgs/layer-14.png" alt="" />' +
        " " +
        row.serviceTime
    );
}

function getCustomerData(row) {
    return (
        '<div class="d-flex align-items-center"><img src="/imgs/layer-719.png"/><span>' +
        row.customerName +
        " <br/>" +
        row.customerAddress +
        "</span></div>"
    );
}

function getSPData(row) {
    if (row.spAvtar == null) {
        if (row.spName != null) {
            if (row.spRating == 0 || row.spRating == null) {
                return " ";
            }
            else {
                return row.spName + '<br>' + '<div class="rateit" data-rateit-mode="font" data-rateit-readonly="true" data-rateit-value=' + (row.spRating).toFixed(2) + '></div><span>' + (row.spRating).toFixed(2) + '</span>' + '</div>';
            }
        }
        else {
            return " ";
        }
    }
    else {
        return '<img class="avtar" src="' + row.spAvtar + '" /> ' + row.spName + '<br />' + '<div class="d-flex">' +
            '<div class="rateit" data-rateit-mode="font" data-rateit-readonly="true" data-rateit-value=' + (row.spRating).toFixed(2) + '></div><span>' + (row.spRating).toFixed(2) + '</span>' + '</div>';
    }
}

function getStatusData(row) {
    var status = "";
    switch (row.status) {
        case 1: status = "Completed";
            break;
        case 2: status = "Cancelled";
            break;
        case 3: status = "Refunded";
            break;
        case 4: status = "Pending";
            break;
        case 5: status = "New";
            break;
    }
    return '<span class="' + status + '">' + status + "</span>";
}

function getActionData(row) {
    var action = "";
    if (row.status == "4" || row.status == "5") {
        action =
            '<li><a class="dropdown-item editLink" onclick="onEditLink(' +
            row.serviceRequestId +
            ')">Edit & Reschedule</a></li> <li><a class="dropdown-item cancelLink" onclick="onCancelLink(' +
            row.serviceRequestId +
            ')" >Cancel SR by Cust</a></li>';
    } else {
        action =
            '<li><a class="dropdown-item editLink disabled">Edit & Reschedule</a></li> <li><a class="dropdown-item cancelLink disabled">Cancel SR by Cust</a></li>';
    }
    return '<div class="dropdown mx-3"> <a id="navbardropdown" role="button" data-bs-toggle="dropdown"aria-expanded="false"><i class="fa fa-ellipsis-v"></i></a><ul class="dropdown-menu" area-labelledby="navbardropdown">' + action + '</ul></div>';
}


function onCancelLink(id) {
    $.ajax({
        url: "/admin/cancelrequest",
        type: "GET",
        success: function (result) {
            $("#adminpopup").html(result);
            $("#adminpopup").modal("show");

            $("#cancelRequestBtn").click(function () {
                requestSend(id);
            });
        },
        error: function () {
            alert("error");
        },
    });
}

function requestSend(servicerequestid) {
    $.ajax({
        url: "/admin/cancelrequest",
        type: "POST",
        data: {
            id: servicerequestid,
            comment: $(".cancel-request textarea").val(),
        },
        success: function (result) {
            $("#adminpopup").html(result);
            $("#adminpopup").modal("show");
        },
        error: function () {
            alert("error");
        },
    });
}


function onEditLink(id) {
    $.ajax({
        url: "/admin/editrequest",
        type: "GET",
        data: {
            id: id,
        },
        success: function (result) {
            $("#adminpopup").html(result);
            $("#adminpopup").modal("show");
        },
        error: function () {
            alert("error");
        },
    });
}

$(document).on("click", ".servicerequestid", function () {
    $.ajax({
        url: "/admin/requestdetail",
        type: "GET",
        data: {
            id: $(this).text(),
        },
        success: function (result) {
            $("#adminpopup").html(result);
            $("#adminpopup").modal("show");
        },
        error: function () {
            alert("error");
        },
    });
});