using System.Web.Mvc;

namespace App.Schedule.Web.Admin.Controllers
{
    public class LoginBaseController : AdminBaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (LoginStatus())
                {
                    filterContext.Result = RedirectToAction("Index", "Dashboard");
                }
            }
            catch
            {

            }
        }
    }
}