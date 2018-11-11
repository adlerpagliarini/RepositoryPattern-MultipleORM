using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Standard;

namespace Infrastructure.Interfaces.Repositories
{
    public interface IUserRepository : IDomainRepository<User>
    {
    }
}
