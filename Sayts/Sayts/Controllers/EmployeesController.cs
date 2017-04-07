using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sayts.Models;

namespace Sayts.Controllers
{
    [Authorize]

    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Clusters).Include(e => e.EmployeePositions).Include(e => e.EmploymentStatus).Include(e => e.SubTeams);
            return View(employees.ToList());
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.ClusterID = new SelectList(db.Clusters, "ClusterID", "ClusterName");
            ViewBag.EmployeePositionID = new SelectList(db.EmployeePositions, "EmployeePositionID", "EmployeePositionName");
            ViewBag.EmploymentStatusID = new SelectList(db.EmploymentStatus, "EmploymentStatusID", "EmploymentStatusName");
            ViewBag.SubTeamID = new SelectList(db.SubTeams, "SubTeamID", "SubTeamName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeNo,GivenName,MiddleName,LastName,DateHired,ImmediateHead,MobileNo,DomainAccount,EmailAddress,OfficeAddress,EmploymentStatusID,EmployeePositionID,SubTeamID,Remarks,ClusterID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClusterID = new SelectList(db.Clusters, "ClusterID", "ClusterName", employee.ClusterID);
            ViewBag.EmployeePositionID = new SelectList(db.EmployeePositions, "EmployeePositionID", "EmployeePositionName", employee.EmployeePositionID);
            ViewBag.EmploymentStatusID = new SelectList(db.EmploymentStatus, "EmploymentStatusID", "EmploymentStatusName", employee.EmploymentStatusID);
            ViewBag.SubTeamID = new SelectList(db.SubTeams, "SubTeamID", "SubTeamName", employee.SubTeamID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClusterID = new SelectList(db.Clusters, "ClusterID", "ClusterName", employee.ClusterID);
            ViewBag.EmployeePositionID = new SelectList(db.EmployeePositions, "EmployeePositionID", "EmployeePositionName", employee.EmployeePositionID);
            ViewBag.EmploymentStatusID = new SelectList(db.EmploymentStatus, "EmploymentStatusID", "EmploymentStatusName", employee.EmploymentStatusID);
            ViewBag.SubTeamID = new SelectList(db.SubTeams, "SubTeamID", "SubTeamName", employee.SubTeamID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeNo,GivenName,MiddleName,LastName,DateHired,ImmediateHead,MobileNo,DomainAccount,EmailAddress,OfficeAddress,EmploymentStatusID,EmployeePositionID,SubTeamID,Remarks,ClusterID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClusterID = new SelectList(db.Clusters, "ClusterID", "ClusterName", employee.ClusterID);
            ViewBag.EmployeePositionID = new SelectList(db.EmployeePositions, "EmployeePositionID", "EmployeePositionName", employee.EmployeePositionID);
            ViewBag.EmploymentStatusID = new SelectList(db.EmploymentStatus, "EmploymentStatusID", "EmploymentStatusName", employee.EmploymentStatusID);
            ViewBag.SubTeamID = new SelectList(db.SubTeams, "SubTeamID", "SubTeamName", employee.SubTeamID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
