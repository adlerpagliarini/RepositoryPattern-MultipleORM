using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories.EFCore
{
    public interface IRepositoryEFCoreAsync
    {
        Task<int> CommitAsync();
    }
}
