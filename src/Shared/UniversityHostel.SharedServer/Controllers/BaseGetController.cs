namespace CommonLibrary.Server;

[ApiController]
public class BaseGetController<TEntity> : ControllerBase, IBaseGetController
where TEntity : BaseEntity
{
    private readonly IBaseGetUnitOfWork<TEntity> _unitOfWork;

    public BaseGetController(IBaseGetUnitOfWork<TEntity> unitOfWork) => _unitOfWork = unitOfWork;

    [HttpGet]
    public virtual async Task<IActionResult> GetAllAsync()
    {
        IEnumerable<TEntity> viewModels = await _unitOfWork.ReadAllAsync();
        return Ok(viewModels);
    }
    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetByIdAsync(Guid id)
    {
        TEntity viewModel = await _unitOfWork.ReadByIdAsync(id);
        return Ok(viewModel);
    }
}
