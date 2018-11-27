using System;
using Application.Interfaces.Services.Domain;
using Application.Services.Standard;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain;

namespace Application.Services.Domain
{
    public class UserEntityFrameworkService : UserService<IUserEntityFrameworkRepository>, IUserEntityFrameworkService
    {
        public UserEntityFrameworkService(Func<Type, IUserRepository> repository) : base(repository)
        {
        }
    }
}
