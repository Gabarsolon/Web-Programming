#pragma checksum "D:\Forest\Didactic\2019-2020\WP\Exemple\ASP.NET examples\AspCoreMVCEF\AspCoreMVCEF\Views\Main\ViewStudent.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b72e5e0beb5c361903c531769244eedb20be67a6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Main_ViewStudent), @"mvc.1.0.view", @"/Views/Main/ViewStudent.cshtml")]
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
#line 1 "D:\Forest\Didactic\2019-2020\WP\Exemple\ASP.NET examples\AspCoreMVCEF\AspCoreMVCEF\Views\_ViewImports.cshtml"
using AspCoreMVCEF;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Forest\Didactic\2019-2020\WP\Exemple\ASP.NET examples\AspCoreMVCEF\AspCoreMVCEF\Views\_ViewImports.cshtml"
using AspCoreMVCEF.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b72e5e0beb5c361903c531769244eedb20be67a6", @"/Views/Main/ViewStudent.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5947ac5c3a5019df7e61e7116c67ea6f70ca809c", @"/Views/_ViewImports.cshtml")]
    public class Views_Main_ViewStudent : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\Forest\Didactic\2019-2020\WP\Exemple\ASP.NET examples\AspCoreMVCEF\AspCoreMVCEF\Views\Main\ViewStudent.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!DOCTYPE html>\r\n\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b72e5e0beb5c361903c531769244eedb20be67a63615", async() => {
                WriteLiteral("\r\n    <meta name=\"viewport\" content=\"width=device-width\" />\r\n    <title>ViewStudent</title>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b72e5e0beb5c361903c531769244eedb20be67a64680", async() => {
                WriteLiteral("\r\n    <div>\r\n");
#nullable restore
#line 15 "D:\Forest\Didactic\2019-2020\WP\Exemple\ASP.NET examples\AspCoreMVCEF\AspCoreMVCEF\Views\Main\ViewStudent.cshtml"
          
            AspCoreMVCEF.Models.Student stud = (AspCoreMVCEF.Models.Student)
                ViewData["student"];
        

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n        <b>Student Details </b><br />\r\n        ID: ");
#nullable restore
#line 21 "D:\Forest\Didactic\2019-2020\WP\Exemple\ASP.NET examples\AspCoreMVCEF\AspCoreMVCEF\Views\Main\ViewStudent.cshtml"
       Write(stud.Id);

#line default
#line hidden
#nullable disable
                WriteLiteral("  <br />\r\n        Name : ");
#nullable restore
#line 22 "D:\Forest\Didactic\2019-2020\WP\Exemple\ASP.NET examples\AspCoreMVCEF\AspCoreMVCEF\Views\Main\ViewStudent.cshtml"
          Write(stud.Name);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <br />\r\n        Password: ");
#nullable restore
#line 23 "D:\Forest\Didactic\2019-2020\WP\Exemple\ASP.NET examples\AspCoreMVCEF\AspCoreMVCEF\Views\Main\ViewStudent.cshtml"
             Write(stud.Password);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <br />\r\n        Group_id: ");
#nullable restore
#line 24 "D:\Forest\Didactic\2019-2020\WP\Exemple\ASP.NET examples\AspCoreMVCEF\AspCoreMVCEF\Views\Main\ViewStudent.cshtml"
             Write(stud.Group_id);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <br />\r\n    </div>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>\r\n");
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
