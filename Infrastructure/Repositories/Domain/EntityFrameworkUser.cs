using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Interfaces.Repositories.EFCore;
using Infrastructure.Repositories.Standard.EFCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Domain
{
    public class EntityFrameworkUser : RepositoryEntityFrameworkAsync<User>,
                                       IUserEntityFrameworkRepository, 
                                       IUserRepository
    {
        public EntityFrameworkUser(ApplicationContext dbContext) : base(dbContext)
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
