namespace CommonLibrary.Server;

public class BaseGetUnitOfWork<TEntity, TViewModel> : IBaseGetUnitOfWork<TEntity, TViewModel>
    where TEntity : BaseEntity
    where TViewModel : BaseViewModel
{
    private readonly IBaseGetRepository<TEntity> _repository;
    protected readonly IMapper _mapper;
    protected readonly ApplicationContext _context;

    public BaseGetUnitOfWork(IBaseGetRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _context = repository.Context;
        _mapper = mapper;
    }
    public virtual async Task<IEnumerable<TViewModel>> ReadAllAsync()
    {
        IEnumerable<TEntity> entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TViewModel>>(entities);
    }
    public virtual async Task<TViewModel> ReadByIdAsync(Guid id)
    {
        TEntity entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<TViewModel>(entity);
    }
    public virtual async Task<IEnumerable<TViewModel>> ReadByExpressionAsync(Expression<Func<TEntity, bool>> expression)
    {
        IEnumerable<TEntity> entities = await _repository.GetByExprissionAsync(expression);
        return _mapper.Map<IEnumerable<TViewModel>>(entities);
    }
}
