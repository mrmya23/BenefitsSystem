using BenefitsSystem.Repository.Models;
using Dapper;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitsSystem.Repository
{
    public class BenefitsSystemRepository: IBenefitsSystemRepository
    {
        private readonly DBContext _context;
        public BenefitsSystemRepository(DBContext context)
        {
            _context = context;
        }

        // Get list of all employees
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            List<Employee> employeeList = null;
            try
            { 
            
                using (var dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    employeeList = (List<Employee>)await dbConnection.QueryAsync<Employee>("SELECT * FROM Employees");                
                }
            } catch (Exception ex)
            {
                throw new Exception($"Error in method GetEmployeesAsync.", ex);                 
            }
            return employeeList;
        }
        
        // Search employee by name
        public async Task<List<Employee>> GetEmployeesByNameAsync(string searchName)
        {
            List<Employee> employeeList = null;
            try
            {
                using (var dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    string query = "SELECT * FROM Employees where firstname like @search or lastname like @search";
                    employeeList = (List<Employee>)await dbConnection.QueryAsync<Employee>(query, new { search = searchName + "%" });

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in method GetEmployeesByNameAsync. searchName : {searchName}", ex);
            }
            return employeeList;
        }
        // Get complete details of the employee which includes dependant information
        public async Task<Employee> GetEmployeeDetailsAsync(int id)
        {
            Employee employee = null;
            try
            {
                using (var dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    using (var employeeDetails = await dbConnection.QueryMultipleAsync("SELECT * FROM Employees where  Id=@id; SELECT * FROM Dependants where EmployeeId=@id", new { id }))
                    {
                        employee = employeeDetails.Read<Employee>().First();
                        employee.DependantList = employeeDetails.Read<Dependant>().ToList();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in method GetEmployeeDetailsAsync. Id : {id}", ex);
            }
            return employee;
        }

        // Get Dependant information based on ID
        public async Task<Dependant> GetDependantAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException($"Invalid Dependant ID! {id}");

            Dependant dependant = null;
            try
            {
                using (var dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    dependant = await dbConnection.QueryFirstOrDefaultAsync<Dependant>("SELECT * FROM Dependants where Id=@id", new { id });                   
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in method GetDependantAsync. Id : {id}", ex);
            }
            return dependant;
        }

        // Add a new dependant 
        public async Task<int> AddDependantAsync(Dependant newDependant)
        {            
            if (newDependant.EmployeeID <= 0)
                throw new ArgumentException($"Invalid Employee ID! {newDependant.EmployeeID}");

            var dependantID = 0;
            try
            {                
                using (var dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    const string sql = "INSERT INTO Dependants(FirstName, MiddleName, LastName, EmployeeID, Relationship)" +
                                        "OUTPUT Inserted.Id " +
                                       " VALUES(@firstName, @middleName, @lastName, @employeeID, @relationship); ";
                                       
                    dependantID = await dbConnection.ExecuteScalarAsync<int>(sql, 
                                                                new { 
                                                                    @firstName = newDependant.FirstName,
                                                                    @lastName = newDependant.LastName,
                                                                    @middleName = newDependant.MiddleName,
                                                                    @employeeID = newDependant.EmployeeID,
                                                                    @relationship = newDependant.Relationship
                                                                });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in  method AddDependantAsync. Name:  {newDependant.FirstName} " +
                                    $" {newDependant.MiddleName} {newDependant.LastName} , EmployeeID: {newDependant.EmployeeID}, " +
                                    $"Relationship: {newDependant.Relationship}", ex);
            }
            return dependantID;
        }

        // Add a new employee
        public async Task<int> AddEmployeeAsync(Employee newEmployee)
        {
            var employeeID = 0;
            try
            {
                using (var dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    const string sql = "INSERT INTO Employees(FirstName, MiddleName, LastName)" +
                                        "OUTPUT Inserted.Id " +
                                       " VALUES(@firstName, @middleName, @lastName); ";

                    employeeID = await dbConnection.ExecuteScalarAsync<int>(sql,
                                                                new
                                                                {
                                                                    @firstName = newEmployee.FirstName,
                                                                    @lastName = newEmployee.LastName,
                                                                    @middleName = newEmployee.MiddleName
                                                                });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in  method AddEmployeeAsync. Name:  {newEmployee.FirstName} " +
                                    $" {newEmployee.MiddleName} {newEmployee.LastName}", ex);
             
            }
            return employeeID;
        }

        // Delete employee by ID
        public async Task DeleteEmployeeAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException($"Invalid Employee ID! {id}");
            try
            {
                using (var dbConnection = _context.CreateConnection())
                {   
                    var returnCode = await dbConnection.ExecuteAsync("DeleteEmployee", new { id }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in  method DeleteEmployeeAsync. Id:  {id} ", ex);
            }
        }

        // Delete dependant by ID
        public async Task DeleteDependantAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException($"Invalid Dependant ID! {id}");
            try
            {
                using (var dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var sqlStatement = "DELETE from Dependants WHERE Id = @Id";
                    await dbConnection.ExecuteAsync(sqlStatement, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in  method DeleteDependantAsync. Id:  {id} ", ex);
            }
        }
    }
}
