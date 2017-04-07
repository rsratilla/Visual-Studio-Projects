using Sayts.Models;
using System.Linq;
using System.Web.Mvc;

namespace Sayts.Controllers
{
    [Authorize]

    public class GetJSONDataController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GetJSONData
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Gensets()
        {
            var gsDetails = from g in db.GenSets
                            join s in db.Sites on g.SiteID equals s.SiteID
                            select new
                            {
                                g.GenSetID,
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

            return Json(gsDetails, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Rectifiers()
        {
            var reDetails = from r in db.Rectifiers
                            join s in db.Sites on r.SiteID equals s.SiteID
                            select new
                            {
                                r.RectifierID,
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

            return Json(reDetails, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Batteries()
        {
            var baDetails = from b in db.Batteries
                            join s in db.Sites on b.SiteID equals s.SiteID
                            select new
                            {
                                b.BatteryID,
                                b.Sites.SiteCode,
                                b.Sites.SiteName,
                                b.Units.BrandModel,
                                b.UnitStatus.Status,
                                b.DateDelivered,
                                b.DateInstalled,
                                b.Remarks,
                                b.UpdateDate
                            };

            return Json(baDetails, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ACUs()
        {
            var acDetails = from c in db.ACUs
                            join s in db.Sites on c.SiteID equals s.SiteID
                            select new
                            {
                                c.ACUID,
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

            return Json(acDetails, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Others()
        {
            var otDetails = from t in db.Others
                            join s in db.Sites on t.SiteID equals s.SiteID
                            select new
                            {
                                t.OthersID,
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

            return Json(otDetails, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PMRMasters()
        {
            var pmrMaster = from p in db.PMRMasters
                            join s in db.Sites on p.SiteID equals s.SiteID
                            select new
                            {
                                p.PMRMasterID,
                                p.PMRDate,
                                p.Sites.SiteFullName,
                                p.SiteClassification,
                                p.PMRType,
                                p.WithGS,
                                p.CoordinationNo,
                                p.Lilo,
                                p.MainsSimulStatus,
                                p.MainsSimulRemarks,
                                p.Employees.FullName,
                                p.Remarks
                            };

            return Json(pmrMaster, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PMRDeficiencies()
        {
            var pmrDeficiencies = from d in db.PMRDeficiencies
                                  join p in db.PMRMasters on d.PMRMasterID equals p.PMRMasterID
                                  select new
                                  {
                                      d.PMRDeficiencyID,
                                      d.PMRMasters.PMRMasterID,
                                      d.PMRMasters.Sites.SiteCode,
                                      d.PMRMasters.Sites.SiteName,
                                      d.PMRCheckLists.Category,
                                      d.PMRCheckLists.Particular,
                                      d.Remarks
                                  };

            return Json(pmrDeficiencies, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Sites()
        {
            var sites = from d in db.SiteDetails
                            join s in db.Sites on d.SiteID equals s.SiteID
                            select new
                                {
                                    d.SiteID,
                                    d.Sites.SiteCode,
                                    d.Sites.SiteName,
                                    d.SunID,
                                    d.Accessibilities.AccessibilityName,
                                    d.ACMainSources.ACMName,
                                    d.Areas.AreaName,
                                    d.AssetOwnershipTypes.AssetOwnership,
                                    d.Baranggays.BarangayName,
                                    d.Cities.CityName,
                                    d.Clusters.ClusterName,
                                    d.CSMPClassifications.CSMPClass,
                                    d.Employees.LastName,
                                    d.Employees.GivenName,
                                    d.LocationTypes.LocationTypeDescription,
                                    d.Regions.RegionName,
                                    d.SecurityAndSafetys.SASName,
                                    d.ServiceCategories.ServiceCat,
                                    d.SiteClassifications.SiteClassificationName,
                                    d.SiteStatus.SiteStatusName,
                                    d.SiteTypes.SiteTypeName,
                                    d.Terrains.TerrainName,
                                    d.TransportCategories.TransportCat,
                                    d.TXTypes.TXTypeName,
                                    d.HouseLotNo,
                                    d.Longitude,
                                    d.Lattitude,
                                    d.SubBase,
                                    d.AccessIssue,
                                    d.TimeOfIssue,
                                    d.RiskCategory,
                                    d.TravelTime,
                                    d.AccessPassTime,
                                    d.MonthlyRevenue,
                                    d.ForCSMPPMR,
                                    d.Remarks
                            };

            return Json(sites, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Ports()
        {
            var portInventories = from i in db.PortInventories
                                  join p in db.Ports on i.PortID equals p.PortID
                                  select new
                                  {
                                      i.PortID,
                                      i.Ports.Facility,
                                      i.Port,
                                      i.ConnectorType,
                                      i.Bandwidth,
                                      i.CircuitAssignment,
                                      i.NeighborPort,
                                      i.Remarks
                                  };

            return Json(portInventories, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Units()
        {
            var units = from u in db.Units
                        select new
                        {
                            u.UnitID,
                            u.Category,
                            u.BrandModel,
                            u.Type,
                            u.Capacity
                        };

            return Json(units, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PMRCheckLists()
        {
            var pmrchecklists = from c in db.PMRCheckLists
                        select new
                        {
                            c.DeficiencyID,
                            c.Type,
                            c.Category,
                            c.Particular,
                            c.Note
                        };

            return Json(pmrchecklists, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UnitStatus()
        {
            var unitStatus = from s in db.UnitStatus
                                select new
                                {
                                    s.StatusID,
                                    s.Status
                                };

            return Json(unitStatus, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ArdaItems()
        {
            var ardaItems = from a in db.ArdaItems
                            join u in db.Units on a.UnitID equals u.UnitID
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

            return Json(ardaItems, JsonRequestBehavior.AllowGet);
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