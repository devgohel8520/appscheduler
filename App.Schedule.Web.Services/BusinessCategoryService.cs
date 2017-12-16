using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Services
{
    public class BusinessCategoryService : AppointmentUserBaseService, IAppointmentUserService<BusinessCategoryViewModel>
    {

        public BusinessCategoryService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public async Task<ResponseViewModel<List<BusinessCategoryViewModel>>> Gets()
        {
            var returnResponse = new ResponseViewModel<List<BusinessCategoryViewModel>>();
            try
            {
                var url = String.Format(AppointmentUserService.GET_BUSINESSCATEGORIES);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<List<BusinessCategoryViewModel>>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public Task<ResponseViewModel<BusinessCategoryViewModel>> Find(Predicate<BusinessCategoryViewModel> pridict)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<BusinessCategoryViewModel>> Get(long? id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<BusinessCategoryViewModel>> Add(BusinessCategoryViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<BusinessCategoryViewModel>> Deactive(long? id, bool status)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<BusinessCategoryViewModel>> Delete(long? id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<BusinessCategoryViewModel>> Update(BusinessCategoryViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
