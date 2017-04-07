using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;

namespace Sayts.Controllers
{
    [Authorize]

    public class GenSetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GenSets
        public ActionResult Index()
        {
            var gensets = db.GenSets.Include(g => g.Sites).Include(g => g.Units).Include(g => g.UnitStatus);
            return View(gensets.ToList());
        }

        // GET: GenSets/Create
        public ActionResult Create()
        {
            var sid = (int)Session["siteid"];
            ViewBag.SiteCode = sid;
            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName");
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "GenSet"), "UnitID", "BrandModel");
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status");
            return View();
        }

        // POST: GenSets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenSetID,SiteID,Serial,UnitID,Capacity,StatusID,DateDelivered,DateInstalled,ACLoading,Remarks,UpdateDate")] GenSet genSet)
        {
            var sid = (int)Session["siteid"];
            ViewBag.SiteCode = sid;
            if (ModelState.IsValid)
            {
                db.GenSets.Add(genSet);
                db.SaveChanges();
                return RedirectToAction("GetDetails", "Inventories", new { id = sid });
            }
            
            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName", genSet.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "GenSet"), "UnitID", "BrandModel", genSet.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", genSet.StatusID);

            return View(genSet);
        }

        // GET: GenSets/Edit/5
        public ActionResult Edit(int? gid)
        {            
            if (gid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenSet genSet = db.GenSets.Find(gid);
            if (genSet == null)
            {
                return HttpNotFound();
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", genSet.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "GenSet"), "UnitID", "BrandModel", genSet.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", genSet.StatusID);

            return View(genSet);
        }

        // POST: GenSets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenSetID,SiteID,Serial,UnitID,Capacity,StatusID,DateDelivered,DateInstalled,ACLoading,Remarks,UpdateDate")] GenSet genSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genSet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetDetails", "Inventories", new { id = genSet.SiteID });
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", genSet.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "GenSet"), "UnitID", "BrandModel", genSet.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", genSet.StatusID);

            return View(genSet);
        }

        // GET: GenSets/Delete/5
        public ActionResult Delete(int? gid)
        {
            if (gid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenSet genSet = db.GenSets.Find(gid);
            if (genSet == null)
            {
                return HttpNotFound();
            }

            return View(genSet);
        }

        // POST: GenSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int gid)
        {
            GenSet genSet = db.GenSets.Find(gid);
            Session["UnitID"] = genSet.UnitID;
            Session["Serial"] = genSet.Serial;

            Site site = db.Sites.Find(genSet.SiteID);
            Session["SiteID"] = site.SiteCode;
            Session["SiteName"] = site.SiteName;

            SiteDetail siteDetail = db.SiteDetails.Find(genSet.SiteID);
            Session["Cluster"] = siteDetail.ClusterID;
            Session["Address"] = siteDetail.HouseLotNo;

            Employee employee = db.Employees.Find(siteDetail.EmployeeNo);

            db.GenSets.Remove(genSet);
            db.SaveChanges();

            return RedirectToAction("Create", "ArdaItems", new { id = genSet.UnitID });
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
