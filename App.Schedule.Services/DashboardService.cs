using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Services
{
    public class DashboardService : AppointmentBaseService
    {
        public DashboardService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public async Task<IEnumerable<AdministratorViewModel>> GetAdmins()
        {
            var model = new List<AdministratorViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_ADMINS);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<List<AdministratorViewModel>>>(result);
                if (res != null)
                {
                    if (res.Status)
                        model = res.Data;
                    else
                        model = null;
                }
                else
                    model = null;
            }
            catch
            {
                model = null;
            }
            return model;
        }

        public async Task<IEnumerable<CountryViewModel>> GetCountries()
        {
            var model = new List<CountryViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_COUNTRIES);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<List<CountryViewModel>>>(result);
                if (res != null)
                {
                    if (res.Status)
                        model = res.Data;
                    else
                        model = null;
                }
                else
                    model = null;
            }
            catch
            {
                model = null;
            }
            return model;
        }

        public async Task<IEnumerable<TimezoneViewModel>> GetTimezones()
        {
            var model = new List<TimezoneViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_TIMEZONES);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<List<TimezoneViewModel>>>(result);
                if (res != null)
                {
                    if (res.Status)
                        model = res.Data;
                    else
                        model = null;
                }
                else
                    model = null;
            }
            catch
            {
                model = null;
            }
            return model;
        }

        public async Task<IEnumerable<MembershipViewModel>> GetMemberships()
        {
            var model = new List<MembershipViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_MEMBERSHIPS);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<List<MembershipViewModel>>>(result);
                if (res != null)
                {
                    if (res.Status)
                        model = res.Data;
                    else
                        model = null;
                }
                else
                    model = null;
            }
            catch
            {
                model = null;
            }
            return model;
        }

        public async Task<IEnumerable<BusinessCategoryViewModel>> GetBusinessCategories()
        {
            var model = new List<BusinessCategoryViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_BUSINESSCATEGORIES);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<List<BusinessCategoryViewModel>>>(result);
                if (res != null)
                {
                    if (res.Status)
                        model = res.Data;
                    else
                        model = null;
                }
                else
                    model = null;
            }
            catch
            {
                model = null;
            }
            return model;
        }

    }
}
