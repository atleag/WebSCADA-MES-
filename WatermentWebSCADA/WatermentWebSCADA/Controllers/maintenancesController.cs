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
    public class MaintenancesController : Controller
    {
        private watermentdbEntities db = new watermentdbEntities();

        // GET: maintenances
        public ActionResult Index()
        {
            var maintenance = db.maintenance.Include(m => m.facilities);
            return View(maintenance.ToList());
        }

        // GET: maintenances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            maintenance maintenance = db.maintenance.Find(id);
            if (maintenance == null)
            {
                return HttpNotFound();
            }
            return View(maintenance);
        }

        // GET: maintenances/Create
        public ActionResult Create()
        {
            ViewBag.Facilities_Name = new SelectList(db.facilities, "Name", "IP");
            return View();
        }

        // POST: maintenances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,Person,Facilities_Name")] maintenance maintenance)
        {
            if (ModelState.IsValid)
            {
                db.maintenance.Add(maintenance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Facilities_Name = new SelectList(db.facilities, "Name", "IP", maintenance.facilities_Name);
            return View(maintenance);
        }

        // GET: maintenances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            maintenance maintenance = db.maintenance.Find(id);
            if (maintenance == null)
            {
                return HttpNotFound();
            }
            ViewBag.Facilities_Name = new SelectList(db.facilities, "Name", "IP", maintenance.facilities_Name);
            return View(maintenance);
        }

        // POST: maintenances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,Person,Facilities_Name")] maintenance maintenance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maintenance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Facilities_Name = new SelectList(db.facilities, "Name", "IP", maintenance.facilities_Name);
            return View(maintenance);
        }

        // GET: maintenances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            maintenance maintenance = db.maintenance.Find(id);
            if (maintenance == null)
            {
                return HttpNotFound();
            }
            return View(maintenance);
        }

        // POST: maintenances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            maintenance maintenance = db.maintenance.Find(id);
            db.maintenance.Remove(maintenance);
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
