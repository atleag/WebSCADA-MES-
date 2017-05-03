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
        [AuthLog(Roles = "Admin, SuperUser")]
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: UsersManagement/Create
        [AuthLog(Roles = "Admin, SuperUser")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersManagement/Create
        [AuthLog(Roles = "Admin, SuperUser")]
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

        [AuthLog(Roles = "Admin, SuperUser")]
        // GET: UsersManagement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        [AuthLog(Roles = "Admin, SuperUser")]
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

        [AuthLog(Roles = "Admin, SuperUser")]
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
            //THESE lines of codes works only if the many to many table is visible to EF. 
            //var result = (from u in context.User
            //                    join uf in context.users_has_facilities on u.Id equals uf.users_Id
            //                    join f in context.facilities on uf.facilities_Id equals f.Id
            //                    orderby u.Id
            //                    select new { uId = u.Id, u.UserName, fid = f.Id, f.Name, f.SerialNumber }).ToList();

            using (watermentdbEntities context = new watermentdbEntities())
            {
                List<UserAndFacilityVM> uafvmReturn = new List<UserAndFacilityVM>();
                var result = (
                                //// instance from context
                                //from a in context.User
                                //    // instance from navigation property
                                //from b in a.facilities
                                //    //join to bring useful data
                                //join c in context.facilities on b.Id equals c.User_Id
                                 from a in context.User
                                 join b in context.facilities on a.Id equals b.User_Id
                                 select new
                                {
                                    uId = a.Id,
                                    a.UserName,
                                    b.Id,
                                    b.Name,
                                    b.SerialNumber
                                }).ToList();

                foreach (var item in result)
                {
                    UserAndFacilityVM uafvm = new UserAndFacilityVM(); // ViewModel
                    uafvm.UserId = item.uId;
                    uafvm.UserName = item.UserName;
                    uafvm.FacilityId = item.Id;
                    uafvm.FacilityName = item.Name;
                    uafvm.SerialNumber = item.SerialNumber;

                    uafvmReturn.Add(uafvm);

                }
                //Using foreach loop fill data from custmerlist to List<CustomerVM>.
                return View(uafvmReturn);
            }
        }

        [AuthLog(Roles = "Admin, SuperUser")]
        public ActionResult LinkUserAndFacility()
        {
            watermentdbEntities context = new watermentdbEntities();
            //ViewBag.Facility = new SelectList(context.facilities.ToList(), "Name", "Name");
            //ViewBag.Users = new SelectList(context.User.ToList(), "UserName", "UserName");
            //return View();

            SelectList UserList= new SelectList(context.User.ToList(), "Id", "UserName");
            SelectList FacilityList = new SelectList(context.facilities.Where(x => x.User_Id == null).ToList(), "Id", "Name");
            ViewData["Users"] = UserList;
            ViewData["Facilities"] = FacilityList;
            ViewData.Model = new UserAndFacilityLinkVM();
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">Id of the user which is to be assigned</param>
        /// <param name="facilityId">Id of facility which is to be assigned.</param>
        /// Source: https://www.codeproject.com/tips/893609/crud-many-to-many-entity-framework
        /// 

        [AuthLog(Roles = "Admin, SuperUser")]
        [HttpPost]
        public ActionResult LinkUserAndFacility(UserAndFacilityLinkVM model)
        {

            int userId = 0;
            int facilityId = 0;
            userId = model.SelectedUserNameId;
            facilityId = model.SelectedFacilityId;

            if (userId == 0 || facilityId == 0)
            {
                return View("LinkUserAndFacility");
            }
            else
            {

                using (watermentdbEntities db = new watermentdbEntities())
                {
                    var facility = (from x in db.facilities
                                    where x.Id == facilityId
                                    select x).First();
                    facility.User_Id = userId;
                    // call SaveChanges
                    db.SaveChanges();
                }
                return View("UserAndFacility");

            }
            return RedirectToAction("LinkUserAndFacility");
        }
        public void DeleteRelationship(int productID, int supplierID)
        {
            using (watermentdbEntities db = new watermentdbEntities())
            {
                // return one instance each entity by primary key
                var product = db.User.FirstOrDefault(u => u.Id == productID);
                var supplier = db.facilities.FirstOrDefault(s => s.Id == supplierID);

                // call Remove method from navigation property for any instance
                // supplier.Product.Remove(product);
                // also works
                product.facilities.Remove(supplier);

                // call SaveChanges from context
                db.SaveChanges();
            }
        }

    }
}
