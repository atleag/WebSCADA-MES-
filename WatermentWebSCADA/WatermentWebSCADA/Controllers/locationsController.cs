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
    public class locationsController : Controller
    {
        private watermentdbEntities db = new watermentdbEntities();

        // GET: locations
        public ActionResult Index()
        {
            var location = db.location.Include(l => l.country);
            return View(location.ToList());
        }

        // GET: locations/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            location location = db.location.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: locations/Create
        public ActionResult Create()
        {
            ViewBag.Country_CountryName = new SelectList(db.country, "CountryName", "CountryName");
            return View();
        }

        // POST: locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Address,Postcode,County,Country_CountryName")] location location)
        {
            if (ModelState.IsValid)
            {
                db.location.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Country_CountryName = new SelectList(db.country, "CountryName", "CountryName", location.Country_CountryName);
            return View(location);
        }

        // GET: locations/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            location location = db.location.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            ViewBag.Country_CountryName = new SelectList(db.country, "CountryName", "CountryName", location.Country_CountryName);
            return View(location);
        }

        // POST: locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Address,Postcode,County,Country_CountryName")] location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Country_CountryName = new SelectList(db.country, "CountryName", "CountryName", location.Country_CountryName);
            return View(location);
        }

        // GET: locations/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            location location = db.location.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            location location = db.location.Find(id);
            db.location.Remove(location);
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
