namespace CommonLibrary.Server;

public class BaseGetRepository<TEntity> : IBaseGetRepository<TEntity> 
    where TEntity : BaseEntity
{
    protected DbSet<TEntity> _table;

    public BaseGetRepository(ApplicationContext context)
    {
        Context = context;
        _table = context.Set<TEntity>();
    }

    public ApplicationContext Context { get; }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _table.OrderBy(e => e.DisplayOrder).ToListAsync();
    public virtual async Task<IEnumerable<TEntity>> GetByExprissionAsync(Expression<Func<TEntity, bool>> expression)
        => await _table.Where(expression).OrderBy(e => e.DisplayOrder).ToListAsync();
    public virtual async Task<TEntity> GetByIdAsync(Guid id) => await _table.Where(e => e.Id == id).OrderBy(e => e.DisplayOrder).FirstOrDefaultAsync();
}
