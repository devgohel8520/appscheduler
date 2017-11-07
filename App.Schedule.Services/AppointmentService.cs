using System;
using System.Net.Http;

namespace App.Schedule.Services
{
    public class AppointmentService
    {
        public HttpClient httpClient;
        //private string baseUrl = "http://appointment.why-fi.com";
        //public static string baseUrl = "http://localhost/appointmentapi/";
        public static string baseUrl = "http://localhost:57433/";

        //Admin Identity Token API
        public static string POST_API_ACCOUNT_REGISTER = "api/account/register";
        public static string PUT_API_ACCOUNT_REGISTER = "api/account/updateadmin";
        public static string GET_ADMIN_TOKEN = "token";

        //Administrator API
        public static string GET_ADMINS = "api/administrator";
        public static string GET_ADMIN_BYID = "api/administrator?Id={0}";
        public static string GET_ADMIN_BYLOGINID = "api/administrator?loginid={0}&password={1}";
        public static string POST_ADMIN = "api/administrator";
        public static string DELETE_ADMIN = "api/administrator";
        public static string DEACTIVE_ADMIN = "api/administrator?id={0}&status={1}";

        //Country API
        public static string GET_COUNTRIES = "api/country";
        public static string POST_COUNTRY = "api/country";
        public static string GET_COUNTRY_BYID = "api/country?id={0}";
        public static string PUT_COUNTRY = "api/country?id={0}";
        public static string DELETE_COUNTRY = "api/country?id={0}";

        //Timezone API
        public static string GET_TIMEZONES = "api/timezone";
        public static string POST_TIMEZONE = "api/timezone";
        public static string GET_TIMEZONE_BYID = "api/timezone?id={0}";
        public static string PUT_TIMEZONE = "api/timezone?id={0}";
        public static string DELETE_TIMEZONE = "api/timezone?id={0}";

        //MEMBERSHIP API
        public static string GET_MEMBERSHIPS = "api/membership";
        public static string POST_MEMBERSHIP = "api/membership";
        public static string GET_MEMBERSHIP_BYID = "api/membership?id={0}";
        public static string PUT_MEMBERSHIP = "api/membership?id={0}";
        public static string DELETE_MEMBERSHIP = "api/membership?id={0}";
        public static string DEACTIVE_MEMBERSHIP = "api/membership?id={0}";
        
        //BUSINESSCATEGORY API
        public static string GET_BUSINESSCATEGORIES = "api/businesscategory";
        public static string POST_BUSINESSCATEGORY = "api/businesscategory";
        public static string GET_BUSINESSCATEGORY_BYID = "api/businesscategory?id={0}";
        public static string PUT_BUSINESSCATEGORY = "api/businesscategory?id={0}";
        public static string DELETE_BUSINESSCATEGORY = "api/businesscategory?id={0}";


        public AppointmentService()
        {
            this.httpClient = new HttpClient() { BaseAddress = new Uri(baseUrl) };
            this.httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
