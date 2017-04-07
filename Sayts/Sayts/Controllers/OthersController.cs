using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;

namespace Sayts.Controllers
{
    [Authorize]

    public class OthersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Others/Create
        public ActionResult Create()
        {
            var sid = (int)Session["siteid"];
            ViewBag.SiteCode = sid;
            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName");
            ViewBag.ATSUnit = new SelectList(db.Units.Where(u => u.Category == "ATS"), "BrandModel", "BrandModel");
            ViewBag.Status = new SelectList(db.UnitStatus, "Status", "Status");
            ViewBag.TowerType = new SelectList(db.Units.Where(u => u.Category == "Tower"), "BrandModel", "BrandModel");
            return View();
        }

        // POST: Others/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OthersID,SiteID,ATSSerial,ATSType,ATSStatus,DateDelivered,DateInstalled,HasCrankModule,HasBatCharger,ATSRemarks,FTStatus,FTCapacity,FTRemarks,FLStatus,FLRemarks,TVSSerial,TVSSType,TVSSStatus,TVSSRemarks,EFStatus,EFRemarks,ToType,ToHeight,ToStatus,ToRemarks,TLStatus,TLRemarks,FenceRemarks,LAStatus,BBStatus,TGStatus,UpdateDate")] Other other)
        {
            var sid = (int)Session["siteid"];
            ViewBag.SiteCode = sid;
            if (ModelState.IsValid)
            {
                db.Others.Add(other);
                db.SaveChanges();
                return RedirectToAction("GetDetails", "Inventories", new { id = sid });
            }

            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName", other.SiteID);
            ViewBag.ATSUnit = new SelectList(db.Units.Where(u => u.Category == "ATS"), "BrandModel", "BrandModel");
            ViewBag.Status = new SelectList(db.UnitStatus, "Status", "Status");
            ViewBag.TowerType = new SelectList(db.Units.Where(u => u.Category == "Tower"), "BrandModel", "BrandModel");
            return View(other);
        }

        // GET: Others/Edit/5
        public ActionResult Edit(int? oid)
        {
            if (oid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Other other = db.Others.Find(oid);
            if (other == null)
            {
                return HttpNotFound();
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", other.SiteID);
            ViewBag.ATSUnit = new SelectList(db.Units.Where(u => u.Category == "ATS"), "BrandModel", "BrandModel");
            ViewBag.Status = new SelectList(db.UnitStatus, "Status", "Status");
            ViewBag.TowerType = new SelectList(db.Units.Where(u => u.Category == "Tower"), "BrandModel", "BrandModel");
            return View(other);
        }

        // POST: Others/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OthersID,SiteID,ATSSerial,ATSType,ATSStatus,DateDelivered,DateInstalled,HasCrankModule,HasBatCharger,ATSRemarks,FTStatus,FTCapacity,FTRemarks,FLStatus,FLRemarks,TVSSerial,TVSSType,TVSSStatus,TVSSRemarks,EFStatus,EFRemarks,ToType,ToHeight,ToStatus,ToRemarks,TLStatus,TLRemarks,FenceRemarks,LAStatus,BBStatus,TGStatus,UpdateDate")] Other other)
        {
            if (ModelState.IsValid)
            {
                db.Entry(other).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetDetails", "Inventories", new { id = other.SiteID });
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", other.SiteID);
            ViewBag.ATSUnit = new SelectList(db.Units.Where(u => u.Category == "ATS"), "BrandModel", "BrandModel");
            ViewBag.Status = new SelectList(db.UnitStatus, "Status", "Status");
            ViewBag.TowerType = new SelectList(db.Units.Where(u => u.Category == "Tower"), "BrandModel", "BrandModel");
            return View(other);
        }

        // GET: Others/Delete/5
        public ActionResult Delete(int? oid)
        {
            if (oid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Other other = db.Others.Find(oid);
            if (other == null)
            {
                return HttpNotFound();
            }
            return View(other);
        }

        // POST: Others/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int oid)
        {
            Other other = db.Others.Find(oid);
            db.Others.Remove(other);
            db.SaveChanges();
            return RedirectToAction("GetDetails", "Inventories", new { id = other.SiteID });
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
