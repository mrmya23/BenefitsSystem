using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitsSystem.Repository.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]  
        public string LastName { get; set; }

    }
}
