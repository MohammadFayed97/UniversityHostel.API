namespace Residences.Mappers;

public class ResidenceMapper : Profile
{
    public ResidenceMapper()
    {
        CreateMap<Residence, ResidenceViewModel>().ReverseMap();
    }
}
