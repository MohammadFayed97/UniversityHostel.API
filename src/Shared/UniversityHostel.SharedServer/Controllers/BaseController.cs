namespace CommonLibrary.Server;

public class BaseController<TEntity> : BaseGetController<TEntity>, IBaseController<TEntity>
    where TEntity : BaseEntity
{
    private readonly IBaseUnitOfWork<TEntity> _unitOfWork;
    private readonly IValidator<TEntity> _validator;

    public BaseController(IBaseUnitOfWork<TEntity> unitOfWork, IValidator<TEntity> validator)
        : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    [HttpPost]
    public virtual async Task<IActionResult> Post(TEntity entity)
    {
        if (entity == null)
            return BadRequest("ViewModel can not be null");

        var result = _validator.Validate(entity);
        if (!result.IsValid)
        {
            string text = string.Join("-", result.Errors.Select(e => e.ErrorMessage));
            throw new Exception("NonValid ViewModel Reason:" + text);
        }

        entity = await _unitOfWork.CreateAsync(entity);

        return Ok(entity);
    }

    [HttpPut]
    public virtual async Task<IActionResult> Put(TEntity entity)
    {

        if (entity == null)
            return BadRequest("ViewModel can not be null");

        var result = _validator.Validate(entity);
        if (!result.IsValid)
        {
            string text = string.Join("-", result.Errors.Select(e => e.ErrorMessage));
            throw new Exception("NonValid ViewModel Reason:" + text);
        }

        entity = await _unitOfWork.UpdateAsync(entity);

        return Ok(entity);
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(Guid id)
    {
        TEntity entity = await _unitOfWork.DeleteByIdAsync(id);
        return Ok(entity);
    }
}

