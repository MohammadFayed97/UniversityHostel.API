namespace Faculties.Server.UnitOfWorks;

public class FacultyUnitOfWork : BaseUnitOfWork<Faculty, FacultyViewModel>, IFacultyUnitOfWork
{
    public FacultyUnitOfWork(IFacultyRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
