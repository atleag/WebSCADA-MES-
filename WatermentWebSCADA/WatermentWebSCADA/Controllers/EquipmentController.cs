using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatermentWebSCADA.Models;
using WatermentWebSCADA.ViewModels;
using System.Threading.Tasks;
using System.Net;
using WatermentWebSCADA.CustomFilters;

namespace WatermentWebSCADA.Controllers
{
    public class EquipmentController : Controller
    {

        /// <summary>
        /// The ActionResult shows the equipment for the selected facility.
        /// </summary>
        /// <param name="id">The ID of the facility.</param>
        /// <returns></returns>
        [AuthLog(Roles = "Admin, Superuser, Maintenance, User")]
        // GET: Equipment with their measurred values.
        public ActionResult Index(int? id)
        {
            //dbcontect class
            watermentdbEntities db = new watermentdbEntities();
            // to hold list of Customer and order details
            List<EquipmentVM> facilityEquipmentVM = new List<EquipmentVM>();
            var equipmentlist = (from Eq in db.equipments.Where(x => x.facilities_Id == id)
                                select new {Eq.Id, Eq.Tag, Eq.SIUnits, Eq.Description, Eq.LastCalibrated, Eq.InstallDate, Eq.Manufacturer, Eq.TypeSpecification, Eq.facilities_Id}).ToList();
            //query getting data from database from joining two tables and storing data in customerlist
            foreach (var item in equipmentlist)
            {
                EquipmentVM fevm = new EquipmentVM(); // ViewModel
                fevm.Tag = item.Tag;
                fevm.SIUnits = item.SIUnits;
                fevm.Description = item.Description;
                fevm.LastCalibrated = item.LastCalibrated;
                fevm.InstallDate = item.InstallDate;
                fevm.Manufacturer = item.Manufacturer;
                fevm.TypeSpecification = item.TypeSpecification;
                fevm.facilities_Id = item.facilities_Id;
                facilityEquipmentVM.Add(fevm);
            }
            //Using foreach loop fill data from custmerlist to List<CustomerVM>.
            return View(facilityEquipmentVM); //List of CustomerVM (ViewModel)
        }

        
        /// <summary>
        /// Used to create the equipment. The list, which is a bit hacky, makes sure that the equipment models is implcity assigned to the desierd facility
        /// </summary>
        /// <param name="id">Id of the facility.</param>
        /// <returns></returns>
        [AuthLog(Roles = "Admin, Superuser")]
        public ActionResult CreateEquipment(int id)
        {
            try
            {
                using (watermentdbEntities context = new watermentdbEntities())
                {
                    //Imprort the list of facilities. Should just be one, so could have made this a simple object.
                    //This is made to make sure the equipment is added to the proper facility.
                    SelectList FacilityList = new SelectList(context.facilities.Where(x => x.Id == id).ToList(), "Id", "Name");
                    //This view data string may be called in the view
                    ViewData["Facilities"] = FacilityList;
                    ViewData.Model = new EquipmentAddVM();
                    return View();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "The creation was not sucessful.");
                throw;
            }

        }



        /// <summary>
        /// The ActionResult saves the newly added equipment to the db context.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AuthLog(Roles = "Admin, Superuser")]
        [HttpPost]
        public ActionResult CreateEquipment(EquipmentAddVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEquipment", model);
            }
            else
            {
                    using (watermentdbEntities db = new watermentdbEntities())
                    {
                        db.equipments.Add(new equipments
                        {
                            Tag = model.Tag,
                            SIUnits = model.SIUnits,
                            Description = model.Description,
                            LastCalibrated = model.LastCalibrated,
                            InstallDate = model.InstallDate,
                            TypeSpecification = model.TypeSpecification,
                            Manufacturer = model.Manufacturer,
                            facilities_Id = model.facilities_Id
                        });
                        db.SaveChanges();
                        ModelState.Clear();
                    }
                    return RedirectToAction("Index", new { id = model.facilities_Id });
               // var db = new watermentdbEntities();
                //Need to create some error handling here.
            }

           
        }


        /// <summary>
        /// Action to deleted the selected item from the database.
        /// </summary>
        /// <param name="id">Id of the equipment</param>
        /// <returns></returns>
        [AuthLog(Roles = "Admin, Superuser")]
        public ActionResult Delete(int? id)
        {
            watermentdbEntities db = new watermentdbEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipments eq = db.equipments.Find(id);
            if (eq == null)
            {
                return HttpNotFound();
            }
            return View(eq);
        }

        /// <summary>
        /// used to confirm the deletation
        /// </summary>
        /// <param name="id">Id of the equipment</param>
        /// <returns></returns>
        [AuthLog(Roles = "Admin, Superuser")]
        // POST: /Equipment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            watermentdbEntities wdbe = new watermentdbEntities();
            equipments evm = wdbe.equipments.Find(id);
            wdbe.equipments.Remove(evm);
            wdbe.SaveChanges();
            return RedirectToAction("Details");
        }


    }
}