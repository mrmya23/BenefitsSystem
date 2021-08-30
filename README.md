# BenefitsSystem

Problem :
----------
Paylocity provides the clients with the ability to pay for their employees benefits packages. A portion of these costs are deducted from their paycheck, and Paylocity needs to handle that deduction. 
We need to build a web application to demonstrate this calculation where employers input employees and their dependents, and get a preview of the costs.

• The cost of benefits is $1000/year for each employee 

• Each dependent (children and possibly spouses) incurs a cost of $500/year 

• Anyone whose name starts with ‘A’ gets a 10% discount, employee or dependent

Assumptions:

• All employees are paid $2000 per paycheck before deductions 
• There are 26 paychecks in a year

Technology Stack Used:
----------------------
• C#, Asp .Net Core 3.1 (MVC Model)
• Bootstrap, Javascript and Jquery 
• Auto Mapper
• Dapper
• Xunit framework with Moq 
• Nlog (writes logs to .\BenefitsSystem\Log)
• Sql server 

Project Setup :
-------------
1. DB Setup
	- Create Database BenefitsSystem in SQL server.
	- Run the following scripts included in the Script folder in Repository project(.\BenefitsSystem\BenefitsSystem.Repository\Scripts).
	• CreateTables.sql: To create tables Employees and Dependants provided in the script folder under 
	• DeleteEmployee.sql: To create storedd procedure to delete the Employee record. This deletes the associated dependants in a transaction.
	• Add Data Employee.sql: To add sample Employee Data 
	• Add Data Dependants.sql : To add sample Dependant data
	
2. Update project settings.
	- Open project solution BenefitsSystem.sln
	- In the project BenefitsSystem.Web, open file appsettings.development.json and update the value for DefaultConnection with the connection string for the corresponding server.
	
Projects included in the solution:
----------------------------------
1. BenefitsSystem.Repository : Responsible for connecting to DB
2. BenefitsSystem.Web : Web application which includes the controller class and services to connect to repository.
3. BenefitsSystem.Tests: Test cases written using XUnit and Moq. 
 
Notes:
------
Due to time constraints
1. A very basic version of the application has been developed to demonstrate the overall flow.
2. Only limited test cases have been included to demonstrate the purpose with focus on BenefitsSystemCalculatorService, for which multiple scenarious were validated.  


