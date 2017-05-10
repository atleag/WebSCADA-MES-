
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System;
using codingfreaks.samples.Identity.Models;

using WatermentWebSCADA.ViewModels;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using WatermentWebSCADA.Models;


namespace WatermentWebSCADA.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        #region constants

        private const string XsrfKey = "XsrfId";
        const string roleNameAdmin = "Admin";

        #endregion

        #region member vars

        private MyUserManager _userManager;


        #endregion

        #region constructors and destructors

        public AccountController()
        {
        }

        public AccountController(MyUserManager userManager)
        {
            UserManager = userManager;


        }

        #endregion

        #region enums

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        #endregion

        #region properties

        public MyUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<MyUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Method is currently not used. Used in the event when a user is registrated and it is disairable to confirm that the email is correct.
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="code">The confirmation code</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(long userId, string code)
        {
            if (userId == 0 || code == null)
            {
                return View("Error");
            }

            var result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }
            AddErrors(result);
            return View();
        }



        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    ModelState.AddModelError("", "The user either does not exist or is not confirmed.");
                    return View();
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }



        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction(
                    "Manage",
                    new
                    {
                        Message = ManageMessageId.Error
                    });
            }
            var result = await UserManager.AddLoginAsync(long.Parse(User.Identity.GetUserId()), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction(
                "Manage",
                new
                {
                    Message = ManageMessageId.Error
                });
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Called on user login attempt. Takes the model and the return URL and checks what to do with the login request.
        /// 
        /// </summary>
        /// <param name="model">Model that contains the login credentials</param>
        /// <param name="returnUrl">Url to redirect other roles than "user" to the main index.</param>
        /// <returns></returns>
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);
                if (user != null)
                {
                    ///Checks to see if not normal user
                    if (await UserManager.IsInRoleAsync(user.Id,"Admin") || await UserManager.IsInRoleAsync(user.Id, "SuperUser") || await UserManager.IsInRoleAsync(user.Id, "Maintenance"))
                    {
                        await SignInAsync(user, model.RememberMe);
                        return RedirectToLocal(returnUrl);
                    }
                    if(await UserManager.IsInRoleAsync(user.Id,"User"))
                    {
                        using (watermentdbEntities context = new watermentdbEntities())
                        {
                            //Try to find the facility of user
                            try
                            {
                                var facilityId = (from f in context.facilities
                                                  where f.User_Id == (user.Id)
                                                  select f).SingleOrDefault();

                                int userDestination = facilityId.Id;
                                await SignInAsync(user, model.RememberMe);
                                return RedirectToAction("FacilityDetails","Facility", new { id = userDestination });
                            }
                            //If user does not belong to facility, redirect to site regarding not having access yet
                            catch (System.Exception)
                            {

                                throw;
                            }
                        }
                    }
                    //Not registrated
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password.");
                    }
                    
                    
                }
                ModelState.AddModelError("", "Invalid username or password.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //Let admin change a user's password with identity
        //Source: https://forums.asp.net/t/1948882.aspx?No+way+for+Administrator+to+Change+Password+


        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage = message == ManageMessageId.ChangePasswordSuccess
                ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess
                    ? "Your password has been set."
                    : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed." : message == ManageMessageId.Error ? "An error has occurred." : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }


        /// <summary>
        /// In the event of a change of password, the model is processed with the old and new password.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            var hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    var result = await UserManager.ChangePasswordAsync(long.Parse(User.Identity.GetUserId()), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(long.Parse(User.Identity.GetUserId()));
                        await SignInAsync(user, false);
                        return RedirectToAction(
                            "Manage",
                            new
                            {
                                Message = ManageMessageId.ChangePasswordSuccess
                            });
                    }
                    AddErrors(result);
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                var state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    var result = await UserManager.AddPasswordAsync(long.Parse(User.Identity.GetUserId()), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(
                            "Manage",
                            new
                            {
                                Message = ManageMessageId.SetPasswordSuccess
                            });
                    }
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        
        /// <summary>
        /// Returns the register view with a list of available roles.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");
            return View();
        }

        /// <summary>
        /// Processed when the "register" button in the view is activated.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string roleName = "";
                roleName = model.Name;
                var user = new MyUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                    

                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await this.UserManager.AddToRoleAsync(user.Id, roleName);

                    //Enable userregister email confirmation.

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "UserManagement");
                }
                AddErrors(result);
                
            }

            // Something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Not used
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(long.Parse(User.Identity.GetUserId()));
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        /// <summary>
        /// Returns the reset password view.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (code == null)
            {
                return View("Error");
            }
            return View();
        }

        /// <summary>
        /// Process the input of the reset password request.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "No user found.");
                    return View();
                }
                var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                AddErrors(result);
                return View();
            }

            // Something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(long.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        /// <summary>
        /// Used for when admins, superusers and maintenace accounts login.
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "MainSite");
        }

        //Not implemented
        private void SendEmail(string email, string callbackUrl, string subject, string message)
        {
            // For information on sending mail, please visit http://go.microsoft.com/fwlink/?LinkID=320771
        }

        //Claims http://stackoverflow.com/questions/32880269/how-to-do-session-management-in-aspnet-identity
        private async Task SignInAsync(MyUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(
                new AuthenticationProperties
                {
                    IsPersistent = isPersistent
                },
                await user.GenerateUserIdentityAsync(UserManager));
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #endregion


        private class ChallengeResult : HttpUnauthorizedResult
        {
            #region constructors and destructors

            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            #endregion

            #region properties

            public string LoginProvider { get; set; }

            public string RedirectUri { get; set; }

            public string UserId { get; set; }

            #endregion

            #region methods

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties
                {
                    RedirectUri = RedirectUri
                };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }



            #endregion
        }
    }
}