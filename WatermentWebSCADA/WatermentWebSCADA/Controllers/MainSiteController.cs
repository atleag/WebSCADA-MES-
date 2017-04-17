using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using WatermentWebSCADA.Models;
using WatermentWebSCADA.ViewModels;
using System.Data;
using System.Data.Entity;
using System.Net;
using MySql.Data.MySqlClient;
using MySql.Data.Entity;
using System.Data.Common;
using System.Web.Helpers;
using WatermentWebSCADA.Models;

namespace WatermentWebSCADA.Controllers
{
    public class MainSiteController : Controller
    {
        Models.watermentdbEntities db = new Models.watermentdbEntities();

        
        
        // GET: Main
        public ActionResult Index(int? id, string sortOrder)
        {
            using (var db1 = new Models.watermentdbEntities())

            {
                var model = new MainViewModel
                {

                    Countries = db.countries.Where(c => c.continents.Id == id).ToList(),
                    AlarmList = db.alarms.Where(o => o.Status == "Active").ToList(),
                    Equipment = db.equipments.Include(c => c.alarms).Include(c => c.facilities).ToList(),
                    Lokasjoner = db.locations.ToList(),
                    Vedlikehold = db.maintenance.OrderBy(x => x.lastMaintenance).Take(10).ToList(),
                    Facilites = db.facilities.ToList(),

                    antallFacilities = db.facilities.Count(),
                    antallOnline = db.facilities.Where(x=> x.FacilityStatus_Id == 1).Count(),
                    antallOffline = db.facilities.Where(x => x.FacilityStatus_Id == 2).Count(),
                };

                   


                return View(model);

            }   
           }      
      
        } 

    }
