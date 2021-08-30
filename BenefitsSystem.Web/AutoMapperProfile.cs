using AutoMapper;
using BenefitsSystem.Repository.Models;
using BenefitsSystem.Web.ViewModels;

namespace BenefitsSystem.Web
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            // Configure the AutoMapper Settings in the following section		
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<Dependant, DependantViewModel>().ReverseMap();
            CreateMap<Person, PersonViewModel>().ReverseMap();

        }
    }
}
