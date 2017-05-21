using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DisplayApplication.Interfaces;
using DisplayApplication.DataProvider;
using Moq;
using System.Collections.Generic;
using DisplayApplication.BusinessModel;
using DisplayApplication.BusinessLayer;

namespace DisplayApplication.Tests
{
    [TestClass]
    public class AdultUserDisplayTest
    {
        private IEnumerable<User> GetEmptyUser()
        {
            return new List<User>
            {
                new User {},
                new User {},
                new User {}
            };
        }

        private IEnumerable<User> GetUsersWithNameOnly()
        {
            return new List<User>
            {
                new User {Name = "Name 1"},
                new User {Name = "Name 2"},
                new User {Name = "Name 3"}
            };
        }

        private IEnumerable<User> GetUsersWithAgeOnly()
        {
            return new List<User>
            {
                new User {Age = 1},
                new User {Age = 100},
                new User {Age = 34},
                new User {Age = -34}
            };
        }

        private IEnumerable<User> GetUsersWithAgeAndName()
        {
            return new List<User>
            {
                new User {Name = "Name 1", Age = 1},
                new User {Name = "Name 2", Age = 100},
                new User {Name = "Name 3", Age = 34},
                new User {Name = "Name 4", Age = -34}
            };
        }

        [TestMethod]
        public void RemoveWrongUsersWithEmptyUsers()
        {
            //var userProvider = new Mock<IUserProvider>();
            //userProvider.Setup(_ => _.GetUsers()).Returns(GetEmptyUser());

            var adultUserDisplay = new AdultUserDisplay();
            var res = (List<User>)adultUserDisplay.RemoveWrongUsers(GetEmptyUser());
            Assert.IsTrue(res.Count == 0);
        }

        [TestMethod]
        public void RemoveWrongUsersWithOnlyName()
        {
            var adultUserDisplay = new AdultUserDisplay();
            var res = (List<User>)adultUserDisplay.RemoveWrongUsers(GetUsersWithNameOnly());
            Assert.IsTrue(res.Count == 0);
        }

        [TestMethod]
        public void RemoveWrongUsersWithOnlyAge()
        {
            var adultUserDisplay = new AdultUserDisplay();
            var res = (List<User>)adultUserDisplay.RemoveWrongUsers(GetUsersWithAgeOnly());
            Assert.IsTrue(res.Count == 0);
        }

        [TestMethod]
        public void RemoveWrongUsersWithAgeAndName()
        {
            var adultUserDisplay = new AdultUserDisplay();
            var res = (List<User>)adultUserDisplay.RemoveWrongUsers(GetUsersWithAgeAndName());
            Assert.IsTrue(res.Count == 4);
        }
    }
}
