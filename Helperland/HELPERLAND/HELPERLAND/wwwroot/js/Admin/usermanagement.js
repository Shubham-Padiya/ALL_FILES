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



// 
$(document).ready(function () {
    $('#usermanagementtable').DataTable({
        searching: true,
        info: true,
        processing: true,
        responsive: true,
        
        "serverSide": true,
        "ajax": {
            "url": "/admin/usermanagementdata",
            "type": "POST",
            "datatype":"json",
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
                name: "User Name",
                "render": function (data, type, row, meta) {
                    return row.firstName + " " + row.lastName;
                }
            },
            {
                targets: 1,
                orderable: false
            },
            {
                targets: 2,
                name: "Registration Date",
                "render": function (data, type, row, meta) {
                    return getRegisteredData(row);
                }
            },
            {
                targets: 3,
                orderable: false,
                "render": function (data, type, row, meta) {
                    return getUserTypeData(row);
                }
            },
            {
                targets: 4,
                name: "Phone",
                "render": function (data, type, row, meta) {
                    return row.mobile;
                }
            },
            {
                targets: 5,
                orderable: false,
                "render": function (data, type, row, meta) {
                    return row.zipCode;
                }
            },
            {
                targets: 6,
                orderable: false,
                "render": function (data, type, row, meta) {
                    return getStatusData(row);
                }
            },
            {
                targets: 7,
                orderable: false,
                "render": function (data, type, row, meta) {
                    return getActionData(row);
                }
            },
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
    $("#usermanagementtable_filter").hide();
    $.fn.dataTable.ext.errMode = "none";
    $("#usermanagementtable").on("error.dt", function (e, settings, techNote, message) {
        console.log("An error occurred: ", message);
    });


    oTable = $('#usermanagementtable').DataTable();

    $('#searchbtn').click(function () {
        oTable.search([
            $("#userName").val(),
            $("#userRole").val(),
            $("#phoneNumber").val(),
            $("#postalCode").val(),
            $("#email").val(),
            $("#fromDate").val(),
            $("#toDate").val()
        ]);
        oTable.draw();
    });

    $('#clearbtn').click(function () {
        $("input").val("");
        $("#userRole").val("userType");
        $("#searchbtn").trigger("click");
    });




    $(".buttons-pdf").hide();
    $(".buttons-copy").hide();
    $(".buttons-csv").hide();
});



$("#export").on("click", function () {
    $(".buttons-csv").trigger("click");
});



function getRegisteredData(row) {
    var date = row.createdDate.split("T");
    var array = date[0].split("-");
    return '<img src="/imgs/calendar2.png" />' + ' ' + array[2] + '/' + array[1] + '/' + array[0];
}

function getUserTypeData(row) {
    var userType = "";
    if (row.userTypeId == 1) {
        userType = "Customer";
    }
    else if (row.userTypeId == 2) {
        userType = "Service Provider";
    }
    else {
        userType = "Admin";
    }
    return '<span>' + userType + '</span>';
}

function getStatusData(row) {
    if (row.isActive) {
        return '<span class="active">Active</span>';
    }
    else {
        return '<span class="inactive">Inactive</span>';
    }
}

function getActionData(row) {
    var actionOptions = "";
    if (row.isActive) {
        actionOptions = '<a class="dropdown-item" onclick="deActiveLink(' + row.userId + ')">Deactive</a>';
    }
    else {
        actionOptions = '<a class="dropdown-item" onclick="activeLink(' + row.userId + ')">Active</a>';
    }

    return '<div class="dropdown mx-3"> <a id="navbardropdown" role="button" data-bs-toggle="dropdown"aria-expanded="false"><i class="fa fa-ellipsis-v"></i></a><ul class="dropdown-menu" area-labelledby="navbardropdown"><li><a class="dropdown-item" onclick="deleteUser(' + row.userId + ') ">Delete</a></li>' + actionOptions + '</ul></div>';
}


function deActiveLink(userId) {
    $.ajax({
        url: "/admin/deactive",
        type: "GET",
        data: {
            id: userId,
        },
        success: function (result) {
            window.location.href = "/admin/usermanagement";
        },
        error: function () {
            alert("error");
        },
    });
}

function activeLink(userId) {
    $.ajax({
        url: "/admin/active",
        type: "GET",
        data: {
            id: userId,
        },
        success: function (result) {
            window.location.href = "/admin/usermanagement";
        },
        error: function () {
            alert("error");
        },
    });
}

function deleteUser(userId) {
    $.ajax({
        url: "/admin/delete",
        type: "GET",
        data: {
            id: userId,
        },
        success: function (result) {
            window.location.href = "/admin/usermanagement";
        },
        error: function () {
            alert("error");
        },
    });
}