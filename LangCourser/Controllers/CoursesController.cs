﻿using System;
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
    public class CoursesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Courses
        public ActionResult Index(string sortBy, string searchBy, string search)
        {
            ViewBag.SortNameParam = sortBy == "name_desc" ? "name" : "name_desc";
            ViewBag.SortLangParam = sortBy == "lang" ? "lang_desc" : "lang";
            var course = db.Course.AsQueryable();

            if (searchBy == "name")
            {
                course = course.Where(x => x.nameC.StartsWith(search) || search == null);
            }
            else
            {
                course = course.Where(x => x.Language.nameL.StartsWith(search) || search == null);
            }

            switch (sortBy)
            {
                case "name":
                    course = course.OrderBy(x => x.nameC);
                    break;
                case "name_desc":
                    course = course.OrderByDescending(x => x.nameC);
                    break;
                case "lang":
                    course = course.OrderBy(x => x.Language.nameL);
                    break;
                case "lang_desc":
                    course = course.OrderByDescending(x => x.Language.nameL);
                    break;
                default:
                    course = course.OrderBy(x => x.nameC);
                    break;
            }

            return View(course.ToList());
        }

        // GET: MyCourses
        public ActionResult MyCourses(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = db.Course.AsQueryable();
            var ucaffs = db.UserCourseAffiliation.AsQueryable();

            ucaffs = ucaffs.Where(x => x.idU == id);
            var affs = new HashSet<int>();

            foreach(var a in ucaffs)
            {
                affs.Add(a.idC);
            }

            course = course.Where(x => affs.Contains(x.idC) || x.lecturerC==id);
            return View(course.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        public ActionResult AdvancedDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        public ActionResult Participants(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            var users = db.Users.AsQueryable();
            var ucaffs = db.UserCourseAffiliation.AsQueryable();
            ucaffs = ucaffs.Where(x => x.idC == id);
            var hash = new HashSet<int>();
            foreach(var item in ucaffs)
            {
                hash.Add(item.idU);
            }
            var users_list = new List<Users>();
            foreach(var item in users)
            {
                if (hash.Contains(item.idU))
                {
                    users_list.Add(item);
                }
            }

            return View(users_list);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.idL = new SelectList(db.Language, "idL", "nameL");
            var users = db.Users.AsQueryable();
            users = users.Where(x => x.UserAffiliation.nameUA == "Lecturer");

            ViewBag.lecturerC = new SelectList(users, "idU", "nameU");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idC,nameC,lecturerC,idL")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Course.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idL = new SelectList(db.Language, "idL", "nameL", course.idL);
            ViewBag.lecturerC = new SelectList(db.Users, "idU", "nameU", course.lecturerC);
            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.idL = new SelectList(db.Language, "idL", "nameL", course.idL);
            ViewBag.lecturerC = new SelectList(db.Users, "idU", "nameU", course.lecturerC);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idC,nameC,lecturerC,idL")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idL = new SelectList(db.Language, "idL", "nameL", course.idL);
            ViewBag.lecturerC = new SelectList(db.Users, "idU", "nameU", course.lecturerC);
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Course.Find(id);
            db.Course.Remove(course);
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
