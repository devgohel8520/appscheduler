using System.Web.Mvc;
using App.Schedule.Domains.ViewModel;

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