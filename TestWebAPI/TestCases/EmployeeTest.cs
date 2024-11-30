using Microsoft.Extensions.Configuration;
using Moq;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebAPI.MockData;
using WebAPI.Controllers;
using Xunit;

namespace TestWebAPI.TestCases
{
    public class EmployeeTest
    {

        [Fact]
        public async Task GetEmpList_Success()
        {
            // Arrange

            var service = new Mock<IEmployeeRepository>();
            service.Setup(x => x.GetEmployeeList()).ReturnsAsync(Data.GetSampleEmployee());
            var sut = new EmployeeTestingController(service.Object);

            // Act
            var Actionresult = await sut.GetEmployeeList();

            // Assert
            Assert.NotNull(Actionresult.Entity);
            Assert.Equal("Record fetched successfully!.",Actionresult.Message);
        }

        [Fact]
        public async Task GetEmpList_Empty()
        {
            // Arrange

            var service = new Mock<IEmployeeRepository>();
            service.Setup(x => x.GetEmployeeList()).ReturnsAsync(Data.GetEmptyEmployee());
            var sut = new EmployeeTestingController(service.Object);

            // Act
            var Actionresult = await sut.GetEmployeeList();

            // Assert
            Assert.Equal("Data not found", Actionresult.Message);
        }
    }
}
