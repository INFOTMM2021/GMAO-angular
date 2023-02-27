
using GMAO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace GMAO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RapportsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RapportsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //public async Task<IActionResult> GetReport()
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    //var demTravails = await client.GetFromJsonAsync<List<DemandeTravail>>("http://localhost:5000/api/DemTravail");
        //    var response = await client.GetAsync("http://localhost:5000/api/DemTravail");
        //    var demTravails = JsonSerializer.Deserialize<List<DemandeTravail>>(await response.Content.ReadAsStringAsync());
        //    var report = new ReportDocument();
        //    report.Load("PathToYourReportDefinitionFile");
        //    report.SetDataSource(demTravails);

        //    return new ViewAsPdf("Report", report);
        //}

        //[AllowAnonymous]
        //[Route("DemandeTravailRapport")]
        //[HttpGet]
        ////[ClientCacheWithEtag(60)]  //1 min client side caching
        //public HttpResponseMessage DemandeTravailRapport()
        //{
        //    string reportPath = "~/Rapports/";
        //    string reportFileName = "DemandeT.rpt";
        //    string exportFilename = "DemandeT.pdf";
        //    HttpResponseMessage result = CrystalReport.RenderReport(reportPath, reportFileName, exportFilename);
        //    return result;
        //}

        //public IActionResult GenerateReport()
        //{
        //    var reportData = GetReportData(); // Get the data for the report
        //    var localReport = new LocalReport();
        //    localReport.ReportPath = "Reports/SampleReport.rdlc";
        //    localReport.DataSources.Add("DemandeT", reportData);
        //    byte[] reportBytes = localReport.Render("PDF");
        //    return File(reportBytes, "application/pdf");
        //}
    }
}
