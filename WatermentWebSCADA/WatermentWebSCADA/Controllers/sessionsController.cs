using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatermentWebSCADA.Models;

namespace WatermentWebSCADA.Controllers
{
    public class SessionsController : Controller
    {
        private watermentdbEntities db = new watermentdbEntities();

        // GET: sessions
        public ActionResult Index()
        {
            var sessions = db.sessions.Include(s => s.users);
            return View(sessions.ToList());
        }

        // GET: sessions/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sessions sessions = db.sessions.Find(id);
            if (sessions == null)
            {
                return HttpNotFound();
            }
            return View(sessions);
        }

        // GET: sessions/Create
        public ActionResult Create()
        {
            ViewBag.users_Email = new SelectList(db.users, "Email", "PasswordHash");
            return View();
        }

        // POST: sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoggedIn,LoggedOut,users_Email")] sessions sessions)
        {
            if (ModelState.IsValid)
            {
                db.sessions.Add(sessions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.users_Email = new SelectList(db.users, "Email", "PasswordHash", sessions.users_Email);
            return View(sessions);
        }

        // GET: sessions/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sessions sessions = db.sessions.Find(id);
            if (sessions == null)
            {
                return HttpNotFound();
            }
            ViewBag.users_Email = new SelectList(db.users, "Email", "PasswordHash", sessions.users_Email);
            return View(sessions);
        }

        // POST: sessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoggedIn,LoggedOut,users_Email")] sessions sessions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.users_Email = new SelectList(db.users, "Email", "PasswordHash", sessions.users_Email);
            return View(sessions);
        }

        // GET: sessions/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sessions sessions = db.sessions.Find(id);
            if (sessions == null)
            {
                return HttpNotFound();
            }
            return View(sessions);
        }

        // POST: sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            sessions sessions = db.sessions.Find(id);
            db.sessions.Remove(sessions);
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
