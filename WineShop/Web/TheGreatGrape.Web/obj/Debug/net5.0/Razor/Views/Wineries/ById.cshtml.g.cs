#pragma checksum "D:\LocalRepository\Repository\WineShop\Web\TheGreatGrape.Web\Views\Wineries\ById.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2d837957b4ab2bc89cf4fc0609a8ffe027835d26"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Wineries_ById), @"mvc.1.0.view", @"/Views/Wineries/ById.cshtml")]
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
#line 1 "D:\LocalRepository\Repository\WineShop\Web\TheGreatGrape.Web\Views\_ViewImports.cshtml"
using TheGreatGrape.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\LocalRepository\Repository\WineShop\Web\TheGreatGrape.Web\Views\_ViewImports.cshtml"
using TheGreatGrape.Web.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2d837957b4ab2bc89cf4fc0609a8ffe027835d26", @"/Views/Wineries/ById.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c1c3e10723675c4f4e9a507039873b2917d6cca5", @"/Views/_ViewImports.cshtml")]
    public class Views_Wineries_ById : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TheGreatGrape.Web.ViewModels.Wineries.WineryViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("nav-link card-title"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Wines", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ById", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\LocalRepository\Repository\WineShop\Web\TheGreatGrape.Web\Views\Wineries\ById.cshtml"
  
    this.ViewData["Title"] = Model.Name;

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"row\">\r\n\r\n    <div class=\"right-column-body full-width\">\r\n\r\n        <div class=\"inner-left-column\">\r\n\r\n            <div class=\"display-4\">");
#nullable restore
#line 11 "D:\LocalRepository\Repository\WineShop\Web\TheGreatGrape.Web\Views\Wineries\ById.cshtml"
                              Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n            <div class=\"product-view-item\">\r\n                <div class=\"tab-cont\">\r\n                    <div id=\"tab-2\" class=\"tab-item\">\r\n                        <div>\r\n");
#nullable restore
#line 16 "D:\LocalRepository\Repository\WineShop\Web\TheGreatGrape.Web\Views\Wineries\ById.cshtml"
                             foreach (var item in Model.WineryImages)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <img");
            BeginWriteAttribute("src", " src=\"", 588, "\"", 608, 1);
#nullable restore
#line 18 "D:\LocalRepository\Repository\WineShop\Web\TheGreatGrape.Web\Views\Wineries\ById.cshtml"
WriteAttributeValue("", 594, item.ImageUrl, 594, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"Alternate Text\" />\r\n");
#nullable restore
#line 19 "D:\LocalRepository\Repository\WineShop\Web\TheGreatGrape.Web\Views\Wineries\ById.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p class=\"font-heading\"> Information:</p>\r\n                            <p class=\"font-body\">");
#nullable restore
#line 21 "D:\LocalRepository\Repository\WineShop\Web\TheGreatGrape.Web\Views\Wineries\ById.cshtml"
                                            Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n                <p>Wines:</p>\r\n                <div class=\"inner-left-column\">\r\n");
#nullable restore
#line 27 "D:\LocalRepository\Repository\WineShop\Web\TheGreatGrape.Web\Views\Wineries\ById.cshtml"
                     foreach (var item in Model.Wines)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"card text-center\" style=\"width: 13rem;\">\r\n                            <img class=\"card-img-top\"");
            BeginWriteAttribute("src", " src=\"", 1184, "\"", 1204, 1);
#nullable restore
#line 30 "D:\LocalRepository\Repository\WineShop\Web\TheGreatGrape.Web\Views\Wineries\ById.cshtml"
WriteAttributeValue("", 1190, item.ImageUrl, 1190, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"Card image cap\">\r\n                            <div class=\"card-body\">\r\n                                <h5 class=\"card-title\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2d837957b4ab2bc89cf4fc0609a8ffe027835d267720", async() => {
#nullable restore
#line 32 "D:\LocalRepository\Repository\WineShop\Web\TheGreatGrape.Web\Views\Wineries\ById.cshtml"
                                                                                                                                                              Write(item.Name);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 32 "D:\LocalRepository\Repository\WineShop\Web\TheGreatGrape.Web\Views\Wineries\ById.cshtml"
                                                                                                                                             WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</h5>\r\n                                <p class=\"card-text\">");
#nullable restore
#line 33 "D:\LocalRepository\Repository\WineShop\Web\TheGreatGrape.Web\Views\Wineries\ById.cshtml"
                                                Write(item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                <p class=\"card-text\">$");
#nullable restore
#line 34 "D:\LocalRepository\Repository\WineShop\Web\TheGreatGrape.Web\Views\Wineries\ById.cshtml"
                                                 Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                <div");
            BeginWriteAttribute("class", " class=\"", 1649, "\"", 1657, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    <a href=\"#\" type=\"submit\" class=\"btn btn-primary\">Add to cart</a>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n");
#nullable restore
#line 40 "D:\LocalRepository\Repository\WineShop\Web\TheGreatGrape.Web\Views\Wineries\ById.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TheGreatGrape.Web.ViewModels.Wineries.WineryViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
