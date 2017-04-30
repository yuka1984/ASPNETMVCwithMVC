using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using ClassLibrary1;
using Microsoft.Reporting.WebForms;

namespace WebApplication5.Controllers
{
    public class MyController : Controller
    {
        // GET: My
        public ActionResult Index()
        {
            var reportAssembly =
                Assembly.GetAssembly(typeof(Child));
            var localreport = new LocalReport();

            using (var stream = reportAssembly.GetManifestResourceStream("ClassLibrary1.Report1.rdlc"))
            {
                localreport.LoadReportDefinition(stream);
            }


            var children = new List<Child>
            {
                new Child {Id = 1, Nmae = "なまえ1です", Description = "Description1です"},
                new Child {Id = 2, Nmae = "なまえ2です", Description = "Description2です"}
            };
            localreport.DataSources.Add(new ReportDataSource("DataSet2") {Value = children});

            var reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            Warning[] warnings;
            string[] streams;
            var renderedBytes = localreport.Render(reportType, null, out mimeType, out encoding, out fileNameExtension,
                out streams, out warnings);

            return File(renderedBytes, mimeType);
        }
    }
}