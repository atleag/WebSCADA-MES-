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
    public class equipmentsController : Controller
    {
        private watermentdbEntities db = new watermentdbEntities();

        // GET: equipments
        public ActionResult Index()
        {
            var equipment = db.equipment.Include(e => e.facilities);
            return View(equipment.ToList());
        }

        // GET: equipments/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipment equipment = db.equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // GET: equipments/Create
        public ActionResult Create()
        {
            ViewBag.Facilities_Name = new SelectList(db.facilities, "Name", "IP");
            return View();
        }

        // POST: equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Tag,SIUnits,Description,LastCalibrated,Facilities_Name")] equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.equipment.Add(equipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Facilities_Name = new SelectList(db.facilities, "Name", "IP", equipment.Facilities_Name);
            return View(equipment);
        }

        // GET: equipments/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipment equipment = db.equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Facilities_Name = new SelectList(db.facilities, "Name", "IP", equipment.Facilities_Name);
            return View(equipment);
        }

        // POST: equipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Tag,SIUnits,Description,LastCalibrated,Facilities_Name")] equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Facilities_Name = new SelectList(db.facilities, "Name", "IP", equipment.Facilities_Name);
            return View(equipment);
        }

        // GET: equipments/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipment equipment = db.equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            equipment equipment = db.equipment.Find(id);
            db.equipment.Remove(equipment);
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
