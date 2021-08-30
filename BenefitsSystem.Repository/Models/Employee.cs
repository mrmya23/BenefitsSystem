using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitsSystem.Repository.Models
{
    public class Employee: Person
    {
        public List<Dependant> DependantList { get; set; }
    }
}
