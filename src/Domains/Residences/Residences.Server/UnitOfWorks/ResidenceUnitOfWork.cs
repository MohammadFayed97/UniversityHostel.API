namespace Residences.Server.UnitOfWorks;

public class ResidenceUnitOfWork : BaseUnitOfWork<Residence, ResidenceViewModel>, IResidenceUnitOfWork
{
    public ResidenceUnitOfWork(IResidenceRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
