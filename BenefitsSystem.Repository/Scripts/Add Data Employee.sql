USE [BenefitsSystem]
GO

--Test cases:
--1. With Middle name, Empty Middle Name ('') and Null Middle Name
--2. With Name starting with A 
INSERT INTO [dbo].[Employees]
           ([FirstName]
           ,[LastName]
           ,[MiddleName])
     VALUES
           ('Rich'
           ,'Harper'
           ,'M.')

INSERT INTO [dbo].[Employees]
           ([FirstName]
           ,[LastName]
           ,[MiddleName])
     VALUES
           ('Angelina'
           ,'Diaz'
           ,'')

INSERT INTO [dbo].[Employees]
           ([FirstName]
           ,[LastName])
     VALUES
           ('David'
           ,'Jones')
GO


