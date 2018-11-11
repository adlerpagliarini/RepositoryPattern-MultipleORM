using Domain.Entities;
using Infrastructure.DBConfiguration.Dapper;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Repositories.Dapper;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace UnitTest.Integration.Repositories.Repositories.Dapper
{
    [TestFixture]
    public class UserRepository
    {
        private IUserRepository userDapper;
        private IOptions<DataOptionFactory> databaseOptions;
        private List<User> users;

        [OneTimeSetUp]
        public void GlobalPrepare()
        {
            databaseOptions = new DapperConnection().DataBaseConfiguration();
            userDapper = new DapperUser(databaseOptions);
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
            var result = userDapper.Add(users[0]);
            Assert.Greater(result.Id, 0);
        }

        [Test]
        public void AddRange()
        {
            var result = userDapper.AddRange(users);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void Remove()
        {
            var inserted = userDapper.Add(users[0]);
            var result = userDapper.Remove(inserted.Id);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void RemoveObj()
        {
            var inserted = userDapper.Add(users[0]);
            var result = userDapper.Remove(inserted);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void RemoveRange()
        {
            var inserted1 = userDapper.Add(users[0]);
            var inserted2 = userDapper.Add(users[1]);
            var usersRange = new List<User>()
            {
                inserted1, inserted2
            };
            var result = userDapper.RemoveRange(usersRange);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void Update()
        {
            var inserted = userDapper.Add(users[0]);
            inserted.Name = "Update";
            var result = userDapper.Update(inserted);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void UpdateRange()
        {
            var inserted1 = userDapper.Add(users[0]);
            var inserted2 = userDapper.Add(users[1]);
            inserted1.Name = "Update1";
            inserted2.Name = "Update2";
            var usersRange = new List<User>()
            {
                inserted1, inserted2
            };
            var result = userDapper.UpdateRange(usersRange);
            Assert.AreEqual(2, result);
        }
    }
}
