namespace UniversityHostel.Server.Services
{
    using CommonLibrary.AssemplyScanning;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog.Formatting.Json;

    public class StateLogInstaller : IInstaller
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

            string performancePath = configuration.GetSection("StateLog:PerformancePath").Value;
            string usagePath = configuration.GetSection("StateLog:PerformancePath").Value;
            string userLogPath = configuration.GetSection("StateLog:UserPath").Value;
            string errorLogPath = configuration.GetSection("StateLog:ErrorPath").Value;

            services.AddStateLog(config =>
            {
                config.PerformanceLogPath = performancePath;
                config.UsageLogPath = usagePath;
                config.UserLogPath = userLogPath;
                config.ErrorLogPath = errorLogPath;
                config.Formatter = new JsonFormatter(",\n");
            });
        }
    }
}