using Sayts.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System;

namespace Sayts.Controllers
{
    [Authorize]

    public class InventoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Inventories
        public ActionResult Index(string searchString1, string searchString2, string searchString3, string searchString4, string searchString5)
        {
            var viewModel = new InventoryIndexData();

            if (!String.IsNullOrEmpty(searchString1))
            {
                viewModel.Sites = db.Sites
                    .Where(s => s.SiteCode.ToUpper().Contains(searchString1.ToUpper()) ||
                        s.SiteName.ToUpper().Contains(searchString1.ToUpper()))
                    .OrderBy(s => s.SiteCode);
            }

            if (!String.IsNullOrEmpty(searchString2))
            {
                viewModel.GenSets = db.GenSets
                    .Where(g => g.Serial.ToUpper().Contains(searchString2.ToUpper()))
                    .OrderBy(g => g.Sites.SiteCode);
            }

            if (!String.IsNullOrEmpty(searchString3))
            {
                viewModel.Others = db.Others
                    .Where(o => o.ATSSerial.ToUpper().Contains(searchString3.ToUpper()))
                    .OrderBy(o => o.Sites.SiteCode);
            }

            if (!String.IsNullOrEmpty(searchString4))
            {
                viewModel.Rectifiers = db.Rectifiers
                    .Where(r => r.Serial.ToUpper().Contains(searchString4.ToUpper()))
                    .OrderBy(r => r.Sites.SiteCode);
            }

            if (!String.IsNullOrEmpty(searchString5))
            {
                viewModel.ACUs = db.ACUs
                    .Where(a => a.Serial.ToUpper().Contains(searchString5.ToUpper()))
                    .OrderBy(a => a.Sites.SiteCode);
            }

            return View(viewModel);
        }


        // GET: get details
        public ActionResult GetDetails(int? id)
        {
            var viewModel = new InventoryIndexData();

            if (id != null)
            {
                viewModel.Sites = db.Sites
                    .Include(s => s.GenSets)
                    .Include(s => s.Rectifiers.Select(r => r.Breakers))
                    .Include(s => s.ACUs)
                    .Include(s => s.Batteries)
                    .Include(s => s.Extinguishers)
                    .Include(s => s.Shelters)
                    .Include(s => s.Others)
                    .Include(s => s.Ports.Select(p => p.PortInventories))
                    .Where(s => s.SiteID == id.Value)
                        .OrderBy(s => s.SiteCode);
            
                viewModel.GenSets = viewModel.Sites.Where(s => s.SiteID == id.Value).Single().GenSets;
                viewModel.Rectifiers = viewModel.Sites.Where(s => s.SiteID == id.Value).Single().Rectifiers;
                viewModel.Batteries = viewModel.Sites.Where(s => s.SiteID == id.Value).Single().Batteries;
                viewModel.ACUs = viewModel.Sites.Where(s => s.SiteID == id.Value).Single().ACUs;
                viewModel.Extinguishers = viewModel.Sites.Where(s => s.SiteID == id.Value).Single().Extinguishers;
                viewModel.Shelters = viewModel.Sites.Where(s => s.SiteID == id.Value).Single().Shelters;
                viewModel.Others = viewModel.Sites.Where(s => s.SiteID == id.Value).Single().Others;
                viewModel.Ports = viewModel.Sites.Where(s => s.SiteID == id.Value).Single().Ports;
            }

            return View(viewModel);
        }
                

        public ActionResult RedirectToGenSetController(int? id)
        {
            Session["siteid"] = id;
            return RedirectToAction("Create", "GenSets");
        }

        public ActionResult RedirectToRectifierController(int? id)
        {
            Session["siteid"] = id;
            return RedirectToAction("Create", "Rectifiers");
        }

        public ActionResult RedirectToBatteryController(int? id)
        {
            Session["siteid"] = id;
            return RedirectToAction("Create", "Batteries");
        }

        public ActionResult RedirectToACUController(int? id)
        {
            Session["siteid"] = id;
            return RedirectToAction("Create", "ACUs");
        }

        public ActionResult RedirectToExtinguisherController(int? id)
        {
            Session["siteid"] = id;
            return RedirectToAction("Create", "Extinguishers");
        }

        public ActionResult RedirectToShelterController(int? id)
        {
            Session["siteid"] = id;
            return RedirectToAction("Create", "Shelters");
        }

        public ActionResult RedirectToOtherController(int? id)
        {
            Session["siteid"] = id;
            return RedirectToAction("Create", "Others");
        }

        public ActionResult RedirectToPortController(int? id)
        {
            Session["siteid"] = id;
            return RedirectToAction("Create", "Ports");
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
