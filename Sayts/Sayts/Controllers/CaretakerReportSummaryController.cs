using OfficeOpenXml;
using Sayts.Models;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Sayts.Controllers
{
    [Authorize]

    public class CaretakerReportSummaryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CaretakerReportSummary
        public ActionResult Index()
        {
            IQueryable<ClusterSiteCTGroup> data = from sites in db.SiteDetails
                                                   join ct in db.Caretakers
                                                      on sites.SiteID equals ct.SiteID
                                                   group sites by sites.ClusterID into clusterGroup
                                                   select new ClusterSiteCTGroup()
                                                   {
                                                       ClusterNo = clusterGroup.Key,
                                                       CTCount = clusterGroup.Count()
                                                   };

            return View(data.ToList());
        }

        public ActionResult Details(int? id)
        {
            var ctDetails = from c in db.Caretakers
                             join s in db.SiteDetails
                               on c.SiteID equals s.SiteID
                             where s.ClusterID == id
                             select c;

            return View(ctDetails);
        }

        public ActionResult ExportToExcel()
        {
            var ctDetails = from s in db.Caretakers
                              select new
                              {
                                  s.Sites.SiteCode,
                                  s.Sites.SiteName,
                                  s.Agency,
                                  s.Address,
                                  s.Active,
                                  s.CTName,
                                  s.EffectivityDate,
                                  s.ContactNo,
                                  s.Remarks
                              };

            ExcelPackage Excel = new ExcelPackage();
            var workSheet = Excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(ctDetails, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=VisayasCaretaker.xlsx");
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