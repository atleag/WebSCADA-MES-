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
using WatermentWebSCADA.CustomFilters;


namespace WatermentWebSCADA.Controllers
{

    //IMPORTANT!!!! READ THIS FOR MANY TO MANY: https://www.codeproject.com/tips/893609/crud-many-to-many-entity-framework
    public class UserManagementController : Controller
    {

        //Source for multiple entities with same name (u.Id, r.Id) 
        // http://stackoverflow.com/questions/3454996/how-to-select-same-columns-name-from-different-table-in-linq
        // GET: UsersManagement
        [AuthLog(Roles = "Admin, SuperUser")]
        public ActionResult Index()
        {
            watermentdbEntities context = new watermentdbEntities();
            List<UsersAndRolesVM> fevmReturn = new List<UsersAndRolesVM>();


            var userrolelist = (from u in context.User
                                join ur in context.UserRole on u.Id equals ur.UserId
                                join r in context.Role on ur.RoleId equals r.Id
                                orderby u.Id
                                select new { u.Id, u.UserName, rId = r.Id, r.Name }).ToList();

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

        [AuthLog(Roles = "Admin, SuperUser, Maintenance")]
        public ActionResult UserFacility()
        {
            watermentdbEntities context = new watermentdbEntities();
            List<UserAndFacilityVM> uafvmReturn = new List<UserAndFacilityVM>();


            var userFacility = (from u in context.User
                                join uf in context.users_has_facilities on u.Id equals uf.users_Id 
                                join f in context.facilities on uf.facilities_Id equals f.Id
                                orderby u.Id
                                select new {uId = u.Id, u.UserName, fid = f.Id, f.Name, f.SerialNumber }).ToList();

            foreach (var item in userFacility)
            {
                UserAndFacilityVM uafvm = new UserAndFacilityVM(); // ViewModel
                uafvm.UserId = item.uId;
                uafvm.UserName = item.UserName;
                uafvm.FacilityId = item.fid;
                uafvm.FacilityName = item.Name;
                uafvm.SerialNumber = item.SerialNumber;

                uafvmReturn.Add(uafvm);
            }
            //Using foreach loop fill data from custmerlist to List<CustomerVM>.
            return View(uafvmReturn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">Id of the user which is to be assigned</param>
        /// <param name="facilityId">Id of facility which is to be assigned.</param>
        /// Source: https://www.codeproject.com/tips/893609/crud-many-to-many-entity-framework
        public void InsertWithData(int userId, int facilityId)
        {
            using (watermentdbEntities conn = new watermentdbEntities())
            {

                /*
                    * this steps follow to both entities
                    * 
                    * 1 - create instance of entity with relative primary key
                    * 
                    * 2 - add instance to context
                    * 
                    * 3 - attach instance to context
                    */

                // 1
                User u = new User { Id = userId };
                // 2
                conn.User.Add(u);
                // 3
                conn.User.Attach(u);

                // 1
                facilities f = new facilities { Id = facilityId };
                // 2
                conn.facilities.Add(f);
                // 3
                conn.facilities.Attach(f);

                // like previous method add instance to navigation property
                u.users_has_facilities.Add(f);

                // call SaveChanges
                conn.SaveChanges();
            }
        }

    }
}
