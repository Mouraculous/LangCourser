using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISBD_project.Models;
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
                SessionMocker.Instance.LoggedUser.Account = obj;
                SessionMocker.Instance.LoggedUser.Role = db.UserAffiliation.FirstOrDefault(f => f.idUA == db.Users.FirstOrDefault(a => a.idU == obj.idU).idUA)?.nameUA;
                return RedirectToAction("Index", "Home");
            }
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
            SessionMocker.Instance.LoggedUser = null;
            return RedirectToAction("Index"); ;
        }
    }
}