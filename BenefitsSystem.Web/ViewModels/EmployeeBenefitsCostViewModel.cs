

namespace BenefitsSystem.Web.ViewModels
{
    public class EmployeeBenefitsCostViewModel
    {
        public decimal DependantBenefitCostPerYear { get; set; }
        public decimal DependantBenefitCostPerPayCheck { get; set; }
        public decimal TotalBenefitCostPerYear { get; set; }
        public decimal TotalBenefitCostPerPayCheck { get; set; }
        public decimal PayPerYear { get; set; }

        public decimal PayPerPaycheck { get; set; }

        // pay after deductions
        public decimal NetPayPerYear { get; set; }
        public decimal NetPayPerPaycheck { get; set; }
        
    }
}
