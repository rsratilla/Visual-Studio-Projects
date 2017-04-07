using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;

namespace Sayts.Controllers
{
    [Authorize]

    public class PortsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Ports/Create
        public ActionResult Create()
        {
            var sid = (int)Session["siteid"];
            ViewBag.SiteCode = sid;
            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName");
            return View();
        }

        // POST: Ports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PortID,SiteID,Facility,PortType,NoOfPorts,StartPort")] Port port)
        {
            var sid = (int)Session["siteid"];
            ViewBag.SiteCode = sid;
            if (ModelState.IsValid)
            {
                var portInventories = new PortInventory();

                db.Ports.Add(port);
                portInventories.PortID = port.PortID;

                int startingIndex = port.StartPort.Trim().LastIndexOf("/");
                string portPrefix = port.StartPort.Trim().Substring(0, startingIndex + 1);
                int startingPort = int.Parse(port.StartPort.Substring(startingIndex + 1));

                for (int i = startingPort; i <= (int)port.NoOfPorts + startingPort - 1; i++)
                {                    
                    portInventories.Port = portPrefix + i.ToString();
                    db.PortInventories.Add(portInventories);
                    db.SaveChanges();
                }

                db.SaveChanges();
                
                return RedirectToAction("GetDetails", "Inventories", new { id = sid });
            }

            ViewBag.SiteID = new SelectList(db.Sites.Where(s => s.SiteID == sid), "SiteID", "SiteFullName", port.SiteID);
            return View(port);
        }

        // GET: Ports/Edit/5
        public ActionResult Edit(int? pid)
        {
            if (pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Port port = db.Ports.Find(pid);
            if (port == null)
            {
                return HttpNotFound();
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", port.SiteID);
            return View(port);
        }

        // POST: Ports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PortID,SiteID,Facility,PortType,NoOfPorts")] Port port)
        {
            if (ModelState.IsValid)
            {
                db.Entry(port).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetDetails", "Inventories", new { id = port.SiteID });
            }

            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", port.SiteID);
            return View(port);
        }

        // GET: Ports/Delete/5
        public ActionResult Delete(int? pid)
        {
            if (pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Port port = db.Ports.Find(pid);
            if (port == null)
            {
                return HttpNotFound();
            }
            return View(port);
        }

        // POST: Ports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int pid)
        {
            Port port = db.Ports.Find(pid);
            db.Ports.Remove(port);
            db.SaveChanges();
            return RedirectToAction("GetDetails", "Inventories", new { id = port.SiteID });
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
