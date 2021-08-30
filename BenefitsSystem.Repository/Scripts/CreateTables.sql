USE [BenefitsSystem] /** Replace with your DB name **/
GO

/****** Object:  Table [dbo].[Employees]    Script Date: 8/29/2021 9:16:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](150) NOT NULL,
	[LastName] [nvarchar](150) NOT NULL,
	[MiddleName] [nvarchar](150) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Dependants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](150) NOT NULL,
	[LastName] [nvarchar](150) NOT NULL,
	[MiddleName] [nvarchar](150) NULL,
	[EmployeeID] [int] NOT NULL,
	[Relationship] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Dependants] ADD  CONSTRAINT [DF_Dependant_Relationship]  DEFAULT ((1)) FOR [Relationship]
GO


