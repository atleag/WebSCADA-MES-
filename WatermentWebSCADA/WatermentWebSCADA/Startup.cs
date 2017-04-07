using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using codingfreaks.samples.Identity.Models;


[assembly: OwinStartupAttribute(typeof(WatermentWebSCADA.Startup))]
namespace WatermentWebSCADA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           // CreateRoleAndUsers();
        }
        //private void CreateRoleAndUsers()
        //{
        //    ApplicationDbContext context = new ApplicationDbContext();

        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<MyRole>(context));
        //    var UserManager = new MyUserManager<MyUser>(new MyUserStore<MyUser>(context));


        //    // In Startup iam creating first Admin Role and creating a default Admin User    
        //    if (!roleManager.RoleExists("Admin"))
        //    {

        //        // first we create Admin rool   
        //        //var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //        var role = new MyRole();
        //        role.Name = "Admin";
        //        roleManager.Create(role);

        //        //Here we create a Admin super user who will maintain the website                  

        //        var user = new MyUser();
        //        user.UserName = "shanu";
        //        user.Email = "syedshanumcain@gmail.com";

        //        string userPWD = "A@Z200711";

        //        var chkUser = UserManager.Create(user, userPWD);

        //        //Add default User to Role Admin   
        //        if (chkUser.Succeeded)
        //        {
        //            var result1 = UserManager.AddToRole(user.Id, "Admin");

        //        }
        //    }

        //    // creating Creating Manager role    
        //    if (!roleManager.RoleExists("Manager"))
        //    {
        //        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //        role.Name = "Manager";
        //        roleManager.Create(role);

        //    }

        //    // creating Creating Employee role    
        //    if (!roleManager.RoleExists("Employee"))
        //    {
        //        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //        role.Name = "Employee";
        //        roleManager.Create(role);

        //    }
       // }
    }

}