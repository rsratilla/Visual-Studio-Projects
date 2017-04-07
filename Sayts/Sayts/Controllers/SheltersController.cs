using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;

namespace Sayts.Controllers
{
    [Authorize]

    public class SheltersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Shelters/Create
        public ActionResult Create()
        {
            var sid = (int)Session["siteid"];
            ViewBag.SiteCode = sid;
            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName");
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "Shelter"), "UnitID", "Type");
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status");
            return View();
        }

        // POST: Shelters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShelterID,SiteID,UnitID,StatusID,Remarks,UpdateDate")] Shelter shelter)
        {
            var sid = (int)Session["siteid"];
            ViewBag.SiteCode = sid;
            if (ModelState.IsValid)
            {
                db.Shelters.Add(shelter);
                db.SaveChanges();
                return RedirectToAction("GetDetails", "Inventories", new { id = sid });
            }

            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName", shelter.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "Shelter"), "UnitID", "Type", shelter.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", shelter.StatusID);
            return View(shelter);
        }

        // GET: Shelters/Edit/5
        public ActionResult Edit(int? hid)
        {
            if (hid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shelter shelter = db.Shelters.Find(hid);
            if (shelter == null)
            {
                return HttpNotFound();
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", shelter.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "Shelter"), "UnitID", "Type", shelter.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", shelter.StatusID);
            return View(shelter);
        }

        // POST: Shelters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShelterID,SiteID,UnitID,StatusID,Remarks,UpdateDate")] Shelter shelter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shelter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetDetails", "Inventories", new { id = shelter.SiteID });
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", shelter.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "Shelter"), "UnitID", "Type", shelter.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", shelter.StatusID);
            return View(shelter);
        }

        // GET: Shelters/Delete/5
        public ActionResult Delete(int? hid)
        {
            if (hid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shelter shelter = db.Shelters.Find(hid);
            if (shelter == null)
            {
                return HttpNotFound();
            }
            return View(shelter);
        }

        // POST: Shelters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int hid)
        {
            Shelter shelter = db.Shelters.Find(hid);
            db.Shelters.Remove(shelter);
            db.SaveChanges();
            return RedirectToAction("GetDetails", "Inventories", new { id = shelter.SiteID });
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
