namespace CommonLibrary.Server;

public interface IBaseController<TEntity, TViewModel>
    where TEntity : BaseEntity
    where TViewModel : BaseViewModel
{
    Task<IActionResult> Delete(Guid id);
    Task<IActionResult> Post([FromBody] TViewModel viewModel);
    Task<IActionResult> Put([FromBody] TViewModel viewModel);
}
