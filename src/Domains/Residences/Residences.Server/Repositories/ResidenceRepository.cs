namespace Residences.Server.Repositories;

public class ResidenceRepository : BaseRepository<Residence>, IResidenceRepository
{
    public ResidenceRepository(ApplicationContext context) : base(context)
    {
    }
}
