namespace CommonLibrary.Server;

public interface IBaseRepository<TEntity> : IBaseGetRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> DeleteAsync(Guid id);
    Task<TEntity> EditAsync(TEntity entity);
}
