using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatermentWebSCADA.ViewModels;
using WatermentWebSCADA.Models;
using System.Threading.Tasks;
using System.Net;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace WatermentWebSCADA.Controllers
{
    public class UserManagementController : Controller
    {
        // GET: UsersManagement
        public ActionResult Index()
        {
            watermentdbEntities context = new watermentdbEntities();
            List<UsersAndRolesVM> fevmReturn = new List<UsersAndRolesVM>();


            var userrolelist = (from u in context.User
                                join ur in context.UserRole on u.Id equals ur.UserId
                                join r in context.Role on ur.RoleId equals r.Id
                                orderby u.Id
                                select new { u.Id, u.UserName, r.Name }).ToList();

            foreach (var item in userrolelist)
            {
                UsersAndRolesVM fevm = new UsersAndRolesVM(); // ViewModel
                fevm.UserId = item.Id;
                fevm.UserName = item.UserName;
                fevm.RoleName = item.Name;

                fevmReturn.Add(fevm);
            }
            //Using foreach loop fill data from custmerlist to List<CustomerVM>.
            return View(fevmReturn);
        }

        // GET: UsersManagement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersManagement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersManagement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersManagement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersManagement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersManagement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UserFacility()
        {
            watermentdbEntities context = new watermentdbEntities();
            List<UserAndFacilityVM> uafvmReturn = new List<UserAndFacilityVM>();


            var userFacility = (from u in context.User
                                join uf in context.users_has_facilities on u.Id equals uf.users_Id 
                                join f in context.facilities on uf.facilities_Id equals f.Id
                                orderby u.Id
                                select new {u.Id, u.UserName, f.Name, f.SerialNumber }).ToList();

            foreach (var item in userFacility)
            {
                UserAndFacilityVM uafvm = new UserAndFacilityVM(); // ViewModel
                uafvm.UserName = item.UserName;
                uafvm.FacilityName = item.Name;
                uafvm.SerialNumber = item.SerialNumber;

                uafvmReturn.Add(uafvm);
            }
            //Using foreach loop fill data from custmerlist to List<CustomerVM>.
            return View(uafvmReturn);
        }

        //[AcceptVerbs("POST")]
        //public ActionResult LinkUserFacility(User newUser)
        //{
        //    watermentdbEntities db = new watermentdbEntities();
        //    if (ModelState.IsValid)
        //    {
        //        var facilityId = Request["FacilityID"];
        //        var facIds = facilityId.Split(',');
        //        foreach (var catId in facIds)
        //        {
        //            int id = int.Parse(catId);

        //            facilities facilitiess = db.facilities.SingleOrDefault(d => d.Id == id);
        //            newUser.facilities = new List<facilities>();
        //            newUser.facilities.Add(facilitiess);
        //        }
        //        db.User.Add(newUser);
        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}
        /// <summary>
        /// Shows the users and their role. 
        /// </summary>
        /// <returns></returns>
        /// ¨Based on http://stackoverflow.com/questions/41933985/how-to-join-3-tables-with-linq
        //public async Task<ActionResult> GetRolesForUser()
        //{
        //    //watermentdbEntities context = new watermentdbEntities();
        //    //List<UsersAndRolesVM> fevmReturn = new List<UsersAndRolesVM>();


        //    //var userrolelist = (from u in context.User
        //    //                    join ur in context.UserRole on u.Id  equals ur.UserId
        //    //                    join r in context.Role on ur.RoleId equals r.Id
        //    //                    orderby u.Id
        //    //                    select new { u.Id, u.UserName, r.Name }).ToList();

        //    //foreach (var item in userrolelist)
        //    //{
        //    //    UsersAndRolesVM fevm = new UsersAndRolesVM(); // ViewModel
        //    //    fevm.UserId = item.Id;
        //    //    fevm.UserName = item.UserName;
        //    //    fevm.RoleName = item.Name;

        //    //    fevmReturn.Add(fevm);
        //    //}
        //    ////Using foreach loop fill data from custmerlist to List<CustomerVM>.
        //    //return View(fevmReturn);
        //}

    }
}
