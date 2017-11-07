using System;
using PagedList;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using App.Schedule.Web.Admin.Models;
using App.Schedule.Domains.ViewModel;


namespace App.Schedule.Web.Admin.Controllers
{
    public class BusinessCategoryController : AdminBaseController
    {
        public async Task<ActionResult> Index(int? page, string search)
        {
            var model = new ServiceDataViewModel<IPagedList<BusinessCategoryViewModel>>();
            try
            {
                Session["HomeLink"] = "Business Category";
                var pageNumber = page ?? 1;
                ViewBag.search = search;

                var response = await businessCategoryService.GetBusinessCategories();
                if (response.Status)
                {
                    //var data = response.Data.Where(d => d.ParentId == null).ToList();
                    var data = response.Data.Where(d => d.ParentId == null).ToList();
                    if (search == null)
                    {
                        model.Data = data.ToPagedList<BusinessCategoryViewModel>(pageNumber, 10);
                        return View(model);
                    }
                    else
                    {
                        model.Data = data.Where(d => d.Name.ToLower().Contains(search.ToLower())).ToList().ToPagedList(pageNumber, 5);
                        return View(model);
                    }
                }
                else
                {
                    model.HasError = !response.Status;
                    model.Error = response.Message;
                }
            }
            catch (Exception ex)
            {
                model.HasError = true;
                model.Error = "There was a problem. Please try again later.";
                model.HasMore = true;
                model.ErrorDescription = ex.Message.ToString();
            }
            return View(model);
        }

    }
}