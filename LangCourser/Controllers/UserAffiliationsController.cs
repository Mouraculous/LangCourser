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
    public class UserAffiliationsController : Controller
    {
        private Model1 db = new Model1();

        // GET: UserAffiliations
        public ActionResult Index()
        {
            return View(db.UserAffiliation.ToList());
        }

        // GET: UserAffiliations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAffiliation userAffiliation = db.UserAffiliation.Find(id);
            if (userAffiliation == null)
            {
                return HttpNotFound();
            }
            return View(userAffiliation);
        }

        // GET: UserAffiliations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserAffiliations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUA,nameUA")] UserAffiliation userAffiliation)
        {
            if (ModelState.IsValid)
            {
                db.UserAffiliation.Add(userAffiliation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userAffiliation);
        }

        // GET: UserAffiliations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAffiliation userAffiliation = db.UserAffiliation.Find(id);
            if (userAffiliation == null)
            {
                return HttpNotFound();
            }
            return View(userAffiliation);
        }

        // POST: UserAffiliations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUA,nameUA")] UserAffiliation userAffiliation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAffiliation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userAffiliation);
        }

        // GET: UserAffiliations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAffiliation userAffiliation = db.UserAffiliation.Find(id);
            if (userAffiliation == null)
            {
                return HttpNotFound();
            }
            return View(userAffiliation);
        }

        // POST: UserAffiliations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAffiliation userAffiliation = db.UserAffiliation.Find(id);
            db.UserAffiliation.Remove(userAffiliation);
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
