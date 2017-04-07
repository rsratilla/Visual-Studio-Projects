using System;
using System.Data;
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

    public class PMRDeficienciesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PMRDeficiencies
        public ActionResult Index()
        {
            var pmasterid = (int)Session["PMRMasterID"];
            ViewBag.PMRMasterID = pmasterid;
            var pMRDeficiencies = db.PMRDeficiencies.Include(p => p.PMRCheckLists)
                .Include(p => p.PMRMasters).Where(d => d.PMRMasterID == pmasterid);
            return View(pMRDeficiencies.ToList());
        }

        // GET: PMRDeficiencies/Create
        public ActionResult Create(int? id)
        {
            var pmasterid = (int)Session["PMRMasterID"];
            ViewBag.PMRMasterID = pmasterid;

            ViewBag.DeficiencyID = new SelectList(db.PMRCheckLists, "DeficiencyID", "CLDefinition");
            ViewBag.PMRMasterID = new SelectList(db.PMRMasters.Where(d => d.PMRMasterID == pmasterid), "PMRMasterID", "PMRMasterID");
            return View();
        }

        // POST: PMRDeficiencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PMRDeficiencyID,PMRMasterID,DeficiencyID,Remarks")] PMRDeficiency pMRDeficiency)
        {
            var pmasterid = (int)Session["PMRMasterID"];
            ViewBag.PMRMasterID = pmasterid;

            if (ModelState.IsValid)
            {
                db.PMRDeficiencies.Add(pMRDeficiency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeficiencyID = new SelectList(db.PMRCheckLists, "DeficiencyID", "CLDefinition", pMRDeficiency.DeficiencyID);
            ViewBag.PMRMasterID = new SelectList(db.PMRMasters.Where(d => d.PMRMasterID == pmasterid), "PMRMasterID", "PMRMasterID", pMRDeficiency.PMRMasterID);
            return View(pMRDeficiency);
        }

        // GET: PMRDeficiencies/Edit/5
        public ActionResult Edit(int? id, int? mid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PMRDeficiency pMRDeficiency = db.PMRDeficiencies.Find(id);
            if (pMRDeficiency == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeficiencyID = new SelectList(db.PMRCheckLists, "DeficiencyID", "CLDefinition", pMRDeficiency.DeficiencyID);
            ViewBag.PMRMasterID = new SelectList(db.PMRMasters.Where(d => d.PMRMasterID == mid), "PMRMasterID", "PMRMasterID", pMRDeficiency.PMRMasterID);
            return View(pMRDeficiency);
        }

        // POST: PMRDeficiencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PMRDeficiencyID,PMRMasterID,DeficiencyID,Remarks")] PMRDeficiency pMRDeficiency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pMRDeficiency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeficiencyID = new SelectList(db.PMRCheckLists, "DeficiencyID", "CLDefinition", pMRDeficiency.DeficiencyID);
            return View(pMRDeficiency);
        }

        // GET: PMRDeficiencies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PMRDeficiency pMRDeficiency = db.PMRDeficiencies.Find(id);
            if (pMRDeficiency == null)
            {
                return HttpNotFound();
            }
            return View(pMRDeficiency);
        }

        // POST: PMRDeficiencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PMRDeficiency pMRDeficiency = db.PMRDeficiencies.Find(id);
            db.PMRDeficiencies.Remove(pMRDeficiency);
            db.SaveChanges();

            string picturePath = Request.MapPath("~/Pictures/PMRPictures/Details/" + id.ToString() + ".pdf");
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
                return File("~/Pictures/PMRPictures/Details/" + id.ToString() + ".pdf", "application/pdf");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "ERROR:" + ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Upload(int? id)
        {
            Session["pmrdeficiencyid"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult SavePicture(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    int id = (int)Session["pmrdeficiencyid"];
                    string newfile = (id.ToString()) + ".pdf";
                    string path = Path.Combine(Server.MapPath("~/Pictures/Pictures/PMRPictures/Details/"), newfile);
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
