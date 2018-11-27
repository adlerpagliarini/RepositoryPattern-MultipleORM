using System;
using Application.Interfaces.Services.Domain;
using Infrastructure.Interfaces.Repositories.Domain;

namespace Application.Services.Domain
{
    public class UserDapperService : UserService<IUserDapperRepository>, IUserDapperService
    {
        public UserDapperService(Func<Type, IUserRepository> repository) : base(repository)
        {
        }
    }
}
