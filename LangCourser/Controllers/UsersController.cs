using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ISBD_project.Models;

namespace ISBD_project.Controllers
{
    public class UsersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Users
        public ActionResult Index(string sortBy, string searchBy, string search)
        {
            ViewBag.SortNameParam = sortBy == "name_desc" ? "name" : "name_desc";
            ViewBag.SortSurnameParam = sortBy == "surname" ? "surname_desc" : "surname";
            ViewBag.SortAgeParam = sortBy == "age" ? "age_desc" : "age";
            ViewBag.SortGenderParam = sortBy == "gender" ? "gender_desc" : "gender";
            ViewBag.SortAffParam = sortBy == "aff" ? "aff_desc" : "aff";
            var users = db.Users.AsQueryable();

            switch (searchBy)
            {
                case "name":
                    users = users.Where(x => x.nameU.StartsWith(search) || search == null);
                    break;
                case "surname":
                    users = users.Where(x => x.surnameU.StartsWith(search) || search == null);
                    break;
                case "aff":
                    users = users.Where(x => x.UserAffiliation.nameUA.StartsWith(search) || search == null);
                    break;
            }

            switch (sortBy)
            {
                case "name":
                    users = users.OrderBy(x => x.nameU);
                    break;
                case "name_desc":
                    users = users.OrderByDescending(x => x.nameU);
                    break;
                case "surname":
                    users = users.OrderBy(x => x.surnameU);
                    break;
                case "surname_desc":
                    users = users.OrderByDescending(x => x.surnameU);
                    break;
                case "age":
                    users = users.OrderBy(x => x.ageU);
                    break;
                case "age_desc":
                    users = users.OrderByDescending(x => x.ageU);
                    break;
                case "gender":
                    users = users.OrderBy(x => x.genderU);
                    break;
                case "gender_desc":
                    users = users.OrderByDescending(x => x.genderU);
                    break;
                case "aff":
                    users = users.OrderBy(x => x.UserAffiliation.nameUA);
                    break;
                case "aff_desc":
                    users = users.OrderByDescending(x => x.UserAffiliation.nameUA);
                    break;
                default:
                    users = users.OrderBy(x => x.nameU);
                    break;
            }
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.idUA = new SelectList(db.UserAffiliation, "idUA", "nameUA");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idU,nameU,surnameU,emailU,BirthDateU,genderU,idUA")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idUA = new SelectList(db.UserAffiliation, "idUA", "nameUA", users.idUA);
            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.idUA = new SelectList(db.UserAffiliation, "idUA", "nameUA", users.idUA);
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idU,nameU,surnameU,emailU,BirthDateU,genderU,idUA")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idUA = new SelectList(db.UserAffiliation, "idUA", "nameUA", users.idUA);
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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
