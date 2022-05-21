namespace CommonLibrary.Server;

public interface IBaseGetRepository<TEntity> 
    where TEntity : BaseEntity
{
    ApplicationContext Context { get; }
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetByExprissionAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> GetByIdAsync(Guid id);
}
