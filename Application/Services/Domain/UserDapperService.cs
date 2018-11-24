using Application.Interfaces.Services.Domain;
using Application.Services.Standard;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Dapper;
using Infrastructure.Interfaces.Repositories.Domain;

namespace Application.Services.Domain
{
    public class UserDapperService : ServiceBase<User>, IUserDapperService
    {
        public UserDapperService(IUserDapperRepository repository) : base(repository)
        {
        }
    }
}
