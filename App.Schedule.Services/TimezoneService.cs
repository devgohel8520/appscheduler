using System;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Services
{
    public class TimezoneService : AppointmentBaseService
    {
        public TimezoneService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public async Task<ResponseViewModel<List<TimezoneViewModel>>> GetTimezones()
        {
            var returnResponse = new ResponseViewModel<List<TimezoneViewModel>>();
            try
            {
                var url = String.Format(AppointmentService.GET_TIMEZONES);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<List<TimezoneViewModel>>>(result);
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

        public async Task<ResponseViewModel<TimezoneViewModel>> PostTimezone(TimezoneViewModel model)
        {
            var returnResponse = new ResponseViewModel<TimezoneViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentService.POST_TIMEZONE);
                var response = await this.appointmentService.httpClient.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<TimezoneViewModel>>(result);
                if (res.Status)
                {
                    returnResponse.Status = true;
                    returnResponse.Message = "Successfully added";
                    returnResponse.Data = res.Data;
                }
                else
                {
                    returnResponse.Status = res.Status;
                    returnResponse.Message = res.Message;
                }
            }
            catch (Exception ex)
            {
                returnResponse.Message = ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<TimezoneViewModel>> GetTimezoneById(long id)
        {
            var returnResponse = new ResponseViewModel<TimezoneViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_TIMEZONE_BYID, id);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<TimezoneViewModel>>(result);
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

        public async Task<ResponseViewModel<TimezoneViewModel>> PutTimezone(TimezoneViewModel model)
        {
            var returnResponse = new ResponseViewModel<TimezoneViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentService.PUT_TIMEZONE, model.Id);
                var response = await this.appointmentService.httpClient.PutAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<TimezoneViewModel>>(result);
                if (res.Status)
                {
                    returnResponse.Status = true;
                    returnResponse.Data = res.Data;
                    returnResponse.Message = "Successfully updated.";
                }
                else
                {
                    returnResponse.Status = res.Status;
                    returnResponse.Message = res.Message;
                }
            }
            catch (Exception ex)
            {
                returnResponse.Message = ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<TimezoneViewModel>> DeleteTimezone(long? Id)
        {
            var returnResponse = new ResponseViewModel<TimezoneViewModel>();
            try
            {
                if (!Id.HasValue)
                {
                    returnResponse.Status = false;
                    returnResponse.Message = "Please enter a valid timezone id.";
                }
                var url = String.Format(AppointmentService.DELETE_TIMEZONE, Id.Value);
                var response = await this.appointmentService.httpClient.DeleteAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<string>>(result);
                if (res.Status)
                {
                    returnResponse.Status = true;
                    returnResponse.Message = "Successfully deleted.";
                }
                else
                {
                    returnResponse.Status = res.Status;
                    returnResponse.Message = res.Message;
                }
            }
            catch (Exception ex)
            {
                returnResponse.Message = ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }
    }
}
