#pragma checksum "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "352166ded3274d0331418aa36bc659be477736a2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_ServiceDetail), @"mvc.1.0.view", @"/Views/Customer/ServiceDetail.cshtml")]
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
#line 1 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml"
using HELPERLAND.Models.Data;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"352166ded3274d0331418aa36bc659be477736a2", @"/Views/Customer/ServiceDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c009d21b848e76a1720136b5aab385bc2e8ac863", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_ServiceDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ServiceRequest>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/imgs/reschedule-icon-small.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("me-2"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/imgs/close-icon-small.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml"
  
    Layout = null;
    string comment;
    string img;

    if (Model.HasPets)
    {
        comment = "I have pets at Home";
        img = "/imgs/included.png";
    }
    else
    {
        comment = "I don't have pets at Home";
        img = "/imgs/not-included.png";
    }

    string extra_item="";
    foreach(var extra in Model.ServiceRequestExtras)
    {
        switch (extra.ServiceExtraId)
        {
            case 1:extra_item += "Inside cabinets";
                break;

            case 2:extra_item += "Inside fridge";
                break;
            case 3:extra_item += "Inside oven";
                break;
            case 4:extra_item += "Laundry wash & dry";
                break;
            case 5:extra_item += "Interior windows";
                break;
        }
    }

    var startTime = Model.ServiceStartDate;
    var endTime = startTime.AddHours(Model.ServiceHours);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""modal-dialog modal-dialog-centered nestedpopup sdmodal"">
    <div class=""modal-content"">
        <div class=""modal-header"">
            <h5 class=""modal-title"">Service Detail</h5>
            <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal""
                    aria-label=""Close""></button>
        </div>
        <div class=""modal-body px-3"">
            <h5>");
#nullable restore
#line 51 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml"
           Write(Model.ServiceStartDate.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 51 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml"
                                                          Write(startTime.ToString("hh:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("-");
#nullable restore
#line 51 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml"
                                                                                       Write(endTime.ToString("hh:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n            <span><b>Duration: </b>");
#nullable restore
#line 52 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml"
                              Write(Model.ServiceHours);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Hrs.</span><br />\r\n            <div class=\"d-flex justify-content-center my-2 px-3\">\r\n                <hr class=\"divider-line\" />\r\n            </div>\r\n            <span><b>Service Id: </b>");
#nullable restore
#line 56 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml"
                                Write(Model.ServiceRequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n            <span><b>Extras: </b>");
#nullable restore
#line 57 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml"
                            Write(extra_item);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n            <span><b>Net Amount: </b><span class=\"ms-5\">");
#nullable restore
#line 58 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml"
                                                   Write(Model.SubTotal);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>&euro;</span>\r\n                <hr class=\"divider-line\" />\r\n            <span><b>Service Address: </b>");
#nullable restore
#line 60 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml"
                                     Write(Model.ServiceRequestAddresses.ElementAt(0).AddressLine1);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 60 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml"
                                                                                              Write(Model.ServiceRequestAddresses.ElementAt(0).AddressLine2);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n            <span><b>Billing Address: </b>Same as cleaning address</span><br />\r\n            <span><b>Phone: </b>");
#nullable restore
#line 62 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml"
                           Write(Model.ServiceRequestAddresses.ElementAt(0).Mobile);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n            <span><b>Email: </b>");
#nullable restore
#line 63 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml"
                           Write(Model.ServiceRequestAddresses.ElementAt(0).Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n                <hr class=\"divider-line\" />\r\n            <span><b>Comments</b></span><br />\r\n            <img");
            BeginWriteAttribute("src", " src=\"", 2538, "\"", 2548, 1);
#nullable restore
#line 66 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml"
WriteAttributeValue("", 2544, img, 2544, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />");
#nullable restore
#line 66 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml"
                         Write(comment);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                <hr class=\"divider-line\" />\r\n            <div class=\"d-flex\">\r\n                <a class=\"btn reschedule mx-2\" data-id=");
#nullable restore
#line 69 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml"
                                                  Write(Model.ServiceRequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral(">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "352166ded3274d0331418aa36bc659be477736a210999", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("Reschedule</a>\r\n                <a class=\"btn cancle mx-2\" data-id=");
#nullable restore
#line 70 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\ServiceDetail.cshtml"
                                              Write(Model.ServiceRequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral(">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "352166ded3274d0331418aa36bc659be477736a212460", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("Cancle</a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
