using Domain.Entities;
using Infrastructure.Interfaces.Repositories.EFCore;

namespace Infrastructure.Interfaces.Repositories.Domain
{
    public interface IUserEntityFrameworkRepository : IUserRepository, IEntityFrameworkRepository<User>
    {
    }
}
