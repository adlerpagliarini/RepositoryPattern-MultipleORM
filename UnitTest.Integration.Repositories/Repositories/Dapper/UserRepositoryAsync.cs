using Domain.Entities;
using Infrastructure.DBConfiguration.Dapper;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Repositories.Domain;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest.Integration.Repositories.Repositories.Dapper
{
    [TestFixture]
    public class UserRepositoryAsync
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
        public async Task AddAsync()
        {
            var result = await userDapper.AddAsync(users[0]);
            Assert.Greater(result.Id, 0);
        }

        [Test]
        public async Task AddRangeAsync()
        {
            var result = await userDapper.AddRangeAsync(users);
            Assert.AreEqual(2, result);
        }

        [Test]
        public async Task RemoveAsync()
        {
            var inserted = await userDapper.AddAsync(users[0]);
            var result = await userDapper.RemoveAsync(inserted.Id);
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task RemoveObj()
        {
            var inserted = await userDapper.AddAsync(users[0]);
            var result = await userDapper.RemoveAsync(inserted);
            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task RemoveRangeAsync()
        {
            var inserted1 = await userDapper.AddAsync(users[0]);
            var inserted2 = await userDapper.AddAsync(users[1]);
            var usersRange = new List<User>()
            {
                inserted1, inserted2
            };
            var result = await userDapper.RemoveRangeAsync(usersRange);
            Assert.AreEqual(2, result);
        }

        [Test]
        public async Task UpdateAsync()
        {
            var inserted = await userDapper.AddAsync(users[0]);
            inserted.Name = "Update";
            var result = await userDapper.UpdateAsync(inserted);
            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task UpdateRangeAsync()
        {
            var inserted1 = await userDapper.AddAsync(users[0]);
            var inserted2 = await userDapper.AddAsync(users[1]);
            inserted1.Name = "Update1";
            inserted2.Name = "Update2";
            var usersRange = new List<User>()
            {
                inserted1, inserted2
            };
            var result = await userDapper.UpdateRangeAsync(usersRange);
            Assert.AreEqual(2, result);
        }
    }
}
