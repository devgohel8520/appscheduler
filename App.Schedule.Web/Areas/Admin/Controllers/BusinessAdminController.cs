using System.Web.Mvc;
using System.Threading.Tasks;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class BusinessAdminController : AdminBaseController
    {
        // GET: Admin/BusinessAdmin
        public async Task<ActionResult> Index()
        {
            var response = new ResponseViewModel<BusinessEmployeeViewModel>();
            var result = await BusinessEmployeeService.Get(RegisterViewModel.Employee.Id);
            response.Status = result.Status;
            response.Message = result.Message;
            if (result.Data != null)
                response.Data = result.Data;
            return View(response);
        }

        // GET: Admin/BusinessAdmin
        public async Task<ActionResult> Edit()
        {
            var response = new ResponseViewModel<BusinessEmployeeViewModel>();
            var result = await BusinessEmployeeService.Get(RegisterViewModel.Employee.Id);
            response.Status = result.Status;
            response.Message = result.Message;
            if (result.Data != null)
                response.Data = result.Data;
            return View(response);
        }
    }
}