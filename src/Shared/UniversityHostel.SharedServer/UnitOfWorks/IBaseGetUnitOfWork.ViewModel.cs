namespace CommonLibrary.Server;

public interface IBaseGetUnitOfWork<TEntity, TViewModel>
    where TEntity : BaseEntity
    where TViewModel : BaseViewModel
{
    Task<IEnumerable<TViewModel>> ReadAllAsync();
    Task<IEnumerable<TViewModel>> ReadByExpressionAsync(Expression<Func<TEntity, bool>> expression);
    Task<TViewModel> ReadByIdAsync(Guid id);
}

