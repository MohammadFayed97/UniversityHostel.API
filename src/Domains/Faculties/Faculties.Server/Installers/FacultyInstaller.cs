namespace Faculties.Server.Services;

public class FacultyInstaller : IInstaller
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IValidator<FacultyViewModel>, FacultyValidator>();
        services.AddScoped<IFacultyRepository, FacultyRepository>();
        services.AddScoped<IFacultyUnitOfWork, FacultyUnitOfWork>();
    }
}
