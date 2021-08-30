using BenefitsSystem.Web.ViewModels;
using Microsoft.Extensions.Options;
using System;


namespace BenefitsSystem.Web.Services
{
    public class BenefitsCalculatorService : IBenefitsCalculatorService
    {
        private readonly AppSettings appSettings;

        public BenefitsCalculatorService(IOptions<AppSettings> _appSettings)
        {
            appSettings = _appSettings.Value;
        }
        public EmployeeViewModel CalculateDeductions(EmployeeViewModel employee)
        {
            if (employee == null)
                throw new ArgumentNullException("Null or missing employee details.");
            employee.BenefitsTotal ??= new EmployeeBenefitsCostViewModel();
            employee = getBenefitCostAfterApplyingDiscount(employee);
            
            employee.BenefitsTotal.TotalBenefitCostPerYear = TotalBenefitCostPerYear(employee);
            employee.BenefitsTotal.TotalBenefitCostPerPayCheck 
                = AmountPerPayCheck(employee.BenefitsTotal.TotalBenefitCostPerYear);
            employee.BenefitsTotal.PayPerPaycheck = appSettings.EmployeePayPerPaycheck;
            employee.BenefitsTotal.PayPerYear = AmountPerYear(appSettings.EmployeePayPerPaycheck); ;
            employee.BenefitsTotal.NetPayPerPaycheck = NetPayPerPayCheck(employee.BenefitsTotal.TotalBenefitCostPerPayCheck);
            employee.BenefitsTotal.NetPayPerYear = NetPayPerYear(employee.BenefitsTotal.TotalBenefitCostPerYear);
            
            return employee;
        }

        private EmployeeViewModel getBenefitCostAfterApplyingDiscount(EmployeeViewModel employee)
        {
            employee.BenefitsCostPerYear = getCostPerYearAfterApplyingDiscount(employee.FirstName, appSettings.EmployeeDeductionPerYear);
            employee.BenefitsCostPerPayCheck = AmountPerPayCheck(employee.BenefitsCostPerYear);
            foreach (DependantViewModel dependant in employee.DependantList)
            {
                dependant.BenefitsCostPerYear = getCostPerYearAfterApplyingDiscount(dependant.FirstName, appSettings.DependantDeductionPerYear);
                dependant.BenefitsCostPerPayCheck = AmountPerPayCheck(dependant.BenefitsCostPerYear);
                employee.BenefitsTotal.DependantBenefitCostPerYear += dependant.BenefitsCostPerYear;
            }
            employee.BenefitsTotal.DependantBenefitCostPerPayCheck = AmountPerPayCheck(employee.BenefitsTotal.DependantBenefitCostPerYear);
            return employee;
        }

        private decimal AmountPerYear(decimal amount)
        {
            return (amount * appSettings.NoOfPayChecks); 
        }
        private decimal NetPayPerYear(decimal benefitCostPerYear)
        {
            return (appSettings.EmployeePayPerPaycheck * appSettings.NoOfPayChecks) - benefitCostPerYear;
        }

        private decimal NetPayPerPayCheck(decimal benefitCostPerPayCheck)
        {
            return (appSettings.EmployeePayPerPaycheck - benefitCostPerPayCheck);
        }

        private decimal TotalBenefitCostPerYear(EmployeeViewModel employee)
        {   
            return (employee.BenefitsCostPerYear + employee.BenefitsTotal.DependantBenefitCostPerYear);
        }
        private decimal AmountPerPayCheck(decimal amount)
        {
            if (appSettings.NoOfPayChecks <= 0)
                throw new ArgumentException("Number of Pay Check Periods cannot be less than or equal to 0.");
            return (amount / appSettings.NoOfPayChecks);
        }
        private decimal getCostPerYearAfterApplyingDiscount(string name, decimal benefitCost )
        {
            decimal discountRate = (name.Trim().ToUpper().StartsWith('A'))?
                                        (benefitCost * (1 - appSettings.NamedDiscountRateForLetterA)) : benefitCost;  
                                        
            return discountRate;
        }
    }
        
}
