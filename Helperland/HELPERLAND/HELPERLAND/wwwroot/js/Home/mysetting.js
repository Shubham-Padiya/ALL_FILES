$(document).ready(function () {

    $("#mydetails").load("/customer/mydetail");
    $("#address").load("/customer/myaddress", function () {
        addressEvent();
    });
});

function addressEvent() {
    $(".editBtn").click(function () {
        $.ajax({
            url: "/customer/editaddress",
            type: "GET",
            data: {
                id: $(this).attr("data-id"),
            },
            success: function (result) {
                $("#customerpopup").html(result);
                $("#customerpopup").modal("show");
            },
            error: function () {
                alert("can't find");
            },
        });
    });

    $(".deleteBtn").click(function () {
        $.ajax({
            url: "/customer/deleteaddress",
            type: "POST",
            data: {
                id: $(this).attr("data-id"),
            },
            success: function (result) {
                $("#address").load("/customer/myaddress", function () {
                    addressEvent();
                });
            },
            error: function () {
                alert("sorry");
            },
        });
    });

   
}

function editAddress() {
    $("#customerpopup").modal("hide");
    $("#address").load("/customer/myaddress", function () {
        addressEvent();
    });
}

function newAddress() {
    var url = "/customer/editaddress";
    $.get(url, function (data) {
        $("#customerpopup").html(data);
        $("#customerpopup").modal("show");
    });
}