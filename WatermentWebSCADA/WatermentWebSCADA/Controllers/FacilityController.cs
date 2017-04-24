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
using System.Web.UI;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace WatermentWebSCADA.Controllers
{
    public class FacilityController : Controller
    {
        Models.watermentdbEntities db = new Models.watermentdbEntities();

        int LandId1;
        int LokasjonsID;
        string IpClient;


        // GET: Facility
        public ActionResult FacilityDetails(int? id)
        {
            if (id == null) //Error handling if "int? id" is missing from the link. 
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

        
            using (var db = new Models.watermentdbEntities())
            {
                var model = new MainViewModel
                {
                    Alarmer = db.alarms.Where(x => x.equipments_facilities_Id == id).Where(o => o.Status == "Active").OrderByDescending(x=>x.AlarmOccured).ToList(),
                    Facilites = db.facilities.Where(c => c.Id == id).ToList(),
                    Countries = db.countries.Where(x => x.Id == LandId1).ToList(),
                    Lokasjoner = db.locations.Where(x => x.Id == LokasjonsID).ToList(),
                    Brukere = db.User.Where(x => x.locations_Id == LokasjonsID).ToList(),
                    Utstyr = db.equipments.ToList(),

                };

                return View(model);
            }
        }
        public ActionResult chart(int? id)
        {//chart er kun for testing
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
                    TempMeasModel = db.measurements.Where(x => x.equipments_facilities_Id == id).ToList(),



                };
              



            

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
            ViewBag.locations_Id = new SelectList(db.locations, "Id", "StreetAddress");
            ViewBag.locations_countries_Id = new SelectList(db.countries, "Id", "Name");
            ViewBag.locations_countries_continents_Id = new SelectList(db.continents, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFacility2([Bind(Include = "Id,Name,IP,Domain,SerialNumber, ProgramVersion,locations_Id,locations_countries_Id,locations_countries_continents_Id")] facilities facilities)
        {
            if (ModelState.IsValid)
            {
                db.facilities.Add(facilities);
                db.SaveChanges();
                return RedirectToAction("FacilityOverview");
            }

            ViewBag.locations_Id = new SelectList(db.locations, "Id", "StreetAddress", facilities.locations_Id);
            ViewBag.locations_countries_Id = new SelectList(db.countries, "Id", "Name", facilities.locations_countries_Id);
            ViewBag.locations_countries_continents_Id = new SelectList(db.continents, "Id", "Name", facilities.locations_countries_continents_Id);

            return View(facilities);
        }

        public ActionResult AddLocation()
        {
            ViewBag.countries_Id = new SelectList(db.countries, "Id", "Name");
            ViewBag.countries_continents_Id = new SelectList(db.continents, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLocation([Bind(Include = "Id,StreetAddress,Postcode,County,City,countries_Id,countries_continents_Id")] locations locations)
        {
            if (ModelState.IsValid)
            {
                db.locations.Add(locations);
                db.SaveChanges();
                return RedirectToAction("AddFacility2");
            }

            ViewBag.locations_Id = new SelectList(db.countries, "Id", "Name", locations.countries_Id);
            ViewBag.locations_continents_id = new SelectList(db.continents, "Id", "Name", locations.countries_continents_Id);
            return View(locations);
        }

        public ActionResult EditFacilities(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            facilities facilities = db.facilities.Find(id);
            if (facilities == null)
            {
                return HttpNotFound();
            }
            ViewBag.locations_Id = new SelectList(db.locations, "Id", "StreetAddress");
            return View(facilities);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFacilities([Bind(Include = "Id,Name,IP,Domain,SerialNumber,ProgramVersion, locations_Id")] facilities facilities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facilities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("FacilityOverview");
            }
            ViewBag.locations_Id = new SelectList(db.locations, "Id", "StreetAddress", facilities.locations_Id);
            return View(facilities);
        }



        public ActionResult Maintenance()
        {
            ViewBag.facilities_Id = new SelectList(db.facilities,"Id", "Name");
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
        public static class Theme
        {

            public const string Green = "<Chart BackColor=\"#ABC27A\" BackGradientStyle=\"TopBottom\" BackSecondaryColor=\"229, 217, 148\" BorderColor=\"#ABC27A\" BorderlineDashStyle=\"Solid\" BorderWidth=\"5\" Palette=\"Chocolate\">\r\n    <ChartAreas>\r\n        <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"#ABC27A\" BackGradientStyle=\"TopBottom\" BackSecondaryColor=\"229, 217, 148\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"171,194,122\" /> \r\n    </ChartAreas>\r\n    <Legends>\r\n        <Legend _Template_=\"All\" BackColor=\"229, 217, 148\" Font=\"Trebuchet MS, 8.25pt, style=Bold\" IsTextAutoFit=\"False\" /> \r\n    </Legends>\r\n   <BorderSkin SkinStyle=\"none\" /> \r\n   </Chart>";
          
        }


        public ActionResult TempChart(int? id)
        {

            
        float?[] Measurement = db.measurements.Where(i => i.equipments_facilities_Id == id).Where(x=>x.equipments.SIUnits=="Degrees").Select(x => x.ProcessValue).ToArray();
            DateTime?[] Date = db.measurements.Where(i => i.equipments_facilities_Id == id).Where(x=>x.equipments.SIUnits=="Degrees").Select(x => x.Recorded).ToArray();

            //Where(i => i.equipments_facilities_Id == id).

            var myChart = new Chart(width: 1100, height: 350,theme:Theme.Green)
                   //var myChart = new Chart(width: 1100, height: 350, theme: ChartTheme.Green)
                   
            .SetYAxis("Temp", 0, 50)
           
            .AddTitle("Temperature Chart")
            .AddSeries(
                chartType: "Line",
                name: "Measurements",
                xValue: Date,
                yValues: Measurement)

            .Write();

            return File(myChart.ToWebImage().GetBytes(), "image/jpeg");
        }
        public ActionResult BarChart(int? id,DateTime? from, DateTime? to, int? tag)
        {
           
            ViewBag.equipments_Id = new SelectList(db.equipments.ToList(),"Id", "Tag");


            float?[] Measurement = db.measurements.Where(i => i.equipments_facilities_Id == id).Where(x => x.equipments.Id == tag).Where(x => x.Recorded >= from && x.Recorded <= to).Select(x => x.ProcessValue).ToArray();
            DateTime?[] Date = db.measurements.Where(i => i.equipments_facilities_Id == id).Where(x => x.equipments.Id == tag).Where(x=>x.Recorded>=from && x.Recorded<=to).Select(x => x.Recorded).ToArray();




            var myChart = new Chart(width: 1100, height: 350, theme: Theme.Green)
            
            .SetYAxis("Pressure", 0, 50)
            .AddTitle("Pressure Chart")
            .AddSeries(
                chartType: "Line",
                name: "Measurements",
                xValue: Date,
                yValues: Measurement)

            .Write();

            return File(myChart.ToWebImage().GetBytes(), "image/jpeg");
        }
    }
              
            
}