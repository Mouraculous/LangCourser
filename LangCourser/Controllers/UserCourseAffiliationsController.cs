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
    public class UserCourseAffiliationsController : Controller
    {
        private Model1 db = new Model1();

        // GET: UserCourseAffiliations
        public ActionResult Index()
        {
            var userCourseAffiliation = db.UserCourseAffiliation.Include(u => u.Course).Include(u => u.Users);
            return View(userCourseAffiliation.ToList());
        }

        public ActionResult SignToCorrect()
        {   
            return View();
        }

        public ActionResult SignToError()
        {
            return View();
        }

        // GET: UserCourseAffiliations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCourseAffiliation userCourseAffiliation = db.UserCourseAffiliation.Find(id);
            if (userCourseAffiliation == null)
            {
                return HttpNotFound();
            }
            return View(userCourseAffiliation);
        }

        public ActionResult Score(int? idU, int? idC)
        {
            if (idU == null || idC == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userCourseAffiliation = db.UserCourseAffiliation.Find(idU, idC);
            if (userCourseAffiliation == null)
            {
                return HttpNotFound();
            }
            ViewBag.idC = new SelectList(db.Course, "idC", "nameC", userCourseAffiliation.idC);
            ViewBag.idU = new SelectList(db.Users, "idU", "nameU", userCourseAffiliation.idU);
            return View(userCourseAffiliation);
        }

        // POST: UserCourseAffiliations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Score([Bind(Include = "scoreUCA")] UserCourseAffiliation userCourseAffiliation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userCourseAffiliation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idC = new SelectList(db.Course, "idC", "nameC", userCourseAffiliation.idC);
            ViewBag.idU = new SelectList(db.Users, "idU", "nameU", userCourseAffiliation.idU);
            return View(userCourseAffiliation);
        }

        // GET: UserCourseAffiliations/Create
        public ActionResult Create()
        {
            ViewBag.idC = new SelectList(db.Course, "idC", "nameC");
            ViewBag.idU = new SelectList(db.Users, "idU", "nameU");
            return View();
        }

        // POST: UserCourseAffiliations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idU,idC")] UserCourseAffiliation userCourseAffiliation)
        {
            if (ModelState.IsValid)
            {
                var ucaff = db.UserCourseAffiliation;
                Boolean is_in_list = false;
                foreach (var uca in ucaff)
                {
                    if(uca.idC == userCourseAffiliation.idC && uca.idU == userCourseAffiliation.idU)
                    {
                        is_in_list = true;
                    }
                }
                if (is_in_list)
                {

                    return RedirectToAction("SignToError"); 
                }
                else
                {
                    db.UserCourseAffiliation.Add(userCourseAffiliation);
                    db.SaveChanges();
                    return RedirectToAction("SignToCorrect");
                }           
            }

            ViewBag.idC = new SelectList(db.Course, "idC", "nameC", userCourseAffiliation.idC);
            ViewBag.idU = new SelectList(db.Users, "idU", "nameU", userCourseAffiliation.idU);
            return View(userCourseAffiliation);
        }

        // GET: UserCourseAffiliations/Edit/5
        public ActionResult Edit(int? idU, int? idC)
        {
            if (idU == null || idC == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userCourseAffiliation = db.UserCourseAffiliation.Find(idU, idC);
            if (userCourseAffiliation == null)
            {
                return HttpNotFound();
            }
            ViewBag.idC = new SelectList(db.Course, "idC", "nameC", userCourseAffiliation.idC);
            ViewBag.idU = new SelectList(db.Users, "idU", "nameU", userCourseAffiliation.idU);
            return View(userCourseAffiliation);
        }

        // POST: UserCourseAffiliations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idU,idC,scoreUCA")] UserCourseAffiliation userCourseAffiliation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userCourseAffiliation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idC = new SelectList(db.Course, "idC", "nameC", userCourseAffiliation.idC);
            ViewBag.idU = new SelectList(db.Users, "idU", "nameU", userCourseAffiliation.idU);
            return View(userCourseAffiliation);
        }

        // GET: UserCourseAffiliations/Delete/5
        public ActionResult Delete(int? idU, int? idC)
        {
            if (idU == null || idC == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCourseAffiliation userCourseAffiliation = db.UserCourseAffiliation.Find(idU, idC);
            if (userCourseAffiliation == null)
            {
                return HttpNotFound();
            }
            return View(userCourseAffiliation);
        }

        // POST: UserCourseAffiliations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idU, int idC)
        {
            var userCourseAffiliation = db.UserCourseAffiliation.Find(idU, idC);
            db.UserCourseAffiliation.Remove(userCourseAffiliation);
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
