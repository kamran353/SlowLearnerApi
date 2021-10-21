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
