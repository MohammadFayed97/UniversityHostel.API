namespace CommonLibrary.Server;

public class BaseUnitOfWork<TEntity> : BaseGetUnitOfWork<TEntity>, IBaseUnitOfWork<TEntity>
where TEntity : BaseEntity
{
    private readonly IBaseRepository<TEntity> _repository;
    public BaseUnitOfWork(IBaseRepository<TEntity> repository)
        : base(repository) => _repository = repository;

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        TEntity entityFromDb = await _repository.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return entityFromDb;
    }
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        TEntity entityFromDb = await _repository.EditAsync(entity);
        await _context.SaveChangesAsync();
        
        return entityFromDb;
    }
    public virtual async Task<TEntity> DeleteByIdAsync(Guid id)
    {
        TEntity entityFromDb = await _repository.DeleteAsync(id);
        await _context.SaveChangesAsync();
        
        return entityFromDb;
    }

}
