using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatermentWebSCADA.Controllers
{
    public class UserManagementController : Controller
    {
        // GET: UsersManagement
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsersManagement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersManagement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersManagement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersManagement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersManagement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersManagement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
