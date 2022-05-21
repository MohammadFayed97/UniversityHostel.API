namespace Faculties.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FacultiesController : BaseController<Faculty, FacultyViewModel>
{
    public FacultiesController(IFacultyUnitOfWork unitOfWork, IValidator<FacultyViewModel> validator) : base(unitOfWork, validator)
    {
    }
}
