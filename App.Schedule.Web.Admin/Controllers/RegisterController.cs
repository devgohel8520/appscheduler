using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Admin.Controllers
{
    public class RegisterController : LoginBaseController
    {
        public ActionResult Index()
        {
            var model = new AdministratorViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(AdministratorViewModel model)
        {
            model.LoginId = model.Email;
            if (!ModelState.IsValid)
            {
                var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                return Json(new { status = false, message = errMessage }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var response = await this.adminService.Add(model);
                if (response.Status)
                {
                    return Json(new { status = true, message = "Successfully registered." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false, message = "There was a problem. Please try again later. " + response.Data }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}