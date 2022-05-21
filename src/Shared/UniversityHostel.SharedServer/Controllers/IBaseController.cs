namespace CommonLibrary.Server;

public interface IBaseController<TEntity> : IBaseGetController
    where TEntity : BaseEntity
{
    Task<IActionResult> Delete(Guid id);
    Task<IActionResult> Post([FromBody] TEntity entity);
    Task<IActionResult> Put([FromBody] TEntity entity);
}
