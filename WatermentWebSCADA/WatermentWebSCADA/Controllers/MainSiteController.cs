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
        private FacilityDBContext db = new FacilityDBContext();
        // GET: Main
        public ActionResult Index()
        {

            //var data = new MainViewModel
            //{
            //    //IP = db.Facility.Select(x => x.IP).ToList(),
            //    //Name = db.facilities.Where(c => c.locations_countries_Id == id2).Select(x => x.Name).ToList(),
            //    //Address = db.locations.Select(x => x.Address).ToList(),
            //    //CountryName = db.countries.Where(c => c.continents_Id == id).OrderBy(y => y.Name).Select(x => x.Name).ToList(),
            //    //County = db.continents.Select(x => x.Name).ToList(),


            
                
            //};

            return View(db.Facility.ToList());
        }
        [HttpPost]
       

        public ActionResult DetailView(int? id)
        {

            var data = new MainViewModel
            {
                //IP = db.facilities.Select(x => x.IP).ToList(),
                //Name = db.facilities.Select(x => x.Name).ToList(),
                //Address = db.locations.Select(x => x.Address).ToList(),
                //County = db.locations.Select(x => x.County).ToList(),
                //CountryName = db.countries.Select(x => x.Name).ToList(),
                //Postcode = db.locations.Select(x => x.Postcode).ToList(),
                //FirstName = db.users.Select(x => x.FirstName).ToList(),
                //LastName = db.users.Select(x => x.LastName).ToList(),
                //Phone = db.users.Select(x => x.Phone).ToList(),
                //Email = db.users.Select(x => x.Email).ToList(),
                //ProcessValue = db.measurements.Select(x => x.ProcessValue).ToList(),
                //Tag = db.equipments.Select(x => x.Tag).ToList(),
                //Timestamp = db.measurements.Select(x => x.Timestamp).ToList(),
                //Description = db.alarms.Select(x => x.Description).ToList(),
                //Alarmsoccured = db.alarms.Select(x => x.AlarmOccured).ToList(),
                //AlarmProcessValue = db.alarms.Select(x => x.ProcessValue).ToList(),


            };
            return View(data);
        }
    }
}