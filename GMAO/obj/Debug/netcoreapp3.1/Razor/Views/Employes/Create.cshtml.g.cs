#pragma checksum "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "46d64128cb25ebfa3ba5c4f0b51aa7d7ec3f9cbd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Employes_Create), @"mvc.1.0.view", @"/Views/Employes/Create.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"46d64128cb25ebfa3ba5c4f0b51aa7d7ec3f9cbd", @"/Views/Employes/Create.cshtml")]
    public class Views_Employes_Create : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<GMAO.Models.Employe>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Create.cshtml"
  
    ViewData["Title"] = "Create";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Create</h1>

<h4>Employe</h4>
<hr />
<div class=""row"">
    <div class=""col-md-4"">
        <form asp-action=""Create"">
            <div asp-validation-summary=""ModelOnly"" class=""text-danger""></div>
            <div class=""form-group"">
                <label asp-for=""Nom"" class=""control-label""></label>
                <input asp-for=""Nom"" class=""form-control"" />
                <span asp-validation-for=""Nom"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""Prenom"" class=""control-label""></label>
                <input asp-for=""Prenom"" class=""form-control"" />
                <span asp-validation-for=""Prenom"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""Categorie"" class=""control-label""></label>
                <input asp-for=""Categorie"" class=""form-control"" />
                <span asp-validation-for=""Categorie"" class=""text-danger""></span>
            ");
            WriteLiteral(@"</div>
            <div class=""form-group"">
                <label asp-for=""specialite"" class=""control-label""></label>
                <input asp-for=""specialite"" class=""form-control"" />
                <span asp-validation-for=""specialite"" class=""text-danger""></span>
            </div>
            <div class=""form-group form-check"">
                <label class=""form-check-label"">
                    <input class=""form-check-input"" asp-for=""Actif"" /> ");
#nullable restore
#line 37 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Create.cshtml"
                                                                  Write(Html.DisplayNameFor(model => model.Actif));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                </label>
            </div>
            <div class=""form-group"">
                <input type=""submit"" value=""Create"" class=""btn btn-primary"" />
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-action=""Index"">Back to List</a>
</div>

");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 52 "E:\SMTMMGMAO\TMMGMAO\GMAO\Views\Employes\Create.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<GMAO.Models.Employe> Html { get; private set; }
    }
}
#pragma warning restore 1591
