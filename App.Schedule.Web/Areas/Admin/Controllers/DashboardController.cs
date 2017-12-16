using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class DashboardController : AdminBaseController
    {
        public List<AppointmentViewModel> SampleTestData(DateTime start, DateTime end)
        {
            return new List<AppointmentViewModel>()
            {
                new AppointmentViewModel()
                {
                    Id = 1,
                    Title = "Test Appointment 1",
                    StartTime = DateTime.Now.AddHours(10),
                    EndTime = DateTime.Now.AddHours(12),
                    BackColor = 123456,
                    TextColor = 121212,
                    IsAllDayEvent = true
                },
                new AppointmentViewModel()
                {
                    Id = 1,
                    Title = "Test Appointment 2",
                    StartTime = DateTime.Now.AddDays(4).AddHours(10),
                    EndTime = DateTime.Now.AddDays(4).AddHours(12),
                    BackColor = 123456,
                    TextColor = 121212,
                    IsAllDayEvent = false
                }
            };
        }

        public JsonResult GetDiaryEvents(DateTime start, DateTime end)
        {
            var result = SampleTestData(start, end).Select(x => new
            {
                id = x.Id,
                title = x.Title,
                start = x.StartTime,
                end = x.EndAfter,
                color = x.BackColor,
                className = "",
                someKey = x.Id,
                allDay = x.IsAllDayEvent
            }).ToArray();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            if (Request.Cookies["aadminappointment"] != null)
            {
                var admin = new HttpCookie("aadminappointment");
                Session["aEmail"] = "";
                admin.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(admin);
                return RedirectToAction("Login", "Home", new { area = "Admin" });
            }
            return RedirectToAction("Login", "Home", new { area = "Admin" });
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}