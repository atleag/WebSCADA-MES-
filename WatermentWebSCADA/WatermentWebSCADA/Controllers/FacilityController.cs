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

using System.Data.Entity.ModelConfiguration.Conventions;


namespace WatermentWebSCADA.Controllers
{
    public class FacilityController : Controller
    {
        Models.watermentdbEntities db = new Models.watermentdbEntities();
       
        int LandId1;
        int LokasjonsID;
        string IpClient;
        double[] arr;

        // GET: Facility
        public ActionResult FacilityDetails(int? id)
        {
            // Fetches the current row id selected in the table Client_Conection
            foreach (var item2 in db.Client_Conection.Where(c => c.user == id.ToString()))
            {
                IpClient = item2.ip;
            }
            // Fetches the current row id seleted in the table facilities and sets the facility IP to
            // the Client IP.
            foreach (var item in db.facilities.Where(c=>c.Id==id))
            {
                LandId1 = item.locations_countries_Id;
                LokasjonsID = item.locations_Id;
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
                    Alarmer = db.alarms.Where(x=>x.equipments_facilities_Id==id).Where(o=>o.Status=="Active").ToList(),
                    Anlegg = db.facilities.Where(c => c.Id == id).ToList(),

                    //Anlegg2=db.facilities.Where(x=>x.locations_countries_Id=landid),

                    Kontinenter = db.continents.ToList(),

                    Land = db.countries.Where(x => x.Id == LandId1).ToList(), /*Se her mer 167 som lokal variabel fra koden før*/
                    //Land = db.countries.ToList(),
                    Utstyr = db.equipments.ToList(),
                    Lokasjoner = db.locations.Where(x=>x.Id==LokasjonsID).ToList(),
                    Vedlikehold = db.maintenance.ToList(),
                    Roller = db.roles.ToList(),
                    Brukere = db.users.Where(x=>x.locations_Id==LokasjonsID).ToList(),
                    Sesjoner = db.sessions.ToList(),
                 
                    Bar = db.measurements.Where(x => x.equipments_facilities_Id == id).Where(i => i.equipments.Description == "Pressure Reactor").ToList(),

                    //Verdier = db.measurements.Where(x => x.equipments_facilities_Id == id).Where(i => i.equipments.Description == "Temperature Reactor").ToList(),

                    Verdier = db.measurements.Where(x => x.equipments_facilities_Id == id).Where(i => i.equipments.Description == "Temperature Reactor").ToList(),

                };

                return View(model);
            }
        }
        public ActionResult chart(int? id)
        {
            foreach (var item in db.facilities.Where(c => c.Id == id))
            {
                LandId1 = item.locations_countries_Id;
                LokasjonsID = item.locations_Id;

            }

          

            using (var db = new Models.watermentdbEntities())
            {
                var model = new MainViewModel
                {
                    Alarmer = db.alarms.Where(x => x.equipments_facilities_Id == id).Where(o => o.Status == "Active").ToList(),
                    Anlegg = db.facilities.Where(c => c.Id == id).ToList(),

                    //Anlegg2=db.facilities.Where(x=>x.locations_countries_Id=landid),

                    Kontinenter = db.continents.ToList(),

                    Land = db.countries.Where(x => x.Id == LandId1).ToList(), /*Se her mer 167 som lokal variabel fra koden før*/

                    Utstyr = db.equipments.ToList(),
                    Lokasjoner = db.locations.Where(x => x.Id == LokasjonsID).ToList(),
                    Vedlikehold = db.maintenance.ToList(),
                    Roller = db.roles.ToList(),
                    Brukere = db.users.Where(x => x.locations_Id == LokasjonsID).ToList(),
                    Sesjoner = db.sessions.ToList(),

                    Verdier = db.measurements.Where(x => x.equipments_facilities_Id == id).Where(i => i.equipments.Description == "Temperature Reactor").ToList(),
                    Bar = db.measurements.Where(x => x.equipments_facilities_Id == id).Where(i => i.equipments.Description == "Pressure Reactor").ToList(),
                    AlarmList = db.alarms.Where(x => x.equipments_facilities_Id == id).Where(o => o.Status == "Active").ToList(),
                  
                  



                };

                JsonSerializerSettings jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

                ViewBag.DataPoints = JsonConvert.SerializeObject(DataService.GetRandomDataForNumericAxis(1000), jsonSetting);

                return View(model);
            }
        }

        public ActionResult FacilityOverview(int? id)
        {
           

            using (var db = new Models.watermentdbEntities())
            {
                var model = new MainViewModel
                {

                    countries = db.countries_with_facilites_view.ToList(),
                    Anlegg = db.facilities.Where(x => x.locations_countries_Id == id).ToList(),

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
    }
}