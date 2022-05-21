namespace CommonLibrary.Server;

public interface IBaseUnitOfWork<TEntity> : IBaseGetUnitOfWork<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteByIdAsync(Guid id);
}
