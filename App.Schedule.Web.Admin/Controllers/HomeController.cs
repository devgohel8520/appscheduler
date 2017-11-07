using System.Web.Mvc;
using App.Schedule.Web.Admin.Models;

namespace App.Schedule.Web.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public ActionResult Index()
        {
            var model = new ServiceDataViewModel<string>();
            Session["HomeLink"] = "Home";
            return View(model);
        }
    }
}