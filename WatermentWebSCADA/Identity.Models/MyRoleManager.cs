using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace codingfreaks.samples.Identity.Models
{
    public class MyRoleManager : RoleManager<MyRole, long>
    {
        public MyRoleManager(IRoleStore<MyRole, long> roleStore)
            : base(roleStore)
        {
        }

        public static MyRoleManager Create(IdentityFactoryOptions<MyRoleManager> options, IOwinContext context)
        {
            return new MyRoleManager(new RoleStore<MyRole, long, MyUserRole>(context.Get<ApplicationDbContext>()));
        }
    }
}