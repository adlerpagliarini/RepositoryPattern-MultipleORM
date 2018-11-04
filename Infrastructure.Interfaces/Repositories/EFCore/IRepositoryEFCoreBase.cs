namespace Infrastructure.Interfaces.Repositories.EFCore
{
    public interface IRepositoryEFCore<TEntity> : IRepositoryBase<TEntity>, 
                                                  IRepositoryEFCoreMethods<TEntity>,
                                                  IRepositoryDapperAsync<TEntity>
                                                    where TEntity : class
    {

    }
}
