// Purpose: Enable ASP.NET Identity to support T4/DB-First and long / INT(11) as Primary Key.
// All credits for this custom project / framework class goes to codingfreaks.de 
// URL 1: http://www.codingfreaks.de/2014/01/11/microsoft-aspnet-identity-in-bestender-anwendung-einsetzen/
// URL 2: http://www.codingfreaks.de/2014/06/16/microsoft-aspnet-identity-in-bestehender-anwendung-einsetzen-teil-2/
// - Moskoskos

namespace codingfreaks.samples.Identity.Models
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;


    /// <summary>
    /// Model for User. Inhertits from IdentityUser and is made to take "long" as primary key type. Additonal attributes are added within the class to extend the fields to be filled in during creation.
    /// </summary>
    public class MyUser : IdentityUser<long, MyLogin, MyUserRole, MyClaim>
    {
        #region properties

        public string ActivationToken { get; set; }

        public string PasswordAnswer { get; set; }

        public string PasswordQuestion { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }




        #endregion

        #region methods

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(MyUserManager userManager)
        {
            var userIdentity = await userManager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        #endregion
    }
}
