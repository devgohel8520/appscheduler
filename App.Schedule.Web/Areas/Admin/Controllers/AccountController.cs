using System.Web.Mvc;
using System.Threading.Tasks;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class AccountController : AdminBaseController
    {
        public async Task<ActionResult> Index()
        {
            var response = new ResponseViewModel<BusinessViewModel>();
            var result = await BusinessService.Get(RegisterViewModel.Business.Id);
            response.Status = result.Status;
            response.Message = result.Message;
            if (result.Data != null)
                response.Data = result.Data.Business;
            return View(response);
        }

        public async Task<ActionResult> Administrator()
        {
            var response = new ResponseViewModel<BusinessEmployeeViewModel>();
            var result = await BusinessEmployeeService.Get(RegisterViewModel.Employee.Id);
            response.Status = result.Status;
            response.Message = result.Message;
            if (result.Data != null)
                response.Data = result.Data;
            return View(response);
        }

        public async Task<ActionResult> Membership()
        {
            var response = new ResponseViewModel<MembershipViewModel>();
            var result = await MembershipService.Get(RegisterViewModel.Business.MembershipId);
            response.Status = result.Status;
            response.Message = result.Message;
            if (result.Data != null)
                response.Data = result.Data;
            return View(response);
        }

        public ActionResult Billing()
        {
            return View();
        }

        public async Task<ActionResult> ProfileEdit()
        {
            var response = new ResponseViewModel<BusinessViewModel>();
            var result = await BusinessService.Get(RegisterViewModel.Business.Id);
            response.Status = result.Status;
            response.Message = result.Message;
            if (result.Data != null)
                response.Data = result.Data.Business;
            return View(response);
        }

    }
}