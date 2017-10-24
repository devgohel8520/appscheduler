namespace App.Schedule.Context
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using App.Schedule.Domains;

    public partial class AppScheduleDbContext : DbContext
    {
        public AppScheduleDbContext()
            : base("name=AppScheduleDbContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        public virtual DbSet<tblAdministrator> tblAdministrators { get; set; }
        public virtual DbSet<tblAppointment> tblAppointments { get; set; }
        public virtual DbSet<tblAppointmentDocument> tblAppointmentDocuments { get; set; }
        public virtual DbSet<tblAppointmentFeedback> tblAppointmentFeedbacks { get; set; }
        public virtual DbSet<tblAppointmentInvitee> tblAppointmentInvitees { get; set; }
        public virtual DbSet<tblAppointmentPayment> tblAppointmentPayments { get; set; }
        public virtual DbSet<tblBusiness> tblBusinesses { get; set; }
        public virtual DbSet<tblBusinessCategory> tblBusinessCategories { get; set; }
        public virtual DbSet<tblBusinessCustomer> tblBusinessCustomers { get; set; }
        public virtual DbSet<tblBusinessEmployee> tblBusinessEmployees { get; set; }
        public virtual DbSet<tblBusinessHoliday> tblBusinessHolidays { get; set; }
        public virtual DbSet<tblBusinessHour> tblBusinessHours { get; set; }
        public virtual DbSet<tblBusinessOffer> tblBusinessOffers { get; set; }
        public virtual DbSet<tblBusinessOfferServiceLocation> tblBusinessOfferServiceLocations { get; set; }
        public virtual DbSet<tblBusinessService> tblBusinessServices { get; set; }
        public virtual DbSet<tblCountry> tblCountries { get; set; }
        public virtual DbSet<tblDocumentCategory> tblDocumentCategories { get; set; }
        public virtual DbSet<tblMembership> tblMemberships { get; set; }
        public virtual DbSet<tblServiceLocation> tblServiceLocations { get; set; }
        public virtual DbSet<tblTimezone> tblTimezones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblCountries)
                .WithRequired(e => e.tblAdministrator)
                .HasForeignKey(e => e.AdministratorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblMemberships)
                .WithRequired(e => e.tblAdministrator)
                .HasForeignKey(e => e.AdministratorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblTimezones)
                .WithRequired(e => e.tblAdministrator)
                .HasForeignKey(e => e.AdministratorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAppointment>()
                .HasMany(e => e.tblAppointmentDocuments)
                .WithOptional(e => e.tblAppointment)
                .HasForeignKey(e => e.AppointmentId);

            modelBuilder.Entity<tblAppointment>()
                .HasMany(e => e.tblAppointmentPayments)
                .WithRequired(e => e.tblAppointment)
                .HasForeignKey(e => e.AppointmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAppointment>()
                .HasMany(e => e.tblAppointmentFeedbacks)
                .WithRequired(e => e.tblAppointment)
                .HasForeignKey(e => e.AppointmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAppointment>()
                .HasMany(e => e.tblAppointmentInvitees)
                .WithRequired(e => e.tblAppointment)
                .HasForeignKey(e => e.AppointmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAppointmentPayment>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tblBusiness>()
                .HasMany(e => e.tblServiceLocations)
                .WithRequired(e => e.tblBusiness)
                .HasForeignKey(e => e.BusinessId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblBusinessCategory>()
                .HasMany(e => e.tblBusinesses)
                .WithRequired(e => e.tblBusinessCategory)
                .HasForeignKey(e => e.BusinessCategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblBusinessCategory>()
                .HasMany(e => e.tblBusinessCategory1)
                .WithOptional(e => e.tblBusinessCategory2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<tblBusinessCustomer>()
                .HasMany(e => e.tblAppointments)
                .WithOptional(e => e.tblBusinessCustomer)
                .HasForeignKey(e => e.BusinessCustomerId);

            modelBuilder.Entity<tblBusinessCustomer>()
                .HasMany(e => e.tblAppointmentFeedbacks)
                .WithOptional(e => e.tblBusinessCustomer)
                .HasForeignKey(e => e.BusinessCustomerId);

            modelBuilder.Entity<tblBusinessEmployee>()
                .HasMany(e => e.tblAppointmentFeedbacks)
                .WithOptional(e => e.tblBusinessEmployee)
                .HasForeignKey(e => e.BusinessEmployeeId);

            modelBuilder.Entity<tblBusinessEmployee>()
                .HasMany(e => e.tblAppointmentInvitees)
                .WithRequired(e => e.tblBusinessEmployee)
                .HasForeignKey(e => e.BusinessEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblBusinessEmployee>()
                .HasMany(e => e.tblBusinessOffers)
                .WithRequired(e => e.tblBusinessEmployee)
                .HasForeignKey(e => e.BusinessEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblBusinessEmployee>()
                .HasMany(e => e.tblBusinessServices)
                .WithRequired(e => e.tblBusinessEmployee)
                .HasForeignKey(e => e.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblBusinessOffer>()
                .HasMany(e => e.tblAppointments)
                .WithOptional(e => e.tblBusinessOffer)
                .HasForeignKey(e => e.BusinessOfferId);

            modelBuilder.Entity<tblBusinessOffer>()
                .HasMany(e => e.tblBusinessOfferServiceLocations)
                .WithRequired(e => e.tblBusinessOffer)
                .HasForeignKey(e => e.BusinessOfferId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblBusinessService>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tblBusinessService>()
                .HasMany(e => e.tblAppointments)
                .WithRequired(e => e.tblBusinessService)
                .HasForeignKey(e => e.BusinessServiceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblCountry>()
                .HasMany(e => e.tblServiceLocations)
                .WithRequired(e => e.tblCountry)
                .HasForeignKey(e => e.CountryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDocumentCategory>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<tblDocumentCategory>()
                .HasMany(e => e.tblAppointmentDocuments)
                .WithOptional(e => e.tblDocumentCategory)
                .HasForeignKey(e => e.DocumentCategoryId);

            modelBuilder.Entity<tblDocumentCategory>()
                .HasMany(e => e.tblDocumentCategory1)
                .WithOptional(e => e.tblDocumentCategory2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<tblMembership>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tblMembership>()
                .HasMany(e => e.tblBusinesses)
                .WithRequired(e => e.tblMembership)
                .HasForeignKey(e => e.MembershipId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblServiceLocation>()
                .HasMany(e => e.tblAppointments)
                .WithOptional(e => e.tblServiceLocation)
                .HasForeignKey(e => e.ServiceLocationId);

            modelBuilder.Entity<tblServiceLocation>()
                .HasMany(e => e.tblBusinessCustomers)
                .WithRequired(e => e.tblServiceLocation)
                .HasForeignKey(e => e.ServiceLocationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblServiceLocation>()
                .HasMany(e => e.tblBusinessEmployees)
                .WithRequired(e => e.tblServiceLocation)
                .HasForeignKey(e => e.ServiceLocationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblServiceLocation>()
                .HasMany(e => e.tblBusinessHolidays)
                .WithRequired(e => e.tblServiceLocation)
                .HasForeignKey(e => e.ServiceLocationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblServiceLocation>()
                .HasMany(e => e.tblBusinessHours)
                .WithOptional(e => e.tblServiceLocation)
                .HasForeignKey(e => e.ServiceLocationId);

            modelBuilder.Entity<tblServiceLocation>()
                .HasMany(e => e.tblBusinessOfferServiceLocations)
                .WithRequired(e => e.tblServiceLocation)
                .HasForeignKey(e => e.ServiceLocationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblTimezone>()
                .HasMany(e => e.tblBusinesses)
                .WithRequired(e => e.tblTimezone)
                .HasForeignKey(e => e.TimezoneId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblTimezone>()
                .HasMany(e => e.tblServiceLocations)
                .WithRequired(e => e.tblTimezone)
                .HasForeignKey(e => e.TimezoneId)
                .WillCascadeOnDelete(false);
        }
    }
}
