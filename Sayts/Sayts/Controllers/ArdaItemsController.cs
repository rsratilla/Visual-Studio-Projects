using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;
using System.Web;
using System;
using System.IO;

namespace Sayts.Controllers
{
    [Authorize]

    public class ArdaItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ArdaItems
        public ActionResult Index()
        {
            var ardaItems = db.ArdaItems.Include(a => a.ArdaStatus).Include(a => a.Units);
            return View(ardaItems.ToList());
        }

        // GET: ArdaItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArdaItem ardaItem = db.ArdaItems.Find(id);
            if (ardaItem == null)
            {
                return HttpNotFound();
            }
            return View(ardaItem);
        }

        // GET: ArdaItems/Create
        public ActionResult Create(int? id)
        {            
            ViewBag.Serial = Session["Serial"];
            ViewBag.SiteID = Session["SiteID"];
            ViewBag.SiteName = Session["SiteName"];
            ViewBag.Cluster = Session["Cluster"];
            ViewBag.Address = Session["Address"];
            ViewBag.SiteOwner = Session["SiteOwner"];
            ViewBag.Contact = Session["Contact"];
            ViewBag.ArdaStatusID = new SelectList(db.ArdaStatus, "ArdaStatusID", "Status");            

            if ( id != null)
            {
                ViewBag.UnitID = new SelectList(db.Units.Where(u => u.UnitID == id), "UnitID", "CategoryBrand");
            }

            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "CategoryBrand");

            return View();
        }

        // POST: ArdaItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArdaID,ArdaCategory,UnitID,Serial,Quantity,SiteID,Sitename,Cluster,Province,SiteAddress,SiteOwner,Contact,Reason,OriginalLocation,PickUpLocation,StagingArea,Origin,RDFControl,ArdaStatusID,Approver,Department,Remarks")] ArdaItem ardaItem)
        {
            if (ModelState.IsValid)
            {
                db.ArdaItems.Add(ardaItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Serial = Session["Serial"];
            ViewBag.SiteID = Session["SiteID"];
            ViewBag.SiteName = Session["SiteName"];
            ViewBag.Cluster = Session["Cluster"];
            ViewBag.Address = Session["Address"];
            ViewBag.SiteOwner = Session["SiteOwner"];
            ViewBag.Contact = Session["Contact"];
            ViewBag.ArdaStatusID = new SelectList(db.ArdaStatus, "ArdaStatusID", "Status", ardaItem.ArdaStatusID);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "CategoryBrand", ardaItem.UnitID);
            return View(ardaItem);
        }

        // GET: ArdaItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArdaItem ardaItem = db.ArdaItems.Find(id);
            if (ardaItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArdaStatusID = new SelectList(db.ArdaStatus, "ArdaStatusID", "Status", ardaItem.ArdaStatusID);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "CategoryBrand", ardaItem.UnitID);
            return View(ardaItem);
        }

        // POST: ArdaItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArdaID,ArdaCategory,UnitID,Serial,Quantity,SiteID,Sitename,Cluster,Province,SiteAddress,SiteOwner,Contact,Reason,OriginalLocation,PickUpLocation,StagingArea,Origin,RDFControl,ArdaStatusID,Approver,Department,Remarks")] ArdaItem ardaItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ardaItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArdaStatusID = new SelectList(db.ArdaStatus, "ArdaStatusID", "Status", ardaItem.ArdaStatusID);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "CategoryBrand", ardaItem.UnitID);
            return View(ardaItem);
        }

        // GET: ArdaItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArdaItem ardaItem = db.ArdaItems.Find(id);
            if (ardaItem == null)
            {
                return HttpNotFound();
            }
            return View(ardaItem);
        }

        // POST: ArdaItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArdaItem ardaItem = db.ArdaItems.Find(id);
            db.ArdaItems.Remove(ardaItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Upload(int? id)
        {
            Session["ardaid"] = id;
            ArdaItem ardaitem = db.ArdaItems.Find(id);
            return View(ardaitem);
        }

        [HttpPost]
        public ActionResult SavePicture(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    int id = (int)Session["ardaid"];
                    string newfile = (id.ToString()) + ".pdf";
                    string path = Path.Combine(Server.MapPath("~/Pictures/ArdaItems/"), newfile);
                    file.SaveAs(path);

                    ViewBag.Message = "Upload succesfull";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            return RedirectToAction("Index");
        }

        public ActionResult View(int? id)
        {
            try
            {
                return File("~/Pictures/ArdaItems/" + id + ".pdf", "application/pdf");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "ERROR:" + ex.Message.ToString();
            }
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
