using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WatermentWebSCADA.Models;
using System.Web.Mvc;

namespace WatermentWebSCADA.ViewModels
{
    public class UserManagementViewModel
    {

    }
    public class UsersAndRolesVM
    {
        [Display(Name = "User Id")]
        public int UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Assigned Role")]
        public string RoleName { get; set; }
    }
    public class UserAndFacilityVM
    {
        [Display(Name = "User Id")]
        public int UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Facility Id")]
        public int FacilityId { get; set; }

        [Display(Name = "Facility Name")]
        public string FacilityName { get; set; }

        [Display(Name = "Facility Serial Number")]
        public string SerialNumber { get; set; }
    }

    public class UserAndFacilityLinkVM
    {
        [Display(Name = "User Name")]
        public int SelectedUserNameId { get; set; }

        public List<SelectListItem> ddlUsers { get; set; }


        [Display(Name = "Facility Name")]
        public int SelectedFacilityId { get; set; }


        public List<SelectListItem> ddlFacilities { get; set; }



    }

}