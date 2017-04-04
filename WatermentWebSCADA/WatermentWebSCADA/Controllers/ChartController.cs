using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatermentWebSCADA.ViewModels;

namespace WatermentWebSCADA.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult AxisZooming()
        {
            //return View(DateTimeXAxisChartData.GetRandom());
            return View();
        }
    }
}