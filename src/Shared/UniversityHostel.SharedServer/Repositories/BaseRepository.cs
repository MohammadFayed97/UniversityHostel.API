namespace CommonLibrary.Server;

public class BaseRepository<TEntity> : BaseGetRepository<TEntity>, IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    public BaseRepository(ApplicationContext context) : base(context)
    {
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        int maxDisplayOrder = GetMaxDisplayOrder();
        entity.DisplayOrder = ++maxDisplayOrder;

        return (await _table.AddAsync(entity)).Entity;
    }
    public virtual async Task<TEntity> EditAsync(TEntity entity)
    {
        TEntity entityFromDb = await GetByIdAsync(entity.Id);
        if (entityFromDb == null)
            return await AddAsync(entity);
        else
            return _table.Update(entity).Entity;
    }
    public virtual async Task<TEntity> DeleteAsync(Guid id)
    {
        TEntity entityFromDb = await GetByIdAsync(id);
        if (entityFromDb is null)
            throw new Exception("Entity dosn't exist in database");

        _table.Remove(entityFromDb);
        
        return entityFromDb;
    }

    public virtual int GetMaxDisplayOrder()
    {
        if (((IQueryable<TEntity>)_table).Any())
        {
            return ((IQueryable<TEntity>)_table).Max(e => e.DisplayOrder);
        }
        return 0;
    }
}
