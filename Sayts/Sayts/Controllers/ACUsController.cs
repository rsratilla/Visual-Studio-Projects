using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;

namespace Sayts.Controllers
{
    [Authorize]

    public class ACUsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ACUs
        public ActionResult Index()
        {
            var acus = db.ACUs.Include(g => g.Sites).Include(g => g.Units).Include(g => g.UnitStatus);
            return View(acus.ToList());
        }

        // GET: ACUs/Create
        public ActionResult Create()
        {
            var sid = (int)Session["siteid"];
            ViewBag.SiteCode = sid;
            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName");
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "ACU"), "UnitID", "BrandModel");
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status");
            return View();
        }

        // POST: ACUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ACUID,SiteID,Serial,UnitID,StatusID,DateDelivered,DateInstalled,Remarks,UpdateDate")] ACU aCU)
        {
            var sid = (int)Session["siteid"];
            ViewBag.SiteCode = sid;

            if (ModelState.IsValid)
            {
                db.ACUs.Add(aCU);
                db.SaveChanges();
                return RedirectToAction("GetDetails", "Inventories", new { id = sid });
            }

            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName", aCU.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "ACU"), "UnitID", "BrandModel", aCU.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", aCU.StatusID);
            return View(aCU);
        }

        // GET: ACUs/Edit/5
        public ActionResult Edit(int? aid)
        {
            if (aid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACU aCU = db.ACUs.Find(aid);
            if (aCU == null)
            {
                return HttpNotFound();
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", aCU.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "ACU"), "UnitID", "BrandModel", aCU.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", aCU.StatusID);
            return View(aCU);
        }

        // POST: ACUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ACUID,SiteID,UnitID,Serial,StatusID,DateDelivered,DateInstalled,Remarks,UpdateDate")] ACU aCU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aCU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetDetails", "Inventories", new { id = aCU.SiteID });
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", aCU.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "ACU"), "UnitID", "BrandModel", aCU.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", aCU.StatusID);
            return View(aCU);
        }

        // GET: ACUs/Delete/5
        public ActionResult Delete(int? aid)
        {
            if (aid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACU aCU = db.ACUs.Find(aid);
            if (aCU == null)
            {
                return HttpNotFound();
            }
            return View(aCU);
        }

        // POST: ACUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int aid)
        {
            ACU aCU = db.ACUs.Find(aid);
            Session["UnitID"] = aCU.UnitID;
            Session["Serial"] = aCU.Serial;

            Site site = db.Sites.Find(aCU.SiteID);
            Session["SiteID"] = site.SiteCode;
            Session["SiteName"] = site.SiteName;

            SiteDetail siteDetail = db.SiteDetails.Find(aCU.SiteID);
            Session["Cluster"] = siteDetail.ClusterID;
            Session["Address"] = siteDetail.HouseLotNo;

            Employee employee = db.Employees.Find(siteDetail.EmployeeNo);

            db.ACUs.Remove(aCU);
            db.SaveChanges();

            return RedirectToAction("Create", "ArdaItems", new { id = aCU.UnitID });
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
