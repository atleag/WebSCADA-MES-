using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using codingfreaks.samples.Identity.Models;

namespace WatermentWebSCADA.Controllers
{
    //Source: http://www.dotnetcurry.com/aspnet-mvc/1102/aspnet-mvc-role-based-security
    public class RoleController : Controller
    {
        // GET: Role
        ApplicationDbContext context;

        public RoleController()
        {
            context = new ApplicationDbContext();
        }
        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var Roles = context.Roles.ToList();
            return View(Roles);
        }

        /// <summary>
        /// Create  a New role
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var Role = new MyRole();
            return View(Role);
        }

        /// <summary>
        /// Create a New Role
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(MyRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int roleId)
        {
            ApplicationDbContext AppDb = new ApplicationDbContext();
            var role = AppDb.Roles.Where(d => d.Name == "my role name").FirstOrDefault();
            AppDb.Roles.Remove(role);
            AppDb.SaveChanges();
            return null;
        }

    }
}