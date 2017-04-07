using System.Web.Mvc;

namespace Sayts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Smart Cell Sites Information Database";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Raul S. Ratilla";

            return View();
        }
    }
}