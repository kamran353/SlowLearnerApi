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


Go

Create table Words(WordId int primary key identity,WordText varchar(max),ImagePath varchar(max),WordCategory varchar(200))

Go



create table PatientWords (PatientWordId int primary key identity,PatientId int,WordId int)

Go

alter table words add WordLevel int

Go






