using System;
using Application.Interfaces.Services.Domain;
using Infrastructure.Interfaces.Repositories.Domain;

namespace Application.Services.Domain
{
    public class TaskToDoDapperService : TaskToDoService<ITaskToDoDapperRepository>, ITaskToDoDapperService
    {
        public TaskToDoDapperService(Func<Type, ITaskToDoRepository> repository) : base(repository)
        {
        }
    }
}
