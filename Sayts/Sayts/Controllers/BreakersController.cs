using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;

namespace Sayts.Controllers
{
    [Authorize]

    public class BreakersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Breakers
        public ActionResult Index(int? rid)
        {
            ViewBag.CurrentRectifierID = rid;
            var breakers = db.Breakers.Include(b => b.Rectifiers).Where(b => b.RectifierID == rid.Value);
            return View(breakers.ToList());
        }

        // GET: Breakers/Create
        public ActionResult Create(int? rid)
        {
            ViewBag.RectifierID = new SelectList(db.Rectifiers.Where(r => r.RectifierID == rid), "RectifierID", "Serial");
            return View();
        }

        // POST: Breakers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BreakerID,RectifierID,Amp,F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23,F24,F25,F26,F27,F28,F29,F30")] Breaker breaker)
        {
            if (ModelState.IsValid)
            {
                db.Breakers.Add(breaker);
                db.SaveChanges();
                return RedirectToAction("Index", new { rid = breaker.RectifierID });
            }

            ViewBag.CurrentRectifierID = breaker.RectifierID;
            ViewBag.RectifierID = new SelectList(db.Rectifiers, "RectifierID", "Serial", breaker.RectifierID);
            return View(breaker);
        }

        // GET: Breakers/Edit/5
        public ActionResult Edit(int? id)
        {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Breaker breaker = db.Breakers.Find(id);
            if (breaker == null)
            {
                return HttpNotFound();
            }

            ViewBag.CurrentRectifierID = breaker.RectifierID;
            ViewBag.RectifierID = new SelectList(db.Rectifiers, "RectifierID", "Serial", breaker.RectifierID);
            return View(breaker);
        }

        // POST: Breakers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BreakerID,RectifierID,Amp,F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23,F24,F25,F26,F27,F28,F29,F30")] Breaker breaker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(breaker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { rid = breaker.RectifierID });
            }

            ViewBag.CurrentRectifierID = breaker.RectifierID;
            ViewBag.RectifierID = new SelectList(db.Rectifiers, "RectifierID", "Serial", breaker.RectifierID);
            return View(breaker);
        }

        // GET: Breakers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Breaker breaker = db.Breakers.Find(id);
            if (breaker == null)
            {
                return HttpNotFound();
            }

            ViewBag.CurrentRectifierID = breaker.RectifierID;
            return View(breaker);
        }

        // POST: Breakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Breaker breaker = db.Breakers.Find(id);
            db.Breakers.Remove(breaker);
            db.SaveChanges();
            return RedirectToAction("Index", new { rid = breaker.RectifierID });
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
