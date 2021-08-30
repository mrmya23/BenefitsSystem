using BenefitsSystem.Repository.Models;
using BenefitsSystem.Repository;
using BenefitsSystem.Web.Services;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BenefitsSystem.Web.ViewModels;
using Microsoft.Extensions.Logging;

namespace BenefitsSystem.Web.Services
{

    public class BenefitsSystemService : IBenefitsSystemService
    {
        private readonly IMapper mapper;
        private readonly ILogger<BenefitsSystemService> logger;
        private readonly IBenefitsSystemRepository repository;
        private readonly IBenefitsCalculatorService calculator;
        public BenefitsSystemService(IMapper _mapper, 
                                        IBenefitsSystemRepository _repo, 
                                        IBenefitsCalculatorService _calc,
                                        ILogger<BenefitsSystemService> _logger)
        {
            mapper = _mapper;   
            repository = _repo;
            calculator = _calc;
            logger = _logger;
        }
        public async Task<List<EmployeeViewModel>> GetEmployeesAsync()
        {
            List<EmployeeViewModel> employeeList = mapper.Map<List<Employee>, List<EmployeeViewModel>>(await repository.GetEmployeesAsync());
            return (employeeList);
        }
        public async Task<List<EmployeeViewModel>> GetEmployeesAsync(string searchName)
        {
            List<EmployeeViewModel> employeeList = mapper.Map<List<Employee>, List<EmployeeViewModel>>(await repository.GetEmployeesByNameAsync(searchName));
            
            return employeeList;
        }
        public async Task<EmployeeViewModel> GetEmployeeDetailsAsync(int id)
        {
            EmployeeViewModel employeeVM;
            try
            {
                var employee = await repository.GetEmployeeDetailsAsync(id);
                employeeVM = mapper.Map<Employee, EmployeeViewModel>(employee);
            }
            catch(Exception)
            {
                // if an error occurs while getting the data, we want to rethrow preserving the call stack
                throw;
            }
            try
            {
                if (employeeVM != null)
                    employeeVM = calculator.CalculateDeductions(employeeVM);
            }
            catch(Exception ex)
            {
                // we got a valid employee but there was something wrong with the calculations
                logger.LogError(ex, $"Error in GetEmployeeDetailsAsync for Employee ID: {id}.");
            }
            return employeeVM;
        }

        public async Task<EmployeeViewModel> GetEmployeeDependantsAsync(int id)
        {
            EmployeeViewModel employeeVM;
            try
            {
                var employee = await repository.GetEmployeeDetailsAsync(id);
                employeeVM = mapper.Map<Employee, EmployeeViewModel>(employee);
            }
            catch (Exception)
            {
                // if an error occurs while getting the data, we want to rethrow preserving the call stack
                throw;
            }
            
            return employeeVM;
        }

        public EmployeeViewModel GetEmployeeCostDetails(EmployeeViewModel employeeVM)
        {
            try
            {
                if (employeeVM != null)
                    employeeVM = calculator.CalculateDeductions(employeeVM);
            }
            catch (Exception ex)
            {
                // we got a valid employee but there was something wrong with the calculations
                logger.LogError(ex,$"Error in GetEmployeeCostDetailsAsync for Employee ID: {employeeVM.Id}.");
            }
            return employeeVM;
        }
        public async Task<DependantViewModel> GetDependantAsync(int id)
        {

            var dependant = await repository.GetDependantAsync(id);
            return mapper.Map<Dependant, DependantViewModel>(dependant);
        }
        public async Task<int> AddEmployeeAsync(EmployeeViewModel newEmployee)
        {
            return await repository.AddEmployeeAsync(mapper.Map<EmployeeViewModel, Employee>(newEmployee));
        }
        public async Task<int> AddDependantAsync(DependantViewModel newDependant)
        {
            return await repository.AddDependantAsync(mapper.Map<DependantViewModel, Dependant>(newDependant));
        }
        public async Task DeleteEmployeeAsync(int id)
        {
            await repository.DeleteEmployeeAsync(id);
        }
        public async Task DeleteDependantAsync(int id)
        {
            await repository.DeleteDependantAsync(id);
        }
    }
}
