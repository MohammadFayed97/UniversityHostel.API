namespace CommonLibrary.Server;

public class BaseGetUnitOfWork<TEntity> : IBaseGetUnitOfWork<TEntity>
    where TEntity : BaseEntity
{
    private readonly IBaseGetRepository<TEntity> _repository;
    protected readonly ApplicationContext _context;

    public BaseGetUnitOfWork(IBaseGetRepository<TEntity> repository)
    {
        _repository = repository;
        _context = repository.Context;
    }
    public virtual async Task<IEnumerable<TEntity>> ReadAllAsync() => await _repository.GetAllAsync();
    public virtual async Task<TEntity> ReadByIdAsync(Guid id) => await _repository.GetByIdAsync(id);
    public virtual async Task<IEnumerable<TEntity>> ReadByExpressionAsync(Expression<Func<TEntity, bool>> expression)
        => await _repository.GetByExprissionAsync(expression);
}
