using System;
using Xunit;
using TestAddUser3.Controllers;
using Microsoft.AspNetCore.Mvc;
using TestAddUser3.Models;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.NotNull(result);

        }

        [Fact]
        public void AddUser_DoesEmailExists_ReturnsStatusEmailAlreadyExists()
        {
            //Arrange
            HomeController controller = new HomeController();
            //Act
            var result = controller.AddUser(new User { Email = "contact@mariecoyne.co.uk", Password="abcdefghi" });
            //Assert
            Assert.Same("Email already exists", result);
        }

        [Fact]
        public void AddUser_DidSaveUser_ReturnsStatusRecordSaved()
        {
            //Arrange
            HomeController controller = new HomeController();
            Random rnd = new Random();
            int number = rnd.Next(3000);
            //Act
            var result = controller.AddUser(new User { Email = number + "test@mariecoyne.co.uk", Password = "abcdefghi" });
            //Assert
            Assert.Same("Record is saved Successfully!", result);
        }


    }
}
