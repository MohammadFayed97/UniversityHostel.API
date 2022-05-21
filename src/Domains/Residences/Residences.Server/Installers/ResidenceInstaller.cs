namespace Residences.Server.Services;

public class ResidenceInstaller : IInstaller
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IValidator<ResidenceViewModel>, ResidenceValidator>();
        services.AddScoped<IResidenceRepository, ResidenceRepository>();
        services.AddScoped<IResidenceUnitOfWork, ResidenceUnitOfWork>();
    }
}
