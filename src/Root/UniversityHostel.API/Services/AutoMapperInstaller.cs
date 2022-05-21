namespace UniversityHostel.Server.Services
{
    using CommonLibrary;
    using CommonLibrary.AssemplyScanning;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AutoMapperInstaller : IInstaller
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(config =>
            {
                config.AllowNullCollections = true;
                config.AllowNullDestinationValues = true;
            }, AssemblyExtentions.GetReferencedAssemblies(typeof(Program).Assembly, "*.Server.dll"));
        }
    }
}
