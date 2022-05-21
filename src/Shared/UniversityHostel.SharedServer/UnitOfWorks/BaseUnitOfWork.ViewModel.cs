namespace CommonLibrary.Server;

public class BaseUnitOfWork<TEntity, TViewModel> : BaseGetUnitOfWork<TEntity, TViewModel>, IBaseUnitOfWork<TEntity, TViewModel>
where TEntity : BaseEntity
where TViewModel : BaseViewModel
{
private readonly IBaseRepository<TEntity> _repository;
public BaseUnitOfWork(IBaseRepository<TEntity> repository, IMapper mapper) 
    : base(repository, mapper) => _repository = repository;

public virtual async Task<TViewModel> CreateAsync(TViewModel viewModel)
{
    TEntity entity = _mapper.Map<TEntity>(viewModel);
    TEntity entityFromDb = await _repository.AddAsync(entity);
    await _context.SaveChangesAsync();

    return _mapper.Map<TViewModel>(entityFromDb);
}
public virtual async Task<TViewModel> UpdateAsync(TViewModel viewModel)
{
    TEntity entity = _mapper.Map<TEntity>(viewModel);
    TEntity entityFromDb = await _repository.EditAsync(entity);
    await _context.SaveChangesAsync();

    return _mapper.Map<TViewModel>(entityFromDb);
}
public virtual async Task<TViewModel> DeleteByIdAsync(Guid id)
{
    TEntity entityFromDb = await _repository.DeleteAsync(id);
    await _context.SaveChangesAsync();

    return _mapper.Map<TViewModel>(entityFromDb);
}

}
