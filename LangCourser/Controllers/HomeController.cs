using System.Web.Mvc;

namespace ISBD_project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Some description";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us with:";

            return View();
        }
    }
}