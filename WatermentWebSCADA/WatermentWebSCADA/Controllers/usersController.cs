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
    public class usersController : Controller
    {
        private watermentdbEntities db = new watermentdbEntities();

        // GET: users
        public ActionResult Index()
        {

            var users = db.users.Include(u => u.location);
            return View(users.ToList());
        }

        // GET: users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: users/Create
        public ActionResult Create()
        {
            ViewBag.location_Address = new SelectList(db.location, "Address", "County");
            return View();
        }

        // POST: users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email,EmailConfirmed,PasswordHash,SecurityStamp,FirstName,LastName,Phone,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,Username,location_Address,location_Postcode,location_Country_CountryName")] users users)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.location_Address = new SelectList(db.location, "Address", "County", users.location_Address);
            return View(users);
        }

        // GET: users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.location_Address = new SelectList(db.location, "Address", "County", users.location_Address);
            return View(users);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Email,EmailConfirmed,PasswordHash,SecurityStamp,FirstName,LastName,Phone,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,Username,location_Address,location_Postcode,location_Country_CountryName")] users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.location_Address = new SelectList(db.location, "Address", "County", users.location_Address);
            return View(users);
        }

        // GET: users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            users users = db.users.Find(id);
            db.users.Remove(users);
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
