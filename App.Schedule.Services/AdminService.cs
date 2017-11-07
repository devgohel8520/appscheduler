using System;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using App.Schedule.Domains.ViewModel;
using System.Collections.Generic;

namespace App.Schedule.Services
{
    public class AdminService : AppointmentBaseService
    { 
        public AdminService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public async Task<ResponseViewModel<List<AdministratorViewModel>>> GetAdmins()
        {
            var returnResponse = new ResponseViewModel<List<AdministratorViewModel>>();
            try
            {
                var url = String.Format(AppointmentService.GET_ADMINS);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<List<AdministratorViewModel>>>(result);
                if (res != null)
                {
                    returnResponse.Status = res.Status;
                    returnResponse.Data = res.Data;
                    returnResponse.Message = res.Message;
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

        public async Task<ResponseViewModel<AdministratorViewModel>> GetAdminById(long id)
        {
            var returnResponse = new ResponseViewModel<AdministratorViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_ADMIN_BYID, id);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<AdministratorViewModel>>(result);
                if (res != null)
                {
                    returnResponse.Status = res.Status;
                    returnResponse.Data = res.Data;
                    returnResponse.Message = res.Message;
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

        public async Task<ResponseViewModel<AdministratorViewModel>> LoginIn(string LoginId, string Password)
        {
            var returnResponse = new ResponseViewModel<AdministratorViewModel>();
            try
            {
                Password = HttpContext.Current.Server.UrlEncode(Password);
                var url = String.Format(AppointmentService.GET_ADMIN_BYLOGINID, LoginId, Password);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<AdministratorViewModel>>(result);
                if (res != null)
                {
                    returnResponse.Status = res.Status;
                    returnResponse.Data = res.Data;
                    returnResponse.Message = res.Message;
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
                returnResponse.Status = false;
                returnResponse.Data = null;
                returnResponse.Message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<string>> PostAdminToken(string Email, string Password)
        {
            var returnResponse = new ResponseViewModel<string>();
            try
            {
                var model = "username=" + Email + "&password=" + Password + "&grant_type=password";
                var content = new StringContent(model, Encoding.UTF8, "text/plain"); 
                var url = String.Format(AppointmentService.GET_ADMIN_TOKEN);
                var response = await this.appointmentService.httpClient.PostAsync(url, content);
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
    }
}
