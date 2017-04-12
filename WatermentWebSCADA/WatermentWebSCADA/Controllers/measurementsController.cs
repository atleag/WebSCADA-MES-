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
            var measurements = db.measurements.Include(m => m.equipments);
            return View(measurements.ToList());
        }

        // GET: measurements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            measurements measurements = db.measurements.Find(id);
            if (measurements == null)
            {
                return HttpNotFound();
            }
            return View(measurements);
        }

        // GET: measurements/Create
        public ActionResult Create()
        {
            ViewBag.equipments_Id = new SelectList(db.equipments, "Id", "Tag");
            return View();
        }

        // POST: measurements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Recorded,ProcessValue,equipments_Id,equipments_facilities_Id")] measurements measurements)
        {
            if (ModelState.IsValid)
            {
                db.measurements.Add(measurements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.equipments_Id = new SelectList(db.equipments, "Id", "Tag", measurements.equipments_Id);
            return View(measurements);
        }

        // GET: measurements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            measurements measurements = db.measurements.Find(id);
            if (measurements == null)
            {
                return HttpNotFound();
            }
            ViewBag.equipments_Id = new SelectList(db.equipments, "Id", "Tag", measurements.equipments_Id);
            return View(measurements);
        }

        // POST: measurements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Recorded,ProcessValue,equipments_Id,equipments_facilities_Id")] measurements measurements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(measurements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.equipments_Id = new SelectList(db.equipments, "Id", "Tag", measurements.equipments_Id);
            return View(measurements);
        }

        // GET: measurements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            measurements measurements = db.measurements.Find(id);
            if (measurements == null)
            {
                return HttpNotFound();
            }
            return View(measurements);
        }

        // POST: measurements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            measurements measurements = db.measurements.Find(id);
            db.measurements.Remove(measurements);
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
