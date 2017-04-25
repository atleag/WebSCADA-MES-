using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WatermentWebSCADA.ViewModels
{
    public class UserManagementViewModel
    {

    }
    public class UsersAndRolesVM
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}