using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatermentWebSCADA.ViewModels;
using System.Data;
using System.Data.Entity;
using System.Net;
using MySql.Data.MySqlClient;
using MySql.Data.Entity;
using System.Data.Common;
using System.Web.Helpers;
using WatermentWebSCADA.Models;
using System.Net.NetworkInformation;
using WatermentWebSCADA.CustomFilters;
using Microsoft.Ajax.Utilities;

namespace WatermentWebSCADA.Controllers
{
    public class MaintenanceController : Controller
    {
        Models.watermentdbEntities db = new Models.watermentdbEntities();

     
        [AuthLog(Roles = "Admin, Superuser, Maintenance")]
        // GET: Maintanance
        public ActionResult Index(int? id)
        {
           

            using (var db = new Models.watermentdbEntities())
            {
                var model = new MaintenanceViewModel
                {
                    //Getting desired data from the database, and returning it to the view.
                    Equipments = db.equipments.Include(c => c.alarms).Include(c => c.facilities).ToList(),
                    Location = db.locations.ToList(),
                    Maintenance = db.maintenance.OrderByDescending(x => x.LastMaintenance).DistinctBy
                    (x => x.facilities_Id).OrderBy(x => x.LastMaintenance).Take(10).ToList(),
                    
                    Facilites = db.facilities.ToList(),

                };

                return View(model);
            }
        }
        [AuthLog(Roles = "Admin, Superuser, Maintenance")]
        public ActionResult MaintenanceEdit()
        {
            ViewBag.facilities_Id = new SelectList(db.facilities, "Id", "Name");

            return View();
        }

        [AuthLog(Roles = "Admin, Superuser, Maintenance")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaintenanceEdit([Bind(Include = "OrderId,Person, Description, facilities_Id, lastMaintenance")] maintenance maintenance)
        {
            if (ModelState.IsValid)
            {
                db.maintenance.Add(maintenance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.facilities_Id = new SelectList(db.facilities, "Id", "Name", maintenance.facilities_Id);
            return View(maintenance);
        }
    }
}