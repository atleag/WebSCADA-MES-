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
    public class facilitiesController : Controller
    {
        private watermentdbEntities db = new watermentdbEntities();

        // GET: facilities
        public ActionResult Index()
        {
            //mongod
            var facilities = db.facilities.Include(f => f.location);
            return View(facilities.ToList());
        }

        // GET: facilities/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            facilities facilities = db.facilities.Find(id);
            if (facilities == null)
            {
                return HttpNotFound();
            }
            return View(facilities);
        }

        // GET: facilities/Create
        public ActionResult Create()
        {
            ViewBag.Location_Address = new SelectList(db.location, "Address", "County");
            return View();
        }

        // POST: facilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,IP,Location_Address,Location_Postcode,Location_Country_CountryName")] facilities facilities)
        {
            if (ModelState.IsValid)
            {
                db.facilities.Add(facilities);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Location_Address = new SelectList(db.location, "Address", "County", facilities.location_Address);
            return View(facilities);
        }

        // GET: facilities/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            facilities facilities = db.facilities.Find(id);
            if (facilities == null)
            {
                return HttpNotFound();
            }
            ViewBag.Location_Address = new SelectList(db.location, "Address", "County", facilities.location_Address);
            return View(facilities);
        }

        // POST: facilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,IP,Location_Address,Location_Postcode,Location_Country_CountryName")] facilities facilities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facilities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Location_Address = new SelectList(db.location, "Address", "County", facilities.location_Address);
            return View(facilities);
        }

        // GET: facilities/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            facilities facilities = db.facilities.Find(id);
            if (facilities == null)
            {
                return HttpNotFound();
            }
            return View(facilities);
        }

        // POST: facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            facilities facilities = db.facilities.Find(id);
            db.facilities.Remove(facilities);
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
