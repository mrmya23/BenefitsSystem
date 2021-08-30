using BenefitsSystem.Repository.Models;
using Dapper;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitsSystem.Web.Repository
{
    public class BenefitsSystemRepository: IBenefitsSystemRepository
    {
        private readonly DBContext _context;
        public BenefitsSystemRepository(DBContext context)
        {
            _context = context;
        }

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
            } catch (Exception)
            {
                // TODO : Log message and throw error?
            }
            return employeeList;
        }
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
            catch (Exception)
            {
                // TODO : Log message and throw error?
                throw;
            }
            return employeeList;
        }
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
                    //employee = await dbConnection.QueryFirstOrDefaultAsync<Employee>("SELECT * FROM Employees inner join Dependants where Employees.Id = Dependants.EmployeeId and Id=@id", new { id });

                }
            }
            catch (Exception)
            {
                // TODO : Log message and throw error?
                throw;
            }
            return employee;
        }

        public async Task<Dependant> GetDependantAsync(int id)
        {
            Dependant dependant = null;
            try
            {
                using (var dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    dependant = await dbConnection.QueryFirstOrDefaultAsync<Dependant>("SELECT * FROM Dependants where Id=@id", new { id });
                    
                }
            }
            catch (Exception)
            {
                // TODO : Log message and throw error?
                throw;
            }
            return dependant;
        }

        public async Task<int> AddDependantAsync(Dependant newDependant)
        {
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
            catch (Exception)
            {
                // TODO : Log message and throw error?
                throw;
            }
            return dependantID;
        }

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
            catch (Exception)
            {
                // TODO : Log message and throw error?
                throw;
            }
            return employeeID;
        }
        public async Task DeleteEmployeeAsync(int id)
        {
            try
            {
                using (var dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    const string sql = "SELECT Id from Employee WHERE id =@Id";
                    var employeeID = dbConnection.QueryFirstAsync<int>(sql, new { id });
                    if (employeeID == null) return;

                    var returnCode = await dbConnection.ExecuteAsync("DeleteEmployee", new { id }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception)
            {
                // TODO : Log message and throw error?
                throw;
            }
        }
        public async Task DeleteDependantAsync(int id)
        {
            try
            {
                using (var dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var sqlStatement = "DELETE from Dependants WHERE Id = @Id";
                    await dbConnection.ExecuteAsync(sqlStatement, new { Id = id });
                }
            }
            catch (Exception)
            {
                // TODO : Log message and throw error?
                throw;
            }
        }
    }
}
