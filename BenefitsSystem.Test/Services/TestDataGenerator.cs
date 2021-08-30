using BenefitsSystem.Web.ViewModels;
using BenefitsSystem.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenefitsSystem.Test.Services
{
    public static class ViewModelTestDataGenerator
    {
        public static IEnumerable<object[]> GetEmployeeNoNameStartingWithA()
        {
            var employeeList = new List<EmployeeViewModel>()
            {
                new EmployeeViewModel
                {
                    Id=1,
                    FirstName="Rich",
                    MiddleName="M.",
                    LastName="Harper",
                    DependantList = new List<DependantViewModel>
                    {
                        new DependantViewModel
                        {
                            FirstName="Rebecca",
                            MiddleName="M.",
                            LastName="Harper",
                            EmployeeID=1,
                            Relationship=Web.ViewModels.Constants.DependantType.Spouse
                        },
                        new DependantViewModel
                        {
                            FirstName="Ron",
                            MiddleName="M.",
                            LastName="Harper",
                            EmployeeID=1,
                            Relationship=Web.ViewModels.Constants.DependantType.Child
                        }
                    }
                },

                
            };
            yield return new object[] { employeeList };
        }

        public static IEnumerable<object[]> GetEmployeeWithNameStartingWithA()
        {
            var employeeList = new List<EmployeeViewModel>()
            {
                new EmployeeViewModel
                {
                    Id = 2,
                    FirstName = "Amit",
                    LastName = "Jain",
                    DependantList = new List<DependantViewModel>
                    {
                        new DependantViewModel
                        {
                            FirstName="Shilpa",
                            LastName="Jain",
                            EmployeeID=2,
                            Relationship=Web.ViewModels.Constants.DependantType.Spouse
                        },
                        new DependantViewModel
                        {
                            FirstName="Navya",
                            LastName="Jain",
                            EmployeeID=2,
                            Relationship=Web.ViewModels.Constants.DependantType.Child
                        }
                    }
                }
            };
            yield return new object[] { employeeList };
        }

        public static IEnumerable<object[]> GetEmployeeWithDependantNameStartingWithA()
        {
            var employeeList = new List<EmployeeViewModel>()
            {
                new EmployeeViewModel
                {
                    Id = 3,
                    FirstName = "Bob",
                    LastName = "Smith",
                    DependantList = new List<DependantViewModel>
                    {
                        new DependantViewModel
                        {
                            FirstName="Ruby",
                            LastName="Smith",
                            EmployeeID=3,
                            Relationship=Web.ViewModels.Constants.DependantType.Spouse
                        },
                        new DependantViewModel
                        {
                            FirstName="Adam",
                            LastName="Smith",
                            EmployeeID=3,
                            Relationship=Web.ViewModels.Constants.DependantType.Child
                        }
                    }
                }
            };
            yield return new object[] { employeeList };
        }

        public static IEnumerable<object[]> GetEmployeeWithEmployeeAndDependantNameStartingWithA()
        {
            var employeeList = new List<EmployeeViewModel>()
            {
                new EmployeeViewModel
                {
                    Id = 4,
                    FirstName = "Adam",
                    LastName = "Smith",
                    DependantList = new List<DependantViewModel>
                    {
                        new DependantViewModel
                        {
                            FirstName="Ruby",
                            LastName="Smith",
                            EmployeeID=4,
                            Relationship=Web.ViewModels.Constants.DependantType.Spouse
                        },
                        new DependantViewModel
                        {
                            FirstName="Adam",
                            LastName="Smith",
                            EmployeeID=4,
                            Relationship=Web.ViewModels.Constants.DependantType.Child
                        }
                    }
                }
            };
            yield return new object[] { employeeList };
        }

    }

    public static class TestDataGenerator
    {
        public static IEnumerable<object[]> GetEmployeeNoNameStartingWithA()
        {
            var employeeList = new List<Employee>()
            {
                new Employee
                {
                    Id=1,
                    FirstName="Rich",
                    MiddleName="M.",
                    LastName="Harper",
                    DependantList = new List<Dependant>
                    {
                        new Dependant
                        {
                            FirstName="Rebecca",
                            MiddleName="M.",
                            LastName="Harper",
                            EmployeeID=1,
                            Relationship=Repository.Models.Constants.DependantType.Spouse
                        },
                        new Dependant
                        {
                            FirstName="Ron",
                            MiddleName="M.",
                            LastName="Harper",
                            EmployeeID=1,
                            Relationship=Repository.Models.Constants.DependantType.Child
                        }
                    }
                },


            };
            yield return new object[] { employeeList };
        }

        public static IEnumerable<object[]> GetEmployeeWithNameStartingWithA()
        {
            var employeeList = new List<Employee>()
            {
                new Employee
                {
                    Id = 2,
                    FirstName = "Amit",
                    LastName = "Jain",
                    DependantList = new List<Dependant>
                    {
                        new Dependant
                        {
                            FirstName="Shilpa",
                            LastName="Jain",
                            EmployeeID=2,
                            Relationship=Repository.Models.Constants.DependantType.Spouse
                        },
                        new Dependant
                        {
                            FirstName="Navya",
                            LastName="Jain",
                            EmployeeID=2,
                            Relationship=Repository.Models.Constants.DependantType.Child
                        }
                    }
                }
            };
            yield return new object[] { employeeList };
        }

        public static IEnumerable<object[]> GetEmployeeWithDependantNameStartingWithA()
        {
            var employeeList = new List<Employee>()
            {
                new Employee
                {
                    Id = 3,
                    FirstName = "Bob",
                    LastName = "Smith",
                    DependantList = new List<Dependant>
                    {
                        new Dependant
                        {
                            FirstName="Ruby",
                            LastName="Smith",
                            EmployeeID=3,
                            Relationship=Repository.Models.Constants.DependantType.Spouse
                        },
                        new Dependant
                        {
                            FirstName="Adam",
                            LastName="Smith",
                            EmployeeID=3,
                            Relationship=Repository.Models.Constants.DependantType.Child
                        }
                    }
                }
            };
            yield return new object[] { employeeList };
        }

        public static IEnumerable<object[]> GetEmployeeWithEmployeeAndDependantNameStartingWithA()
        {
            var employeeList = new List<Employee>()
            {
                new Employee
                {
                    Id = 4,
                    FirstName = "Adam",
                    LastName = "Smith",
                    DependantList = new List<Dependant>
                    {
                        new Dependant
                        {
                            FirstName="Ruby",
                            LastName="Smith",
                            EmployeeID=4,
                            Relationship=Repository.Models.Constants.DependantType.Spouse
                        },
                        new Dependant
                        {
                            FirstName="Adam",
                            LastName="Smith",
                            EmployeeID=4,
                            Relationship=Repository.Models.Constants.DependantType.Child
                        }
                    }
                }
            };
            yield return new object[] { employeeList };
        }

    }
}
