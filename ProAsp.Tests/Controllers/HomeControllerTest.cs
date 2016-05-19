using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProAsp;
using ProAsp.Controllers;
using Moq;
using ProAsp.Core.Services;

namespace ProAsp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            HomeController controller = new HomeController(userServiceMock.Object);

            // Act
            ViewResult result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            HomeController controller = new HomeController(userServiceMock.Object);

            // Act
            ViewResult result = controller.About();

            // Assert
            Assert.AreEqual("About me.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            HomeController controller = new HomeController(userServiceMock.Object);

            // Act
            ViewResult result = controller.Contact();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
