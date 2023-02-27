
using System;
using System.IO;
using System.Net;
using System.Net.Http;


namespace GMAO.Models
{
    public static class CrystalReport
    {
        //public static HttpResponseMessage RenderReport(string reportPath, string reportFileName, string exportFilename)
        //{
        //    var rd = new ReportDocument();

        //    //rd.Load(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(reportPath), reportFileName));

        //    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        //    string reportFilePath = Path.Combine(baseDir, reportPath, reportFileName);
        //    rd.Load(reportFilePath);

        //    MemoryStream ms = new MemoryStream();
        //    using (var stream = rd.ExportToStream(ExportFormatType.PortableDocFormat))
        //    {
        //        stream.CopyTo(ms);
        //    }

        //    var result = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new ByteArrayContent(ms.ToArray())
        //    };
        //    result.Content.Headers.ContentDisposition =
        //        new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
        //        {
        //            FileName = exportFilename
        //        };
        //    result.Content.Headers.ContentType =
        //        new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
        //    return result;
        //}
    }
}
