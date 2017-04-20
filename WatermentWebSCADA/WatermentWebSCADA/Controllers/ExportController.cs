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





        public ActionResult ExportTempMeasurementsToExcel(int? id)
        {

            using (var db = new Models.watermentdbEntities())
            {
           
                var grid = new System.Web.UI.WebControls.GridView();

                grid.DataSource =
                                  from d in db.measurements.Where(x => x.equipments_facilities_Id == id).Where(x => x.equipments.SIUnits == "Degrees").ToList()
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
                Response.AddHeader("content-disposition", "attachment; filename=TempMeasurementList.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);

                Response.Write(sw.ToString());

                Response.End();

                return null;
            }
        }
        public ActionResult ExportBarMeasurementsToExcel(int? id)
        {

            using (var db = new Models.watermentdbEntities())
            {

                var grid = new System.Web.UI.WebControls.GridView();

                grid.DataSource =
                                  from d in db.measurements.Where(x => x.equipments_facilities_Id == id).Where(x => x.equipments.SIUnits == "Bar").ToList()
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
                Response.AddHeader("content-disposition", "attachment; filename=BarMeasurementList.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);

                Response.Write(sw.ToString());

                Response.End();

                return null;
            }
        }

        public ActionResult ExportAlarmsToExcel(int? id)
        {
            using (var db = new Models.watermentdbEntities())
            {

                var grid = new System.Web.UI.WebControls.GridView();

                grid.DataSource =
                                  from d in db.alarms.Where(x => x.equipments_facilities_Id == id).ToList()
                                  select new
                                  {
                                      ID = d.Id,
                                      ProcessValue = d.ProcessValue,
                                      Recorded = d.AlarmOccured,
                                      Status = d.Status,
                                      EqupmentName = d.equipments.Tag,
                                      Description = d.Description


                                  };

                grid.DataBind();

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=AlarmList.xls");
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
