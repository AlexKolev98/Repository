#pragma checksum "D:\LocalRepository\WineShop\Web\TheGreatGrape.Web\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9fa11a26d9ec294e0c1de7fd590ad4f30e58a44a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "D:\LocalRepository\WineShop\Web\TheGreatGrape.Web\Views\_ViewImports.cshtml"
using TheGreatGrape.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\LocalRepository\WineShop\Web\TheGreatGrape.Web\Views\_ViewImports.cshtml"
using TheGreatGrape.Web.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\LocalRepository\WineShop\Web\TheGreatGrape.Web\Views\Home\Index.cshtml"
using TheGreatGrape.Common;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9fa11a26d9ec294e0c1de7fd590ad4f30e58a44a", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c1c3e10723675c4f4e9a507039873b2917d6cca5", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TheGreatGrape.Web.ViewModels.Home.IndexViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "D:\LocalRepository\WineShop\Web\TheGreatGrape.Web\Views\Home\Index.cshtml"
  
    this.ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4\">Welcome to ");
#nullable restore
#line 9 "D:\LocalRepository\WineShop\Web\TheGreatGrape.Web\Views\Home\Index.cshtml"
                                Write(GlobalConstants.SystemName);

#line default
#line hidden
#nullable disable
            WriteLiteral("!</h1>\r\n    <p>\r\n        <style>\r\n            ul#menu li {\r\n                display: inline\r\n            }\r\n        </style>\r\n        <ul id=\"menu\">\r\n");
#nullable restore
#line 17 "D:\LocalRepository\WineShop\Web\TheGreatGrape.Web\Views\Home\Index.cshtml"
             foreach (var item in Model.Categories)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li>");
#nullable restore
#line 19 "D:\LocalRepository\WineShop\Web\TheGreatGrape.Web\Views\Home\Index.cshtml"
               Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n");
#nullable restore
#line 20 "D:\LocalRepository\WineShop\Web\TheGreatGrape.Web\Views\Home\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </ul>\r\n    </p>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TheGreatGrape.Web.ViewModels.Home.IndexViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
