using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using WatermentWebSCADA.Models;

namespace WatermentWebSCADA.Controllers
{
    public class MaxViewController : Controller
    {
        // GET: MaxView
        public ActionResult Index()
        {
            MaxView viewModel = new MaxView();
            return View(viewModel);



        }
        public ActionResult MyEditActionOne(MaxView model)
        {
            if (ModelState.IsValid)
            {
                return View("Index", model);
            }

            throw new Exception("My Model state is not valid");
        }
    }
}