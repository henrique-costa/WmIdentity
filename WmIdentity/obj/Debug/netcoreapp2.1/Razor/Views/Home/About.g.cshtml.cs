#pragma checksum "F:\Projects\WmIdentity\WmIdentity\Views\Home\About.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a1866578ffd04583a7ad3c4f28c99db50513fca8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_About), @"mvc.1.0.view", @"/Views/Home/About.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/About.cshtml", typeof(AspNetCore.Views_Home_About))]
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
#line 1 "F:\Projects\WmIdentity\WmIdentity\Views\_ViewImports.cshtml"
using WmIdentity;

#line default
#line hidden
#line 2 "F:\Projects\WmIdentity\WmIdentity\Views\_ViewImports.cshtml"
using WmIdentity.Models;

#line default
#line hidden
#line 4 "F:\Projects\WmIdentity\WmIdentity\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#line 5 "F:\Projects\WmIdentity\WmIdentity\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Razor.Language;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a1866578ffd04583a7ad3c4f28c99db50513fca8", @"/Views/Home/About.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a0f21f395c4fab162883f780ea4fd3a55e1008cd", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_About : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "F:\Projects\WmIdentity\WmIdentity\Views\Home\About.cshtml"
  
    ViewData["Title"] = _localizer["About"];

#line default
#line hidden
            BeginContext(53, 4, true);
            WriteLiteral("<h1>");
            EndContext();
            BeginContext(58, 19, false);
#line 4 "F:\Projects\WmIdentity\WmIdentity\Views\Home\About.cshtml"
Write(ViewData["Message"]);

#line default
#line hidden
            EndContext();
            BeginContext(77, 38, true);
            WriteLiteral("</h1>\r\n<h2>Authenticated!</h2>\r\n<ul>\r\n");
            EndContext();
#line 7 "F:\Projects\WmIdentity\WmIdentity\Views\Home\About.cshtml"
     foreach (var claim in User.Claims)
    {

#line default
#line hidden
            BeginContext(163, 20, true);
            WriteLiteral("        <li><strong>");
            EndContext();
            BeginContext(184, 10, false);
#line 9 "F:\Projects\WmIdentity\WmIdentity\Views\Home\About.cshtml"
               Write(claim.Type);

#line default
#line hidden
            EndContext();
            BeginContext(194, 11, true);
            WriteLiteral("</strong>: ");
            EndContext();
            BeginContext(206, 11, false);
#line 9 "F:\Projects\WmIdentity\WmIdentity\Views\Home\About.cshtml"
                                     Write(claim.Value);

#line default
#line hidden
            EndContext();
            BeginContext(217, 7, true);
            WriteLiteral("</li>\r\n");
            EndContext();
#line 10 "F:\Projects\WmIdentity\WmIdentity\Views\Home\About.cshtml"
    }

#line default
#line hidden
            BeginContext(231, 5, true);
            WriteLiteral("</ul>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IViewLocalizer _localizer { get; private set; }
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