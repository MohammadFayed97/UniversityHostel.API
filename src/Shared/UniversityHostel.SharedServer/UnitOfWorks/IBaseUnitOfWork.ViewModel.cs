namespace CommonLibrary.Server;

public interface IBaseUnitOfWork<TEntity, TViewModel> : IBaseGetUnitOfWork<TEntity, TViewModel>
    where TEntity : BaseEntity
    where TViewModel : BaseViewModel
{
    Task<TViewModel> CreateAsync(TViewModel viewModel);
    Task<TViewModel> UpdateAsync(TViewModel viewModel);
    Task<TViewModel> DeleteByIdAsync(Guid id);
}
