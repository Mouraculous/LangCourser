using System;
using ISBD_project.Models;
using ISBD_project.Resources;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISBD_project.Session;

namespace ISBD_project.Controllers
{
    public class AccountController : Controller
    {

        private Model1 db = new Model1();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Login(Account objUser)
        {
            if (!ModelState.IsValid) return RedirectToRoute("Home");
            var obj = db.Account.FirstOrDefault(a => a.loginA.Equals(objUser.loginA) && a.passwordA.Equals(objUser.passwordA));
            if (obj != null)
            {
                var cookieName = new HttpCookie(Strings.Name)
                {
                    Value = db.Users.First(f => f.idU == obj.idU).nameU
                };
                var cookieRole = new HttpCookie(Strings.Role)
                {
                    Value = db.UserAffiliation
                        .FirstOrDefault(f => f.idUA == db.Users.FirstOrDefault(a => a.idU == obj.idU).idUA)?.nameUA
                };
                var cookieLogged = new HttpCookie(Strings.IsLogged)
                {
                    Value = "true"
                };
                var cookieId = new HttpCookie(Strings.AccountId)
                {
                    Value = obj.idA.ToString()
                };
                var cookieUserId = new HttpCookie(Strings.UserId)
                {
                    Value = obj.idA.ToString()
                };
                Response.Cookies.Add(cookieName);
                Response.Cookies.Add(cookieRole);
                Response.Cookies.Add(cookieLogged);
                Response.Cookies.Add(cookieId);
                Response.Cookies.Add(cookieUserId);
                if (TempData.ContainsKey("msg")) TempData.Remove("msg");
                return RedirectToAction("Index", "Home");
            }

            TempData["msg"] = $"<font color=\"red\">{Shared.LoginFailed}</font>";
            return RedirectToAction("Index");
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        
        public ActionResult RegisterUser(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = new Users
                {
                    ageU = registerModel.AgeU,
                    emailU = registerModel.EmailU,
                    nameU = registerModel.NameU,
                    surnameU = registerModel.SurnameU,
                    idUA = registerModel.IdUa,
                    genderU = registerModel.GenderU
                };
                db.Users.Add(user);
                db.SaveChanges();

                var account = new Account
                {
                    loginA = registerModel.LoginA,
                    passwordA = registerModel.PasswordA,
                    idU = db.Users.First(f => f.emailU == registerModel.EmailU).idU
                };
                db.Account.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Logout()
        {
            var nameCookie = HttpContext.Request.Cookies[Strings.Name];
            HttpContext.Response.Cookies.Remove(Strings.Name);
            nameCookie.Expires = DateTime.Now.AddDays(-10);
            nameCookie.Value = null;
            HttpContext.Response.SetCookie(nameCookie);

            var accountCookie = HttpContext.Request.Cookies[Strings.AccountId];
            HttpContext.Response.Cookies.Remove(Strings.AccountId);
            accountCookie.Expires = DateTime.Now.AddDays(-10);
            accountCookie.Value = null;
            HttpContext.Response.SetCookie(accountCookie);

            var userCookie = HttpContext.Request.Cookies[Strings.UserId];
            HttpContext.Response.Cookies.Remove(Strings.UserId);
            userCookie.Expires = DateTime.Now.AddDays(-10);
            userCookie.Value = null;
            HttpContext.Response.SetCookie(userCookie);

            var roleCookie = HttpContext.Request.Cookies[Strings.Role];
            HttpContext.Response.Cookies.Remove(Strings.Role);
            roleCookie.Expires = DateTime.Now.AddDays(-10);
            roleCookie.Value = null;
            HttpContext.Response.SetCookie(roleCookie);

            var isLoggedCookie = HttpContext.Request.Cookies[Strings.IsLogged];
            HttpContext.Response.Cookies.Remove(Strings.IsLogged);
            isLoggedCookie.Expires = DateTime.Now.AddDays(-10);
            isLoggedCookie.Value = null;
            HttpContext.Response.SetCookie(isLoggedCookie);

            return RedirectToAction("Index"); ;
        }
    }
}