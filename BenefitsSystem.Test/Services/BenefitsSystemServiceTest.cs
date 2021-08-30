using BenefitsSystem.Web.ViewModels;
using BenefitsSystem.Web.Services;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AutoMapper;
using BenefitsSystem.Repository;
using Microsoft.Extensions.Logging;
using BenefitsSystem.Repository.Models;
using System.Threading.Tasks;
using BenefitsSystem.Web;

namespace BenefitsSystem.Test.Services
{
    public class BenefitsSystemServiceTest
    {
        private readonly IMapper mapper;
        private readonly Mock<IBenefitsSystemRepository> repository = new Mock<IBenefitsSystemRepository>();
        private readonly Mock<IBenefitsCalculatorService> calculator = new Mock<IBenefitsCalculatorService>();
        private readonly Mock<ILogger<BenefitsSystemService>> logger = new Mock<ILogger<BenefitsSystemService>>();

        
        public BenefitsSystemServiceTest()
        {
            var autoMapperProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(autoMapperProfile));
            mapper = new Mapper(configuration);
        }

        #region GetEmployeesAsyncTestCases

        [Theory(DisplayName = "GetEmployeesAsync: Returns list of employees.")]
        [MemberData(nameof(TestDataGenerator.GetEmployeeNoNameStartingWithA), MemberType = typeof(TestDataGenerator))]
        public async Task GetEmployeesAsync_ReturnsListOfEmployees(List<Employee> testEmployee)
        {
            //Arrange
            BenefitsSystemService service = new BenefitsSystemService(mapper, repository.Object, calculator.Object, logger.Object);
            repository.Setup(y => y.GetEmployeesAsync()).ReturnsAsync(testEmployee); 
           
            ////Act
            List<EmployeeViewModel> employeeVMList = await service.GetEmployeesAsync();
            ////Assert
            Assert.True(employeeVMList != null);
            Assert.True(employeeVMList.Count == testEmployee.Count);
            Assert.Equal(testEmployee[0].FirstName, employeeVMList[0].FirstName);
            Assert.Equal(testEmployee[0].MiddleName, employeeVMList[0].MiddleName);
            Assert.Equal(testEmployee[0].LastName, employeeVMList[0].LastName);
        }

        [Theory(DisplayName = "GetEmployeesAsync: Search by name Returns list of employees when repo returns successfully.")]
        [MemberData(nameof(TestDataGenerator.GetEmployeeNoNameStartingWithA), MemberType = typeof(TestDataGenerator))]
        public async Task GetEmployeesAsyncWithSearchName_ReturnsListOfEmployees(List<Employee> testEmployee)
        {
            //Arrange
            BenefitsSystemService service = new BenefitsSystemService(mapper, repository.Object, calculator.Object, logger.Object);
            repository.Setup(y => y.GetEmployeesByNameAsync(It.IsAny<String>())).ReturnsAsync(testEmployee);

            ////Act
            List<EmployeeViewModel> employeeVMList = await service.GetEmployeesAsync("SearchText");
            ////Assert
            Assert.True(employeeVMList != null);
            Assert.True(employeeVMList.Count == testEmployee.Count);
            Assert.Equal(testEmployee[0].FirstName, employeeVMList[0].FirstName);
            Assert.Equal(testEmployee[0].MiddleName, employeeVMList[0].MiddleName);
            Assert.Equal(testEmployee[0].LastName, employeeVMList[0].LastName);
        }

        [Fact(DisplayName = "GetEmployeesAsync: throws exception when repo returns throws an exception.")]
        public async Task GetEmployeesAsync_ThrowsException()
        {
            //Arrange
            BenefitsSystemService service = new BenefitsSystemService(mapper, repository.Object, calculator.Object, logger.Object);
            repository.Setup(y => y.GetEmployeesAsync()).ThrowsAsync(new Exception());

            //Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.GetEmployeesAsync());
        }

        #endregion

        #region GetEmployeeDetailsAsyncTestCases

        [Theory(DisplayName = "GetEmployeeDetailsAsync: Returns Employee with given ID when repo returns successfully.")]
        [MemberData(nameof(TestDataGenerator.GetEmployeeNoNameStartingWithA), MemberType = typeof(TestDataGenerator))]
        public async Task GetEmployeeDetailsAsync_ReturnsListOfEmployees(List<Employee> testEmployee)
        {
            //Arrange
            BenefitsSystemService service = new BenefitsSystemService(mapper, repository.Object, calculator.Object, logger.Object);
            var empVM = new EmployeeViewModel{
                FirstName = testEmployee[0].FirstName,
                LastName = testEmployee[0].LastName,
                MiddleName = testEmployee[0].MiddleName,
                Id = testEmployee[0].Id
                };
            empVM.BenefitsTotal = new EmployeeBenefitsCostViewModel();
            calculator.Setup(c => c.CalculateDeductions(It.IsAny<EmployeeViewModel>())).Returns(empVM);
            repository.Setup(y => y.GetEmployeeDetailsAsync(It.IsAny<int>())).ReturnsAsync(testEmployee[0]);
            
            ////Act
            EmployeeViewModel employeeVM = await service.GetEmployeeDetailsAsync(testEmployee[0].Id);
            ////Assert
            Assert.True(employeeVM != null);
            Assert.True(employeeVM.BenefitsTotal != null);
            Assert.Equal(testEmployee[0].Id, employeeVM.Id);
            Assert.Equal(testEmployee[0].FirstName, employeeVM.FirstName);
            Assert.Equal(testEmployee[0].MiddleName, employeeVM.MiddleName);
            Assert.Equal(testEmployee[0].LastName, employeeVM.LastName);
        }

        #endregion
    }
}
