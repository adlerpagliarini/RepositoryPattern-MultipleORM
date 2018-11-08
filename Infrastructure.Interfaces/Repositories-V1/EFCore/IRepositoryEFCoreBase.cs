namespace Infrastructure.Interfaces.Repositories_V1.EFCore
{
    public interface IRepositoryEFCore<TEntity> : IRepositoryBase<TEntity>, 
                                                  IRepositoryEFCoreMethods<TEntity>,
                                                  IRepositoryDapperAsync<TEntity>
                                                    where TEntity : class
    {

    }
}
