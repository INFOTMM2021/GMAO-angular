#pragma checksum "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f5e3be9f5bc3b587d891f0f043cb1666212f0125"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Employes_Index), @"mvc.1.0.view", @"/Views/Employes/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f5e3be9f5bc3b587d891f0f043cb1666212f0125", @"/Views/Employes/Index.cshtml")]
    public class Views_Employes_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<GMAO.Models.Employe>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Index</h1>\r\n\r\n<p>\r\n    <a asp-action=\"Create\">Create New</a>\r\n</p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 16 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Nom));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 19 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Prenom));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 22 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Categorie));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 25 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Specialite));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 28 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Actif));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 34 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 37 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Nom));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 40 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Prenom));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 43 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Categorie));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 46 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Specialite));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 49 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Actif));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                <a asp-action=\"Edit\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 1354, "\"", 1384, 1);
#nullable restore
#line 52 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Index.cshtml"
WriteAttributeValue("", 1369, item.Matricule, 1369, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Edit</a> |\r\n                <a asp-action=\"Details\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 1437, "\"", 1467, 1);
#nullable restore
#line 53 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Index.cshtml"
WriteAttributeValue("", 1452, item.Matricule, 1452, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Details</a> |\r\n                <a asp-action=\"Delete\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 1522, "\"", 1552, 1);
#nullable restore
#line 54 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Index.cshtml"
WriteAttributeValue("", 1537, item.Matricule, 1537, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Delete</a>\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 57 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<GMAO.Models.Employe>> Html { get; private set; }
    }
}
#pragma warning restore 1591
