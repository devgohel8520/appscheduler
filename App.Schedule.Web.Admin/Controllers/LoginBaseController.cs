using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Schedule.Web.Admin.Controllers
{
    public class LoginBaseController : AdminBaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LoginStatus();
        }
    }
}