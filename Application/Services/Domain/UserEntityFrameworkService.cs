using Application.Interfaces.Services.Domain;
using Application.Services.Standard;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain;

namespace Application.Services.Domain
{
    public class UserEntityFrameworkService : ServiceBase<User>, IUserEntityFrameworkService
    {
        public UserEntityFrameworkService(IUserEntityFrameworkRepository repository) : base(repository)
        {
        }
    }
}
