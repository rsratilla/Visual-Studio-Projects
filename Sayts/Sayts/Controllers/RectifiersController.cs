using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;

namespace Sayts.Controllers
{
    [Authorize]

    public class RectifiersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: rectifiers
        public ActionResult Index()
        {
            var rectifiers = db.Rectifiers.Include(g => g.Sites).Include(g => g.Units).Include(g => g.UnitStatus);
            return View(rectifiers.ToList());
        }

        // GET: Rectifiers/Create
        public ActionResult Create()
        {
            var sid = (int)Session["siteid"];
            ViewBag.SiteCode = sid;
            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName");
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "Rectifier"), "UnitID", "BrandModel");
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status");
            return View();
        }

        // POST: Rectifiers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RectifierID,SiteID,Serial,UnitID,StatusID,DateDelivered,DateInstalled,DCPPType,RMActive,RMDefective,Voltage,BatteryCurr,RectifierCurr,DCLoading,TimeBackUp,Remarks,UpdateDate")] Rectifier rectifier)
        {
            var sid = (int)Session["siteid"];
            ViewBag.SiteCode = sid;
            if (ModelState.IsValid)
            {
                db.Rectifiers.Add(rectifier);
                db.SaveChanges();
                return RedirectToAction("GetDetails", "Inventories", new { id = sid });
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", rectifier.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "Rectifier"), "UnitID", "BrandModel", rectifier.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", rectifier.StatusID);
            return View(rectifier);
        }

        // GET: Rectifiers/Edit/5
        public ActionResult Edit(int? rid)
        {
            if (rid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rectifier rectifier = db.Rectifiers.Find(rid);
            if (rectifier == null)
            {
                return HttpNotFound();
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", rectifier.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "Rectifier"), "UnitID", "BrandModel", rectifier.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", rectifier.StatusID);
            return View(rectifier);
        }

        // POST: Rectifiers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RectifierID,SiteID,Serial,UnitID,StatusID,DateDelivered,DateInstalled,DCPPType,RMActive,RMDefective,Voltage,BatteryCurr,RectifierCurr,DCLoading,TimeBackUp,Remarks,UpdateDate")] Rectifier rectifier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rectifier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetDetails", "Inventories", new { id = rectifier.SiteID });
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", rectifier.SiteID);
            ViewBag.UnitID = new SelectList(db.Units.Where(u => u.Category == "Rectifier"), "UnitID", "BrandModel", rectifier.UnitID);
            ViewBag.StatusID = new SelectList(db.UnitStatus, "StatusID", "Status", rectifier.StatusID);
            return View(rectifier);
        }

        // GET: Rectifiers/Delete/5
        public ActionResult Delete(int? rid)
        {
            if (rid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rectifier rectifier = db.Rectifiers.Find(rid);
            if (rectifier == null)
            {
                return HttpNotFound();
            }
            return View(rectifier);
        }

        // POST: Rectifiers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int rid)
        {
            Rectifier rectifier = db.Rectifiers.Find(rid);
            Session["Serial"] = rectifier.Serial;

            Site site = db.Sites.Find(rectifier.SiteID);
            Session["SiteID"] = site.SiteCode;
            Session["SiteName"] = site.SiteName;

            SiteDetail siteDetail = db.SiteDetails.Find(rectifier.SiteID);
            Session["Cluster"] = siteDetail.ClusterID;
            Session["Address"] = siteDetail.HouseLotNo;

            Employee employee = db.Employees.Find(siteDetail.EmployeeNo);

            db.Rectifiers.Remove(rectifier);
            db.SaveChanges();

            return RedirectToAction("Create", "ArdaItems", new { id = rectifier.UnitID });
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
