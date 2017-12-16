using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class MembershipController : AdminBaseController
    {
        // GET: Admin/Membership
        public async Task<ActionResult> Index()
        {
            var response = new ResponseViewModel<MembershipViewModel>();
            var result = await MembershipService.Get(RegisterViewModel.Business.MembershipId);
            response.Status = result.Status;
            response.Message = result.Message;
            if (result.Data != null)
                response.Data = result.Data;
            return View(response);
        }

        public async Task<ActionResult> Edit()
        {
            var response = new ResponseViewModel<MembershipViewModel>();
            var result = await MembershipService.Get(RegisterViewModel.Business.MembershipId);
            response.Status = result.Status;
            response.Message = result.Message;
            if (result.Data != null)
                response.Data = result.Data;

            var Memberships = await this.GetMemberships();
            ViewBag.MembershipId = Memberships.Select(s => new SelectListItem()
            {
                Value = Convert.ToString(s.Id),
                Text = s.Title
            });

            return View(response);
        }

        [NonAction]
        private async Task<List<MembershipViewModel>> GetMemberships()
        {
            var response = await this.MembershipService.Gets();
            if (response != null)
            {
                return response.Data;
            }
            else
            {
                return new List<MembershipViewModel>();
            }
        }
    }
}