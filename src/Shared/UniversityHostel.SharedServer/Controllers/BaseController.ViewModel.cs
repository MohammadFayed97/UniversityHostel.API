namespace CommonLibrary.Server;

public class BaseController<TEntity, TViewModel> : BaseGetController<TEntity, TViewModel>, IBaseController<TEntity, TViewModel>
    where TEntity : BaseEntity
    where TViewModel : BaseViewModel
{
    private readonly IBaseUnitOfWork<TEntity, TViewModel> _unitOfWork;
    private readonly IValidator<TViewModel> _validator;

    public BaseController(IBaseUnitOfWork<TEntity, TViewModel> unitOfWork, IValidator<TViewModel> validator)
        : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    [HttpPost]
    public virtual async Task<IActionResult> Post(TViewModel viewModel)
    {
        if (viewModel == null)
            return BadRequest("ViewModel can not be null");

        var result = _validator.Validate(viewModel);
        if (!result.IsValid)
        {
            string text = string.Join("-", result.Errors.Select(e => e.ErrorMessage));
            throw new Exception("NonValid ViewModel Reason:" + text);
        }

        viewModel = await _unitOfWork.CreateAsync(viewModel);

        return Ok(viewModel);
    }

    [HttpPut]
    public virtual async Task<IActionResult> Put(TViewModel viewModel)
    {

        if (viewModel == null)
            return BadRequest("ViewModel can not be null");

        var result = _validator.Validate(viewModel);
        if (!result.IsValid)
        {
            string text = string.Join("-", result.Errors.Select(e => e.ErrorMessage));
            throw new Exception("NonValid ViewModel Reason:" + text);
        }

        viewModel = await _unitOfWork.UpdateAsync(viewModel);

        return Ok(viewModel);
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(Guid id)
    {
        TViewModel viewModel = await _unitOfWork.DeleteByIdAsync(id);
        return Ok(viewModel);
    }
}

