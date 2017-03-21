using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatermentWebSCADA.Models;
using WatermentWebSCADA.ViewModels;

namespace WatermentWebSCADA.Controllers
{
    public class MainController : Controller
    {
        private watermentdbEntities db = new watermentdbEntities();
        // GET: Main
        public ActionResult Index(int? id)
        {

            var data = new MainViewModel
            {
                IP = db.facilities.Select(x => x.IP).FirstOrDefault(),
                Name = db.facilities.Select(x => x.Name).ToList(),
                Address = db.country.Select(x => x.CountryName).FirstOrDefault(),
            };

            return View(data);
        }
    }
}