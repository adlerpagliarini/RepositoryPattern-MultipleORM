using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces.Repositories.EFCore
{
    public interface IRepositoryEFCore
    {
        int Commit();
    }
}
