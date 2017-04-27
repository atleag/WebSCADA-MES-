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

namespace WatermentWebSCADA.Controllers
{
    public class MaintenanceController : Controller
    {
        Models.watermentdbEntities db = new Models.watermentdbEntities();

     

        // GET: Maintanance
        public ActionResult Index(int? id)
        {
           

            using (var db = new Models.watermentdbEntities())
            {
                var model = new MainViewModel
                {
                    //Getting desired data from the database, and returning it to the view.
         
                   
                    Equipment = db.equipments.Include(c => c.alarms).Include(c => c.facilities).ToList(),
                    Lokasjoner = db.locations.ToList(),
                    Vedlikehold = db.maintenance.OrderBy(x => x.LastMaintenance).Take(10).ToList(),
                    Facilites = db.facilities.ToList(),

                };

                return View(model);
            }
        }
        public ActionResult Maintenance()
        {
            ViewBag.facilities_Id = new SelectList(db.facilities, "Id", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Maintenance([Bind(Include = "OrderId,Person, Description, facilities_Id, lastMaintenance")] maintenance maintenance)
        {
            if (ModelState.IsValid)
            {
                db.maintenance.Add(maintenance);
                db.SaveChanges();
                return RedirectToAction("FacilityOverview");
            }

            ViewBag.facilities_Id = new SelectList(db.facilities, "Id", "Name", maintenance.facilities_Id);
            return View(maintenance);
        }


    }
}