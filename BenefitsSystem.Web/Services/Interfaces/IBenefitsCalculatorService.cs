using BenefitsSystem.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitsSystem.Web.Services
{
    public interface IBenefitsCalculatorService
    {
        EmployeeViewModel CalculateDeductions(EmployeeViewModel employee);
    }
}
