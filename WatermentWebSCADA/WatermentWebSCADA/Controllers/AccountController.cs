
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using codingfreaks.samples.Identity.Models;

using WatermentWebSCADA.ViewModels;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;


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


        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(long userId, string code)
        {
            if (userId == null || code == null)
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

        //
        // GET: /Account/ForgotPassword

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            var result = await UserManager.RemoveLoginAsync(long.Parse(User.Identity.GetUserId()), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(long.Parse(User.Identity.GetUserId()));
                await SignInAsync(user, false);
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction(
                "Manage",
                new
                {
                    Message = message
                });
        }

        //
        // GET: /Account/Manage

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(
                provider,
                Url.Action(
                    "ExternalLoginCallback",
                    "Account",
                    new
                    {
                        ReturnUrl = returnUrl
                    }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, false);
                return RedirectToLocal(returnUrl);
            }
            // If the user does not have an account, then prompt the user to create an account
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
            return View(
                "ExternalLoginConfirmation",
                new ExternalLoginConfirmationViewModel
                {
                    Email = loginInfo.Email
                });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new MyUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // SendEmail(user.Email, callbackUrl, "Confirm your account", "Please confirm your account by clicking this link");

                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
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
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
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
        // POST: /Account/ExternalLoginConfirmation

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

        //
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
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError("", "Invalid username or password.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

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

        //
        // POST: /Account/Manage
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

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
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
                    result = UserManager.AddToRole(user.Id, "User");

                    //REMEMBER TO REPLACE THIS CODE WITH SOMETHING ELSE. 
                    //await SignInAsync(user, false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Login", "Account");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(long.Parse(User.Identity.GetUserId()));
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (code == null)
            {
                return View("Error");
            }
            return View();
        }

        //
        // POST: /Account/ResetPassword
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

            // If we got this far, something failed, redisplay form
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

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "MainSite");
        }

        private void SendEmail(string email, string callbackUrl, string subject, string message)
        {
            // For information on sending mail, please visit http://go.microsoft.com/fwlink/?LinkID=320771
        }

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

        #region
        //public async Task<ActionResult> Create(RoleVM roleViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Initialize ApplicationRole instead of IdentityRole:
        //        var role = new MyRole(roleViewModel.Name);
        //        var roleresult = await MyRoleManager.CreateAsync(role);
        //        if (!roleresult.Succeeded)
        //        {
        //            ModelState.AddModelError("", roleresult.Errors.First());
        //            return View();
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
        ////RoleManagement
        //[Authorize(Roles = "Admin")]
        //public ActionResult RoleCreate()
        //{
        //    return View();
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult RoleCreate(string roleName)
        //{
        //    var roleManager = HttpContext.GetOwinContext().Get<MyRoleManager>();



        //    //Create Role Admin if it does not exist
        //    var role = roleManager.FindByName(roleName);
        //    if (role == null)
        //    {
        //        role = new MyRole();
        //        role.Name = roleName;

        //        var roleresult = roleManager.Create(role);
        //    }


        //    using (var rolesManager = HttpContext.GetOwinContext().Get<MyRoleManager>())
        //    {
        //        var roleStore = new RoleStore<MyRole>(context);
        //        var _userManager = new RoleManager<MyRole>(roleStore);

        //        rolesManager.Create(new MyRole, long(roleName));
        //    }

        //    ViewBag.ResultMessage = "Role created successfully !";
        //    return RedirectToAction("RoleIndex", "Account");
        //}

        //[Authorize(Roles = "Admin")]
        //public ActionResult RoleIndex()
        //{
        //    List<string> roles;
        //    using (var context = new ApplicationDbContext())
        //    {
        //        var roleStore = new RoleStore<MyRole, long>(context);
        //        var roleManager = new RoleManager<MyRole, long >(roleStore);

        //        roles = (from r in roleManager.Roles select r.Name).ToList();
        //    }

        //    return View(roles.ToList());
        //}

        //[Authorize(Roles = "Admin")]
        //public ActionResult RoleDelete(string roleName)
        //{
        //    using (var context = new ApplicationDbContext())
        //    {
        //        var roleStore = new RoleStore<MyRole>(context);
        //        var roleManager = new RoleManager<MyRole>(roleStore);
        //        var role = roleManager.FindByName(roleName);

        //        roleManager.Delete(role);
        //        context.SaveChanges();
        //    }

        //    ViewBag.ResultMessage = "Role deleted succesfully !";
        //    return RedirectToAction("RoleIndex", "Account");
        //}

        //[Authorize(Roles = "Admin")]
        //public ActionResult RoleAddToUser()
        //{
        //    List<string> roles;
        //    List<string> users;
        //    using (var context = new ApplicationDbContext())
        //    {
        //        var roleStore = new RoleStore<MyRole>(context);
        //        var roleManager = new RoleManager<MyRole>(roleStore);

        //        var userStore = new UserStore<MyUser>(context);
        //        var userManager = new UserManager<MyUser>(userStore);

        //        users = (from u in userManager.Users select u.UserName).ToList();
        //        roles = (from r in roleManager.Roles select r.Name).ToList();
        //    }

        //    ViewBag.Roles = new SelectList(roles);
        //    ViewBag.Users = new SelectList(users);
        //    return View();
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult RoleAddToUser(string roleName, string userName)
        //{
        //    List<string> roles;
        //    List<string> users;
        //    using (var context = new ApplicationDbContext())
        //    {
        //        var roleStore = new RoleStore<MyRole>(context);
        //        var roleManager = new RoleManager<IdentityRole>(roleStore);

        //        var userStore = new UserStore<MyUser>(context);
        //        var userManager = new UserManager<MyUser>(userStore);

        //        users = (from u in userManager.Users select u.UserName).ToList();

        //        var user = userManager.FindByName(userName);
        //        if (user == null)
        //            throw new Exception("User not found!");

        //        var role = roleManager.FindByName(roleName);
        //        if (role == null)
        //            throw new Exception("Role not found!");

        //        if (userManager.IsInRole(user.Id, role.Name))
        //        {
        //            ViewBag.ResultMessage = "This user already has the role specified !";
        //        }
        //        else
        //        {
        //            userManager.AddToRole(user.Id, role.Name);
        //            context.SaveChanges();

        //            ViewBag.ResultMessage = "Username added to the role succesfully !";
        //        }

        //        roles = (from r in roleManager.Roles select r.Name).ToList();
        //    }

        //    ViewBag.Roles = new SelectList(roles);
        //    ViewBag.Users = new SelectList(users);
        //    return View();
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult GetRoles(string userName)
        //{
        //    if (!string.IsNullOrWhiteSpace(userName))
        //    {
        //        List<string> userRoles;
        //        List<string> roles;
        //        List<string> users;
        //        using (var context = new ApplicationDbContext())
        //        {
        //            var roleStore = new RoleStore<MyRole>(context);
        //            var roleManager = new RoleManager<MyRole>(roleStore);

        //            roles = (from r in roleManager.Roles select r.Name).ToList();

        //            var userStore = new UserStore<MyUser>(context);
        //            var userManager = new UserManager<MyUser>(userStore);

        //            users = (from u in userManager.Users select u.UserName).ToList();

        //            var user = userManager.FindByName(userName);
        //            if (user == null)
        //                throw new Exception("User not found!");

        //            var userRoleIds = (from r in user.Roles select r.RoleId);
        //            userRoles = (from id in userRoleIds
        //                         let r = roleManager.FindById(id)
        //                         select r.Name).ToList();
        //        }

        //        ViewBag.Roles = new SelectList(roles);
        //        ViewBag.Users = new SelectList(users);
        //        ViewBag.RolesForThisUser = userRoles;
        //    }

        //    return View("RoleAddToUser");
        //}

        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteRoleForUser(string userName, string roleName)
        //{
        //    List<string> userRoles;
        //    List<string> roles;
        //    List<string> users;
        //    using (var context = new ApplicationDbContext())
        //    {
        //        var roleStore = new RoleStore<MyRole>(context);
        //        var roleManager = new RoleManager<MyRole>(roleStore);

        //        roles = (from r in roleManager.Roles select r.Name).ToList();

        //        var userStore = new UserStore<MyUser>(context);
        //        var userManager = new UserManager<MyUser>(userStore);

        //        users = (from u in userManager.Users select u.UserName).ToList();

        //        var user = userManager.FindByName(userName);
        //        if (user == null)
        //            throw new Exception("User not found!");

        //        if (userManager.IsInRole(user.Id, roleName))
        //        {
        //            userManager.RemoveFromRole(user.Id, roleName);
        //            context.SaveChanges();

        //            ViewBag.ResultMessage = "Role removed from this user successfully !";
        //        }
        //        else
        //        {
        //            ViewBag.ResultMessage = "This user doesn't belong to selected role.";
        //        }

        //        var userRoleIds = (from r in user.Roles select r.RoleId);
        //        userRoles = (from id in userRoleIds
        //                     let r = roleManager.FindById(id)
        //                     select r.Name).ToList();
        //    }

        //    ViewBag.RolesForThisUser = userRoles;
        //    ViewBag.Roles = new SelectList(roles);
        //    ViewBag.Users = new SelectList(users);
        //    return View("RoleAddToUser");
        //}

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