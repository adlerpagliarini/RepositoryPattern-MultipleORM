using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces.Repositories.Domain
{
    public interface IUserRepository : IRepositoryBase<User>, IRepositoryBaseAsync<User>
    {
    }
}
