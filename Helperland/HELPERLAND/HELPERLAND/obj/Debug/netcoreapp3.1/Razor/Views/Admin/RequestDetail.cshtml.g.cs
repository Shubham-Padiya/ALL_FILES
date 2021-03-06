#pragma checksum "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Admin\RequestDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b4b107b317ec05ca83f535936a17e49943c4fba8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_RequestDetail), @"mvc.1.0.view", @"/Views/Admin/RequestDetail.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\_ViewImports.cshtml"
using HELPERLAND;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\_ViewImports.cshtml"
using HELPERLAND.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Admin\RequestDetail.cshtml"
using HELPERLAND.Models.Data;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b4b107b317ec05ca83f535936a17e49943c4fba8", @"/Views/Admin/RequestDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c009d21b848e76a1720136b5aab385bc2e8ac863", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_RequestDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ServiceRequest>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Admin\RequestDetail.cshtml"
  
    Layout = null;
    string comment;
    string img;


    if (Model.HasPets)
    {
        comment = "I have pets at home,";
        img = "/imgs/included.png";
    }
    else
    {
        comment = "I don't have pets at home.";
        img = "/imgs/not-included.png";
    }


    string extra_item = "";
    foreach (var extra in Model.ServiceRequestExtras)
    {
        switch (extra.ServiceExtraId)
        {
            case 1:
                extra_item += "Inside cabinet , ";
                break;
            case 2:
                extra_item += "Inside fridge , ";
                break;
            case 3:
                extra_item += "Inside oven , ";
                break;
            case 4:
                extra_item += "Laundry wash & dry , ";
                break;
            case 5:
                extra_item += "Interior windows";
                break;
        }
    }

    var startTime = Model.ServiceStartDate;
    var endTime = startTime.AddHours(Model.ServiceHours);


#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""modal-dialog modal-dialog-centered"">
    <div class=""modal-content"">
        <div class=""modal-header"">
            <h5 style=""color:#4f4f4f"">Service Details</h5>
            <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal""
                    aria-label=""Close""></button>
        </div>
        <div class=""modal-body nestedpopup px-3"">
            <div class=""d-flex"">
                <div style=""width:100%"" class=""me-3"">
                    <h4>");
#nullable restore
#line 60 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Admin\RequestDetail.cshtml"
                   Write(Model.ServiceStartDate.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 60 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Admin\RequestDetail.cshtml"
                                                                  Write(startTime.ToString("hh:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("-");
#nullable restore
#line 60 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Admin\RequestDetail.cshtml"
                                                                                               Write(endTime.ToString("hh:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4><br />\r\n                    <span><b>Duration: </b>");
#nullable restore
#line 61 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Admin\RequestDetail.cshtml"
                                      Write(Model.ServiceHours);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Hrs.</span><br />\r\n                    <hr class=\"divider-line\" />\r\n                    <span><b>Service Id: </b>");
#nullable restore
#line 63 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Admin\RequestDetail.cshtml"
                                        Write(Model.ServiceRequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n                    <span><b>Extras: </b>");
#nullable restore
#line 64 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Admin\RequestDetail.cshtml"
                                    Write(extra_item);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n                    <span><b>Total Payment: </b><span class=\"subtotal\">");
#nullable restore
#line 65 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Admin\RequestDetail.cshtml"
                                                                  Write(Model.SubTotal);

#line default
#line hidden
#nullable disable
            WriteLiteral(" &euro;</span></span><br />\r\n                    <hr class=\"divider-line\" />\r\n                    <span><b>Customer Name: </b>");
#nullable restore
#line 67 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Admin\RequestDetail.cshtml"
                                           Write(Model.User.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 67 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Admin\RequestDetail.cshtml"
                                                                 Write(Model.User.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n                    <span><b>Service Address: </b>");
#nullable restore
#line 68 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Admin\RequestDetail.cshtml"
                                             Write(Model.ServiceRequestAddresses.ElementAt(0).AddressLine1);

#line default
#line hidden
#nullable disable
            WriteLiteral(" , ");
#nullable restore
#line 68 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Admin\RequestDetail.cshtml"
                                                                                                        Write(Model.ServiceRequestAddresses.ElementAt(0).AddressLine2);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n                    <span><b>Distance: </b>...</span><br />\r\n                    <hr class=\"divider-line\" />\r\n                    <span><b>Comments</b></span><br />\r\n                    <img");
            BeginWriteAttribute("src", " src=\"", 2635, "\"", 2645, 1);
#nullable restore
#line 72 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Admin\RequestDetail.cshtml"
WriteAttributeValue("", 2641, img, 2641, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" /> ");
#nullable restore
#line 72 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Admin\RequestDetail.cshtml"
                                  Write(comment);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n                    <hr class=\"divider-line\" />\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ServiceRequest> Html { get; private set; }
    }
}
#pragma warning restore 1591
