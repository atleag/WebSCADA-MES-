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
    //adwwadwadawdawd
    public class alarmsController : Controller
    {
        private watermentdbEntities db = new watermentdbEntities();

        // GET: alarms
        public ActionResult Index()
        {
            var alarms = db.alarms.Include(a => a.equipment);
            return View(alarms.ToList());
        }

        // GET: alarms/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alarms alarms = db.alarms.Find(id);
            if (alarms == null)
            {
                return HttpNotFound();
            }
            return View(alarms);
        }

        // GET: alarms/Create
        public ActionResult Create()
        {
            ViewBag.Equipment_Tag = new SelectList(db.equipment, "Tag", "SIUnits");
            return View();
        }

        // POST: alarms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlarmOccured,Status,ProcessValue,Description,Equipment_Tag,Equipment_Facilities_Name")] alarms alarms)
        {
            if (ModelState.IsValid)
            {
                db.alarms.Add(alarms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Equipment_Tag = new SelectList(db.equipment, "Tag", "SIUnits", alarms.equipment_Tag);
            return View(alarms);
        }

        // GET: alarms/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alarms alarms = db.alarms.Find(id);
            if (alarms == null)
            {
                return HttpNotFound();
            }
            ViewBag.Equipment_Tag = new SelectList(db.equipment, "Tag", "SIUnits", alarms.equipment_Tag);
            return View(alarms);
        }

        // POST: alarms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlarmOccured,Status,ProcessValue,Description,Equipment_Tag,Equipment_Facilities_Name")] alarms alarms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alarms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Equipment_Tag = new SelectList(db.equipment, "Tag", "SIUnits", alarms.equipment_Tag);
            return View(alarms);
        }

        // GET: alarms/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alarms alarms = db.alarms.Find(id);
            if (alarms == null)
            {
                return HttpNotFound();
            }
            return View(alarms);
        }

        // POST: alarms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            alarms alarms = db.alarms.Find(id);
            db.alarms.Remove(alarms);
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
