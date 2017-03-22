using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatermentWebSCADA.Models;
using WatermentWebSCADA.ViewModels;

namespace WatermentWebSCADA.Controllers
{
    public class FacilityController : Controller
    {
        private watermentdbEntities db = new watermentdbEntities();

        // GET: Facility
        public ActionResult Index(int? id)
        {

            var data = new FacilityViewModel
            {
                IP = db.facilities.Select(x => x.IP).ToList(),
                Name = db.facilities.Select(x => x.Name).ToList(),
                Address = db.location.Select(x => x.Address).ToList(),
                County = db.location.Select(x => x.County).ToList(),
                CountryName = db.country.Select(x => x.Name).ToList(),
                Postcode = db.location.Select(x => x.Postcode).ToList(),
                FirstName = db.users.Select(x => x.FirstName).ToList(),
                LastName = db.users.Select(x => x.LastName).ToList(),
                Phone = db.users.Select(x => x.Phone).ToList(),
                Email = db.users.Select(x => x.Email).ToList(),
                ProcessValue=db.measurement.Select(x=>x.ProcessValue).ToList(),
                Tag = db.equipment.Select(x => x.Tag).ToList(),
                Timestamp = db.measurement.Select(x => x.Timestamp).ToList(),
                Description = db.alarms.Select(x => x.Description).ToList(),
                Alarmsoccured = db.alarms.Select(x => x.AlarmOccured).ToList(),

            };

            return View(data);
        }
        public ActionResult Details(int? id)
        {
            var data = new FacilityViewModel
            {
                IP = db.facilities.Select(x => x.IP).ToList(),
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

//public List<string> Name { get; set; }
//public List<string> IP { get; set; }
//public List<string> Domain { get; set; }
////locations model
//public List<string> Address { get; set; }
//public List<int> Postcode { get; set; }
//public List<string> County { get; set; }
//public List<string> CountryName { get; set; }
//public List<string> FirstName { get; set; }
//public List<string> LastName { get; set; }
//public List<string> Phone { get; set; }
//public List<string> Email { get; set; }