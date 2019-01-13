using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ISBD_project.Controllers
{
    public class WebLanguageController : Controller
    {
        // GET: WebLanguage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Change(string LanguageAbbrevation)
        {
            if(LanguageAbbrevation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbrevation);
            }
            var cookie = new HttpCookie("Language")
            {
                Value = LanguageAbbrevation
            };
            Response.Cookies.Add(cookie);

            return View("~/Views/Home/Index.cshtml");
        }
    }
}