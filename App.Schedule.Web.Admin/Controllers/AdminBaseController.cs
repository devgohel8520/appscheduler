﻿using System;
using System.Web;
using System.Web.Mvc;
using App.Schedule.Services;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Admin.Controllers
{
    public class AdminBaseController : Controller
    {
        protected AdministratorViewModel admin;
        protected AdminService adminService;
        protected RegisterService registerService;
        protected DashboardService dashboardService;
        protected CountryService countryService;
        protected TimezoneService timezoneService;
        protected MembershipService membershipService;
        protected BusinessCategoryService businessCategoryService;

        protected HttpCookie adminCookie;
        protected string token;

        public AdminBaseController()
        {
            admin = new AdministratorViewModel();
        }

        [NonAction]
        public bool SetAdminSession(AdministratorViewModel model, bool isKeepLoggedIn, string token)
        {
            try
            {
                Session["aEmail"] = model.Email;
                var admin = new HttpCookie("aappointment");

                if (isKeepLoggedIn)
                    admin.Expires = DateTime.Now.AddDays(1);
                else
                    admin.Expires = DateTime.Now.AddDays(365);

                admin.Values["aFirstName"] = model.FirstName;
                admin.Values["aLastName"] = model.LastName;
                admin.Values["aEmail"] = model.Email;
                admin.Values["aPassword"] = model.Password;
                admin.Values["aIsAdmin"] = model.IsAdmin ? "true" : "false";
                admin.Values["aIsActive"] = model.IsActive ? "true" : "false";
                admin.Values["aToken"] = token;
                admin.Values["aId"] = Convert.ToString(model.Id);
                Response.Cookies.Add(admin);

                return true;
            }
            catch
            {
                return false;
            }
        }

        [NonAction]
        public AdministratorViewModel GetAdminSession()
        {
            try
            {
                admin = new AdministratorViewModel();
                if (Request.Cookies["aappointment"] != null)
                {
                    adminCookie = HttpContext.Request.Cookies["aappointment"];
                    if (adminCookie != null)
                    {
                        admin.FirstName = adminCookie.Values["aFirstName"];
                        admin.LastName = adminCookie.Values["aLastName"];
                        admin.Email = adminCookie.Values["aEmail"];
                        admin.IsActive = Convert.ToBoolean(adminCookie.Values["aIsActive"]);
                        admin.IsAdmin = Convert.ToBoolean(adminCookie.Values["aIsAdmin"]);
                        token = adminCookie.Values["aToken"];
                        admin.Id = Convert.ToInt64(adminCookie.Values["aId"]);
                        return admin;
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
                admin = GetAdminSession();
                this.adminService = new AdminService(token);
                this.registerService = new RegisterService(token);
                this.dashboardService = new DashboardService(token);
                this.countryService = new CountryService(token);
                this.timezoneService = new TimezoneService(token);
                this.membershipService = new MembershipService(token);
                this.businessCategoryService = new BusinessCategoryService(token);
                //if (string.IsNullOrEmpty(accessToken))
                //    return false;
                if (admin != null)
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
                filterContext.Result = RedirectToAction("Index", "Login");
        }
    }
}