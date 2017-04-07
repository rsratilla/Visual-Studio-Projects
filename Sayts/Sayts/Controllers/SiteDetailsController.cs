using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;

namespace Sayts.Controllers
{
    [Authorize]

    public class SiteDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SiteDetails
        public ActionResult Index()
        {
            var sid = (int)Session["siteid"];
            var siteDetails = db.SiteDetails
                .Where(s => s.SiteID == sid)
                .Include(s => s.Accessibilities).Include(s => s.ACMainSources).Include(s => s.Areas).Include(s => s.AssetOwnershipTypes).Include(s => s.Baranggays).Include(s => s.Cities).Include(s => s.Clusters).Include(s => s.CSMPClassifications).Include(s => s.Employees).Include(s => s.LocationTypes).Include(s => s.Regions).Include(s => s.SecurityAndSafetys).Include(s => s.ServiceCategories).Include(s => s.SiteClassifications).Include(s => s.Sites).Include(s => s.SiteStatus).Include(s => s.SiteTypes).Include(s => s.Terrains).Include(s => s.TransportCategories).Include(s => s.TXTypes);
            return View(siteDetails.ToList());
        }

        // GET: SiteDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteDetail siteDetail = db.SiteDetails.Find(id);
            if (siteDetail == null)
            {
                return HttpNotFound();
            }
            return View(siteDetail);
        }

        // GET: SiteDetails/Create
        public ActionResult Create()
        {
            var sid = (int)Session["siteid"];
            ViewBag.AccessibilityID = new SelectList(db.Accessibilities, "AccessibilityID", "AccessibilityName");
            ViewBag.ACMID = new SelectList(db.ACMainSources, "ACMID", "ACMName");
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName");
            ViewBag.AssetOwnershipID = new SelectList(db.AssetOwnershipTypes, "AssetOwnershipID", "AssetOwnership");
            ViewBag.BarangayID = new SelectList(db.Baranggays, "BarangayID", "AreaClusterCityBrgy");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "AreaClusterCities");
            ViewBag.ClusterID = new SelectList(db.Clusters, "ClusterID", "AreaClusters");
            ViewBag.CSMPClassID = new SelectList(db.CSMPClassifications, "CSMPClassID", "CSMPClass");
            ViewBag.EmployeeNo = new SelectList(db.Employees, "EmployeeNo", "FullName");
            ViewBag.LocationTypeID = new SelectList(db.LocationTypes, "LocationTypeID", "LocationTypeDescription");
            ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "RegionName");
            ViewBag.SASID = new SelectList(db.SecurityAndSafetys, "SASID", "SASName");
            ViewBag.ServiceCatID = new SelectList(db.ServiceCategories, "ServiceCatID", "ServiceCat");
            ViewBag.SiteClassificationID = new SelectList(db.SiteClassifications, "SiteClassificationID", "SiteClassificationName");
            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName");
            ViewBag.SiteStatusID = new SelectList(db.SiteStatus, "SiteStatusID", "SiteStatusName");
            ViewBag.SiteTypeID = new SelectList(db.SiteTypes, "SiteTypeID", "SiteTypeName");
            ViewBag.TerrainID = new SelectList(db.Terrains, "TerrainID", "TerrainName");
            ViewBag.TransportCatID = new SelectList(db.TransportCategories, "TransportCatID", "TransportCat");
            ViewBag.TXTypeID = new SelectList(db.TXTypes, "TXTypeID", "TXTypeName");

            return View();
        }

        // POST: SiteDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SiteDetailID,SiteID,EmployeeNo,HouseLotNo,AreaID,ClusterID,RegionID,CityID,BarangayID,Longitude,Lattitude,SiteStatusID,SiteTypeID,SiteClassificationID,AssetOwnershipID,CSMPClassID,TransportCatID,ServiceCatID,LocationTypeID,TerrainID,AccessibilityID,SASID,ACMID,SunID,SubBase,TXTypeID,AccessIssue,TimeOfIssue,RiskCategory,TravelTime,AccessPassTime,MonthlyRevenue,ForCSMPPMR,Remarks")] SiteDetail siteDetail)
        {
            if (ModelState.IsValid)
            {
                db.SiteDetails.Add(siteDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var sid = (int)Session["siteid"];
            ViewBag.AccessibilityID = new SelectList(db.Accessibilities, "AccessibilityID", "AccessibilityName", siteDetail.AccessibilityID);
            ViewBag.ACMID = new SelectList(db.ACMainSources, "ACMID", "ACMName", siteDetail.ACMID);
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName", siteDetail.AreaID);
            ViewBag.AssetOwnershipID = new SelectList(db.AssetOwnershipTypes, "AssetOwnershipID", "AssetOwnership", siteDetail.AssetOwnershipID);
            ViewBag.BarangayID = new SelectList(db.Baranggays, "BarangayID", "AreaClusterCityBrgy", siteDetail.BarangayID);
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "AreaClusterCities", siteDetail.CityID);
            ViewBag.ClusterID = new SelectList(db.Clusters, "ClusterID", "AreaClusters", siteDetail.ClusterID);
            ViewBag.CSMPClassID = new SelectList(db.CSMPClassifications, "CSMPClassID", "CSMPClass", siteDetail.CSMPClassID);
            ViewBag.EmployeeNo = new SelectList(db.Employees, "EmployeeNo", "FullName", siteDetail.EmployeeNo);
            ViewBag.LocationTypeID = new SelectList(db.LocationTypes, "LocationTypeID", "LocationTypeDescription", siteDetail.LocationTypeID);
            ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "RegionName", siteDetail.RegionID);
            ViewBag.SASID = new SelectList(db.SecurityAndSafetys, "SASID", "SASName", siteDetail.SASID);
            ViewBag.ServiceCatID = new SelectList(db.ServiceCategories, "ServiceCatID", "ServiceCat", siteDetail.ServiceCatID);
            ViewBag.SiteClassificationID = new SelectList(db.SiteClassifications, "SiteClassificationID", "SiteClassificationName", siteDetail.SiteClassificationID);
            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName", siteDetail.SiteID);
            ViewBag.SiteStatusID = new SelectList(db.SiteStatus, "SiteStatusID", "SiteStatusName", siteDetail.SiteStatusID);
            ViewBag.SiteTypeID = new SelectList(db.SiteTypes, "SiteTypeID", "SiteTypeName", siteDetail.SiteTypeID);
            ViewBag.TerrainID = new SelectList(db.Terrains, "TerrainID", "TerrainName", siteDetail.TerrainID);
            ViewBag.TransportCatID = new SelectList(db.TransportCategories, "TransportCatID", "TransportCat", siteDetail.TransportCatID);
            ViewBag.TXTypeID = new SelectList(db.TXTypes, "TXTypeID", "TXTypeName", siteDetail.TXTypeID);

            return View(siteDetail);
        }

        // GET: SiteDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteDetail siteDetail = db.SiteDetails.Find(id);
            if (siteDetail == null)
            {
                return HttpNotFound();
            }

            var sid = (int)Session["siteid"];
            ViewBag.AccessibilityID = new SelectList(db.Accessibilities, "AccessibilityID", "AccessibilityName", siteDetail.AccessibilityID);
            ViewBag.ACMID = new SelectList(db.ACMainSources, "ACMID", "ACMName", siteDetail.ACMID);
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName", siteDetail.AreaID);
            ViewBag.AssetOwnershipID = new SelectList(db.AssetOwnershipTypes, "AssetOwnershipID", "AssetOwnership", siteDetail.AssetOwnershipID);
            ViewBag.BarangayID = new SelectList(db.Baranggays, "BarangayID", "AreaClusterCityBrgy", siteDetail.BarangayID);
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "AreaClusterCities", siteDetail.CityID);
            ViewBag.ClusterID = new SelectList(db.Clusters, "ClusterID", "AreaClusters", siteDetail.ClusterID);
            ViewBag.CSMPClassID = new SelectList(db.CSMPClassifications, "CSMPClassID", "CSMPClass", siteDetail.CSMPClassID);
            ViewBag.EmployeeNo = new SelectList(db.Employees, "EmployeeNo", "FullName", siteDetail.EmployeeNo);
            ViewBag.LocationTypeID = new SelectList(db.LocationTypes, "LocationTypeID", "LocationTypeDescription", siteDetail.LocationTypeID);
            ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "RegionName", siteDetail.RegionID);
            ViewBag.SASID = new SelectList(db.SecurityAndSafetys, "SASID", "SASName", siteDetail.SASID);
            ViewBag.ServiceCatID = new SelectList(db.ServiceCategories, "ServiceCatID", "ServiceCat", siteDetail.ServiceCatID);
            ViewBag.SiteClassificationID = new SelectList(db.SiteClassifications, "SiteClassificationID", "SiteClassificationName", siteDetail.SiteClassificationID);
            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName", siteDetail.SiteID);
            ViewBag.SiteStatusID = new SelectList(db.SiteStatus, "SiteStatusID", "SiteStatusName", siteDetail.SiteStatusID);
            ViewBag.SiteTypeID = new SelectList(db.SiteTypes, "SiteTypeID", "SiteTypeName", siteDetail.SiteTypeID);
            ViewBag.TerrainID = new SelectList(db.Terrains, "TerrainID", "TerrainName", siteDetail.TerrainID);
            ViewBag.TransportCatID = new SelectList(db.TransportCategories, "TransportCatID", "TransportCat", siteDetail.TransportCatID);
            ViewBag.TXTypeID = new SelectList(db.TXTypes, "TXTypeID", "TXTypeName", siteDetail.TXTypeID);

            return View(siteDetail);
        }

        // POST: SiteDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SiteDetailID,SiteID,EmployeeNo,HouseLotNo,AreaID,ClusterID,RegionID,CityID,BarangayID,Longitude,Lattitude,SiteStatusID,SiteTypeID,SiteClassificationID,AssetOwnershipID,CSMPClassID,TransportCatID,ServiceCatID,LocationTypeID,TerrainID,AccessibilityID,SASID,ACMID,SunID,SubBase,TXTypeID,AccessIssue,TimeOfIssue,RiskCategory,TravelTime,AccessPassTime,MonthlyRevenue,ForCSMPPMR,Remarks")] SiteDetail siteDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siteDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var sid = (int)Session["siteid"];
            ViewBag.AccessibilityID = new SelectList(db.Accessibilities, "AccessibilityID", "AccessibilityName", siteDetail.AccessibilityID);
            ViewBag.ACMID = new SelectList(db.ACMainSources, "ACMID", "ACMName", siteDetail.ACMID);
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName", siteDetail.AreaID);
            ViewBag.AssetOwnershipID = new SelectList(db.AssetOwnershipTypes, "AssetOwnershipID", "AssetOwnership", siteDetail.AssetOwnershipID);
            ViewBag.BarangayID = new SelectList(db.Baranggays, "BarangayID", "AreaClusterCityBrgy", siteDetail.BarangayID);
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "AreaClusterCities", siteDetail.CityID);
            ViewBag.ClusterID = new SelectList(db.Clusters, "ClusterID", "AreaClusters", siteDetail.ClusterID);
            ViewBag.CSMPClassID = new SelectList(db.CSMPClassifications, "CSMPClassID", "CSMPClass", siteDetail.CSMPClassID);
            ViewBag.EmployeeNo = new SelectList(db.Employees, "EmployeeNo", "FullName", siteDetail.EmployeeNo);
            ViewBag.LocationTypeID = new SelectList(db.LocationTypes, "LocationTypeID", "LocationTypeDescription", siteDetail.LocationTypeID);
            ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "RegionName", siteDetail.RegionID);
            ViewBag.SASID = new SelectList(db.SecurityAndSafetys, "SASID", "SASName", siteDetail.SASID);
            ViewBag.ServiceCatID = new SelectList(db.ServiceCategories, "ServiceCatID", "ServiceCat", siteDetail.ServiceCatID);
            ViewBag.SiteClassificationID = new SelectList(db.SiteClassifications, "SiteClassificationID", "SiteClassificationName", siteDetail.SiteClassificationID);
            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName", siteDetail.SiteID);
            ViewBag.SiteStatusID = new SelectList(db.SiteStatus, "SiteStatusID", "SiteStatusName", siteDetail.SiteStatusID);
            ViewBag.SiteTypeID = new SelectList(db.SiteTypes, "SiteTypeID", "SiteTypeName", siteDetail.SiteTypeID);
            ViewBag.TerrainID = new SelectList(db.Terrains, "TerrainID", "TerrainName", siteDetail.TerrainID);
            ViewBag.TransportCatID = new SelectList(db.TransportCategories, "TransportCatID", "TransportCat", siteDetail.TransportCatID);
            ViewBag.TXTypeID = new SelectList(db.TXTypes, "TXTypeID", "TXTypeName", siteDetail.TXTypeID);

            return View(siteDetail);
        }

        // GET: SiteDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteDetail siteDetail = db.SiteDetails.Find(id);
            if (siteDetail == null)
            {
                return HttpNotFound();
            }
            return View(siteDetail);
        }

        // POST: SiteDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SiteDetail siteDetail = db.SiteDetails.Find(id);
            db.SiteDetails.Remove(siteDetail);
            db.SaveChanges();
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
