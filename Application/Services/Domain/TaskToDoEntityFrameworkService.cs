using System;
using Application.Interfaces.Services.Domain;
using Application.Services.Standard;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain;

namespace Application.Services.Domain
{
    public class TaskToDoEntityFrameworkService : TaskToDoService<ITaskToDoEntityFrameworkRepository>, ITaskToDoEntityFrameworkService
    {
        public TaskToDoEntityFrameworkService(Func<Type, ITaskToDoRepository> repository) : base(repository)
        {
        }
    }
}
