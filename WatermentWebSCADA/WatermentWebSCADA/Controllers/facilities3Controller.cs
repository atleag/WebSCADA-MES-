using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatermentWebSCADA.Models;

namespace WatermentWebSCADA.Controllers
{
    public class facilities3Controller : Controller
    {
        private watermentdbEntities db = new watermentdbEntities();

        // GET: facilities3
        public async Task<ActionResult> Index()
        {
            var facilities = db.facilities.Include(f => f.FacilityStatus).Include(f => f.locations);
            return View(await facilities.ToListAsync());
        }

        // GET: facilities3/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            facilities facilities = await db.facilities.FindAsync(id);
            if (facilities == null)
            {
                return HttpNotFound();
            }
            return View(facilities);
        }

        // GET: facilities3/Create
        public ActionResult Create()
        {
            ViewBag.FacilityStatus_Id = new SelectList(db.FacilityStatus, "Id", "Name");
            ViewBag.locations_Id = new SelectList(db.locations, "Id", "StreetAddress");
            return View();
        }

        // POST: facilities3/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,IP,Domain,locations_Id,locations_countries_Id,locations_countries_continents_Id,FacilityStatus_Id")] facilities facilities)
        {
            if (ModelState.IsValid)
            {
                db.facilities.Add(facilities);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FacilityStatus_Id = new SelectList(db.FacilityStatus, "Id", "Name", facilities.FacilityStatus_Id);
            ViewBag.locations_Id = new SelectList(db.locations, "Id", "StreetAddress", facilities.locations_Id);
            return View(facilities);
        }

        // GET: facilities3/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            facilities facilities = await db.facilities.FindAsync(id);
            if (facilities == null)
            {
                return HttpNotFound();
            }
            ViewBag.FacilityStatus_Id = new SelectList(db.FacilityStatus, "Id", "Name", facilities.FacilityStatus_Id);
            ViewBag.locations_Id = new SelectList(db.locations, "Id", "StreetAddress", facilities.locations_Id);
            return View(facilities);
        }

        // POST: facilities3/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,IP,Domain,locations_Id,locations_countries_Id,locations_countries_continents_Id,FacilityStatus_Id")] facilities facilities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facilities).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FacilityStatus_Id = new SelectList(db.FacilityStatus, "Id", "Name", facilities.FacilityStatus_Id);
            ViewBag.locations_Id = new SelectList(db.locations, "Id", "StreetAddress", facilities.locations_Id);
            return View(facilities);
        }

        // GET: facilities3/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            facilities facilities = await db.facilities.FindAsync(id);
            if (facilities == null)
            {
                return HttpNotFound();
            }
            return View(facilities);
        }

        // POST: facilities3/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            facilities facilities = await db.facilities.FindAsync(id);
            db.facilities.Remove(facilities);
            await db.SaveChangesAsync();
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
