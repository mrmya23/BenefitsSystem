using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitsSystem.Web.ViewModels
{
    public class EmployeeViewModel: PersonViewModel 
    {
        public List<DependantViewModel> DependantList { get; set; }
        public EmployeeBenefitsCostViewModel BenefitsTotal { get; set; }
    }
}
