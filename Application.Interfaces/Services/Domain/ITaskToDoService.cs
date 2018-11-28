using Application.Interfaces.Services.Standard;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Dapper;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Interfaces.Repositories.EFCore;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Domain
{
    public interface ITaskToDoService<TRepository> : IServiceBase<TaskToDo>
                                 where TRepository : ITaskToDoRepository
    {
        Task UpdateStatusAsync(int id, bool status);
    }
}
