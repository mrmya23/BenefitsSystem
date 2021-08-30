using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitsSystem.Repository.Models
{
    public class Dependant: Person
    {
        public int EmployeeID { get; set; }
        public Constants.DependantType Relationship { get; set; }
    }
}
