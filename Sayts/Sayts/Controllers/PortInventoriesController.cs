using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;

namespace Sayts.Controllers
{
    [Authorize]

    public class PortInventoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PortInventories
        public ActionResult Index(int? pid)
        {
            ViewBag.CurrentPortID = pid;
            var portInventories = db.PortInventories.Include(p => p.Ports).Where(b => b.PortID == pid.Value); ;
            return View(portInventories.ToList());
        }

        // GET: PortInventories/Create
        public ActionResult Create(int? pid)
        {            
            ViewBag.PortID = new SelectList(db.Ports.Where(r => r.PortID == pid), "PortID", "Facility");
            return View();
        }

        // POST: PortInventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PortInventoryID,PortID,Port,ConnectorType,Bandwidth,CircuitAssignment,NeighborPort,Remarks")] PortInventory portInventory)
        {
            if (ModelState.IsValid)
            {
                db.PortInventories.Add(portInventory);
                db.SaveChanges();
                return RedirectToAction("Index", new { pid = portInventory.PortID });
            }

            ViewBag.CurrentPortID = portInventory.PortID;
            ViewBag.PortID = new SelectList(db.Ports, "PortID", "Facility", portInventory.PortID);
            return View(portInventory);
        }

        // GET: PortInventories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortInventory portInventory = db.PortInventories.Find(id);
            if (portInventory == null)
            {
                return HttpNotFound();
            }

            ViewBag.CurrentPortID = portInventory.PortID;
            ViewBag.PortID = new SelectList(db.Ports, "PortID", "Facility", portInventory.PortID);
            return View(portInventory);
        }

        // POST: PortInventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PortInventoryID,PortID,Port,ConnectorType,Bandwidth,CircuitAssignment,NeighborPort,Remarks")] PortInventory portInventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(portInventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { pid = portInventory.PortID });
            }

            ViewBag.CurrentPortID = portInventory.PortID;
            ViewBag.PortID = new SelectList(db.Ports, "PortID", "Facility", portInventory.PortID);
            return View(portInventory);
        }

        // GET: PortInventories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortInventory portInventory = db.PortInventories.Find(id);
            if (portInventory == null)
            {
                return HttpNotFound();
            }

            ViewBag.CurrentPortID = portInventory.PortID;
            return View(portInventory);
        }

        // POST: PortInventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PortInventory portInventory = db.PortInventories.Find(id);
            db.PortInventories.Remove(portInventory);
            db.SaveChanges();
            return RedirectToAction("Index", new { pid = portInventory.PortID });
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
