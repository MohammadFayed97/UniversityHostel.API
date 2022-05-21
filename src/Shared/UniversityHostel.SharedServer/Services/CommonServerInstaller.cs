namespace CommonLibrary.Server.Services;

public class CommonServerInstaller : IInstaller
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IBaseGetRepository<>), typeof(BaseGetRepository<>));
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped(typeof(IBaseGetUnitOfWork<,>), typeof(BaseGetUnitOfWork<,>));
        services.AddScoped(typeof(IBaseUnitOfWork<,>), typeof(BaseUnitOfWork<,>));

        services.AddFluentValidation(options =>
        {
            options.AutomaticValidationEnabled = true;
            options.DisableDataAnnotationsValidation = true;
        });
    }
}
