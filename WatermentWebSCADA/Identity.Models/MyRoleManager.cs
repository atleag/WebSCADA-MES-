// Purpose: Enable ASP.NET Identity to support T4/DB-First and long / INT(11) as Primary Key.
// All credits for this custom project / framework class goes to codingfreaks.de 
// URL 1: http://www.codingfreaks.de/2014/01/11/microsoft-aspnet-identity-in-bestender-anwendung-einsetzen/
// URL 2: http://www.codingfreaks.de/2014/06/16/microsoft-aspnet-identity-in-bestehender-anwendung-einsetzen-teil-2/
// - Moskoskos


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
    /// <summary>
    /// Extension of codefreaks custom framework for Identity to handle the creation of roles.
    /// Moskoskos
    /// </summary>
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

