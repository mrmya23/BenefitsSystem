using BenefitsSystem.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitsSystem.Web.Services
{
    public interface IBenefitsSystemService
    {
        Task<List<EmployeeViewModel>> GetEmployeesAsync();
        Task<List<EmployeeViewModel>> GetEmployeesAsync(string searchName);
        Task<EmployeeViewModel> GetEmployeeDetailsAsync(int id);
        EmployeeViewModel GetEmployeeCostDetails(EmployeeViewModel employeeVM);
        Task<EmployeeViewModel> GetEmployeeDependantsAsync(int id);
        Task<DependantViewModel> GetDependantAsync(int id);
        Task<int> AddDependantAsync(DependantViewModel newDependant);
        Task<int> AddEmployeeAsync(EmployeeViewModel newEmployee);
        Task DeleteEmployeeAsync(int id);
        Task DeleteDependantAsync(int id);

    }
}
