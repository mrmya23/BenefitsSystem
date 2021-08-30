using BenefitsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitsSystem.Web.Repository
{
    public interface IBenefitsSystemRepository
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<List<Employee>> GetEmployeesByNameAsync(string searchName);
        Task<Employee> GetEmployeeDetailsAsync(int id);
        Task DeleteEmployeeAsync(int id);
        Task<Dependant> GetDependantAsync(int id);
        Task<int> AddDependantAsync(Dependant newDependant);
        Task<int> AddEmployeeAsync(Employee newEmployee);
        Task DeleteDependantAsync(int id);
        
    }
}
