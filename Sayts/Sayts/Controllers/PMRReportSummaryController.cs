using OfficeOpenXml;
using Sayts.Models;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Sayts.Controllers
{
    [Authorize]

    public class PMRReportSummaryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActInactSites
        public ActionResult Index()
        {
            IQueryable<ClusterSitePMRGroup> data = from sites in db.SiteDetails
                                                   join pmr in db.PMRMasters
                                                      on sites.SiteID equals pmr.SiteID
                                                   group sites by sites.ClusterID into clusterGroup
                                                   select new ClusterSitePMRGroup()
                                                   {
                                                        ClusterNo = clusterGroup.Key,
                                                        PMRCount = clusterGroup.Count()
                                                   };

            return View(data.ToList());
        }

        public ActionResult Details(int? id)
        {
            var pmrDetails = from p in db.PMRMasters
                             join s in db.SiteDetails
                               on p.SiteID equals s.SiteID
                             where s.ClusterID == id
                             select p;            
                      
            return View(pmrDetails);
        }

        public ActionResult ExportToExcel()
        {
            var pmrDetails = from s in db.PMRMasters
                              select new
                              {
                                  s.Sites.SiteCode,
                                  s.Sites.SiteName,
                                  s.Employees.LastName,
                                  s.Employees.GivenName,
                                  s.PMRDate,
                                  s.SiteClassification,
                                  s.PMRType,
                                  s.WithGS,
                                  s.CoordinationNo,
                                  s.Lilo,
                                  s.MainsSimulStatus,
                                  s.MainsSimulRemarks,
                                  s.Remarks
                              };

            ExcelPackage Excel = new ExcelPackage();
            var workSheet = Excel.Workbook.Worksheets.Add("Sites");
            workSheet.Cells[1, 1].LoadFromCollection(pmrDetails, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=VisayasPMRReport.xlsx");
                Excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}