using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using App.Schedule.Web.Admin.Models;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Admin.Controllers
{
    public class LoginController : LoginBaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new ServiceDataViewModel<LoginViewModel>();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ServiceDataViewModel<LoginViewModel> model)
        {
            var result = new ResponseViewModel<AdministratorViewModel>();
            try
            {
                if (!ModelState.IsValid)
                {
                    var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                    result.Status = false;
                    result.Message = errMessage;
                }
                else
                {
                    if (adminService != null)
                    {
                        var response = await adminService.LoginIn(model.Data.Email, model.Data.Password);
                        result.Status = response.Status;
                        result.Message = response.Message;
                        result.Data = response.Data;
                        if (response.Status)
                        {
                            var tokenResponse = await adminService.PostAdminToken(model.Data.Email, model.Data.Password);
                            result.Status = result.Status;
                            result.Message = result.Message;
                            if (tokenResponse.Status)
                            {
                                if (string.IsNullOrEmpty(tokenResponse.Data))
                                    Logout();

                                SetAdminSession(response.Data, model.Data.IsKeepLoggedIn, tokenResponse.Data);
                            }
                        }
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = "There was a problem. Please try again later.";
                    }
                }
            }
            catch
            {
                result.Status = false;
                result.Message = "There was a problem. Please try again later.";
            }
            return Json(new { status = result.Status, message = result.Message, data = result.Data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            if (Request.Cookies["aappointment"] != null)
            {
                var admin = new HttpCookie("aappointment");
                Session["aEmail"] = "";
                admin.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(admin);
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}