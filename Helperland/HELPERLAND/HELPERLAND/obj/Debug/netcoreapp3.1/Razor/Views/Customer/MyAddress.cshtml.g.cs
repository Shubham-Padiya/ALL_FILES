#pragma checksum "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\MyAddress.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "212cb312961e2439db49ce86a27fa572b879e962"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_MyAddress), @"mvc.1.0.view", @"/Views/Customer/MyAddress.cshtml")]
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
#line 1 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\MyAddress.cshtml"
using HELPERLAND.Models.Data;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"212cb312961e2439db49ce86a27fa572b879e962", @"/Views/Customer/MyAddress.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c009d21b848e76a1720136b5aab385bc2e8ac863", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_MyAddress : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<UserAddress>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/imgs/edit-icon.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("ms-2"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/imgs/delete-icon.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 3 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\MyAddress.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("<!--table start-->\r\n");
#nullable restore
#line 7 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\MyAddress.cshtml"
 if (Model.Count() > 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <table id=\"table_id\" class=\"table table-borderless\">\r\n        <thead>\r\n            <tr>\r\n                <th scope=\"col\">Addresses</th>\r\n                <th scope=\"col\">Action</th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 17 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\MyAddress.cshtml"
             foreach (var address in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        <b>Address: </b>");
#nullable restore
#line 21 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\MyAddress.cshtml"
                                   Write(address.AddressLine1);

#line default
#line hidden
#nullable disable
            WriteLiteral(",");
#nullable restore
#line 21 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\MyAddress.cshtml"
                                                         Write(address.AddressLine2);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 21 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\MyAddress.cshtml"
                                                                               Write(address.City);

#line default
#line hidden
#nullable disable
            WriteLiteral("-");
#nullable restore
#line 21 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\MyAddress.cshtml"
                                                                                             Write(address.State);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 21 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\MyAddress.cshtml"
                                                                                                            Write(address.PostalCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br /><b>Phone number:</b> ");
#nullable restore
#line 21 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\MyAddress.cshtml"
                                                                                                                                                          Write(address.Mobile);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        <div class=\"d-flex justify-content-end\">\r\n                            <a data-id=");
#nullable restore
#line 25 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\MyAddress.cshtml"
                                  Write(address.AddressId);

#line default
#line hidden
#nullable disable
            WriteLiteral(" class=\"editBtn\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "212cb312961e2439db49ce86a27fa572b879e9627786", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</a>\r\n                            <a data-id=");
#nullable restore
#line 26 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\MyAddress.cshtml"
                                  Write(address.AddressId);

#line default
#line hidden
#nullable disable
            WriteLiteral(" class=\"deleteBtn\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "212cb312961e2439db49ce86a27fa572b879e9629138", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</a>\r\n                        </div>\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 30 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\MyAddress.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n");
#nullable restore
#line 33 "D:\tatvasoft\folder\Helperland\HELPERLAND\HELPERLAND\Views\Customer\MyAddress.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<button class=\"btn\" id=\"newAddress\" onclick=\"newAddress()\">Add New Address</button>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<UserAddress>> Html { get; private set; }
    }
}
#pragma warning restore 1591