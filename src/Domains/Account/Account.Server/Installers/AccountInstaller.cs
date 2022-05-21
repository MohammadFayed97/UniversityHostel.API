namespace Account.Server.Services;

public class AccountInstaller : IInstaller
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IValidator<UserForLoginViewModel>, UserForLoginValidator>();
        services.AddScoped<IValidator<UserForRegisterViewModel>, UserForRegisterValidator>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
    }
}
