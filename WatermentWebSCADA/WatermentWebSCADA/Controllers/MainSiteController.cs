using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatermentWebSCADA.Models;
using WatermentWebSCADA.ViewModels;

namespace WatermentWebSCADA.Controllers
{
    public class MainSiteController : Controller
    {
        private watermentdbEntities db = new watermentdbEntities();
        // GET: Main
        public ActionResult Index()
        {

            var data = new MainViewModel
            {
                IP = db.facilities.Select(x => x.IP).ToList(),
                Name = db.facilities.Select(x => x.Name).ToList(),
                Address = db.location.Select(x => x.Address).ToList(),
                CountryName = db.country.Select(x => x.Name).ToList(),
            };

            return View(data);
        }
        [HttpPost]
        public ActionResult Details(int? id)
        {

            var data = new MainViewModel
            {
                IP = db.facilities.Where(c => c.Id == id).Select(x => x.IP).ToList(),
                Name = db.facilities.Select(x => x.Name).ToList(),
                Address = db.location.Select(x => x.Address).ToList(),
                County = db.location.Select(x => x.County).ToList(),
                CountryName = db.country.Select(x => x.Name).ToList(),
                Postcode = db.location.Select(x => x.Postcode).ToList(),
                FirstName = db.users.Select(x => x.FirstName).ToList(),
                LastName = db.users.Select(x => x.LastName).ToList(),
                Phone = db.users.Select(x => x.Phone).ToList(),
                Email = db.users.Select(x => x.Email).ToList(),
                ProcessValue = db.measurement.Select(x => x.ProcessValue).ToList(),
                Tag = db.equipment.Select(x => x.Tag).ToList(),
                Timestamp = db.measurement.Select(x => x.Timestamp).ToList(),
                Description = db.alarms.Select(x => x.Description).ToList(),
                Alarmsoccured = db.alarms.Select(x => x.AlarmOccured).ToList(),

            };
            return View(data);
        }
    }
}