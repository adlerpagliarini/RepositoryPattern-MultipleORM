using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Dapper;
using Infrastructure.Interfaces.Repositories.EFCore;
using Infrastructure.Interfaces.Repositories.Standard;

namespace Infrastructure.Interfaces.Repositories.Domain
{
    public interface IUserRepository : IDomainRepository<User>
    {
    }
}
