using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Repositories.EFCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace UnitTest.Integration.Repositories.Repositories.EntityFramework
{
    [TestFixture]
    public class UserRepository
    {
        private ApplicationContext dbContext;

        private IUserRepository userEntityFramework;
        private List<User> users;

        [OneTimeSetUp]
        public void GlobalPrepare()
        {
            dbContext = new EntityFrameworkConnection().DataBaseConfiguration();
            userEntityFramework = new EntityFrameworkUser(dbContext);
        }

        [SetUp]
        public void Inicializa()
        {
            users = new List<User>()
            {
                new User () { Name = "Adler"},
                new User () { Name = "Pagliarini"}
            };
        }

        [Test]
        public void Add()
        {
            var result = userEntityFramework.Add(users[0]);
            Assert.Greater(result.Id, 0);
        }

        [Test]
        public void AddRange()
        {
            var result = userEntityFramework.AddRange(users);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void Remove()
        {
            var inserted = userEntityFramework.Add(users[0]);
            var result = userEntityFramework.Remove(inserted.Id);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void RemoveObj()
        {
            var inserted = userEntityFramework.Add(users[0]);
            var result = userEntityFramework.Remove(inserted);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void RemoveRange()
        {
            var inserted1 = userEntityFramework.Add(users[0]);
            var inserted2 = userEntityFramework.Add(users[1]);
            var usersRange = new List<User>()
            {
                inserted1, inserted2
            };
            var result = userEntityFramework.RemoveRange(usersRange);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void Update()
        {
            var inserted = userEntityFramework.Add(users[0]);
            inserted.Name = "Update";
            var result = userEntityFramework.Update(inserted);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void UpdateRange()
        {
            var inserted1 = userEntityFramework.Add(users[0]);
            var inserted2 = userEntityFramework.Add(users[1]);
            inserted1.Name = "Update1";
            inserted2.Name = "Update2";
            var usersRange = new List<User>()
            {
                inserted1, inserted2
            };
            var result = userEntityFramework.UpdateRange(usersRange);
            Assert.AreEqual(2, result);
        }
    }
}
