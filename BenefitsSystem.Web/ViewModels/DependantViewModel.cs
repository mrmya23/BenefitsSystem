
namespace BenefitsSystem.Web.ViewModels
{
    public class DependantViewModel: PersonViewModel
    {
        public int EmployeeID { get; set; }
        public Constants.DependantType Relationship { get; set; }
    }
}
