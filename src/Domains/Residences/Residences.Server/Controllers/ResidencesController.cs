namespace Residences.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResidencesController : BaseController<Residence, ResidenceViewModel>
{
    public ResidencesController(IResidenceUnitOfWork unitOfWork, IValidator<ResidenceViewModel> validator) : base(unitOfWork, validator)
    {
    }
}
