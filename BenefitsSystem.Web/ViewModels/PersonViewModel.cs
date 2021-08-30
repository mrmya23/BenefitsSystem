using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitsSystem.Web.ViewModels
{
    public class PersonViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]        
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public Decimal BenefitsCostPerPayCheck { get; set; }
        public Decimal BenefitsCostPerYear { get; set; }

    }
}
