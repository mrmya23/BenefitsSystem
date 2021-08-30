USE [BenefitsSystem]
GO

INSERT INTO [dbo].[Dependants]
           ([FirstName]
           ,[LastName]
           ,[MiddleName]
           ,[EmployeeID]
           ,[Relationship])
     VALUES
           ('Katy'
           ,'Harper'
           ,'M.'
           ,1
           ,0)
GO

INSERT INTO [dbo].[Dependants]
           ([FirstName]
           ,[LastName]
           ,[MiddleName]
           ,[EmployeeID]
           ,[Relationship])
     VALUES
           ('George'
           ,'Harper'
           ,'M.'
           ,1
           ,1)
	INSERT INTO [dbo].[Dependants]
           ([FirstName]
           ,[LastName]
           ,[MiddleName]
           ,[EmployeeID]
           ,[Relationship])
		   OUTPUT Inserted.Id 
     VALUES
           ('John'
           ,'Diaz'
           ,null
           ,2
           ,0)

INSERT INTO [dbo].[Dependants]
           ([FirstName]
           ,[LastName]
           ,[MiddleName]
           ,[EmployeeID]
           ,[Relationship])
		   OUTPUT Inserted.Id 
     VALUES
           ('Rebecca'
           ,'Diaz'
           ,null
           ,2
           ,1)
GO




