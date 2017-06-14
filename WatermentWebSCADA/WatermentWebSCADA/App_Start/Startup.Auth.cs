namespace WatermentWebSCADA
{
    using System;

    using codingfreaks.samples.Identity.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using System.Web.Http;
    using System.Web.Security;

    using Owin;

    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864

        #region methods

        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<MyUserManager>(MyUserManager.Create);
            app.CreatePerOwinContext<MyRoleManager>(MyRoleManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(
                new CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    LoginPath = new PathString("/Account/Login"),
                    Provider = new CookieAuthenticationProvider
                    {
                        OnValidateIdentity =
                            SecurityStampValidator.OnValidateIdentity<MyUserManager, MyUser, long>(
                                TimeSpan.FromMinutes(30),
                                (manager, user) => user.GenerateUserIdentityAsync(manager),
                                identity => long.Parse(identity.GetUserId()))
                    }
                });



            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //// Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
        //protected override void Seed(SecurityModule.DataContexts.IdentityDb context)
        //{
        //    if (!context.Roles.Any(r => r.Name == "AppAdmin"))
        //    {
        //        var store = new RoleStore<IdentityRole>(context);
        //        var manager = new RoleManager<IdentityRole>(store);
        //        var role = new IdentityRole { Name = "AppAdmin" };

        //        manager.Create(role);
        //    }

        //    if (!context.Users.Any(u => u.UserName == "founder"))
        //    {
        //        var store = new UserStore<ApplicationUser>(context);
        //        var manager = new UserManager<ApplicationUser>(store);
        //        var user = new ApplicationUser { UserName = "founder" };

        //        manager.Create(user, "ChangeItAsap!");
        //        manager.AddToRole(user.Id, "AppAdmin");
        //    }
        //}

        #endregion
    }
}
