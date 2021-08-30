using BenefitsSystem.Web.ViewModels;
using BenefitsSystem.Web.Services;
using BenefitsSystem.Web;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BenefitsSystem.Test.Services
{
    public class BenefitsCalculatorServiceTest
    {
        private readonly Mock<IOptions<AppSettings>> appSettings = new Mock<IOptions<AppSettings>>();

        public BenefitsCalculatorServiceTest()
        {
            AppSettings appValues = new AppSettings()
            {
                EmployeePayPerPaycheck = 2000,
                NoOfPayChecks = 26,
                EmployeeDeductionPerYear = 1000,
                DependantDeductionPerYear = 500,
                NamedDiscountRateForLetterA = 0.10M
            };
            appSettings.Setup(y => y.Value).Returns(appValues);
        }
        
        [Theory(DisplayName = "Successful CalculateDeductions :No names starting with A")]
        [MemberData(nameof(ViewModelTestDataGenerator.GetEmployeeNoNameStartingWithA), MemberType = typeof(ViewModelTestDataGenerator))]
        public void CalculateDeductionsNoNameStartingWithA(List<EmployeeViewModel> testEmployee)
        {
            // Arrange
            BenefitsCalculatorService calculatorService = new BenefitsCalculatorService(appSettings.Object);
            
            //Act
            var resultEmployee = calculatorService.CalculateDeductions(testEmployee[0]);
            Assert.Equal(1000,resultEmployee.BenefitsCostPerYear);
            Assert.Equal(38.46M, Math.Round(resultEmployee.BenefitsCostPerPayCheck, 2));
            Assert.Equal(50000.00M, Math.Round(resultEmployee.BenefitsTotal.NetPayPerYear, 2));
            Assert.Equal(1923.08M, Math.Round(resultEmployee.BenefitsTotal.NetPayPerPaycheck,2));
            Assert.Equal(2000.00M, Math.Round(resultEmployee.BenefitsTotal.TotalBenefitCostPerYear,2));
            Assert.Equal(76.92M, Math.Round(resultEmployee.BenefitsTotal.TotalBenefitCostPerPayCheck,2));
            foreach (DependantViewModel dependant in resultEmployee.DependantList)
            {
                Assert.Equal(500, dependant.BenefitsCostPerYear);

            }
        }

        [Theory(DisplayName = "Successful CalculateDeductions :Employee name starting with A")]
        [MemberData(nameof(ViewModelTestDataGenerator.GetEmployeeWithNameStartingWithA), MemberType = typeof(ViewModelTestDataGenerator))]
        public void CalculateDeductionsEmpNameStartingWithA(List<EmployeeViewModel> testEmployee)
        {
            // Arrange
            BenefitsCalculatorService calculatorService = new BenefitsCalculatorService(appSettings.Object);

            //Act
            var resultEmployee = calculatorService.CalculateDeductions(testEmployee[0]);
            Assert.Equal(900, resultEmployee.BenefitsCostPerYear);
            Assert.Equal(34.62M, Math.Round(resultEmployee.BenefitsCostPerPayCheck, 2));
            Assert.Equal(50100.00M, Math.Round(resultEmployee.BenefitsTotal.NetPayPerYear, 2));
            Assert.Equal(1926.92M, Math.Round(resultEmployee.BenefitsTotal.NetPayPerPaycheck, 2));
            Assert.Equal(1900.00M, Math.Round(resultEmployee.BenefitsTotal.TotalBenefitCostPerYear, 2));
            Assert.Equal(73.08M, Math.Round(resultEmployee.BenefitsTotal.TotalBenefitCostPerPayCheck, 2));
            foreach (DependantViewModel dependant in resultEmployee.DependantList)
            {
                    Assert.Equal(500, dependant.BenefitsCostPerYear);

            }
        }

        [Theory(DisplayName = "Successful CalculateDeductions: Dependant name starting with A")]
        [MemberData(nameof(ViewModelTestDataGenerator.GetEmployeeWithDependantNameStartingWithA), MemberType = typeof(ViewModelTestDataGenerator))]
        public void CalculateDeductionsDependantNameStartingWithA(List<EmployeeViewModel> testEmployee)
        {
            // Arrange
            BenefitsCalculatorService calculatorService = new BenefitsCalculatorService(appSettings.Object);

            //Act
            var resultEmployee = calculatorService.CalculateDeductions(testEmployee[0]);
            Assert.Equal(1000, resultEmployee.BenefitsCostPerYear);
            Assert.Equal(38.46M, Math.Round(resultEmployee.BenefitsCostPerPayCheck, 2));
            Assert.Equal(50050.00M, Math.Round(resultEmployee.BenefitsTotal.NetPayPerYear, 2));
            Assert.Equal(1925.00M, Math.Round(resultEmployee.BenefitsTotal.NetPayPerPaycheck, 2));
            Assert.Equal(1950.00M, Math.Round(resultEmployee.BenefitsTotal.TotalBenefitCostPerYear, 2));
            Assert.Equal(75.00M, Math.Round(resultEmployee.BenefitsTotal.TotalBenefitCostPerPayCheck, 2));
            foreach (DependantViewModel dependant in resultEmployee.DependantList)
            {
                if ((dependant.FirstName.Trim().ToUpper().StartsWith('A')))
                {
                    Assert.Equal(450, dependant.BenefitsCostPerYear);

                }
                else
                {
                    Assert.Equal(500, dependant.BenefitsCostPerYear);
                }

            }
        }

        [Theory(DisplayName = "Successful CalculateDeductions: Employee and Dependant name starting with A")]
        [MemberData(nameof(ViewModelTestDataGenerator.GetEmployeeWithEmployeeAndDependantNameStartingWithA), MemberType = typeof(ViewModelTestDataGenerator))]
        public void CalculateDeductionsEmpDependantNameStartingWithA(List<EmployeeViewModel> testEmployee)
        {
            // Arrange
            BenefitsCalculatorService calculatorService = new BenefitsCalculatorService(appSettings.Object);

            //Act
            var resultEmployee = calculatorService.CalculateDeductions(testEmployee[0]);
            Assert.Equal(900, resultEmployee.BenefitsCostPerYear);
            Assert.Equal(34.62M, Math.Round(resultEmployee.BenefitsCostPerPayCheck, 2));
            Assert.Equal(50150.00M, Math.Round(resultEmployee.BenefitsTotal.NetPayPerYear, 2));
            Assert.Equal(1928.85M, Math.Round(resultEmployee.BenefitsTotal.NetPayPerPaycheck, 2));
            Assert.Equal(1850.00M, Math.Round(resultEmployee.BenefitsTotal.TotalBenefitCostPerYear, 2));
            Assert.Equal(71.15M, Math.Round(resultEmployee.BenefitsTotal.TotalBenefitCostPerPayCheck, 2));
            foreach (DependantViewModel dependant in resultEmployee.DependantList)
            {
                if ((dependant.FirstName.Trim().ToUpper().StartsWith('A')))
                {
                    Assert.Equal(450, dependant.BenefitsCostPerYear);
                    
                }
                else
                {
                    Assert.Equal(500, dependant.BenefitsCostPerYear);
                }
                
            }
        }

        [Fact(DisplayName = "Null employee throws ArgumentNullException")]
        public void CalculateDeductionsNullEmployeeThrowsException()
        {
            // Arrange
            BenefitsCalculatorService calculatorService = new BenefitsCalculatorService(appSettings.Object);

            //Act
            Assert.Throws<ArgumentNullException>(() => calculatorService.CalculateDeductions(null));
            
            
        }
    }
}
