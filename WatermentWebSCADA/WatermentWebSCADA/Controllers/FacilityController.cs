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
using Newtonsoft.Json;
using System.Threading.Tasks;

using System.Data.Entity.ModelConfiguration.Conventions;


namespace WatermentWebSCADA.Controllers
{
    public class FacilityController : Controller
    {
        Models.watermentdbEntities db = new Models.watermentdbEntities();

        int LandId1;
        int LokasjonsID;
        string IpClient;
        string[] arr;

        // GET: Facility
        public ActionResult FacilityDetails(int? id)
        {
            if (id == null) //int id = 0 handling. 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Fetches the current row id selected in the table Client_Conection
            foreach (var item2 in db.Client_Conection.Where(c => c.user == id.ToString()))
            {
                IpClient = item2.ip;
            }
            // Fetches the current row id seleted in the table facilities and sets the facility IP to
            // the Client IP.
            foreach (var item in db.facilities.Where(c => c.Id == id))
            {
                LandId1 = item.locations_countries_Id.GetValueOrDefault();
                LokasjonsID = item.locations_Id.GetValueOrDefault();
                item.IP = IpClient;
            }

            // Saves the changes to the DB
            db.SaveChanges();

            //foreach (var item in db.measurements.Where(x => x.equipments_facilities_Id == id).Where(i => i.equipments.Description == "Temperature Reactor"))
            //{
            //    for (int i = 0; i < db.measurements.Select(x => x.ProcessValue).Max(); i++)
            //    {
            //        arr[i] = db.measurements.Select(x => x.ProcessValue).ToArray[i]
            //    }
            //}



            using (var db = new Models.watermentdbEntities())
            {
                var model = new MainViewModel
                {
                    Alarmer = db.alarms.Where(x => x.equipments_facilities_Id == id).Where(o => o.Status == "Active").ToList(),
                    Facilites = db.facilities.Where(c => c.Id == id).ToList(),
                    Kontinenter = db.continents.ToList(),
                    Countries = db.countries.Where(x => x.Id == LandId1).ToList(), /*Se her mer 167 som lokal variabel fra koden før*/
                    Utstyr = db.equipments.ToList(),
                    Lokasjoner = db.locations.Where(x => x.Id == LokasjonsID).ToList(),
                    Vedlikehold = db.maintenance.ToList(),
                    Roller = db.Role.ToList(),
                    Brukere = db.User.Where(x => x.locations_Id == LokasjonsID).ToList(),
                    Sesjoner = db.sessions.ToList(),

                    Verdier = db.measurements.Where(x => x.equipments_facilities_Id == id).Where(i => i.equipments.Description == "Temperature Reactor").ToList(),
                    BarValues = db.measurements.Where(x => x.equipments_facilities_Id == id).Where(i => i.equipments.Description == "Pressure Reactor").ToList(),
                    AlarmList = db.alarms.Where(x => x.equipments_facilities_Id == id).Where(o => o.Status == "Active").ToList(),

                };

                return View(model);
            }
        }
        public ActionResult chart(int? id)
        {
            foreach (var item in db.facilities.Where(c => c.Id == id))
            {
                LandId1 = item.locations_countries_Id.GetValueOrDefault();
                LokasjonsID = item.locations_Id.GetValueOrDefault();

            }



            using (var db = new Models.watermentdbEntities())
            {
                var model = new MainViewModel
                {
                    Alarmer = db.alarms.Where(x => x.equipments_facilities_Id == id).Where(o => o.Status == "Active").ToList(),
                    Facilites = db.facilities.Where(c => c.Id == id).ToList(),

                    //Anlegg2=db.facilities.Where(x=>x.locations_countries_Id=landid),

                    Kontinenter = db.continents.ToList(),

                    Countries = db.countries.Where(x => x.Id == LandId1).ToList(), /*Se her mer 167 som lokal variabel fra koden før*/

                    Utstyr = db.equipments.ToList(),
                    Lokasjoner = db.locations.Where(x => x.Id == LokasjonsID).ToList(),
                    Vedlikehold = db.maintenance.ToList(),
                    Roller = db.Role.ToList(),
                    Brukere = db.User.Where(x => x.locations_Id == LokasjonsID).ToList(),
                    Sesjoner = db.sessions.ToList(),

                    Verdier = db.measurements.Where(x => x.equipments_facilities_Id == id).Where(i => i.equipments.Description == "Temperature Reactor").ToList(),
                    BarValues = db.measurements.Where(x => x.equipments_facilities_Id == id).Where(i => i.equipments.Description == "Pressure Reactor").ToList(),
                    AlarmList = db.alarms.Where(x => x.equipments_facilities_Id == id).Where(o => o.Status == "Active").ToList(),




                };

                //arr = db.measurements.Where(x => x.equipments_facilities_Id == id).Where(i => i.equipments.Description == "Temperature Reactor".ToArray();


                JsonSerializerSettings jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

                ViewBag.DataPoints = JsonConvert.SerializeObject(DataService.GetRandomDataForNumericAxis(10000), jsonSetting);

                //Select(u => u.ProcessValue).

                return View(model);
            }
        }
        public ContentResult GetData()
        {
            using (var db = new watermentdbEntities())
            {
                var result = (from tags in db.measurements
                              orderby tags.Recorded ascending
                              select new { tags.ProcessValue }).ToList();
                //return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
                return Content(JsonConvert.SerializeObject(result), "application/json");
            }
        }
        public ActionResult FacilityOverview(int? id)
        {

            using (var db = new Models.watermentdbEntities())
            {
                var model = new MainViewModel
                {

                    countries = db.countries_with_facilites_view.ToList(),
                    Facilites = db.facilities.Where(x => x.locations_countries_Id == id).ToList(),

                };


                return View(model);
            }
        }

        public ActionResult Get(int id)
        {
            using (var db = new Models.watermentdbEntities())
            {
                var model = new MainViewModel
                {
                    //Convention = db.Client_Conection.Select(x => x.user).FirstOrDefault(),                  
                };
                return View(model);
            }
        }

        public ActionResult AddFacility2()
        {
            //ViewBag.continents_Id = new SelectList(db.continents, "Id", "Code");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> AddFacility([Bind(Include = "Id,CountryCode,Name,continents_Id")] AddFacilityViewModel Facility)
        //{
        //    LocationViewModel LVM = new LocationViewModel();
        //    if (ModelState.IsValid)
        //    {
        //        db.facilities.Add(Facility);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.continents_Id = new SelectList(db.continents, "Id", "Code");
        //    return View();
        //}
         public async Task<ActionResult> AddFacilityVersionTwo(FacilityViewModel fmodel)
         {
             if (ModelState.IsValid)
             {
                 using (var context = new watermentdbEntities())
                 {
                     var facilites = new facilities();
                     {
                         facilites.Name = fmodel.Name;
                         facilites.Domain = fmodel.Name;
                         await context.SaveChangesAsync();

                     };
                 }
                 return RedirectToAction("Login", "Account");

             }
             return View(fmodel);
         }
    }
              
            
}