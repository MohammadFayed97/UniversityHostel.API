namespace CommonLibrary.Server;

public interface IBaseGetUnitOfWork<TEntity>
    where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> ReadAllAsync();
    Task<IEnumerable<TEntity>> ReadByExpressionAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> ReadByIdAsync(Guid id);
}

