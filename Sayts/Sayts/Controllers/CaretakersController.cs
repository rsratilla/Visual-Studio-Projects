using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;

namespace Sayts.Controllers
{
    [Authorize]

    public class CaretakersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Caretakers
        public ActionResult Index()
        {
            var caretakers = db.Caretakers.Include(c => c.Sites);
            return View(caretakers.ToList());
        }

        // GET: Caretakers/Create
        public ActionResult Create()
        {
            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName");
            return View();
        }

        // POST: Caretakers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CaretakerID,SiteID,Agency,Address,Active,CTName,EffectivityDate,ContactNo,Remarks")] Caretaker caretaker)
        {
            if (ModelState.IsValid)
            {
                db.Caretakers.Add(caretaker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", caretaker.SiteID);
            return View(caretaker);
        }

        // GET: Caretakers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caretaker caretaker = db.Caretakers.Find(id);
            if (caretaker == null)
            {
                return HttpNotFound();
            }
            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", caretaker.SiteID);
            return View(caretaker);
        }

        // POST: Caretakers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CaretakerID,SiteID,Agency,Address,Active,CTName,EffectivityDate,ContactNo,Remarks")] Caretaker caretaker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caretaker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", caretaker.SiteID);
            return View(caretaker);
        }

        // GET: Caretakers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caretaker caretaker = db.Caretakers.Find(id);
            if (caretaker == null)
            {
                return HttpNotFound();
            }
            return View(caretaker);
        }

        // POST: Caretakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Caretaker caretaker = db.Caretakers.Find(id);
            db.Caretakers.Remove(caretaker);
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
