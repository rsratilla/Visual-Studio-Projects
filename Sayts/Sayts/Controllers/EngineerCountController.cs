using OfficeOpenXml;
using Sayts.Models;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Sayts.Controllers
{
    [Authorize]

    public class EngineerCountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EngineerCount
        public ActionResult Index()
        {
            IQueryable<ClusterEmployeeGroup> data = from emp in db.Employees
                                                group emp by emp.ClusterID into clusterGroup
                                                select new ClusterEmployeeGroup()
                                                {
                                                    ClusterNo = clusterGroup.Key,
                                                    EmployeeCount = clusterGroup.Count()
                                                };

            return View(data.ToList());
        }

        public ActionResult Details(int? id)
        {
            var empDetails = from e in db.Employees select e;

            if (empDetails == null)
            {
                return HttpNotFound();
            }

            if (id == null)
            {
                empDetails = empDetails.Where(s => s.ClusterID != null);
            }
            else
            {
                empDetails = empDetails.Where(s => s.ClusterID == id);
            }

            return View(empDetails);
        }

        public ActionResult ExportToExcel()
        {
            var empDetails = from e in db.Employees                             
                             select new
                             {
                                 e.Clusters.ClusterName,
                                 e.EmployeeNo,
                                 e.GivenName,
                                 e.MiddleName,
                                 e.LastName,
                                 e.EmployeePositions.EmployeePositionName,
                                 e.EmploymentStatus.EmploymentStatusName,
                                 e.SubTeams.SubTeamName,
                                 e.DateHired,
                                 e.ImmediateHead,
                                 e.MobileNo,
                                 e.DomainAccount,
                                 e.EmailAddress,
                                 e.OfficeAddress,
                                 e.Remarks
                             };

            ExcelPackage Excel = new ExcelPackage();
            var workSheet = Excel.Workbook.Worksheets.Add("Employees");
            workSheet.Cells[1, 1].LoadFromCollection(empDetails, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=VisayasEmployees.xlsx");
                Excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }

            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}