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
    public class countriesController : Controller
    {
        private watermentdbEntities db = new watermentdbEntities();

        // GET: countries
        public async Task<ActionResult> Index()
        {
            var countries = db.countries.Include(c => c.continents);
            return View(await countries.ToListAsync());
        }

        // GET: countries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            countries countries = await db.countries.FindAsync(id);
            if (countries == null)
            {
                return HttpNotFound();
            }
            return View(countries);
        }

        // GET: countries/Create
        public ActionResult Create()
        {
            ViewBag.continents_Id = new SelectList(db.continents, "Id", "Code");
            return View();
        }

        // POST: countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CountryCode,Name,continents_Id")] countries countries)
        {
            if (ModelState.IsValid)
            {
                db.countries.Add(countries);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.continents_Id = new SelectList(db.continents, "Id", "Code", countries.continents_Id);
            return View(countries);
        }

        // GET: countries/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            countries countries = await db.countries.FindAsync(id);
            if (countries == null)
            {
                return HttpNotFound();
            }
            ViewBag.continents_Id = new SelectList(db.continents, "Id", "Code", countries.continents_Id);
            return View(countries);
        }

        // POST: countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CountryCode,Name,continents_Id")] countries countries)
        {
            if (ModelState.IsValid)
            {
                db.Entry(countries).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.continents_Id = new SelectList(db.continents, "Id", "Code", countries.continents_Id);
            return View(countries);
        }

        // GET: countries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            countries countries = await db.countries.FindAsync(id);
            if (countries == null)
            {
                return HttpNotFound();
            }
            return View(countries);
        }

        // POST: countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            countries countries = await db.countries.FindAsync(id);
            db.countries.Remove(countries);
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
