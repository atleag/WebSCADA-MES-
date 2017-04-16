using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WatermentWebSCADA.Models;
using System.IO;
using System.Web.UI;

using WatermentWebSCADA.ViewModels;

namespace WatermentWebSCADA.Controllers
{
    public class ExportController : Controller
    {
        Models.watermentdbEntities db = new Models.watermentdbEntities();


        

        public ActionResult Index(int? id)
        {

            using (var db = new Models.watermentdbEntities())
            {
                var model = new MainViewModel
                {

                    AlarmList = db.alarms.Where(x => x.equipments_facilities_Id == id).Where(o => o.Status == "Active").ToList(),
              };
                return View(model);
            }
        }


        public ActionResult ExportClientsListToExcel()
        {

            using (var db = new Models.watermentdbEntities())
            {
                var model = new MainViewModel
                {

                    AlarmList = db.alarms.Where(o => o.Status == "Active").ToList(),




                };
                var grid = new System.Web.UI.WebControls.GridView();

                grid.DataSource = 
                                  from d in db.measurements.ToList()
                                  select new
                                  {
                                      ID = d.Id,
                                      ProcessValue = d.ProcessValue,
                                      Recorded = d.Recorded,
                                      Equipment = d.equipments_Id,
                                      EqupmentName = d.equipments.Tag
                                      

                                  };

                grid.DataBind();

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=MeasurementList.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);

                Response.Write(sw.ToString());

                Response.End();

                return null;
            }
        }

        
    }
}
