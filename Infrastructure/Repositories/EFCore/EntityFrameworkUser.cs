using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.EFCore
{
    public class EntityFrameworkUser : RepositoryEntityFrameworkAsync<User>, IUserRepository
    {
        public EntityFrameworkUser(DbContext dbContext) : base(dbContext)
        {

        }

        public override IEnumerable<User> GetAll()
        {
            IQueryable<User> query = GenerateQuery(null, null, "ToDoList");
            return query.AsEnumerable();
        }

        public async override Task<IEnumerable<User>> GetAllAsync()
        {
            IQueryable<User> query = await Task.FromResult(GenerateQuery(null, null, "ToDoList"));
            return query.AsEnumerable();
        }

    }
}
