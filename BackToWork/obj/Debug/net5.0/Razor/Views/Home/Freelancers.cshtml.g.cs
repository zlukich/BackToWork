#pragma checksum "C:\Users\shoot\source\repos\BackToWork\BackToWork\Views\Home\Freelancers.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c9cf2aff5cfb7078d8a26ac6b2b3b4c6dc9090b8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Freelancers), @"mvc.1.0.view", @"/Views/Home/Freelancers.cshtml")]
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
#line 1 "C:\Users\shoot\source\repos\BackToWork\BackToWork\Views\_ViewImports.cshtml"
using BackToWork;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\shoot\source\repos\BackToWork\BackToWork\Views\_ViewImports.cshtml"
using BackToWork.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c9cf2aff5cfb7078d8a26ac6b2b3b4c6dc9090b8", @"/Views/Home/Freelancers.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"15a69f423ef906e460ecc3b90521ecfe7f829188", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Freelancers : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DbResultViewModel>
    {
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
#line 5 "C:\Users\shoot\source\repos\BackToWork\BackToWork\Views\Home\Freelancers.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<table style=\"width:100%\">\r\n");
            WriteLiteral("        <tr>\r\n");
#nullable restore
#line 12 "C:\Users\shoot\source\repos\BackToWork\BackToWork\Views\Home\Freelancers.cshtml"
             foreach (string str in Model.column_names)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <td>");
#nullable restore
#line 14 "C:\Users\shoot\source\repos\BackToWork\BackToWork\Views\Home\Freelancers.cshtml"
           Write(str);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 15 "C:\Users\shoot\source\repos\BackToWork\BackToWork\Views\Home\Freelancers.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tr>\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 19 "C:\Users\shoot\source\repos\BackToWork\BackToWork\Views\Home\Freelancers.cshtml"
      
        foreach (object[] exmpl in Model.exemplars)
        {


#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n");
#nullable restore
#line 24 "C:\Users\shoot\source\repos\BackToWork\BackToWork\Views\Home\Freelancers.cshtml"
                 for (int i = 0; i < exmpl.Length; i++)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "C:\Users\shoot\source\repos\BackToWork\BackToWork\Views\Home\Freelancers.cshtml"
                     if (i == 3)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <td>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c9cf2aff5cfb7078d8a26ac6b2b3b4c6dc9090b84978", async() => {
#nullable restore
#line 28 "C:\Users\shoot\source\repos\BackToWork\BackToWork\Views\Home\Freelancers.cshtml"
                                                                 Write(exmpl[i]);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 642, "~/Home/FreelancerPage/", 642, 22, true);
#nullable restore
#line 28 "C:\Users\shoot\source\repos\BackToWork\BackToWork\Views\Home\Freelancers.cshtml"
AddHtmlAttributeValue("", 664, exmpl[i], 664, 9, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</td>\r\n");
#nullable restore
#line 29 "C:\Users\shoot\source\repos\BackToWork\BackToWork\Views\Home\Freelancers.cshtml"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <td>");
#nullable restore
#line 32 "C:\Users\shoot\source\repos\BackToWork\BackToWork\Views\Home\Freelancers.cshtml"
                       Write(exmpl[i]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 33 "C:\Users\shoot\source\repos\BackToWork\BackToWork\Views\Home\Freelancers.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 33 "C:\Users\shoot\source\repos\BackToWork\BackToWork\Views\Home\Freelancers.cshtml"
                     

                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tr>\r\n");
#nullable restore
#line 37 "C:\Users\shoot\source\repos\BackToWork\BackToWork\Views\Home\Freelancers.cshtml"
        }
    

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DbResultViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
