using Application.Interfaces.Services;
using Application.Interfaces.Services.Domain;
using Application.Services.Standard;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Dapper;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Interfaces.Repositories.EFCore;
using Infrastructure.Interfaces.Repositories.Standard;
using System;
using System.Threading.Tasks;

namespace Application.Services.Domain
{
    public class TaskToDoService<TRepository> : ServiceBase<TaskToDo>, 
                                                ITaskToDoService<TRepository>
                            where TRepository : ITaskToDoRepository
    {
        internal TaskToDoService(Func<Type, ITaskToDoRepository> repository) : base(repository(typeof(TRepository)))
        {
        }

        public async override Task UpdateAsync(TaskToDo obj)
        {
            var taskToDo = await GetByIdAsync(obj.Id);
            obj.Status = taskToDo.Status;
            await base.UpdateAsync(obj);
        }
        public async Task UpdateStatusAsync(int id, bool status)
        {
            var taskToDo = await GetByIdAsync(id);
            taskToDo.Status = status;
            await base.UpdateAsync(taskToDo);
        }
    }
}
