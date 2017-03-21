using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatermentWebSCADA.Models;
using WatermentWebSCADA.ViewModels;

namespace WatermentWebSCADA.Controllers
{
    public class mainController : Controller
    {
        private watermentdbEntities db = new watermentdbEntities();
        // GET: Main
        public ActionResult Index(int? id)
        {

            var data = new mainViewModel {
                IP = db.facilities.Select(x => x.IP).FirstOrDefault(),
                Name = db.facilities.Select(x => x.Name).FirstOrDefault(),

                //Country[] = db.country.Select(x => x.CountryName).ToArray(),

                Postcode = db.facilities.Select(x=>x.location_Postcode).FirstOrDefault(),
                County = db.facilities.Select(x=>x.location.County).FirstOrDefault(),

                //var facilities = db.facilities.Include(f => f.location);
        };
            
            return View(data);
        }
    }
}