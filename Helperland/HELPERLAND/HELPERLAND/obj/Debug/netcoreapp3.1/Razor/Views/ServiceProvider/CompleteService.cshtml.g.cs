#pragma checksum "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\ServiceProvider\CompleteService.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "965562d7ebb680542b6fe836784d7007fd14c534"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ServiceProvider_CompleteService), @"mvc.1.0.view", @"/Views/ServiceProvider/CompleteService.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"965562d7ebb680542b6fe836784d7007fd14c534", @"/Views/ServiceProvider/CompleteService.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c009d21b848e76a1720136b5aab385bc2e8ac863", @"/Views/_ViewImports.cshtml")]
    public class Views_ServiceProvider_CompleteService : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\ServiceProvider\CompleteService.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""modal-dialog modal-dialog-centered"">
    <div class=""modal-content"">
        <div class=""modal-header"">
            <h4 class=""modal-title"">Complete Request</h4>
            <button type=""button""
                    class=""btn-close""
                    data-bs-dismiss=""modal""
                    aria-label=""Close""></button>
        </div>
        <div class=""modal-body"">
            <button id=""completeRequestbtn"" style=""background-color:lightgreen;color:white;width:100%;padding:7px;border:none;border-radius:27px;"">
                Complete
            </button>
            ");
#nullable restore
#line 17 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\ServiceProvider\CompleteService.cshtml"
       Write(Html.Raw(@ViewBag.Alert));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
