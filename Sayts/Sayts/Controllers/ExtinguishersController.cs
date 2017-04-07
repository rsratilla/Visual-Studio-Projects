using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;

namespace Sayts.Controllers
{
    [Authorize]

    public class ExtinguishersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Extinguishers/Create
        public ActionResult Create()
        {
            var sid = (int)Session["siteid"];
            ViewBag.SiteCode = sid;
            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName");
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "Extinguisher"), "UnitID", "BrandModel");
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status");
            return View();
        }

        // POST: Extinguishers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExtinguisherID,SiteID,UnitID,Serial,StatusID,Remarks,UpdateDate")] Extinguisher extinguisher)
        {
            var sid = (int)Session["siteid"];
            ViewBag.SiteCode = sid;

            if (ModelState.IsValid)
            {
                db.Extinguishers.Add(extinguisher);
                db.SaveChanges();
                return RedirectToAction("GetDetails", "Inventories", new { id = sid });
            }

            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName", extinguisher.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "Extinguisher"), "UnitID", "BrandModel", extinguisher.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", extinguisher.StatusID);
            return View(extinguisher);
        }

        // GET: Extinguishers/Edit/5
        public ActionResult Edit(int? eid)
        {
            if (eid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Extinguisher extinguisher = db.Extinguishers.Find(eid);
            if (extinguisher == null)
            {
                return HttpNotFound();
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", extinguisher.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "Extinguisher"), "UnitID", "BrandModel", extinguisher.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", extinguisher.StatusID);
            return View(extinguisher);
        }

        // POST: Extinguishers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExtinguisherID,SiteID,UnitID,Serial,StatusID,Remarks,UpdateDate")] Extinguisher extinguisher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(extinguisher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetDetails", "Inventories", new { id = extinguisher.SiteID });
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", extinguisher.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "Extinguisher"), "UnitID", "BrandModel", extinguisher.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", extinguisher.StatusID);
            return View(extinguisher);
        }

        // GET: Extinguishers/Delete/5
        public ActionResult Delete(int? eid)
        {
            if (eid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Extinguisher extinguisher = db.Extinguishers.Find(eid);
            if (extinguisher == null)
            {
                return HttpNotFound();
            }
            return View(extinguisher);
        }

        // POST: Extinguishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int eid)
        {
            Extinguisher extinguisher = db.Extinguishers.Find(eid);
            db.Extinguishers.Remove(extinguisher);
            db.SaveChanges();
            return RedirectToAction("GetDetails", "Inventories", new { id = extinguisher.SiteID });
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
