namespace Faculties.Mappers;

public class FacultyMapper : Profile
{
    public FacultyMapper()
    {
        CreateMap<Faculty, FacultyViewModel>().ReverseMap();
    }
}
