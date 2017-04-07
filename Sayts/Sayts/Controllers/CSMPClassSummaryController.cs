using Sayts.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.IO;

namespace Sayts.Controllers
{
    [Authorize]

    public class CSMPClassSummaryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CSMPClassSummary
        public ActionResult Index()
        {
            string query = "SELECT sd.CSMPClassID AS CSMPClassID, cc.CSMPClass as CSMPDefinition, COUNT(sd.CSMPClassID) AS SiteCount "
                + "FROM SiteDetails as sd "
                + "INNER JOIN CSMPClassifications as cc "
                + "ON sd.CSMPClassID=cc.CSMPClassID "
                + "GROUP BY sd.CSMPClassID, cc.CSMPClass";

            IEnumerable<CSMPSiteGroup> data = db.Database.SqlQuery<CSMPSiteGroup>(query);
  
            return View(data.ToList());
        }

        public ActionResult Details(int? id)
        {
            var siteDetails = from s in db.SiteDetails
                 .Include(s => s.CSMPClassifications)
                 .Include(s => s.Sites)
                 .Include(s => s.Clusters)
                 .OrderBy(s => s.Sites.SiteCode) select s;

            if (siteDetails == null)
            {
                return HttpNotFound();
            }

            if (id == null)
            {
                siteDetails = siteDetails.Where(s => s.CSMPClassID != null);
            }
            else
            {
                siteDetails = siteDetails.Where(s => s.CSMPClassID==id);
            }

            return View(siteDetails);
        }

        public ActionResult ExportToExcel()
        {
            var siteDetails = from s in db.SiteDetails
                            select new
                            {
                                s.Sites.SiteCode,
                                s.Sites.SiteName,
                                s.Clusters.ClusterName,
                                s.CSMPClassifications.CSMPClass
                            };

            ExcelPackage Excel = new ExcelPackage();
            var workSheet = Excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(siteDetails, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=VisayasSitesWithCSMPClassification.xlsx");
                Excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}