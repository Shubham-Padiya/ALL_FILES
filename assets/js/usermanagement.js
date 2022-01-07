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
$(document).ready(function() {
  $('#usermanagementtable').DataTable( {
    searching:false,
      responsive: {
          details: {
              display: $.fn.dataTable.Responsive.display.modal( {
                  header: function ( row ) {
                      var data = row.data();
                      return 'Details for '+data[0]+' '+data[1];
                  }
              } ),
              renderer: $.fn.dataTable.Responsive.renderer.tableAll( {
                  tableClass: 'table'
              } )
          }
      }
  } );
} );