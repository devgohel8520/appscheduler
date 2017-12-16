using System;
using System.Text;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Services
{
    public class BusinessEmployeeService : AppointmentUserBaseService, IAppointmentUserService<BusinessEmployeeViewModel>
    {
        public BusinessEmployeeService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public async Task<ResponseViewModel<RegisterViewModel>> VerifyLoginCredential(string Email, string Password)
        {
            var returnResponse = new ResponseViewModel<RegisterViewModel>();
            try
            {
                Password = HttpContext.Current.Server.UrlEncode(Password);
                var url = String.Format(AppointmentUserService.GET_BUSINESS_EMP_BYLOGINID, Email, Password);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<RegisterViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<string>> VerifyAndGetAdminAccessToken(string Email, string Password)
        {
            var returnResponse = new ResponseViewModel<string>();
            try
            {
                var model = "username=emp" + Email + "&password=" + Password + "&grant_type=password";
                var content = new StringContent(model, Encoding.UTF8, "text/plain");
                var url = String.Format(AppointmentUserService.GET_ADMIN_TOKEN);
                var response = await this.appointmentUserService.httpClient.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                dynamic res = JsonConvert.DeserializeObject(result);
                if (res != null)
                {
                    try
                    {
                        returnResponse.Status = true;
                        returnResponse.Data = res.access_token;
                        returnResponse.Message = "Success";
                    }
                    catch
                    {
                        returnResponse.Status = false;
                        returnResponse.Data = null;
                        returnResponse.Message = "Please check your id and password";
                    }
                }
                else
                {
                    returnResponse.Status = false;
                    returnResponse.Data = null;
                    returnResponse.Message = "There was a problem. Please try agian later.";
                }
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<BusinessEmployeeViewModel>> Get(long? id)
        {
            var returnResponse = new ResponseViewModel<BusinessEmployeeViewModel>()
            {
                Status = false,
                Message = "",
                Data = new BusinessEmployeeViewModel()
            };
            try
            {
                var url = String.Format(AppointmentUserService.GET_BUSINESS_EMP_BYID, id);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                var result = await base.GetHttpResponse<BusinessEmployeeViewModel>(response);

                returnResponse.Status = result.Status;
                returnResponse.Message = result.Message;
                returnResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }

            return returnResponse;
        }

        public Task<ResponseViewModel<List<BusinessEmployeeViewModel>>> Gets()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<BusinessEmployeeViewModel>> Find(Predicate<BusinessEmployeeViewModel> pridict)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<BusinessEmployeeViewModel>> Add(BusinessEmployeeViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<BusinessEmployeeViewModel>> Update(BusinessEmployeeViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<BusinessEmployeeViewModel>> Delete(long? id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<BusinessEmployeeViewModel>> Deactive(long? id, bool status)
        {
            throw new NotImplementedException();
        }
    }
}
