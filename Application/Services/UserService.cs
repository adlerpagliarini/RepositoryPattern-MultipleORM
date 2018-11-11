using Application.Services.Standard;
using Domain.Entities;
using Domain.Interfaces.Services;
using Infrastructure.Interfaces.Repositories;

namespace Application.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
        }
    }
}
