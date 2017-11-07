﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Schedule.Domains.ViewModel
{
    public class AdministratorViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Please enter first name.")]
        [MinLength(3, ErrorMessage = "FirstName should have atleast 3 character long")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name.")]
        [MinLength(3, ErrorMessage = "LastName should have atleast 3 character long")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Please enter your email id.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email id")]
        public string LoginId { get; set; }

        [MinLength(8, ErrorMessage = "Password must be greater than 8 character")]
        [MaxLength(50, ErrorMessage = "Password must be less than 50 character")]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "Please enter your password")]
        [MinLength(8, ErrorMessage = "Password must be greater than 8 character")]
        [MaxLength(50, ErrorMessage = "Password must be less than 50 character")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Please cheque your confirm password")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Please enter your email id.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email id")]
        public string Email { get; set; }

        [StringLength(50)]
        public string ContactNumber { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public long AdministratorId { get; set; }
    }

    public class CountryViewModel
    {
        public int Id { get; set; }
        
        [StringLength(250)]
        [Required(ErrorMessage = "Please enter country name.")]
        public string Name { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Please enter ISO 2.")]
        [MinLength(2, ErrorMessage = "ISO2 should have 2 character long.")]
        [MaxLength(2, ErrorMessage = "ISO2 should have 2 character only.")]
        public string ISO { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Please enter ISO 3.")]
        [MinLength(3, ErrorMessage = "ISO3 should have 3 character long.")]
        [MaxLength(3, ErrorMessage = "ISO3 should have 3 character only.")]
        public string ISO3 { get; set; }

        [StringLength(50)]
        public string CurrencyName { get; set; }

        [StringLength(50)]
        public string CurrencyCode { get; set; }

        public int? PhoneCode { get; set; }

        public long AdministratorId { get; set; }

        //public List<tblServiceLocation> tblServiceLocations { get; set; }
    }

    public class TimezoneViewModel
    {
        public int Id { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Please enter Timezone title.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter UTC offset.")]
        public int UtcOffset { get; set; }

        public bool IsDST { get; set; }

        [Required(ErrorMessage = "Please enter country id.")]
        public int CountryId { get; set; }

        public long AdministratorId { get; set; }

        public virtual AdministratorViewModel Administrator { get; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblBusiness> tblBusinesses { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblBusinessCustomer> tblBusinessCustomers { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblServiceLocation> tblServiceLocations { get; set; }
    }

    public class BusinessCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(250)]
        public string Type { get; set; }

        public string PictureLink { get; set; }

        public bool IsActive { get; set; }

        public DateTime? Created { get; set; }

        public int? OrderNumber { get; set; }

        public long? AdministratorId { get; set; }

        public int? ParentId { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblBusiness> tblBusinesses { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblBusinessCategory> tblBusinessCategory1 { get; set; }

        //public virtual tblBusinessCategory tblBusinessCategory2 { get; set; }
    }

    public class MembershipViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        public string Benifits { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public bool IsUnlimited { get; set; }

        public int TotalEmployee { get; set; }

        public int TotalCustomer { get; set; }

        public int TotalAppointment { get; set; }

        public int TotalLocation { get; set; }

        public int TotalOffers { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public long AdministratorId { get; set; }

        //public virtual tblAdministrator tblAdministrator { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblBusiness> tblBusinesses { get; set; }

    }

    public class BusinessViewModel
    {
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(50)]
        public string ShortName { get; set; }

        public string Logo { get; set; }

        [StringLength(250)]
        public string PhoneNumbers { get; set; }

        [StringLength(250)]
        public string FaxNumbers { get; set; }

        [StringLength(250)]
        [EmailAddress(ErrorMessage = "Please provide a valid email id.")]
        public string Email { get; set; }

        [StringLength(250)]
        public string Website { get; set; }

        [StringLength(250)]
        public string Add1 { get; set; }

        [StringLength(250)]
        public string Add2 { get; set; }

        [StringLength(250)]
        public string City { get; set; }

        [StringLength(250)]
        public string State { get; set; }

        [StringLength(50)]
        public string Zip { get; set; }

        public int? CountryId { get; set; }

        public bool IsInternational { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public int MembershipId { get; set; }

        public int BusinessCategoryId { get; set; }

        public int TimezoneId { get; set; }

        //public virtual tblTimezone tblTimezone { get; set; }

        //public virtual tblMembership tblMembership { get; set; }

        //public virtual tblBusinessCategory tblBusinessCategory { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblServiceLocation> tblServiceLocations { get; set; }
    }

    public class BusinessEmployeeViewModel
    {
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(250)]
        public string LastName { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        public int? STD { get; set; }

        [StringLength(250)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(250)]
        public string LoginId { get; set; }

        [Required]
        [StringLength(250)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public bool IsAdmin { get; set; }

        public long ServiceLocationId { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblAppointmentFeedback> tblAppointmentFeedbacks { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblAppointmentInvitee> tblAppointmentInvitees { get; set; }

        //public virtual tblServiceLocation tblServiceLocation { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblBusinessOffer> tblBusinessOffers { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblBusinessService> tblBusinessServices { get; set; }
    }

    public class ServiceLocationViewModel
    {
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(250)]
        public string Add1 { get; set; }

        [StringLength(250)]
        public string Add2 { get; set; }

        [StringLength(250)]
        public string City { get; set; }

        [StringLength(250)]
        public string State { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        public int CountryId { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public int TimezoneId { get; set; }

        public long BusinessId { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblAppointment> tblAppointments { get; set; }

        //public virtual tblBusiness tblBusiness { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblBusinessCustomer> tblBusinessCustomers { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblBusinessEmployee> tblBusinessEmployees { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblBusinessHoliday> tblBusinessHolidays { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblBusinessHour> tblBusinessHours { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblBusinessOfferServiceLocation> tblBusinessOfferServiceLocations { get; set; }

        //public virtual tblCountry tblCountry { get; set; }

        //public virtual tblTimezone tblTimezone { get; set; }
    }

    public class BusinessHourViewModel
    {
        public long Id { get; set; }

        public int WeekDayId { get; set; }

        public bool IsStartDay { get; set; }

        public bool IsHoliday { get; set; }

        [Column(TypeName = "date")]
        public DateTime From { get; set; }

        [Column(TypeName = "date")]
        public DateTime To { get; set; }

        public bool? IsSplit1 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FromSplit1 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ToSplit1 { get; set; }

        public bool? IsSplit2 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FromSplit2 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ToSplit2 { get; set; }

        public long? ServiceLocationId { get; set; }

        //public virtual tblServiceLocation tblServiceLocation { get; set; }
    }

    public class BusinessHolidayViewModel
    {
        public long Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime OnDate { get; set; }

        public int Type { get; set; }

        public long ServiceLocationId { get; set; }

        //public virtual tblServiceLocation tblServiceLocation { get; set; }
    }

    public class BusinessOfferViewModel
    {
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [StringLength(100)]
        public string Code { get; set; }

        [Column(TypeName = "date")]
        public DateTime ValidFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime ValidTo { get; set; }

        public bool IsActive { get; set; }

        public DateTime? Created { get; set; }

        public long BusinessEmployeeId { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblAppointment> tblAppointments { get; set; }

        //public virtual tblBusinessEmployee tblBusinessEmployee { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblBusinessOfferServiceLocation> tblBusinessOfferServiceLocations { get; set; }
    }

    public class BusinessServiceViewModel
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Column(TypeName = "money")]
        public decimal? Cost { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public long EmployeeId { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblAppointment> tblAppointments { get; set; }

        //public virtual tblBusinessEmployee tblBusinessEmployee { get; set; }
    }

    public class BusinessCustomerViewMdoel
    {
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(250)]
        public string LastName { get; set; }

        [Column(TypeName = "image")]
        public byte[] ProfilePicture { get; set; }

        public int? StdCode { get; set; }

        [StringLength(250)]
        public string PhoneNumber { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Add1 { get; set; }

        [StringLength(250)]
        public string Add2 { get; set; }

        [StringLength(250)]
        public string City { get; set; }

        [StringLength(250)]
        public string State { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        [Required]
        [StringLength(250)]
        public string LoginId { get; set; }

        [Required]
        [StringLength(250)]
        public string Password { get; set; }

        public DateTime Created { get; set; }

        public bool IsActive { get; set; }

        public int? TimezoneId { get; set; }

        public long ServiceLocationId { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblAppointment> tblAppointments { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblAppointmentFeedback> tblAppointmentFeedbacks { get; set; }

        //public virtual tblTimezone tblTimezone { get; set; }

        //public virtual tblServiceLocation tblServiceLocation { get; set; }
    }

    public class AppointmentViewModel
    {
        public long Id { get; set; }

        public long GlobalAppointmentId { get; set; }

        public long BusinessServiceId { get; set; }

        public string Title { get; set; }

        public int PatternType { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool IsRecuring { get; set; }

        public bool IsAllDayEvent { get; set; }

        public int? TextColor { get; set; }

        public int? BackColor { get; set; }

        public int? RecureEvery { get; set; }

        public int? EndAfter { get; set; }

        public DateTime? EndAfterDate { get; set; }

        public int? StatusType { get; set; }

        [Column(TypeName = "ntext")]
        public string CancelReason { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "date")]
        public DateTime Created { get; set; }

        public long? BusinessOfferId { get; set; }

        public long? ServiceLocationId { get; set; }

        public long? BusinessEmployeeId { get; set; }

        public long? BusinessCustomerId { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblAppointmentDocument> tblAppointmentDocuments { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblAppointmentPayment> tblAppointmentPayments { get; set; }

        //public virtual tblBusinessCustomer tblBusinessCustomer { get; set; }

        //public virtual tblBusinessOffer tblBusinessOffer { get; set; }

        //public virtual tblBusinessService tblBusinessService { get; set; }

        //public virtual tblServiceLocation tblServiceLocation { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblAppointmentInvitee> tblAppointmentInvitees { get; set; }
    }

    public class AppointmentInviteeViewModel
    {
        public long Id { get; set; }

        public long BusinessEmployeeId { get; set; }

        public long AppointmentId { get; set; }

        //public virtual tblAppointment tblAppointment { get; set; }

        //public virtual tblBusinessEmployee tblBusinessEmployee { get; set; }
    }

    public class AppointmentPaymentViewModel
    {
        public long Id { get; set; }

        public bool IsPaid { get; set; }

        public DateTime PaidDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        public int BillingType { get; set; }

        [StringLength(250)]
        public string PurchaseOrderNo { get; set; }

        [StringLength(250)]
        public string ChequeNumber { get; set; }

        public int? CardType { get; set; }

        [StringLength(250)]
        public string CCFirstName { get; set; }

        [StringLength(250)]
        public string CCLastName { get; set; }

        [StringLength(250)]
        public string CCardNumber { get; set; }

        [StringLength(250)]
        public string CCSecurityCode { get; set; }

        public DateTime? CCExpirationDate { get; set; }

        public DateTime? Created { get; set; }

        public long AppointmentId { get; set; }

        //public virtual tblAppointment tblAppointment { get; set; }
    }

    public class AppointmentFeedbackViewModel
    {
        public long Id { get; set; }

        public bool IsEmployee { get; set; }

        public long? BusinessEmployeeId { get; set; }

        public long? BusinessCustomerId { get; set; }

        public int? Rating { get; set; }

        [Column(TypeName = "ntext")]
        public string Feedback { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? Created { get; set; }

        public long AppointmentId { get; set; }

        //public virtual tblAppointment tblAppointment { get; set; }

        //public virtual tblBusinessCustomer tblBusinessCustomer { get; set; }

        //public virtual tblBusinessEmployee tblBusinessEmployee { get; set; }
    }

    public class DocumentCategoryViewModel
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int OrderNo { get; set; }

        public string PictureLink { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? Created { get; set; }

        public bool IsParent { get; set; }

        public long? ParentId { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblAppointmentDocument> tblAppointmentDocuments { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tblDocumentCategory> tblDocumentCategory1 { get; set; }

        //public virtual tblDocumentCategory tblDocumentCategory2 { get; set; }
    }

    public class AppointmentDocumentViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public int? DocumentType { get; set; }

        public bool IsEmployeeUpload { get; set; }

        public string DocumentLink { get; set; }

        public long? DocumentCategoryId { get; set; }

        public long? AppointmentId { get; set; }

        //public virtual tblAppointment tblAppointment { get; set; }

        //public virtual tblDocumentCategory tblDocumentCategory { get; set; }
    }

    //All Enum Types fro Appointment Scheduler
    public enum AddressType
    {
        Business = 1,
        Home = 2,
        Other = 3
    }

    public enum BillingType
    {
        Cash = 1,
        Cheque = 2,
        CreditCard = 3,
        Account = 4
    }

    public enum CardType
    {
        AmericanExpress = 1,
        Discover = 2,
        MasterCard = 3,
        Visa = 4
    }

    public enum DayType
    {
        Sun = 1,
        Mon = 2,
        Tue = 3,
        Wed = 4,
        Thu = 5,
        Fri = 6,
        Sat = 7
    }

    public enum PhoneNumberType
    {
        Assistant = 1,
        Business = 2,
        Business2 = 3,
        BusinessFax = 4,
        Callback = 5,
        Car = 6,
        Company = 7,
        Home = 8,
        Home2 = 9,
        HomeFax = 10,
        ISDN = 11,
        Mobile = 12,
        Other = 13,
        OtherFax = 14,
        Pager = 15,
        Primary = 16,
        Radio = 17,
        Telex = 18,
        TtyTdd = 19
    }

    public enum StatusType
    {
        Confirmed = 0,
        Resheduled = 1,
        Canceled = 2,
        Completed = 3,
        Attended = 4,
        NotAttended = 5
    }

    public enum PatternType
    {
        Once = 0,
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        Yearly = 4,
        Custom = 5
    }

    public enum HolidayType
    {
        Specified = 0,
        RpeatEveryWeek = 1,
        RepeatEveryMonth = 2,
        RepeatEveryYear = 4,
    }

    public enum FileType
    {
        Image = 0,
        Text = 1,
        PDF = 2,
        Doc = 3,
        Excel = 4
    }

    public enum DocumentType
    {
        Image = 0,
        Pdf = 1,
        Text = 2,
        WordDocument = 3,
        Excel = 4,
        Access = 5
    }
}
