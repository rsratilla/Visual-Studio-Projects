using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;

namespace Sayts.Controllers
{
    [Authorize]

    public class BatteriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: batteries
        public ActionResult Index()
        {
            var batteries = db.Batteries.Include(g => g.Sites).Include(g => g.Units).Include(g => g.UnitStatus);
            return View(batteries.ToList());
        }

        // GET: Batteries/Create
        public ActionResult Create()
        {
            var sid = (int)Session["siteid"];
            ViewBag.SiteCode = sid;
            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName");
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "Battery"), "UnitID", "BrandModel");
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status");
            return View();
        }

        // POST: Batteries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BatteryID,SiteID,UnitID,StatusID,DateDelivered,DateInstalled,Remarks,UpdateDate")] Battery battery)
        {
            var sid = (int)Session["siteid"];
            ViewBag.SiteCode = sid;

            if (ModelState.IsValid)
            {
                db.Batteries.Add(battery);
                db.SaveChanges();
                return RedirectToAction("GetDetails", "Inventories", new { id = sid });
            }

            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName", battery.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "Battery"), "UnitID", "BrandModel", battery.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", battery.StatusID);

            return View(battery);
        }

        // GET: Batteries/Edit/5
        public ActionResult Edit(int? bid)
        {
            if (bid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Battery battery = db.Batteries.Find(bid);
            if (battery == null)
            {
                return HttpNotFound();
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", battery.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "Battery"), "UnitID", "BrandModel", battery.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", battery.StatusID);

            return View(battery);
        }

        // POST: Batteries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BatteryID,SiteID,UnitID,StatusID,DateDelivered,DateInstalled,Remarks,UpdateDate")] Battery battery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(battery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetDetails", "Inventories", new { id = battery.SiteID });
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", battery.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "Battery"), "UnitID", "BrandModel", battery.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", battery.StatusID);

            return View(battery);
        }

        // GET: Batteries/Delete/5
        public ActionResult Delete(int? bid)
        {
            if (bid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Battery battery = db.Batteries.Find(bid);
            if (battery == null)
            {
                return HttpNotFound();
            }
            return View(battery);
        }

        // POST: Batteries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int bid)
        {
            Battery battery = db.Batteries.Find(bid);
            Session["UnitID"] = battery.UnitID;

            Site site = db.Sites.Find(battery.SiteID);
            Session["SiteID"] = site.SiteCode;
            Session["SiteName"] = site.SiteName;

            SiteDetail siteDetail = db.SiteDetails.Find(battery.SiteID);
            Session["Cluster"] = siteDetail.ClusterID;
            Session["Address"] = siteDetail.HouseLotNo;

            Employee employee = db.Employees.Find(siteDetail.EmployeeNo);

            db.Batteries.Remove(battery);
            db.SaveChanges();

            return RedirectToAction("Create", "ArdaItems", new { id = battery.UnitID });
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
