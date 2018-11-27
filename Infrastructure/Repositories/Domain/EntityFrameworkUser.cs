using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Repositories.Domain;
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
            IQueryable<User> query = GenerateQuery(null, null, nameof(User.TasksToDo));
            return query.AsEnumerable();
        }

        public async override Task<IEnumerable<User>> GetAllAsync()
        {
            IQueryable<User> query = await Task.FromResult(GenerateQuery(null, null, nameof(User.TasksToDo)));
            return query.AsEnumerable();
        }

    }
}
