using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace App.Schedule.Services
{
    public class CountryService : AppointmentBaseService
    {
        public CountryService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public async Task<ResponseViewModel<List<CountryViewModel>>> GetCountries()
        {
            var returnResponse = new ResponseViewModel<List<CountryViewModel>>();
            try
            {
                var url = String.Format(AppointmentService.GET_COUNTRIES);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<List<CountryViewModel>>>(result);
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

        public async Task<ResponseViewModel<CountryViewModel>> PostCountry(CountryViewModel model)
        {
            var returnResponse = new ResponseViewModel<CountryViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentService.POST_COUNTRY);
                var response = await this.appointmentService.httpClient.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<CountryViewModel>>(result);
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

        public async Task<ResponseViewModel<CountryViewModel>> GetCountryById(long id)
        {
            var returnResponse = new ResponseViewModel<CountryViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_COUNTRY_BYID, id);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<CountryViewModel>>(result);
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

        public async Task<ResponseViewModel<CountryViewModel>> PutCountry(CountryViewModel model)
        {
            var returnResponse = new ResponseViewModel<CountryViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentService.PUT_COUNTRY,model.Id);
                var response = await this.appointmentService.httpClient.PutAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<CountryViewModel>>(result);
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

        public async Task<ResponseViewModel<CountryViewModel>> DeleteCountry(long? Id)
        {
            var returnResponse = new ResponseViewModel<CountryViewModel>();
            try
            {
                if (!Id.HasValue)
                {
                    returnResponse.Status = false;
                    returnResponse.Message = "Please enter a valid country id.";
                }
                var url = String.Format(AppointmentService.DELETE_COUNTRY, Id.Value);
                var response = await this.appointmentService.httpClient.DeleteAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<CountryViewModel>>(result);
                if (res.Status)
                {
                    returnResponse.Status = true;
                    returnResponse.Data = res.Data;
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
