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
    public class measurementsController : Controller
    {
        private watermentdbEntities db = new watermentdbEntities();

        // GET: measurements
        public ActionResult Index()
        {
            var measurement = db.measurement.Include(m => m.equipment);
            return View(measurement.ToList());
        }

        // GET: measurements/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            measurement measurement = db.measurement.Find(id);
            if (measurement == null)
            {
                return HttpNotFound();
            }
            return View(measurement);
        }

        // GET: measurements/Create
        public ActionResult Create()
        {
            ViewBag.Equipment_Tag = new SelectList(db.equipment, "Tag", "SIUnits");
            return View();
        }

        // POST: measurements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Timestamp,ProcessValue,Equipment_Tag,Equipment_Facilities_Name")] measurement measurement)
        {
            if (ModelState.IsValid)
            {
                db.measurement.Add(measurement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Equipment_Tag = new SelectList(db.equipment, "Tag", "SIUnits", measurement.Equipment_Tag);
            return View(measurement);
        }

        // GET: measurements/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            measurement measurement = db.measurement.Find(id);
            if (measurement == null)
            {
                return HttpNotFound();
            }
            ViewBag.Equipment_Tag = new SelectList(db.equipment, "Tag", "SIUnits", measurement.Equipment_Tag);
            return View(measurement);
        }

        // POST: measurements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Timestamp,ProcessValue,Equipment_Tag,Equipment_Facilities_Name")] measurement measurement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(measurement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Equipment_Tag = new SelectList(db.equipment, "Tag", "SIUnits", measurement.Equipment_Tag);
            return View(measurement);
        }

        // GET: measurements/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            measurement measurement = db.measurement.Find(id);
            if (measurement == null)
            {
                return HttpNotFound();
            }
            return View(measurement);
        }

        // POST: measurements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            measurement measurement = db.measurement.Find(id);
            db.measurement.Remove(measurement);
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
