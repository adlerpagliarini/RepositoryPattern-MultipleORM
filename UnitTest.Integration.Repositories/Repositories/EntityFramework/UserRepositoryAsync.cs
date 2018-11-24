using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Repositories.Domain;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest.Integration.Repositories.Repositories.EntityFramework
{
    [TestFixture]
    public class UserRepositoryAsync
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
        public async Task AddAsync()
        {
            var result = await userEntityFramework.AddAsync(users[0]);
            Assert.Greater(result.Id, 0);
        }

        [Test]
        public async Task AddRangeAsync()
        {
            var result = await userEntityFramework.AddRangeAsync(users);
            Assert.AreEqual(2, result);
        }

        [Test]
        public async Task RemoveAsync()
        {
            var inserted = await userEntityFramework.AddAsync(users[0]);
            var result = await userEntityFramework.RemoveAsync(inserted.Id);
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task RemoveAsyncObj()
        {
            var inserted = await userEntityFramework.AddAsync(users[0]);
            var result = await userEntityFramework.RemoveAsync(inserted);
            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task RemoveRangeAsync()
        {
            var inserted1 = await userEntityFramework.AddAsync(users[0]);
            var inserted2 = await userEntityFramework.AddAsync(users[1]);
            var usersRange = new List<User>()
            {
                inserted1, inserted2
            };
            var result = await userEntityFramework.RemoveRangeAsync(usersRange);
            Assert.AreEqual(2, result);
        }

        [Test]
        public async Task UpdateAsync()
        {
            var inserted = await userEntityFramework.AddAsync(users[0]);
            inserted.Name = "Update";
            var result = await userEntityFramework.UpdateAsync(inserted);
            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task UpdateRangeAsync()
        {
            var inserted1 = await userEntityFramework.AddAsync(users[0]);
            var inserted2 = await userEntityFramework.AddAsync(users[1]);
            inserted1.Name = "Update1";
            inserted2.Name = "Update2";
            var usersRange = new List<User>()
            {
                inserted1, inserted2
            };
            var result = await userEntityFramework.UpdateRangeAsync(usersRange);
            Assert.AreEqual(2, result);
        }
    }
}
