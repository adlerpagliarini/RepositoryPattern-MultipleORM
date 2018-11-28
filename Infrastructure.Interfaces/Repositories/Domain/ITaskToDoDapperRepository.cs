using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Dapper;

namespace Infrastructure.Interfaces.Repositories.Domain
{
    public interface ITaskToDoDapperRepository : ITaskToDoRepository, IDapperRepository<TaskToDo>
    {
    }
}
