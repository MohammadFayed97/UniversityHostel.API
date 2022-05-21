namespace CommonLibrary.Server;

public interface IBaseGetController
{
    Task<IActionResult> GetAllAsync();
    Task<IActionResult> GetByIdAsync(Guid id);
}
