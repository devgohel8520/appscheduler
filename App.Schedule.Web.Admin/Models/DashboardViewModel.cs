namespace App.Schedule.Web.Admin.Models
{
    public class DashboardViewModel
    {
        public bool HasError { get; set; }
        public string Error { get; set; }
        public bool HasMore { get; set; }
        public string ErrorDescription { get; set; }
        public long AdminsCount { get; set; }
        public long CountryCount { get; set; }
        public long TimezonCount { get; set; }
        public long MembershipCount { get; set; }
        public long BusinessCategoryCount { get; set; }
    }
}