#pragma checksum "D:\Belgelerim\yazılım\Murat Yücedağ Eğitim\projects\ek projeler\E_Ticaret\E_Ticaret.Web\Views\Shared\_ResultMessage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8733e6b092eea229ee484c6dc599bdb2394edffc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__ResultMessage), @"mvc.1.0.view", @"/Views/Shared/_ResultMessage.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8733e6b092eea229ee484c6dc599bdb2394edffc", @"/Views/Shared/_ResultMessage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"85dd21f60d7cbffd91aabf51cbbb49039897ca39", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__ResultMessage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AlertMessage>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col-md-12\">\r\n        <div");
            BeginWriteAttribute("class", " class=\"", 83, "\"", 119, 3);
            WriteAttributeValue("", 91, "alert", 91, 5, true);
            WriteAttributeValue(" ", 96, "alert-", 97, 7, true);
#nullable restore
#line 5 "D:\Belgelerim\yazılım\Murat Yücedağ Eğitim\projects\ek projeler\E_Ticaret\E_Ticaret.Web\Views\Shared\_ResultMessage.cshtml"
WriteAttributeValue("", 103, Model.AlertType, 103, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n            <h4 class=\"alert-title\">");
#nullable restore
#line 6 "D:\Belgelerim\yazılım\Murat Yücedağ Eğitim\projects\ek projeler\E_Ticaret\E_Ticaret.Web\Views\Shared\_ResultMessage.cshtml"
                               Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n            <p>");
#nullable restore
#line 7 "D:\Belgelerim\yazılım\Murat Yücedağ Eğitim\projects\ek projeler\E_Ticaret\E_Ticaret.Web\Views\Shared\_ResultMessage.cshtml"
          Write(Model.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AlertMessage> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
