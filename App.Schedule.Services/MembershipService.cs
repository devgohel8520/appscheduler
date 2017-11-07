﻿using System;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Services
{
    public class MembershipService : AppointmentBaseService
    {
        public MembershipService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public async Task<ResponseViewModel<List<MembershipViewModel>>> GetMemberships()
        {
            var returnResponse = new ResponseViewModel<List<MembershipViewModel>>();
            try
            {
                var url = String.Format(AppointmentService.GET_MEMBERSHIPS);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<List<MembershipViewModel>>>(result);
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

        public async Task<ResponseViewModel<MembershipViewModel>> PostMembership(MembershipViewModel model)
        {
            var returnResponse = new ResponseViewModel<MembershipViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentService.POST_MEMBERSHIP);
                var response = await this.appointmentService.httpClient.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<MembershipViewModel>>(result);
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

        public async Task<ResponseViewModel<MembershipViewModel>> GetMembershipById(long id)
        {
            var returnResponse = new ResponseViewModel<MembershipViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_MEMBERSHIP_BYID, id);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<MembershipViewModel>>(result);
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

        public async Task<ResponseViewModel<MembershipViewModel>> PutMembership(MembershipViewModel model)
        {
            var returnResponse = new ResponseViewModel<MembershipViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentService.PUT_MEMBERSHIP, model.Id);
                var response = await this.appointmentService.httpClient.PutAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<MembershipViewModel>>(result);
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

        public async Task<ResponseViewModel<MembershipViewModel>> DeactiveMembership(long? Id)
        {
            var returnResponse = new ResponseViewModel<MembershipViewModel>();
            try
            {
                var url = String.Format(AppointmentService.DEACTIVE_MEMBERSHIP, Id.Value);
                var response = await this.appointmentService.httpClient.DeleteAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<MembershipViewModel>>(result);
                if (res.Status)
                {
                    returnResponse.Status = true;
                    returnResponse.Message = "Successfully updated status.";
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
