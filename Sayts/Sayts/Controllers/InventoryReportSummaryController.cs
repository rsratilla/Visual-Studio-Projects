using OfficeOpenXml;
using Sayts.Models;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Sayts.Controllers
{
    [Authorize]

    public class InventoryReportSummaryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InventoryReportSummary
        public ActionResult Index(string category)
        {
            switch (category) {
                case "Genset":
                    return RedirectToAction("GensetIndex");
                case "Rectifier":
                    return RedirectToAction("RectifierIndex");
                case "Battery":
                    return RedirectToAction("BatteryIndex");
                case "ACU":
                    return RedirectToAction("ACUIndex");
            }

            return View();
        }


        public ActionResult GensetIndex()
        {
            var gensets = from g in db.GenSets join s in db.Sites on g.SiteID equals s.SiteID select g;
            IQueryable<ClusterSiteInventoryGroup> genset = from g in gensets
                                                           group g by g.Sites.SiteCode + " - " + g.Sites.SiteName
                                                                into siteGroup
                                                           select new ClusterSiteInventoryGroup()
                                                           {
                                                               Sitecode = siteGroup.Key,
                                                               GenSetCount = siteGroup.Count()
                                                           };
            return View(genset.ToList());
        }

        public ActionResult RectifierIndex()
        {
            var rectifiers = from r in db.Rectifiers join s in db.Sites on r.SiteID equals s.SiteID select r;
            IQueryable<ClusterSiteInventoryGroup> rectifier = from r in rectifiers
                                                              group r by r.Sites.SiteCode + " - " + r.Sites.SiteName
                                                                into siteGroup
                                                              select new ClusterSiteInventoryGroup()
                                                              {
                                                                  Sitecode = siteGroup.Key,
                                                                  RectifierCount = siteGroup.Count()
                                                              };

            return View(rectifier.ToList());
        }

        public ActionResult BatteryIndex()
        {
            var batteries = from b in db.Batteries join s in db.Sites on b.SiteID equals s.SiteID select b;
            IQueryable < ClusterSiteInventoryGroup > battery = from b in batteries
                                                               group b by b.Sites.SiteCode + " - " + b.Sites.SiteName
                                                                 into siteGroup
                                                               select new ClusterSiteInventoryGroup()
                                                               {
                                                                  Sitecode = siteGroup.Key,
                                                                  BatteryCount = siteGroup.Count()
                                                               };
            return View(battery.ToList());
        }
        
        public ActionResult ACUIndex()
        {
            var acus = from a in db.ACUs join s in db.Sites on a.SiteID equals s.SiteID select a;
            IQueryable < ClusterSiteInventoryGroup > ACU = from a in acus
                                                           group a by a.Sites.SiteCode + " - " + a.Sites.SiteName
                                                                 into siteGroup
                                                           select new ClusterSiteInventoryGroup()
                                                           {
                                                               Sitecode = siteGroup.Key,
                                                               ACUCount = siteGroup.Count()
                                                           };
            return View(ACU.ToList());
        }

        public ActionResult ExportGensetToExcel()
        {
            var gsDetails = from g in db.GenSets
                            join s in db.Sites on g.SiteID equals s.SiteID
                            select new
                            {
                                g.Sites.SiteCode,
                                g.Sites.SiteName,
                                g.Units.BrandModel,
                                g.Serial,
                                g.UnitStatus.Status,
                                g.Capacity,
                                g.DateDelivered,
                                g.DateInstalled,
                                g.ACLoading,
                                g.Remarks,
                                g.UpdateDate
                            };

            ExcelPackage Excel = new ExcelPackage();

            var workSheet = Excel.Workbook.Worksheets.Add("ACSupport");
            workSheet.Cells[1, 1].LoadFromCollection(gsDetails, true);

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=VisayasACSupport.xlsx");
                Excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }

            return RedirectToAction("Index");
        }

        public ActionResult ExportRectifierToExcel()
        {
            var reDetails = from r in db.Rectifiers
                            join s in db.Sites on r.SiteID equals s.SiteID
                            select new
                            {
                                r.Sites.SiteCode,
                                r.Sites.SiteName,
                                r.Units.BrandModel,
                                r.Serial,
                                r.UnitStatus.Status,
                                r.DCPPType,
                                r.RMActive,
                                r.RMDefective,
                                r.Voltage,
                                r.BatteryCurr,
                                r.RectifierCurr,
                                r.DCLoading,
                                r.TimeBackUp,
                                r.DateDelivered,
                                r.DateInstalled,
                                r.Remarks,
                                r.UpdateDate
                            };

            ExcelPackage Excel = new ExcelPackage();

            var workSheet = Excel.Workbook.Worksheets.Add("DCSupport");
            workSheet.Cells[1, 1].LoadFromCollection(reDetails, true);

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=VisayasDCSupport.xlsx");
                Excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }

            return RedirectToAction("Index");
        }

        public ActionResult ExportBatteryToExcel()
        {
            var baDetails = from b in db.Rectifiers
                            join s in db.Sites on b.SiteID equals s.SiteID
                            select new
                            {
                                b.Sites.SiteCode,
                                b.Sites.SiteName,
                                b.Units.BrandModel,
                                b.UnitStatus.Status,
                                b.DateDelivered,
                                b.DateInstalled,
                                b.Remarks,
                                b.UpdateDate
                            };

            ExcelPackage Excel = new ExcelPackage();

            var workSheet = Excel.Workbook.Worksheets.Add("Batteries");
            workSheet.Cells[1, 1].LoadFromCollection(baDetails, true);

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=VisayasBatteries.xlsx");
                Excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }

            return RedirectToAction("Index");
        }

        public ActionResult ExportACUToExcel()
        {
            var acDetails = from c in db.Rectifiers
                            join s in db.Sites on c.SiteID equals s.SiteID
                            select new
                            {
                                c.Sites.SiteCode,
                                c.Sites.SiteName,
                                c.Units.BrandModel,
                                c.Serial,
                                c.UnitStatus.Status,
                                c.DateDelivered,
                                c.DateInstalled,
                                c.Remarks,
                                c.UpdateDate
                            };

            ExcelPackage Excel = new ExcelPackage();

            var workSheet = Excel.Workbook.Worksheets.Add("Cooling");
            workSheet.Cells[1, 1].LoadFromCollection(acDetails, true);

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=VisayasCooling.xlsx");
                Excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }

            return RedirectToAction("Index");
        }

        public ActionResult ExportToExcel()
        {
            var gsDetails = from g in db.GenSets
                            join s in db.Sites on g.SiteID equals s.SiteID
                            select new
                            {
                                g.Sites.SiteCode,
                                g.Sites.SiteName,
                                g.Units.BrandModel,
                                g.Serial,
                                g.UnitStatus.Status,
                                g.Capacity,
                                g.DateDelivered,
                                g.DateInstalled,
                                g.ACLoading,
                                g.Remarks,
                                g.UpdateDate
                            };

            var reDetails = from r in db.Rectifiers
                            join s in db.Sites on r.SiteID equals s.SiteID
                            select new
                            {
                                r.Sites.SiteCode,
                                r.Sites.SiteName,
                                r.Units.BrandModel,
                                r.Serial,
                                r.UnitStatus.Status,
                                r.DCPPType,
                                r.RMActive,
                                r.RMDefective,
                                r.Voltage,
                                r.BatteryCurr,
                                r.RectifierCurr,
                                r.DCLoading,
                                r.TimeBackUp,
                                r.DateDelivered,
                                r.DateInstalled,
                                r.Remarks,
                                r.UpdateDate
                            };

            var baDetails = from b in db.Batteries
                            join s in db.Sites on b.SiteID equals s.SiteID
                            select new
                            {
                                b.Sites.SiteCode,
                                b.Sites.SiteName,
                                b.Units.BrandModel,
                                b.UnitStatus.Status,
                                b.DateDelivered,
                                b.DateInstalled,
                                b.Remarks,
                                b.UpdateDate
                            };

            var acDetails = from c in db.ACUs
                            join s in db.Sites on c.SiteID equals s.SiteID
                            select new
                            {
                                c.Sites.SiteCode,
                                c.Sites.SiteName,
                                c.Units.BrandModel,
                                c.Serial,
                                c.UnitStatus.Status,
                                c.DateDelivered,
                                c.DateInstalled,
                                c.Remarks,
                                c.UpdateDate
                            };

            var atDetails = from t in db.Others
                            join s in db.Sites on t.SiteID equals s.SiteID
                            select new
                            {
                                t.Sites.SiteCode,
                                t.Sites.SiteName,
                                t.ATSSerial,
                                t.ATSType,
                                t.ATSStatus,
                                t.DateDelivered,
                                t.DateInstalled,
                                t.HasCrankModule,
                                t.HasBatCharger,
                                t.ATSRemarks,
                                t.FTCapacity,
                                t.FTStatus,
                                t.FTRemarks,
                                t.FLStatus,
                                t.FLRemarks,
                                t.TVSSType,
                                t.TVSSerial,
                                t.TVSSStatus,
                                t.TVSSRemarks,
                                t.EFStatus,
                                t.EFRemarks,
                                t.ToType,
                                t.ToHeight,
                                t.ToStatus,
                                t.ToRemarks,
                                t.TLStatus,
                                t.TLRemarks,
                                t.FenceRemarks,
                                t.LAStatus,
                                t.BBStatus,
                                t.TGStatus,
                                t.UpdateDate
                            };

            ExcelPackage Excel = new ExcelPackage();

            var workSheet = Excel.Workbook.Worksheets.Add("ACSupport");
            workSheet.Cells[1, 1].LoadFromCollection(gsDetails, true);

            workSheet = Excel.Workbook.Worksheets.Add("DCSupport");
            workSheet.Cells[1, 1].LoadFromCollection(reDetails, true);

            workSheet = Excel.Workbook.Worksheets.Add("Batteries");
            workSheet.Cells[1, 1].LoadFromCollection(baDetails, true);

            workSheet = Excel.Workbook.Worksheets.Add("Cooling");
            workSheet.Cells[1, 1].LoadFromCollection(acDetails, true);

            workSheet = Excel.Workbook.Worksheets.Add("Others");
            workSheet.Cells[1, 1].LoadFromCollection(atDetails, true);

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=VisayasInventory.xlsx");
                Excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }

            return RedirectToAction("Index");
        }


        public ActionResult GensetDetails(string id)
        {
            var gsDetails = from g in db.GenSets
                             join s in db.Sites on g.SiteID equals s.SiteID
                             where s.SiteCode == id
                             select g;

            return View(gsDetails);
        }

        public ActionResult RectifierDetails(string id)
        {
            var reDetails = from r in db.Rectifiers
                            join s in db.Sites on r.SiteID equals s.SiteID
                            where s.SiteCode == id
                            select r;

            return View(reDetails);
        }

        public ActionResult BatteryDetails(string id)
        {
            var baDetails = from b in db.Batteries
                            join s in db.Sites on b.SiteID equals s.SiteID
                            where s.SiteCode == id
                            select b;

            return View(baDetails);
        }

        public ActionResult ACUDetails(string id)
        {
            var acDetails = from a in db.ACUs
                            join s in db.Sites on a.SiteID equals s.SiteID
                            where s.SiteCode == id
                            select a;

            return View(acDetails);
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