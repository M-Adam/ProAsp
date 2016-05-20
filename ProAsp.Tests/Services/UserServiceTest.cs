using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProAsp.Core.Services;
using ProAsp.Data;
using ProAsp.Data.Models;
using ProAsp.Data.Repository;

namespace ProAsp.Tests.Services
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void ShouldReturnAllUsers()
        {
            //Arrange
            Mock<IRepository<User>> userRepo = new Mock<IRepository<User>>();
            var setOfUsers = new List<User>()
            {
                new User() {Id = Guid.NewGuid(), Name = "aaaa"},
                new User() {Id = Guid.NewGuid(), Name = "bbbb"},
                new User() {Id = Guid.NewGuid(), Name = "cccc"},
            };
            userRepo.Setup(x => x.GetAll()).Returns(() => new EnumerableQuery<User>(setOfUsers));

            //Act
            UserService userService = new UserService(userRepo.Object);
            var result = userService.GetAllUsers().ToList();

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(setOfUsers, result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(User));
        }
    }
}
