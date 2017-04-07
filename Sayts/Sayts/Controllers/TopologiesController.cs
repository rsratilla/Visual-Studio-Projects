using Sayts.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Sayts.Controllers
{
    [Authorize]

    public class TopologiesController : Controller
    {
        DataClasses objData;
        public TopologiesController()
        {
            objData = new DataClasses();
        }

        public ActionResult Index()
        {
            var files = objData.GetFiles();
            return View(files);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {            
                if (file != null && file.ContentLength > 0)
                {
                    try
                    {
                        string path = Path.Combine(Server.MapPath("~/Pictures/Topology"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);

                        ViewBag.Message = "Upload succesfull";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                }
                else
                {
                    ViewBag.Message = "You have not specified a file.";
                }                

            return RedirectToAction("Index");
        }

        public ActionResult Download(string filename)
        {
            return File("~/Pictures/Topology/" + filename, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }
    }
}