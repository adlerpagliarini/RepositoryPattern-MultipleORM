using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories.EFCore
{
    public interface IRepositoryEFCoreAsync
    {
        Task<int> CommitAsync();
    }
}
