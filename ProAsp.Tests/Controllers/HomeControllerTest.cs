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
        readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();
        readonly Mock<ILoggerService> _loggerServiceMock = new Mock<ILoggerService>();

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(_userServiceMock.Object, _loggerServiceMock.Object);

            // Act
            ViewResult result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController(_userServiceMock.Object, _loggerServiceMock.Object);

            // Act
            ViewResult result = controller.About();

            // Assert
            Assert.AreEqual("About me.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController(_userServiceMock.Object, _loggerServiceMock.Object);

            // Act
            ViewResult result = controller.Contact();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
