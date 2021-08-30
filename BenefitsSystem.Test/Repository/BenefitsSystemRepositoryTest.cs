using System;
using Moq;
using Xunit;
using BenefitsSystem.Models;
using BenefitsSystem.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BenefitsSystem.Test.Repository
{
    public class BenefitsSystemRepositoryTest
    {
        private readonly DBContext _database = new DBContext("Server=DESKTOP-MJSMTPH\\SQLEXPRESS;Database=BenefitsSystem;User Id=sa;Password=mramyasql;Integrated Security=False;");
        private readonly Mock<BenefitsSystemRepository> _repo = new Mock<BenefitsSystemRepository>();

        [Fact(DisplayName = "Get All Employees Returns All Records.")]
        public async Task GetAllEmployeesReturnsAllRecords()
        {
            // Arrange
            BenefitsSystemRepository repo = new BenefitsSystemRepository(_database);
            //Act
            List<Employee> result = await repo.GetEmployeesAsync();
            Assert.Equal(3, result.Count);
        }

        [Fact(DisplayName = "Given an ID the corresponding employee information is returned.")]
        public async Task GetEmployeeDetailsByID()
        {
            // Arrange
            BenefitsSystemRepository repo = new BenefitsSystemRepository(_database);
            //Act
            Employee result = await repo.GetEmployeeDetailsAsync(1);
            Assert.Equal("Rich", result.FirstName);
            Assert.Equal("Harper", result.LastName);
            Assert.Equal("M.", result.MiddleName);
            Assert.NotNull(result.DependantList);
            Assert.Equal(3, result.DependantList.Count);
        }
    }
}
