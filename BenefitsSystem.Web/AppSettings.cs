using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitsSystem.Web
{
    [ExcludeFromCodeCoverage]
    public class AppSettings
    {
        public Decimal EmployeePayPerPaycheck { get; set; }
        public int NoOfPayChecks { get; set; }
        public Decimal EmployeeDeductionPerYear { get; set; }
        public Decimal DependantDeductionPerYear { get; set; }
        public Decimal NamedDiscountRateForLetterA { get; set; }
    }
}
