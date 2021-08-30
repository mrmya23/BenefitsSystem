using BenefitsSystem.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitsSystem.Repository
{
    public interface IBenefitsSystemRepository
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<List<Employee>> GetEmployeesByNameAsync(string searchName);
        Task<Employee> GetEmployeeDetailsAsync(int id);
        Task<int> AddEmployeeAsync(Employee newEmployee);
        Task DeleteEmployeeAsync(int id);
        Task<Dependant> GetDependantAsync(int id);
        Task<int> AddDependantAsync(Dependant newDependant);        
        Task DeleteDependantAsync(int id);
        
    }
}
