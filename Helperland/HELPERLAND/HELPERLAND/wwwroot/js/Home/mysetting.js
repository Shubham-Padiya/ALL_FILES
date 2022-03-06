$(document).ready(function () {
    //var url = "/customer/mydetail";
    //$.get(url, function (data) {
    //    $("#mydetailtab").html(data);
    //});
    //var url1 = "/customer/myaddress";
    //$.get(url1, function (data) {
    //    $("#addresstab").html(data);
    //});

    $("#mydetailtab").load("/customer/mydetail");
    $("#addresstab").load("/customer/myaddress");
});