using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;
using System.Web;
using System.IO;

namespace Sayts.Controllers
{
    [Authorize]

    public class PMRMastersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PMRMasters
        public ActionResult Index()
        {            
            var pMRMasters = db.PMRMasters.Include(p => p.Employees).Include(p => p.Sites);
            return View(pMRMasters.ToList());
        }

        // GET: PMRDeficiencies
        public ActionResult Details(int? id)
        {
            Session["PMRMasterID"] = id;
            return RedirectToAction("Index", "PMRDeficiencies");
        }

        // GET: PMRMasters/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeNo = new SelectList(db.Employees, "EmployeeNo", "FullName");
            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName");
            return View();
        }

        // POST: PMRMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PMRMasterID,PMRDate,SiteID,EmployeeNo,SiteClassification,PMRType,WithGS,CoordinationNo,Lilo,MainsSimulStatus,MainsSimulRemarks,Remarks")] PMRMaster pMRMaster)
        {
            if (ModelState.IsValid)
            {
                db.PMRMasters.Add(pMRMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeNo = new SelectList(db.Employees, "EmployeeNo", "FullName", pMRMaster.EmployeeNo);
            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", pMRMaster.SiteID);
            return View(pMRMaster);
        }

        // GET: PMRMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PMRMaster pMRMaster = db.PMRMasters.Find(id);
            if (pMRMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeNo = new SelectList(db.Employees, "EmployeeNo", "FullName", pMRMaster.EmployeeNo);
            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", pMRMaster.SiteID);
            return View(pMRMaster);
        }

        // POST: PMRMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PMRMasterID,PMRDate,SiteID,EmployeeNo,SiteClassification,PMRType,WithGS,CoordinationNo,Lilo,MainsSimulStatus,MainsSimulRemarks,Remarks")] PMRMaster pMRMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pMRMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeNo = new SelectList(db.Employees, "EmployeeNo", "FullName", pMRMaster.EmployeeNo);
            ViewBag.SiteID = new SelectList(db.Sites, "SiteID", "SiteFullName", pMRMaster.SiteID);
            return View(pMRMaster);
        }

        // GET: PMRMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PMRMaster pMRMaster = db.PMRMasters.Find(id);
            if (pMRMaster == null)
            {
                return HttpNotFound();
            }
            return View(pMRMaster);
        }

        // POST: PMRMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PMRMaster pMRMaster = db.PMRMasters.Find(id);
            db.PMRMasters.Remove(pMRMaster);
            db.SaveChanges();

            string picturePath = Request.MapPath("~/Pictures/PMRPictures/Master/" + id.ToString() + ".pdf");
            if (System.IO.File.Exists(picturePath))
            {
                System.IO.File.Delete(picturePath);
            }

            return RedirectToAction("Index");
        }

        public ActionResult View(int? id)
        {
            try
            {
                return File("~/Pictures/PMRPictures/Master/" + id + ".pdf", "application/pdf");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "ERROR:" + ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Upload(int? id)
        {
            Session["pmrmasterid"] = id;
            PMRMaster pMRMaster = db.PMRMasters.Find(id);
            return View(pMRMaster);
        }

        [HttpPost]
        public ActionResult SavePicture(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    int id = (int)Session["pmrmasterid"];
                    string newfile = (id.ToString()) + ".pdf";
                    string path = Path.Combine(Server.MapPath("~/Pictures/PMRPictures/Master/"), newfile);
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
