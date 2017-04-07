using Sayts.Models;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using OfficeOpenXml;

namespace Sayts.Controllers
{
    [Authorize]

    public class ActInactSitesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActInactSites
        public ActionResult Index()
        {
            IQueryable<ClusterSiteGroup> data = from sites in db.SiteDetails
                                                group sites by sites.ClusterID into clusterGroup
                                                   select new ClusterSiteGroup()
                                                   {
                                                       ClusterNo = clusterGroup.Key,
                                                       SiteCount = clusterGroup.Count()
                                                   };

            return View(data.ToList());
        }

        public ActionResult Details(int? id)
        {
            var siteDetails = from s in db.SiteDetails
                             select s;

            if (siteDetails == null)
            {
                return HttpNotFound();
            }

            if (id == null)
            {
                siteDetails = siteDetails.Where(s => s.ClusterID != null);
            }
            else
            {
                siteDetails = siteDetails.Where(s => s.ClusterID == id);
            }

            return View(siteDetails);
        }

        public ActionResult ExportToExcel()
        {
            var siteDetails = from s in db.SiteDetails
                              select new { s.Sites.SiteCode, s.Sites.SiteName, s.Employees.LastName, s.Employees.GivenName,
                                           s.HouseLotNo, s.Baranggays.BarangayName, s.Cities.CityName,
                                           s.Cities.ProvinceName, s.Regions.RegionName, s.Areas.AreaName,
                                           s.Clusters.AORName, s.Clusters.ClusterName, s.Longitude, 
                                           s.Lattitude, s.SiteStatus.SiteStatusName, s.SiteTypes.SiteTypeName,
                                           s.SiteClassifications.SiteClassificationName,
                                           s.AssetOwnershipTypes.AssetOwnership, s.CSMPClassifications.CSMPClass,
                                           s.TransportCategories.TransportCat, s.ServiceCategories.ServiceCat,
                                           s.LocationTypes.LocationTypeDescription, s.Terrains.TerrainName,
                                           s.Accessibilities.AccessibilityName, s.SecurityAndSafetys.SASName,
                                           s.SunID, s.SubBase, s.TXTypes.TXTypeName, s.AccessIssue,
                                           s.TimeOfIssue, s.RiskCategory, s.TravelTime, s.AccessPassTime,
                                           s.MonthlyRevenue, s.ForCSMPPMR,
                                           s.ACMainSources.ACMName, s.Remarks
                                         };

            ExcelPackage Excel = new ExcelPackage();
            var workSheet = Excel.Workbook.Worksheets.Add("Sites");
            workSheet.Cells[1, 1].LoadFromCollection(siteDetails, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=VisayasSiteDB.xlsx");
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