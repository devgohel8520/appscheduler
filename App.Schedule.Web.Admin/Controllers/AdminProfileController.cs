using App.Schedule.Domains.ViewModel;
using App.Schedule.Services;
using App.Schedule.Web.Admin.Models;
using System.Web.Mvc;

namespace App.Schedule.Web.Admin.Controllers
{
    public class AdminProfileController : AdminBaseController
    {
        public ActionResult Index()
        {
            Session["HomeLink"] = "Profile";
            var model = new ServiceDataViewModel<AdministratorViewModel>() {
                Data = admin,
                HasError = false
            };
            ViewBag.Token = token;
            return View(model);
        }
    }
}