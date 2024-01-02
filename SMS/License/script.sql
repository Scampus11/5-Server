

CREATE TABLE [dbo].[AdminCustomerMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Organization] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[ContryCode] [nvarchar](max) NULL,
	[Contact] [nvarchar](max) NULL,
	[ProductName] [nvarchar](max) NULL,
	[LicenseCreationDate] [datetime] NULL,
	[LicenseExprieyDate] [nvarchar](max) NULL,
	[LicenseKey] [nvarchar](max) NULL,
	[MAC] [nvarchar](max) NULL,
	[SoftwareRegistrationDate] [datetime] NULL,
	[IsApproved] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_AdminCustomerMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AdminCustomerMaster] ADD  CONSTRAINT [DF_AdminCustomerMaster_IsApproved]  DEFAULT ((1)) FOR [IsApproved]
GO

ALTER TABLE [dbo].[AdminCustomerMaster] ADD  CONSTRAINT [DF_AdminCustomerMaster_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO


create  procedure [dbo].[checklicense]
@MAC nvarchar(max)
as
begin

DECLARE @var INT; 
SELECT @var = (select count(*) FROM AdminCustomerMaster)
if @var>1
begin

select '0' as MAC

end
else
begin
select top 1 * from AdminCustomerMaster
--where LicenseKey is not null 
--and LicenseExprieyDate is not null
--and LicenseExprieyDate >= getdate()
end

End
GO

create  Procedure [dbo].[UpdateMACAddress]
@MAC nvarchar(Max)
as
Begin

DECLARE @var INT; 

SELECT @var = (select count(*) FROM AdminCustomerMaster)

if @var=0
Begin

insert into AdminCustomerMaster
(
MAC,
SoftwareRegistrationDate,
IsApproved,
CreatedDate
)
values
(
@MAC,
GETDATE(),
1,
GETDATE()
)

End

End
GO

create procedure [dbo].[UpdateRegistarionDetail]
@CustomerId nvarchar(max)=null,
@Name nvarchar(max)=null,
@Organization nvarchar(max)=null,
@Email nvarchar(max)=null,
@Contact nvarchar(max)=null,
@ContryCode nvarchar(max)=null,
@ProductName nvarchar(max)=null,
@LicenseCreationDate datetime =null,
@LicenseExprieyDate nvarchar(max)=null,
@LicenseKey nvarchar(max)=null,
@MAC nvarchar(max)=null,
@SoftwareRegistrationDate datetime=null,
@IsApproved bit=null
as
begin

UPDATE [dbo].[AdminCustomerMaster]
   SET [CustomerId] = @CustomerId
      ,[Name] = @Name
      ,[Organization] = @Organization
      ,[Email] = @Email
      ,[Contact] = @Contact
	  ,[ContryCode] = @ContryCode
      ,[ProductName] = @ProductName
      ,[LicenseCreationDate] = @LicenseCreationDate
      ,[LicenseExprieyDate] = @LicenseExprieyDate
      ,[LicenseKey] = @LicenseKey
      ,[MAC] = @MAC
      ,[SoftwareRegistrationDate] = @SoftwareRegistrationDate
      ,[IsApproved] = @IsApproved

end
GO

create procedure [dbo].[UpdateRegistarionDetailNew]
@CustomerId nvarchar(max)=null,
@Name nvarchar(max)=null,
@Organization nvarchar(max)=null,
@Email nvarchar(max)=null,
@Contact nvarchar(max)=null,
@ContryCode nvarchar(max)=null,
@ProductName nvarchar(max)=null,
@LicenseCreationDate datetime =null,
@MAC nvarchar(max)=null,
@SoftwareRegistrationDate datetime=null,
@IsApproved bit=null
as
begin

UPDATE [dbo].[AdminCustomerMaster]
   SET [CustomerId] = @CustomerId
      ,[Name] = @Name
      ,[Organization] = @Organization
      ,[Email] = @Email
      ,[Contact] = @Contact
	  ,[ContryCode] = @ContryCode
      ,[ProductName] = @ProductName
      ,[LicenseCreationDate] = @LicenseCreationDate     
      ,[MAC] = @MAC
      ,[SoftwareRegistrationDate] = @SoftwareRegistrationDate
      ,[IsApproved] = @IsApproved

end
GO

create procedure [dbo].[UpdateRegistarionDetailCheckWithLive]
@CustomerId nvarchar(max)=null,
@Name nvarchar(max)=null,
@Organization nvarchar(max)=null,
@Email nvarchar(max)=null,
@Contact nvarchar(max)=null,
@ContryCode nvarchar(max)=null,
@ProductName nvarchar(max)=null,
@LicenseCreationDate datetime =null,
@LicenseExprieyDate nvarchar(max)=null,
@MAC nvarchar(max)=null,
@SoftwareRegistrationDate datetime=null,
@IsApproved bit=null
as
begin

UPDATE [dbo].[AdminCustomerMaster]
   SET [CustomerId] = @CustomerId
      ,[Name] = @Name
      ,[Organization] = @Organization
      ,[Email] = @Email
      ,[Contact] = @Contact
	  ,[ContryCode] = @ContryCode
      ,[ProductName] = @ProductName
      ,[LicenseCreationDate] = @LicenseCreationDate
      ,[LicenseExprieyDate] = @LicenseExprieyDate      
      ,[MAC] = @MAC
      ,[SoftwareRegistrationDate] = @SoftwareRegistrationDate
      ,[IsApproved] = @IsApproved

end
GO




CREATE TABLE [dbo].[AdminCustomerData](
	[MAC] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

create procedure [dbo].[getCustomMAC]
as
begin
select * from AdminCustomerData

end
GO


create  Procedure [dbo].[UpdateCustomMACAddress]
@MAC nvarchar(Max)
as
Begin

DECLARE @var INT; 

SELECT @var = (select count(*) FROM AdminCustomerData)

if @var=0
Begin

insert into AdminCustomerData
(
MAC
)
values
(
@MAC
)

End

End
GO
