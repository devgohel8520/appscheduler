using System;
using System.Web;
using System.Web.Mvc;
using App.Schedule.Domains.ViewModel;
using App.Schedule.Web.Services;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class AdminBaseController : Controller
    {
        protected string Token;
        protected HttpCookie AdminCookie;
        protected RegisterViewModel RegisterViewModel;
        protected CountryService CountryService;
        protected BusinessCategoryService BusinessCategoryService;
        protected TimezoneService TimezoneService;
        protected MembershipService MembershipService;
        protected BusinessService BusinessService;
        protected BusinessEmployeeService BusinessEmployeeService;
        protected BusinessHourService BusinessHourService;
        protected BusinessHolidayService BusinessHolidayService;

        public AdminBaseController()
        {
            RegisterViewModel = new RegisterViewModel();
        }

        [NonAction]
        public bool SetAdminSession(RegisterViewModel model, bool isKeepLoggedIn, string token)
        {
            try
            {
                Session["aEmail"] = model.Employee.Email;
                var businessEmployee = new HttpCookie("aadminappointment");

                if (isKeepLoggedIn)
                    businessEmployee.Expires = DateTime.Now.AddDays(1);
                else
                    businessEmployee.Expires = DateTime.Now.AddDays(365);

                businessEmployee.Values["aFirstName"] = model.Employee.FirstName;
                businessEmployee.Values["aLastName"] = model.Employee.LastName;
                businessEmployee.Values["aEmail"] = model.Employee.Email;
                businessEmployee.Values["aPassword"] = model.Employee.Password;
                businessEmployee.Values["aIsAdmin"] = model.Employee.IsAdmin ? "true" : "false";
                businessEmployee.Values["aIsActive"] = model.Employee.IsActive ? "true" : "false";
                businessEmployee.Values["aToken"] = token;
                businessEmployee.Values["aEmpId"] = Convert.ToString(model.Employee.Id);
                businessEmployee.Values["aBusinessId"] =Convert.ToString(model.Business.Id);
                businessEmployee.Values["aMembershipId"] = Convert.ToString(model.Business.MembershipId);
                businessEmployee.Values["aBusinessCategoryId"] = Convert.ToString(model.Business.BusinessCategoryId);
                businessEmployee.Values["aTimezoneId"] = Convert.ToString(model.Business.TimezoneId);
                businessEmployee.Values["aServiceLocationId"] = Convert.ToString(model.Employee.ServiceLocationId);

                Response.Cookies.Add(businessEmployee);

                return true;
            }
            catch
            {
                return false;
            }
        }

        [NonAction]
        public RegisterViewModel GetAdminSession()
        {
            try
            {
                RegisterViewModel = new RegisterViewModel()
                {
                    Business = new BusinessViewModel(),
                    Employee = new BusinessEmployeeViewModel()
                };
                if (Request.Cookies["aadminappointment"] != null)
                {
                    AdminCookie = HttpContext.Request.Cookies["aadminappointment"];
                    if (AdminCookie != null)
                    {
                        RegisterViewModel.Employee.FirstName = AdminCookie.Values["aFirstName"];
                        RegisterViewModel.Employee.LastName = AdminCookie.Values["aLastName"];
                        RegisterViewModel.Employee.Email = AdminCookie.Values["aEmail"];
                        RegisterViewModel.Employee.IsActive = Convert.ToBoolean(AdminCookie.Values["aIsActive"]);
                        RegisterViewModel.Employee.IsAdmin = Convert.ToBoolean(AdminCookie.Values["aIsAdmin"]);
                        Token = AdminCookie.Values["aToken"];
                        RegisterViewModel.Employee.Id = Convert.ToInt64(AdminCookie.Values["aEmpId"]);
                        RegisterViewModel.Business.Id = Convert.ToInt64(AdminCookie.Values["aBusinessId"]);
                        RegisterViewModel.Business.TimezoneId = Convert.ToInt32(AdminCookie.Values["aTimezoneId"]);
                        RegisterViewModel.Business.BusinessCategoryId = Convert.ToInt32(AdminCookie.Values["aBusinessCategoryId"]);
                        RegisterViewModel.Business.MembershipId = Convert.ToInt32(AdminCookie.Values["aMembershipId"]);
                        RegisterViewModel.Employee.ServiceLocationId = Convert.ToInt64(AdminCookie.Values["aServiceLocationId"]);
                        return RegisterViewModel;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        [NonAction]
        protected bool LoginStatus()
        {
            try
            {
                RegisterViewModel = GetAdminSession();
                this.CountryService = new CountryService(Token);
                this.BusinessCategoryService = new BusinessCategoryService(Token);
                this.TimezoneService = new TimezoneService(Token);
                this.MembershipService = new MembershipService(Token);
                this.BusinessService = new BusinessService(Token);
                this.BusinessEmployeeService = new BusinessEmployeeService(Token);
                this.BusinessHourService = new BusinessHourService(Token);
                this.BusinessHolidayService = new BusinessHolidayService(Token);
                //Call service;
                if (RegisterViewModel != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!LoginStatus())
                filterContext.Result = RedirectToAction("Login", "Home", new { area = "Admin"});
        }
    }
}