#pragma checksum "D:\Belgelerim\yazılım\Murat Yücedağ Eğitim\projects\ek projeler\E_Ticaret\E_Ticaret.Web\Views\Shop\Search.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "969b674917743ffea20e1b5fcf5bcd6f326b8c2d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shop_Search), @"mvc.1.0.view", @"/Views/Shop/Search.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 2 "D:\Belgelerim\yazılım\Murat Yücedağ Eğitim\projects\ek projeler\E_Ticaret\E_Ticaret.Web\Views\_ViewImports.cshtml"
using E_Ticaret.Entity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Belgelerim\yazılım\Murat Yücedağ Eğitim\projects\ek projeler\E_Ticaret\E_Ticaret.Web\Views\_ViewImports.cshtml"
using E_Ticaret.Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Belgelerim\yazılım\Murat Yücedağ Eğitim\projects\ek projeler\E_Ticaret\E_Ticaret.Web\Views\_ViewImports.cshtml"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Belgelerim\yazılım\Murat Yücedağ Eğitim\projects\ek projeler\E_Ticaret\E_Ticaret.Web\Views\_ViewImports.cshtml"
using E_Ticaret.Web.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Belgelerim\yazılım\Murat Yücedağ Eğitim\projects\ek projeler\E_Ticaret\E_Ticaret.Web\Views\_ViewImports.cshtml"
using System.Collections.Generic;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Belgelerim\yazılım\Murat Yücedağ Eğitim\projects\ek projeler\E_Ticaret\E_Ticaret.Web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Belgelerim\yazılım\Murat Yücedağ Eğitim\projects\ek projeler\E_Ticaret\E_Ticaret.Web\Views\_ViewImports.cshtml"
using E_Ticaret.Web.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"969b674917743ffea20e1b5fcf5bcd6f326b8c2d", @"/Views/Shop/Search.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"85dd21f60d7cbffd91aabf51cbbb49039897ca39", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shop_Search : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ProductListViewModel>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col-md-3\">\r\n        ");
#nullable restore
#line 6 "D:\Belgelerim\yazılım\Murat Yücedağ Eğitim\projects\ek projeler\E_Ticaret\E_Ticaret.Web\Views\Shop\Search.cshtml"
   Write(await Component.InvokeAsync("Categories"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"col-md-9\">\r\n        <div class=\"row\">\r\n");
#nullable restore
#line 10 "D:\Belgelerim\yazılım\Murat Yücedağ Eğitim\projects\ek projeler\E_Ticaret\E_Ticaret.Web\Views\Shop\Search.cshtml"
             foreach (var product in Model.Products)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"col-md-4\">\r\n                    ");
#nullable restore
#line 13 "D:\Belgelerim\yazılım\Murat Yücedağ Eğitim\projects\ek projeler\E_Ticaret\E_Ticaret.Web\Views\Shop\Search.cshtml"
               Write(await Html.PartialAsync("_product", product));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n");
#nullable restore
#line 15 "D:\Belgelerim\yazılım\Murat Yücedağ Eğitim\projects\ek projeler\E_Ticaret\E_Ticaret.Web\Views\Shop\Search.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script src=""https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"" integrity=""sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo"" crossorigin=""anonymous""></script>
    <script src=""https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"" integrity=""sha384- wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6"" crossorigin=""anonymous""></script>
");
            }
            );
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ProductListViewModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
