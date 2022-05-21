namespace Faculties.Server.Repositories;

public class FacultyRepository : BaseRepository<Faculty>, IFacultyRepository
{
    public FacultyRepository(ApplicationContext context) : base(context)
    {
    }
}
