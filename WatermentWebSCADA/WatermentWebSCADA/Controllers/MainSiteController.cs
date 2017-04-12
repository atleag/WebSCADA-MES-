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

        string AlarmNavn;
        
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
                    Vedlikehold = db.maintenance.ToList(),
                    Brukere = db.User.ToList(),
                    Facilites = db.facilities.ToList(),
                    


                };

                //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                //var students = from s in db.facilities
                //               select s;
                //switch (sortOrder)
                //{
                //    case "name_desc":
                //        students = students.OrderByDescending(s => s.Name);
                //        break;
                //    case "Date":
                //        students = students.OrderBy(s => s.maintenance);
                //        break;
                //    case "date_desc":
                //        students = students.OrderByDescending(s => s.locations);
                //        break;
                //    default:
                //        students = students.OrderBy(s => s.locations_Id);
                //        break;
                //}
                //return View(students.ToList());

                return View(model);

            }   
           }      
      
        } 

    }
