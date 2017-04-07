using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;
using System;

namespace Sayts.Controllers
{
    [Authorize]

    public class SitesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sites
        public ActionResult Index(string searchString1, string searchString2, string searchString3, string searchString4, string searchString5)
        {
            if (!String.IsNullOrEmpty(searchString1))
            {                
                var siteDetails = db.SiteDetails.Where(s => s.Sites.SiteCode.ToUpper().Contains(searchString1.ToUpper()) ||
                        s.Sites.SiteName.ToUpper().Contains(searchString1.ToUpper()));

                if (siteDetails.Count() > 0)
                {
                    return View(siteDetails.ToList());
                }
                else
                {
                    Session["searchString"] = searchString1;
                    return RedirectToAction("Index2");
                }
            }

            if (!String.IsNullOrEmpty(searchString2))
            {
                return View(db.SiteDetails.Where(s => s.Clusters.ClusterName.ToUpper().Contains(searchString2.ToUpper())));
            }

            if (!String.IsNullOrEmpty(searchString3))
            {
                return View(db.SiteDetails.Where(s => s.SunID.ToUpper().Contains(searchString3.ToUpper())));
            }

            if (!String.IsNullOrEmpty(searchString4))
            {
                return View(db.SiteDetails.Where(s => s.Cities.CityName.ToUpper().Contains(searchString4.ToUpper())));
            }

            if (!String.IsNullOrEmpty(searchString5))
            {
                return View(db.SiteDetails.Where(s => s.Regions.RegionName.ToUpper().Contains(searchString5.ToUpper())));
            }

            return View();
        }


        public ActionResult Index2()
        {
            var ss = Session["searchString"].ToString();
            var sites = db.Sites.Where(s => s.SiteCode.ToUpper().Contains(ss.ToUpper()) ||
                s.SiteName.ToUpper().Contains(ss.ToUpper()));

            return View(sites);
        }

        // GET: Sites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SiteID,SiteCode,SiteName")] Site site)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Sites.Add(site);
                    db.SaveChanges();

                    Session["siteid"] = site.SiteID;
                    return RedirectToAction("Create", "SiteDetails");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }

            return View(site);            
        }

        // GET: Sites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = db.Sites.Find(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        // POST: Sites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SiteID,SiteCode,SiteName")] Site site)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(site).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }
            return View(site);
        }

        // GET: Sites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = db.Sites.Find(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        // POST: Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Site site = db.Sites.Find(id);
            db.Sites.Remove(site);
            db.SaveChanges();

            Session["sitecode"] = site.SiteCode;
            Session["sitename"] = site.SiteName;

            return RedirectToAction("Create", "Decommissions");
        }
        public ActionResult RedirectToSiteDetailsController(int? id)
        {
            Session["siteid"] = id;
            return RedirectToAction("Index", "SiteDetails");
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
