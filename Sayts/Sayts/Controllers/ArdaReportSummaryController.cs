using OfficeOpenXml;
using Sayts.Models;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Sayts.Controllers
{
    [Authorize]

    public class ArdaReportSummaryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CaretakerReportSummary
        public ActionResult Index(string countby)
        {            
            switch (countby)
            {
                case "Category":
                    return RedirectToAction("CategoryIndex");
                case "Item":
                    return RedirectToAction("ItemIndex");
                case "Site":
                    return RedirectToAction("SiteIndex");
                case "Cluster":
                    return RedirectToAction("ClusterIndex");
                case "SiteOwner":
                    return RedirectToAction("SiteOwnerIndex");
                case "Status":
                    return RedirectToAction("StatusIndex");
            }

            return View();
        }

        public ActionResult CategoryIndex()
        {
            var ardaitems = from ar in db.ArdaItems select ar;
            IQueryable < ArdaGroup > ardaitem = from ar in ardaitems group ar by ar.ArdaCategory into ardaGroup
                                                select new ArdaGroup()
                                                {
                                                    Category = (int)ardaGroup.Key,
                                                    GroupBy = ardaGroup.Key.ToString(),
                                                    RecordCount = ardaGroup.Count()
                                                };
            return View(ardaitem.ToList());
        }

        public ActionResult CategoryDetails(int id)
        {
            var arDetails = from ar in db.ArdaItems where (int)ar.ArdaCategory == id select ar;

            return View(arDetails);
        }

        public ActionResult ItemIndex()
        {
            var ardaitems = from ar in db.ArdaItems join u in db.Units on ar.UnitID equals u.UnitID select ar;
            IQueryable<ArdaGroup> ardaitem = from ar in ardaitems
                                             group ar by ar.Units.Category into ardaGroup
                                             select new ArdaGroup()
                                             {
                                                 GroupBy = ardaGroup.Key,
                                                 RecordCount = ardaGroup.Count()
                                             };
            return View(ardaitem.ToList());
        }

        public ActionResult ItemDetails(string id)
        {
            var arDetails = from ar in db.ArdaItems join u in db.Units on ar.UnitID equals u.UnitID
                            where ar.Units.Category == id select ar;

            return View(arDetails);
        }

        public ActionResult SiteIndex()
        {
            var ardaitems = from ar in db.ArdaItems select ar;
            IQueryable<ArdaGroup> ardaitem = from ar in ardaitems
                                             group ar by ar.SiteID + " - " + ar.Sitename into ardaGroup
                                             select new ArdaGroup()
                                             {
                                                 GroupBy = ardaGroup.Key,
                                                 RecordCount = ardaGroup.Count()
                                             };
            return View(ardaitem.ToList());
        }

        public ActionResult SiteDetails(string id)
        {
            var arDetails = from ar in db.ArdaItems where ar.SiteID == id select ar;

            return View(arDetails);
        }

        public ActionResult ClusterIndex()
        {
            var ardaitems = from ar in db.ArdaItems select ar;
            IQueryable<ArdaGroup> ardaitem = from ar in ardaitems
                                             group ar by ar.Cluster into ardaGroup
                                             select new ArdaGroup()
                                             {
                                                 GroupBy = ardaGroup.Key.ToString(),
                                                 RecordCount = ardaGroup.Count()
                                             };
            return View(ardaitem.ToList());
        }

        public ActionResult ClusterDetails(int id)
        {
            var arDetails = from ar in db.ArdaItems where ar.Cluster == id select ar;

            return View(arDetails);
        }


        public ActionResult SiteOwnerIndex()
        {
            var ardaitems = from ar in db.ArdaItems select ar;
            IQueryable<ArdaGroup> ardaitem = from ar in ardaitems
                                             group ar by ar.SiteOwner into ardaGroup
                                             select new ArdaGroup()
                                             {
                                                 GroupBy = ardaGroup.Key.ToString(),
                                                 RecordCount = ardaGroup.Count()
                                             };
            return View(ardaitem.ToList());
        }

        public ActionResult SiteOwnerDetails(string id)
        {
            var arDetails = from ar in db.ArdaItems where ar.SiteOwner == id select ar;

            return View(arDetails);
        }

        public ActionResult StatusIndex()
        {
            var ardaitems = from ar in db.ArdaItems join s in db.ArdaStatus on ar.ArdaStatusID equals s.ArdaStatusID select ar;
            IQueryable<ArdaGroup> ardaitem = from ar in ardaitems
                                             group ar by ar.ArdaStatus.Status into ardaGroup
                                             select new ArdaGroup()
                                             {
                                                 GroupBy = ardaGroup.Key.ToString(),
                                                 RecordCount = ardaGroup.Count()
                                             };
            return View(ardaitem.ToList());
        }

        public ActionResult StatusDetails(string id)
        {
            var arDetails = from ar in db.ArdaItems join s in db.ArdaStatus on ar.ArdaStatusID equals s.ArdaStatusID
                            where ar.ArdaStatus.Status == id select ar;

            return View(arDetails);
        }

        public ActionResult ExportToExcel()
        {
            var arDetails = from a in db.ArdaItems
                            select new
                            {
                                a.ArdaCategory,
                                a.Serial,
                                a.Units.Category,
                                a.Units.BrandModel,
                                a.Quantity,
                                a.SiteID,
                                a.Sitename,
                                a.Cluster,
                                a.Province,
                                a.SiteAddress,
                                a.SiteOwner,
                                a.Contact,
                                a.Reason,
                                a.OriginalLocation,
                                a.PickUpLocation,
                                a.StagingArea,
                                a.Origin,
                                a.RDFControl,
                                a.Approver,
                                a.Department,
                                a.Remarks
                            };

            ExcelPackage Excel = new ExcelPackage();
            var workSheet = Excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(arDetails, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=VisayasArdaItems.xlsx");
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