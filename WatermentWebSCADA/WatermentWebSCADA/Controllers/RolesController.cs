using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatermentWebSCADA.ViewModels;

namespace WatermentWebSCADA.Controllers
{
    public class RolesController : Controller
    {
        private RoleViewModel db = new RoleViewModel();

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ListRoles(RoleViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                var result = rvm; 

                return View(rvm);

            }
            return View(rvm);
        }

        // GET: Roles
        public ActionResult Index()
        {
            return View(db);
        }

        // GET: Roles/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    if (db.Id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(role);
        //}

        //// GET: Roles/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Roles/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Description")] RoleViewModel roleviewmodel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Add(role);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(db);
        //}

        //// GET: Roles/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Role role = db.Role.Find(id);
        //    if (role == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(role);
        //}

        //// POST: Roles/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Description")] Role role)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(role).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(role);
        //}

        //// GET: Roles/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Role role = db.Role.Find(id);
        //    if (role == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(role);
        //}

        //// POST: Roles/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Role role = db.Role.Find(id);
        //    db.Role.Remove(role);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
