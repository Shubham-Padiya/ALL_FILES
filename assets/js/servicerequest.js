$("#open").show();
$("#close").hide();

$("#open").click(function () {
  openNavbar();
});

$("#close").click(function () {
  closeNavbar();
});
function closeNavbar() {
  $(".navigation").animate({
    width: "0px",
  });
  $(".navigation").hide();
  $("#open").show();
  $("#close").hide();
}
function openNavbar() {
  $(".navigation").animate({
    width: "100%",
  });
  $(".navigation").show();
  $("#open").hide();
  $("#close").show();
}

//
$(document).ready(function () {
  $("#servicerequest").DataTable({
    searching: false,
    dom: '<"float-left"B><"float-right"f>rt<"row"<"col-sm-4"l><"col-sm-4"i><"col-sm-4"p>>',
    language: {
      paginate: {
        next: '<img src="assets/images/polygon-1-copy-5.png" style="transform:rotate(180deg);">',
        previous: '<img src="assets/images/polygon-1-copy-5.png">',
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
});
