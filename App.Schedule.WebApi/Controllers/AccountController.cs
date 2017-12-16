using System;
using System.Web;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using App.Schedule.Domains;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using App.Schedule.WebApi.Models;
using System.Collections.Generic;
using App.Schedule.WebApi.Results;
using System.Security.Cryptography;
using Microsoft.Owin.Security.OAuth;
using App.Schedule.WebApi.Providers;
using App.Schedule.Domains.ViewModel;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Schedule.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null)
            {
                return null;
            }

            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

            foreach (IdentityUserLogin linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);
            
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                
                 ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

       
        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result); 
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(AdministratorViewModel model)
        {
            var result = new ResponseViewModel<tblAdministrator>();

            if (!ModelState.IsValid)
            {
                var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                result.Status = false;
                result.Message = errMessage;
                result.Data = null;
                return Ok(result);
            }
            else
            {
                var message = "";
                var data = new tblAdministrator();
                var adminController = new AdministratorController();
                var createStatus = adminController.RegisterAdmin(model, out data, out message);
                model.Email = "admin" + model.Email;
                if (createStatus)
                {
                    var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

                    var response = await UserManager.CreateAsync(user, model.Password);
                    if (response.Succeeded)
                    {
                        result.Status = createStatus;
                        result.Message = "Saved data successfully.";
                        result.Data = data;
                        return Ok(result);
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = string.Join(", ", response.Errors);
                        result.Data = null;
                        return Ok(result);
                    }
                }
                else
                {
                    result.Status = false;
                    result.Message = message;
                    result.Data = null;
                    return Ok(result);
                }
            }
        }


        // POST api/Account/UpdateAdmin
        [Route("UpdateAdmin")]
        public async Task<IHttpActionResult> UpdateAdmin(AdministratorViewModel model)
        {
            var result = new ResponseViewModel<AdministratorViewModel>();
            if (!ModelState.IsValid)
            {
                var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                result.Status = false;
                result.Message = errMessage;
            }
            var user = await UserManager.FindByEmailAsync(model.Email);
            var response = await UserManager.ChangePasswordAsync(user.Id, model.OldPassword, model.Password);
            if (response.Succeeded)
            {
                string message = "";
                var adminController = new AdministratorController();
                var updateStatus = adminController.UpdateAdmin(model, out message);
                if (updateStatus)
                {
                    result.Status = updateStatus;
                    result.Message = message;
                }
            }
            else
            {
                result.Status = false;
                result.Message = string.Join(", ", response.Errors).ToLower();
                if (result.Message.Contains("incorrect password"))
                    result.Message = "Please check your old password.";
            }
            return Ok(result);
        }


        // POST api/Account/Register
        [AllowAnonymous]
        [Route("RegisterBusinessEmployee")]
        public async Task<IHttpActionResult> RegisterBusinessEmployee(RegisterViewModel model)
        {
            var result = new ResponseViewModel<RegisterViewModel>();

            if (!ModelState.IsValid)
            {
                var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                result.Status = false;
                result.Message = errMessage;
                result.Data = null;
                return Ok(result);
            }
            else
            {
                var message = "";
                var status = false;
                var businessController = new BusinessController();
                var business = businessController.Register(model, out status, out message);
                model.Employee.Email = "emp" + model.Employee.Email;
                if (status)
                {
                    var user = new ApplicationUser() { UserName = model.Employee.Email, Email = model.Employee.Email };

                    var response = await UserManager.CreateAsync(user, model.Employee.Password);
                    if (response.Succeeded)
                    {
                        result.Status = status;
                        result.Message = "User has registered successfully.";
                        result.Data = business;
                        return Ok(result);
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = string.Join(", ", response.Errors);
                        result.Data = null;
                        return Ok(result);
                    }
                }
                else
                {
                    result.Status = false;
                    result.Message = message;
                    result.Data = null;
                    return Ok(result);
                }
            }
        }

        //// POST api/Account/UpdateAdmin
        //[Route("UpdateBusinessEmployee")]
        //public async Task<IHttpActionResult> UpdateBusinessEmployee(RegisterViewModel model)
        //{
        //    var result = new ResponseViewModel<AdministratorViewModel>();
        //    if (!ModelState.IsValid)
        //    {
        //        var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
        //        result.Status = false;
        //        result.Message = errMessage;
        //    }
        //    var user = await UserManager.FindByEmailAsync(model.Email);
        //    var response = await UserManager.ChangePasswordAsync(user.Id, model.OldPassword, model.Password);
        //    if (response.Succeeded)
        //    {
        //        string message = "";
        //        var adminController = new AdministratorController();
        //        var updateStatus = adminController.UpdateAdmin(model, out message);
        //        if (updateStatus)
        //        {
        //            result.Status = updateStatus;
        //            result.Message = message;
        //        }
        //    }
        //    else
        //    {
        //        result.Status = false;
        //        result.Message = string.Join(", ", response.Errors).ToLower();
        //        if (result.Message.Contains("incorrect password"))
        //            result.Message = "Please check your old password.";
        //    }
        //    return Ok(result);
        //}

        //[AllowAnonymous]
        //[HttpGet]
        //[Route("SignIn")]
        //public IHttpActionResult SignIn(string LoginId, string Password)
        //{
        //    var result = new ResponseViewModel<AdministratorViewModel>();
        //    var claims = new List<Claim>();
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(LoginId) && !string.IsNullOrEmpty(Password))
        //        {
        //            claims.Add(new Claim(ClaimTypes.Name, LoginId));
        //            var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
        //            var ctx = Request.GetOwinContext();
        //            var authenticationManager = ctx.Authentication;
        //            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, claimIdenties);
        //            result.Status = true;
        //            result.Message = "Login in successfully.";
        //            if (result.Status)
        //            {
        //                Password = HttpContext.Current.Server.UrlEncode(Password);
        //                var adminController = new AdministratorController();
        //                var adminInfo = adminController.GetAdminByLoginId(LoginId,Password);
        //                if (adminInfo != null)
        //                {
        //                    result.Data = adminInfo;
        //                }
        //                else
        //                {
        //                    result.Status = false;
        //                    result.Message = "Email id and password incorrect or you are not active user.";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            result.Status = false;
        //            result.Message = "Please provide login credential.";
        //        }
        //    }
        //    catch
        //    {
        //        result.Status = false;
        //        result.Message = "There was a problem. please try again later.";
        //    }
        //    return Ok(result);
        //}

        //[HttpGet]
        //[Route("SignOut")]
        //public IHttpActionResult SignOut()
        //{
        //    var result = new ResponseViewModel<AdministratorViewModel>();
        //    try
        //    {
        //        var ctx = Request.GetOwinContext();
        //        var authenticationManager = ctx.Authentication;
        //        authenticationManager.SignOut();
        //        result.Status = true;
        //        result.Message = "Louout successfully.";
        //    }
        //    catch
        //    {
        //        result.Status = false;
        //        result.Message = "There was a problem. please try again later.";
        //    }
        //    return Ok(result);
        //}
    }
}
