using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;

namespace Sayts.Controllers
{
    [Authorize]

    public class AreasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Areas
        public ActionResult Index(int? id, int? clusterID)
        {
            var viewModel = new AreaIndexData();
            viewModel.Areas = db.Areas
                .Include(a => a.Clusters.Select(c => c.Cities))
                .Include(a => a.Clusters.Select(c => c.Employees))
                .Include(a => a.Clusters.Select(c => c.ElectricCos))
                .Include(a => a.Clusters.Select(c => c.SubBases))
                .OrderBy(a => a.AreaName);

            if (id != null)
            {
                ViewBag.AreaID = id.Value;
                viewModel.Clusters = viewModel.Areas.Where(
                    i => i.AreaID == id.Value).Single().Clusters;
            }

            if (clusterID != null)
            {
                ViewBag.ClusterID = clusterID.Value;
                viewModel.Cities = viewModel.Clusters.Where(
                    c => c.ClusterID == clusterID).Single().Cities;

                viewModel.Employees = viewModel.Clusters.Where(
                    e => e.ClusterID == clusterID).Single().Employees;

                viewModel.ElectricCos = viewModel.Clusters.Where(
                    e => e.ClusterID == clusterID).Single().ElectricCos;

                viewModel.SubBases = viewModel.Clusters.Where(
                    e => e.ClusterID == clusterID).Single().SubBases;
            }

            return View(viewModel);
        }

        // GET: Areas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AreaID,AreaName")] Area area)
        {
            if (ModelState.IsValid)
            {
                db.Areas.Add(area);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(area);
        }

        // GET: Areas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AreaID,AreaName")] Area area)
        {
            if (ModelState.IsValid)
            {
                db.Entry(area).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(area);
        }

        // GET: Areas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Area area = db.Areas.Find(id);
            db.Areas.Remove(area);
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
