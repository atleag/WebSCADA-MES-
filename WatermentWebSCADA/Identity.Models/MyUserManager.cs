// Purpose: Enable ASP.NET Identity to support T4/DB-First and long / INT(11) as Primary Key.
// All credits for this class goes to codingfreaks.de 
// URL 1: http://www.codingfreaks.de/2014/01/11/microsoft-aspnet-identity-in-bestender-anwendung-einsetzen/
// URL 2: http://www.codingfreaks.de/2014/06/16/microsoft-aspnet-identity-in-bestehender-anwendung-einsetzen-teil-2/
// - Moskoskos

namespace codingfreaks.samples.Identity.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;

    /// <summary>
    /// Custom implementation of the Identity UserManager.
    /// </summary>
    public class MyUserManager : UserManager<MyUser, long>
    {
        #region constructors and destructors


        /// <summary>
        /// 
        /// </summary>
        /// <param name="store"></param>
        public MyUserManager(IUserStore<MyUser, long> store) : base(store)
        {
        }

        #endregion

        #region methods

        public static MyUserManager Create(IdentityFactoryOptions<MyUserManager> options, IOwinContext context)
        {
            var manager = new MyUserManager(new UserStore<MyUser, MyRole, long, MyLogin, MyUserRole, MyClaim>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<MyUser, long>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            manager.RegisterTwoFactorProvider(
                "PhoneCode",
                new PhoneNumberTokenProvider<MyUser, long>
                {
                    MessageFormat = "Your security code is: {0}"
                });
            
            manager.RegisterTwoFactorProvider(
                "EmailCode",
                new EmailTokenProvider<MyUser, long>
                {
                    Subject = "Security Code",
                    BodyFormat = "Your security code is: {0}"
                });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<MyUser, long>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        #endregion
    }
}