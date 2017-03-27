using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatermentWebSCADA.Models;
using WatermentWebSCADA.ViewModels;
using System.Data;
using System.Data.Entity;
using System.Net;
using MySql.Data.MySqlClient;
using MySql.Data.Entity;
using System.Data.Common;
using System.Web.Helpers;


namespace WatermentWebSCADA.Controllers
{
    public class MainSiteController : Controller
    {
        Models.watermentdbEntities db = new Models.watermentdbEntities();
        // GET: Main
        public ActionResult Index()
        {
            var info = new MainSiteController
                (
              alarms = db.alarms.ToList(),
             facilities = db.facilities.ToList(),
            )

          return View(info);
        }
     
       

        public ActionResult DetailView(int? id)
        {
            return View();
        }
    }
}