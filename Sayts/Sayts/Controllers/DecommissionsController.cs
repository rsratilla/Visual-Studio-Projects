using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;

namespace Sayts.Controllers
{
    [Authorize]

    public class DecommissionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Decommissions
        public ActionResult Index()
        {
            var decommissions = db.Decommissions.Include(d => d.Clusters);
            return View(decommissions.ToList());
        }

        // GET: Decommissions/Create
        public ActionResult Create()
        {
            ViewBag.SiteCode = Session["sitecode"];
            ViewBag.SiteName = Session["sitename"];
            ViewBag.ClusterID = new SelectList(db.Clusters, "ClusterID", "ClusterName");
            return View();
        }

        // POST: Decommissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DecommID,ClusterID,SiteCode,Sitename,DateDecom,Services,Reason")] Decommission decommission)
        {
            if (ModelState.IsValid)
            {
                db.Decommissions.Add(decommission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClusterID = new SelectList(db.Clusters, "ClusterID", "ClusterName", decommission.ClusterID);
            return View(decommission);
        }

        // GET: Decommissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decommission decommission = db.Decommissions.Find(id);
            if (decommission == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClusterID = new SelectList(db.Clusters, "ClusterID", "ClusterName", decommission.ClusterID);
            return View(decommission);
        }

        // POST: Decommissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DecommID,ClusterID,SiteCode,Sitename,DateDecom,Services,Reason")] Decommission decommission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(decommission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClusterID = new SelectList(db.Clusters, "ClusterID", "ClusterName", decommission.ClusterID);
            return View(decommission);
        }

        // GET: Decommissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decommission decommission = db.Decommissions.Find(id);
            if (decommission == null)
            {
                return HttpNotFound();
            }
            return View(decommission);
        }

        // POST: Decommissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Decommission decommission = db.Decommissions.Find(id);
            db.Decommissions.Remove(decommission);
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
