USE [AppointmentScheduler]
GO
/****** Object:  Table [dbo].[tblAdministrator]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAdministrator](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](250) NULL,
	[LastName] [nvarchar](250) NULL,
	[LoginId] [nvarchar](250) NOT NULL,
	[Password] [nvarchar](250) NOT NULL,
	[Email] [nvarchar](250) NULL,
	[ContactNumber] [nvarchar](50) NULL,
	[IsAdmin] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[AdministratorId] [bigint] NULL,
 CONSTRAINT [PK_Administrator] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblAppointment]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAppointment](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[GlobalAppointmentId] [bigint] NOT NULL,
	[BusinessServiceId] [bigint] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[PatternType] [int] NOT NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[IsRecuring] [bit] NOT NULL,
	[IsAllDayEvent] [bit] NOT NULL,
	[TextColor] [int] NULL,
	[BackColor] [int] NULL,
	[RecureEvery] [int] NULL,
	[EndAfter] [int] NULL,
	[EndAfterDate] [datetime] NULL,
	[StatusType] [int] NULL,
	[CancelReason] [ntext] NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [date] NOT NULL,
	[BusinessOfferId] [bigint] NULL,
	[ServiceLocationId] [bigint] NULL,
	[BusinessEmployeeId] [bigint] NULL,
	[BusinessCustomerId] [bigint] NULL,
 CONSTRAINT [PK_tblAppointment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblAppointmentDocument]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAppointmentDocument](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[DocumentType] [int] NULL,
	[IsEmployeeUpload] [bit] NOT NULL,
	[DocumentLink] [nvarchar](max) NULL,
	[DocumentCategoryId] [bigint] NULL,
	[AppointmentId] [bigint] NULL,
 CONSTRAINT [PK_AppointmentDocument] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblAppointmentFeedback]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAppointmentFeedback](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IsEmployee] [bit] NOT NULL,
	[BusinessEmployeeId] [bigint] NULL,
	[BusinessCustomerId] [bigint] NULL,
	[Rating] [int] NULL,
	[Feedback] [ntext] NULL,
	[IsActive] [bit] NULL,
	[Created] [datetime] NULL,
	[AppointmentId] [bigint] NOT NULL,
 CONSTRAINT [PK_tblAppointmentFeedback] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblAppointmentInvitee]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAppointmentInvitee](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BusinessEmployeeId] [bigint] NOT NULL,
	[AppointmentId] [bigint] NOT NULL,
 CONSTRAINT [PK_tblAppointmentInvitee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblAppointmentPayment]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAppointmentPayment](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IsPaid] [bit] NOT NULL,
	[PaidDate] [datetime] NOT NULL,
	[Amount] [money] NULL,
	[BillingType] [int] NOT NULL,
	[PurchaseOrderNo] [nvarchar](250) NULL,
	[ChequeNumber] [nvarchar](250) NULL,
	[CardType] [int] NULL,
	[CCFirstName] [nvarchar](250) NULL,
	[CCLastName] [nvarchar](250) NULL,
	[CCardNumber] [nvarchar](250) NULL,
	[CCSecurityCode] [nvarchar](250) NULL,
	[CCExpirationDate] [datetime] NULL,
	[Created] [datetime] NULL,
	[AppointmentId] [bigint] NOT NULL,
 CONSTRAINT [PK_AppointmentPayment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblBusiness]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBusiness](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[ShortName] [nvarchar](50) NULL,
	[Logo] [nvarchar](max) NULL,
	[PhoneNumbers] [nvarchar](250) NULL,
	[FaxNumbers] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[Website] [nvarchar](250) NULL,
	[Add1] [nvarchar](250) NULL,
	[Add2] [nvarchar](250) NULL,
	[City] [nvarchar](250) NULL,
	[State] [nvarchar](250) NULL,
	[Zip] [nvarchar](50) NULL,
	[CountryId] [int] NULL,
	[IsInternational] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[MembershipId] [int] NOT NULL,
	[BusinessCategoryId] [int] NOT NULL,
	[TimezoneId] [int] NOT NULL,
 CONSTRAINT [PK_Business] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblBusinessCategory]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBusinessCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Type] [nvarchar](250) NULL,
	[PictureLink] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetime] NULL,
	[OrderNumber] [int] NULL,
	[AdministratorId] [bigint] NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_BusinessCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblBusinessCustomer]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBusinessCustomer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](250) NOT NULL,
	[LastName] [nvarchar](250) NOT NULL,
	[ProfilePicture] [image] NULL,
	[StdCode] [int] NULL,
	[PhoneNumber] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[Add1] [nvarchar](250) NULL,
	[Add2] [nvarchar](250) NULL,
	[City] [nvarchar](250) NULL,
	[State] [nvarchar](250) NULL,
	[Zip] [nvarchar](10) NULL,
	[LoginId] [nvarchar](250) NOT NULL,
	[Password] [nvarchar](250) NOT NULL,
	[Created] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[TimezoneId] [int] NULL,
	[ServiceLocationId] [bigint] NOT NULL,
 CONSTRAINT [PK_tblBusinessCustomer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblBusinessEmployee]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBusinessEmployee](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](250) NOT NULL,
	[LastName] [nvarchar](250) NOT NULL,
	[Email] [nvarchar](250) NULL,
	[STD] [int] NULL,
	[PhoneNumber] [nvarchar](250) NULL,
	[LoginId] [nvarchar](250) NOT NULL,
	[Password] [nvarchar](250) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[ServiceLocationId] [bigint] NOT NULL,
 CONSTRAINT [PK_BusinessEmployee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblBusinessHolidays]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBusinessHolidays](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OnDate] [date] NOT NULL,
	[Type] [int] NOT NULL,
	[ServiceLocationId] [bigint] NOT NULL,
 CONSTRAINT [PK_tblBusinessHolidays] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblBusinessHours]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBusinessHours](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[WeekDayId] [int] NOT NULL,
	[IsStartDay] [bit] NOT NULL,
	[IsHoliday] [bit] NOT NULL,
	[From] [date] NOT NULL,
	[To] [date] NOT NULL,
	[IsSplit1] [bit] NULL,
	[FromSplit1] [date] NULL,
	[ToSplit1] [date] NULL,
	[IsSplit2] [bit] NULL,
	[FromSplit2] [date] NULL,
	[ToSplit2] [date] NULL,
	[ServiceLocationId] [bigint] NULL,
 CONSTRAINT [PK_BusinessHours] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblBusinessOffer]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBusinessOffer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [ntext] NULL,
	[Code] [nvarchar](100) NULL,
	[ValidFrom] [date] NOT NULL,
	[ValidTo] [date] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetime] NULL,
	[BusinessEmployeeId] [bigint] NOT NULL,
 CONSTRAINT [PK_tblBusinessOffer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblBusinessOfferServiceLocation]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBusinessOfferServiceLocation](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BusinessOfferId] [bigint] NOT NULL,
	[ServiceLocationId] [bigint] NOT NULL,
 CONSTRAINT [PK_tblBusinessOfferServiceLocation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblBusinessService]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBusinessService](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [ntext] NULL,
	[Cost] [money] NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
 CONSTRAINT [PK_tblBusinessService] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblCountry]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCountry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[ISO] [nvarchar](10) NULL,
	[ISO3] [nvarchar](10) NULL,
	[CurrencyName] [nvarchar](50) NULL,
	[CurrencyCode] [nvarchar](50) NULL,
	[PhoneCode] [int] NOT NULL,
	[AdministratorId] [bigint] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblDocumentCategory]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblDocumentCategory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[OrderNo] [int] NOT NULL,
	[PictureLink] [nvarchar](max) NULL,
	[Type] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[Created] [datetime] NULL,
	[IsParent] [bit] NOT NULL,
	[ParentId] [bigint] NULL,
 CONSTRAINT [PK_DocumentCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblMembership]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMembership](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Benifits] [ntext] NULL,
	[Price] [money] NULL,
	[IsUnlimited] [bit] NOT NULL,
	[TotalEmployee] [int] NOT NULL,
	[TotalCustomer] [int] NOT NULL,
	[TotalAppointment] [int] NOT NULL,
	[TotalLocation] [int] NOT NULL,
	[TotalOffers] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[AdministratorId] [bigint] NOT NULL,
 CONSTRAINT [PK_Membership] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblServiceLocation]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblServiceLocation](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Add1] [nvarchar](250) NULL,
	[Add2] [nvarchar](250) NULL,
	[City] [nvarchar](250) NULL,
	[State] [nvarchar](250) NULL,
	[Zip] [nvarchar](10) NULL,
	[CountryId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[TimezoneId] [int] NOT NULL,
	[BusinessId] [bigint] NOT NULL,
 CONSTRAINT [PK_tblBusinessLocation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblTimezone]    Script Date: 10/19/2017 9:21:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTimezone](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[UtcOffset] [int] NOT NULL,
	[IsDST] [bit] NOT NULL,
	[CountryId] [int] NOT NULL,
	[AdministratorId] [bigint] NOT NULL,
 CONSTRAINT [PK_Timezone] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[tblAppointment]  WITH CHECK ADD  CONSTRAINT [FK_tblAppointment_tblBusinessCustomer] FOREIGN KEY([BusinessCustomerId])
REFERENCES [dbo].[tblBusinessCustomer] ([Id])
GO
ALTER TABLE [dbo].[tblAppointment] CHECK CONSTRAINT [FK_tblAppointment_tblBusinessCustomer]
GO
ALTER TABLE [dbo].[tblAppointment]  WITH CHECK ADD  CONSTRAINT [FK_tblAppointment_tblBusinessOffer] FOREIGN KEY([BusinessOfferId])
REFERENCES [dbo].[tblBusinessOffer] ([Id])
GO
ALTER TABLE [dbo].[tblAppointment] CHECK CONSTRAINT [FK_tblAppointment_tblBusinessOffer]
GO
ALTER TABLE [dbo].[tblAppointment]  WITH CHECK ADD  CONSTRAINT [FK_tblAppointment_tblBusinessService] FOREIGN KEY([BusinessServiceId])
REFERENCES [dbo].[tblBusinessService] ([Id])
GO
ALTER TABLE [dbo].[tblAppointment] CHECK CONSTRAINT [FK_tblAppointment_tblBusinessService]
GO
ALTER TABLE [dbo].[tblAppointment]  WITH CHECK ADD  CONSTRAINT [FK_tblAppointment_tblServiceLocation] FOREIGN KEY([ServiceLocationId])
REFERENCES [dbo].[tblServiceLocation] ([Id])
GO
ALTER TABLE [dbo].[tblAppointment] CHECK CONSTRAINT [FK_tblAppointment_tblServiceLocation]
GO
ALTER TABLE [dbo].[tblAppointmentDocument]  WITH CHECK ADD  CONSTRAINT [FK_AppointmentDocument_AppointmentDocument] FOREIGN KEY([DocumentCategoryId])
REFERENCES [dbo].[tblDocumentCategory] ([Id])
GO
ALTER TABLE [dbo].[tblAppointmentDocument] CHECK CONSTRAINT [FK_AppointmentDocument_AppointmentDocument]
GO
ALTER TABLE [dbo].[tblAppointmentDocument]  WITH CHECK ADD  CONSTRAINT [FK_AppointmentDocument_tblAppointment] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[tblAppointment] ([Id])
GO
ALTER TABLE [dbo].[tblAppointmentDocument] CHECK CONSTRAINT [FK_AppointmentDocument_tblAppointment]
GO
ALTER TABLE [dbo].[tblAppointmentFeedback]  WITH CHECK ADD  CONSTRAINT [FK_tblAppointmentFeedback_tblAppointment] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[tblAppointment] ([Id])
GO
ALTER TABLE [dbo].[tblAppointmentFeedback] CHECK CONSTRAINT [FK_tblAppointmentFeedback_tblAppointment]
GO
ALTER TABLE [dbo].[tblAppointmentFeedback]  WITH CHECK ADD  CONSTRAINT [FK_tblAppointmentFeedback_tblBusinessCustomer] FOREIGN KEY([BusinessCustomerId])
REFERENCES [dbo].[tblBusinessCustomer] ([Id])
GO
ALTER TABLE [dbo].[tblAppointmentFeedback] CHECK CONSTRAINT [FK_tblAppointmentFeedback_tblBusinessCustomer]
GO
ALTER TABLE [dbo].[tblAppointmentFeedback]  WITH CHECK ADD  CONSTRAINT [FK_tblAppointmentFeedback_tblBusinessEmployee] FOREIGN KEY([BusinessEmployeeId])
REFERENCES [dbo].[tblBusinessEmployee] ([Id])
GO
ALTER TABLE [dbo].[tblAppointmentFeedback] CHECK CONSTRAINT [FK_tblAppointmentFeedback_tblBusinessEmployee]
GO
ALTER TABLE [dbo].[tblAppointmentInvitee]  WITH CHECK ADD  CONSTRAINT [FK_tblAppointmentInvitee_tblAppointment] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[tblAppointment] ([Id])
GO
ALTER TABLE [dbo].[tblAppointmentInvitee] CHECK CONSTRAINT [FK_tblAppointmentInvitee_tblAppointment]
GO
ALTER TABLE [dbo].[tblAppointmentInvitee]  WITH CHECK ADD  CONSTRAINT [FK_tblAppointmentInvitee_tblBusinessEmployee] FOREIGN KEY([BusinessEmployeeId])
REFERENCES [dbo].[tblBusinessEmployee] ([Id])
GO
ALTER TABLE [dbo].[tblAppointmentInvitee] CHECK CONSTRAINT [FK_tblAppointmentInvitee_tblBusinessEmployee]
GO
ALTER TABLE [dbo].[tblAppointmentPayment]  WITH CHECK ADD  CONSTRAINT [FK_AppointmentPayment_tblAppointment] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[tblAppointment] ([Id])
GO
ALTER TABLE [dbo].[tblAppointmentPayment] CHECK CONSTRAINT [FK_AppointmentPayment_tblAppointment]
GO
ALTER TABLE [dbo].[tblBusiness]  WITH CHECK ADD  CONSTRAINT [FK_Business_BusinessCategory] FOREIGN KEY([TimezoneId])
REFERENCES [dbo].[tblTimezone] ([Id])
GO
ALTER TABLE [dbo].[tblBusiness] CHECK CONSTRAINT [FK_Business_BusinessCategory]
GO
ALTER TABLE [dbo].[tblBusiness]  WITH CHECK ADD  CONSTRAINT [FK_Business_Membership] FOREIGN KEY([MembershipId])
REFERENCES [dbo].[tblMembership] ([Id])
GO
ALTER TABLE [dbo].[tblBusiness] CHECK CONSTRAINT [FK_Business_Membership]
GO
ALTER TABLE [dbo].[tblBusiness]  WITH CHECK ADD  CONSTRAINT [FK_tblBusiness_tblBusinessCategory] FOREIGN KEY([BusinessCategoryId])
REFERENCES [dbo].[tblBusinessCategory] ([Id])
GO
ALTER TABLE [dbo].[tblBusiness] CHECK CONSTRAINT [FK_tblBusiness_tblBusinessCategory]
GO
ALTER TABLE [dbo].[tblBusinessCategory]  WITH CHECK ADD  CONSTRAINT [FK_BusinessCategory_Administrator] FOREIGN KEY([ParentId])
REFERENCES [dbo].[tblBusinessCategory] ([Id])
GO
ALTER TABLE [dbo].[tblBusinessCategory] CHECK CONSTRAINT [FK_BusinessCategory_Administrator]
GO
ALTER TABLE [dbo].[tblBusinessCustomer]  WITH CHECK ADD  CONSTRAINT [FK_tblBusinessCustomer_tblServiceLocation1] FOREIGN KEY([ServiceLocationId])
REFERENCES [dbo].[tblServiceLocation] ([Id])
GO
ALTER TABLE [dbo].[tblBusinessCustomer] CHECK CONSTRAINT [FK_tblBusinessCustomer_tblServiceLocation1]
GO
ALTER TABLE [dbo].[tblBusinessEmployee]  WITH CHECK ADD  CONSTRAINT [FK_BusinessEmployee_Business] FOREIGN KEY([ServiceLocationId])
REFERENCES [dbo].[tblServiceLocation] ([Id])
GO
ALTER TABLE [dbo].[tblBusinessEmployee] CHECK CONSTRAINT [FK_BusinessEmployee_Business]
GO
ALTER TABLE [dbo].[tblBusinessHolidays]  WITH CHECK ADD  CONSTRAINT [FK_tblBusinessHolidays_tblServiceLocation] FOREIGN KEY([ServiceLocationId])
REFERENCES [dbo].[tblServiceLocation] ([Id])
GO
ALTER TABLE [dbo].[tblBusinessHolidays] CHECK CONSTRAINT [FK_tblBusinessHolidays_tblServiceLocation]
GO
ALTER TABLE [dbo].[tblBusinessHours]  WITH CHECK ADD  CONSTRAINT [FK_BusinessHours_tblServiceLocation] FOREIGN KEY([ServiceLocationId])
REFERENCES [dbo].[tblServiceLocation] ([Id])
GO
ALTER TABLE [dbo].[tblBusinessHours] CHECK CONSTRAINT [FK_BusinessHours_tblServiceLocation]
GO
ALTER TABLE [dbo].[tblBusinessOffer]  WITH CHECK ADD  CONSTRAINT [FK_tblBusinessOffer_tblBusinessEmployee] FOREIGN KEY([BusinessEmployeeId])
REFERENCES [dbo].[tblBusinessEmployee] ([Id])
GO
ALTER TABLE [dbo].[tblBusinessOffer] CHECK CONSTRAINT [FK_tblBusinessOffer_tblBusinessEmployee]
GO
ALTER TABLE [dbo].[tblBusinessOfferServiceLocation]  WITH CHECK ADD  CONSTRAINT [FK_tblBusinessOfferServiceLocation_tblBusinessOffer] FOREIGN KEY([BusinessOfferId])
REFERENCES [dbo].[tblBusinessOffer] ([Id])
GO
ALTER TABLE [dbo].[tblBusinessOfferServiceLocation] CHECK CONSTRAINT [FK_tblBusinessOfferServiceLocation_tblBusinessOffer]
GO
ALTER TABLE [dbo].[tblBusinessOfferServiceLocation]  WITH CHECK ADD  CONSTRAINT [FK_tblBusinessOfferServiceLocation_tblServiceLocation] FOREIGN KEY([ServiceLocationId])
REFERENCES [dbo].[tblServiceLocation] ([Id])
GO
ALTER TABLE [dbo].[tblBusinessOfferServiceLocation] CHECK CONSTRAINT [FK_tblBusinessOfferServiceLocation_tblServiceLocation]
GO
ALTER TABLE [dbo].[tblBusinessService]  WITH CHECK ADD  CONSTRAINT [FK_tblBusinessService_tblBusinessEmployee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tblBusinessEmployee] ([Id])
GO
ALTER TABLE [dbo].[tblBusinessService] CHECK CONSTRAINT [FK_tblBusinessService_tblBusinessEmployee]
GO
ALTER TABLE [dbo].[tblCountry]  WITH CHECK ADD  CONSTRAINT [FK_Country_Administrator] FOREIGN KEY([AdministratorId])
REFERENCES [dbo].[tblAdministrator] ([Id])
GO
ALTER TABLE [dbo].[tblCountry] CHECK CONSTRAINT [FK_Country_Administrator]
GO
ALTER TABLE [dbo].[tblDocumentCategory]  WITH CHECK ADD  CONSTRAINT [FK_DocumentCategory_DocumentCategory] FOREIGN KEY([ParentId])
REFERENCES [dbo].[tblDocumentCategory] ([Id])
GO
ALTER TABLE [dbo].[tblDocumentCategory] CHECK CONSTRAINT [FK_DocumentCategory_DocumentCategory]
GO
ALTER TABLE [dbo].[tblMembership]  WITH CHECK ADD  CONSTRAINT [FK_Membership_Administrator] FOREIGN KEY([AdministratorId])
REFERENCES [dbo].[tblAdministrator] ([Id])
GO
ALTER TABLE [dbo].[tblMembership] CHECK CONSTRAINT [FK_Membership_Administrator]
GO
ALTER TABLE [dbo].[tblServiceLocation]  WITH CHECK ADD  CONSTRAINT [FK_tblServiceLocation_tblBusiness] FOREIGN KEY([CountryId])
REFERENCES [dbo].[tblCountry] ([Id])
GO
ALTER TABLE [dbo].[tblServiceLocation] CHECK CONSTRAINT [FK_tblServiceLocation_tblBusiness]
GO
ALTER TABLE [dbo].[tblServiceLocation]  WITH CHECK ADD  CONSTRAINT [FK_tblServiceLocation_tblBusiness1] FOREIGN KEY([BusinessId])
REFERENCES [dbo].[tblBusiness] ([Id])
GO
ALTER TABLE [dbo].[tblServiceLocation] CHECK CONSTRAINT [FK_tblServiceLocation_tblBusiness1]
GO
ALTER TABLE [dbo].[tblServiceLocation]  WITH CHECK ADD  CONSTRAINT [FK_tblServiceLocation_tblTimezone] FOREIGN KEY([TimezoneId])
REFERENCES [dbo].[tblTimezone] ([Id])
GO
ALTER TABLE [dbo].[tblServiceLocation] CHECK CONSTRAINT [FK_tblServiceLocation_tblTimezone]
GO
ALTER TABLE [dbo].[tblTimezone]  WITH CHECK ADD  CONSTRAINT [FK_Timezone_Timezone] FOREIGN KEY([AdministratorId])
REFERENCES [dbo].[tblAdministrator] ([Id])
GO
ALTER TABLE [dbo].[tblTimezone] CHECK CONSTRAINT [FK_Timezone_Timezone]
GO
