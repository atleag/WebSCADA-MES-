using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatermentWebSCADA.Controllers
{
    public class mainController : Controller
    {
        private watermentdbEntities db = new watermentdbEntities();
        // GET: Main
        public ActionResult Index(int? id)
        {

            var data = new mai
            {
                IP = db.facilities.Select(x => x.IP).FirstOrDefault(),
                Name = db.facilities.Select(x => x.Name).FirstOrDefault(),
                Address = db.country.Select(x => x.CountryName).FirstOrDefault(),
            };

            return View(data);
        }
    }