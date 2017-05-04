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
using System.Net.NetworkInformation;
using WatermentWebSCADA.CustomFilters;

namespace WatermentWebSCADA.Controllers
{
    public class MainSiteController : Controller
    {
        Models.watermentdbEntities db = new Models.watermentdbEntities();
        Models.watermentdbEntities db2 = new Models.watermentdbEntities();


        

        public bool PingHost(string ip)
        {
            
            bool pingable = false;
            Ping pinger = new Ping();

            try
            {
                PingReply reply = pinger.Send(ip,200);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
             
            }
            catch (Exception)
            {

            }
            return pingable;
        }

        string IpFacility;

        [AuthLog(Roles = "Admin, Superuser, Maintenance")]
        // GET: Main
        public ActionResult Index(int? id, string sortOrder)
        {
            foreach (var item in db.facilities)
            {
                item.IP = "";
                foreach (var item2 in db2.Client_Conection.Where(c => c.user == item.Id.ToString()))
                {
                    item.IP = item2.ip;
                }
            }
            // Fetches the current row id seleted in the table facilities and sets the facility IP to
            // the Client IP.
            foreach (var item in db.facilities)
            {
                IpFacility = item.IP;

               if ( PingHost(IpFacility) == true)
                {
                    item.FacilityStatus_Id = 2;

                }
               else
                {
                    item.FacilityStatus_Id = 1;
                }

           
              
            }
            db.SaveChanges();

            using (var db1 = new Models.watermentdbEntities())

                {
                    var model = new MainViewModel
                    {
                        //Getting desired data from the database, and returning it to the view.
                        Countries = db.countries.Where(c => c.continents.Id == id).ToList(),
                        Alarmer = db.alarms.Where(o => o.Status == "Active").ToList(),
                        Equipment = db.equipments.Include(c => c.alarms).Include(c => c.facilities).ToList(),
                        Lokasjoner = db.locations.ToList(),
                        Vedlikehold = db.maintenance.OrderBy(x => x.LastMaintenance).Take(10).ToList(),
                        Facilites = db.facilities.ToList(),

                        antallFacilities = db.facilities.Count(),
                        antallOnline = db.facilities.Where(x => x.FacilityStatus_Id == 2).Count(),
                        antallOffline = db.facilities.Where(x => x.FacilityStatus_Id == 1).Count(),
                        noAlarms = db.alarms.Where(x=> x.Status == "Active").Count(),
                    };




                    return View(model);

                }
            }
        [AllowAnonymous]
        public ActionResult About()
        {
            return View();           
        }

        }

    }

