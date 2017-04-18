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
    public class facilities2Controller : Controller
    {
        private watermentdbEntities db = new watermentdbEntities();

        // GET: facilities2
        public ActionResult Index()
        {
            var facilities = db.facilities.Include(f => f.locations);
            return View(facilities.ToList());
        }

        // GET: facilities2/Details/5
        public ActionResult Details(int? id)
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

        // GET: facilities2/Create
        public ActionResult Create()
        {
            ViewBag.locations_Id = new SelectList(db.locations, "Id", "StreetAddress");
            return View();
        }

        // POST: facilities2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,IP,Domain,locations_Id,locations_countries_Id,locations_countries_continents_Id")] facilities facilities)
        {
            if (ModelState.IsValid)
            {
                db.facilities.Add(facilities);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.locations_Id = new SelectList(db.locations, "Id", "StreetAddress", facilities.locations_Id);
            return View(facilities);
        }

        // GET: facilities2/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.locations_Id = new SelectList(db.locations, "Id", "StreetAddress", facilities.locations_Id);
            return View(facilities);
        }

        // POST: facilities2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IP,Domain,locations_Id,locations_countries_Id,locations_countries_continents_Id")] facilities facilities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facilities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.locations_Id = new SelectList(db.locations, "Id", "StreetAddress", facilities.locations_Id);
            return View(facilities);
        }

        // GET: facilities2/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: facilities2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            facilities facilities = db.facilities.Find(id);
            db.facilities.Remove(facilities);
            db.SaveChanges();
            return RedirectToAction("FacilityOverview");
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
