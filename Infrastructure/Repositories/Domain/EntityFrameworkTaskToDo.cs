using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Repositories.Standard.EFCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Domain
{
    public class EntityFrameworkTaskToDo : RepositoryEntityFrameworkAsync<TaskToDo>,
                                           ITaskToDoEntityFrameworkRepository,
                                           ITaskToDoRepository
    {
        public EntityFrameworkTaskToDo(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public override TaskToDo GetById(object id)
        {
            IQueryable<TaskToDo> query = GenerateQuery((taskToDo => taskToDo.Id == (int)id), null, nameof(TaskToDo.User));
            return query.SingleOrDefault();
        }

        public async override Task<TaskToDo> GetByIdAsync(object id)
        {
            IQueryable<TaskToDo> query = await Task.FromResult(GenerateQuery((taskToDo => taskToDo.Id == (int)id), null, nameof(TaskToDo.User)));
            return query.SingleOrDefault();
        }

        public override IEnumerable<TaskToDo> GetAll()
        {
            IQueryable<TaskToDo> query = GenerateQuery(null, null, nameof(TaskToDo.User));
            return query.AsEnumerable();
        }

        public async override Task<IEnumerable<TaskToDo>> GetAllAsync()
        {
            IQueryable<TaskToDo> query = await Task.FromResult(GenerateQuery(null, null, nameof(TaskToDo.User)));
            return query.AsEnumerable();
        }

    }
}
