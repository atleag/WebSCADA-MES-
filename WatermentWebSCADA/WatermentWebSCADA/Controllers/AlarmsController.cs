using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatermentWebSCADA.Controllers
{
    public class AlarmsController : Controller
    {
        Models.watermentdbEntities db = new Models.watermentdbEntities();
        // GET: Alarms
        public ActionResult Index()
        {
            //var data

            return View(db.alarms.ToList());
        }
        public ActionResult Alarmindex()
        {

            return View(db.alarms.ToList());
        }


    }
}