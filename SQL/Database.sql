Create database SlowLearner
Go
USE [SlowLearner]

GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](1000) NULL,
	[UserPhone] [varchar](100) NULL,
	[UserGender] [varchar](100) NULL,
	[UserPassword] [varchar](100) NULL,
	[UserDOB] [datetime] NULL,
	[IsApproved] [bit] NULL,
	[UserRole] [varchar](100) NULL,
	[ReferenceUserId] [int] NULL)

Go
create table PatientAttendants (PatientAttendantId int primary key identity,PatientId int,AttendantId int)
Create table PracticeCollection(PracticeCollectionId int primary key identity,PracticeId int ,CollectionId int)
Create Table Practice(PracticeId int primary key identity,PracticeTitle nvarchar(max),PracticeLevel int)
Create table Collections(CollectionId int primary key identity,CollectionType varchar(200),CollectionText nvarchar(max),CollectionImage varchar(max))

go

alter table Practice add DoctorId int

go

alter table Collections add DoctorId int

go
create table Appointment(AppId int primary key identity,AppDate datetime ,IsActive bit,DoctorId int,PatientId int ,Remarks nvarchar(max))
go
Create table AppoinmentPractice(AppPracticeId int primary key identity,AppId int ,PracticeId int ,PaRemarks nvarchar(max))
go
alter table Appointment add LevelNo int