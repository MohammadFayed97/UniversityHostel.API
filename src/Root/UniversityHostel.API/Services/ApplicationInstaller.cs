namespace UniversityHostel.Server.Services;

using CommonLibrary.AssemplyScanning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using AutoMapper;
using StateLog.AspNetCore;

public class ApplicationInstaller : IInstaller
{
    private const string _myAllowSpecificOrigins = "_myAllowSpecificOrigins";

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(config =>
        {
            config.Filters.Add(new TrackPerformanceFilter("UniversityHostel", "API"));
            config.Filters.Add(new TrackUsageAttribute("UniversityHostel", "API"));
        }).AddNewtonsoftJson();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(action =>
        {
            action.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Version = "1.0v",
                Title = "University Hostel API Documentation",
            });
        });

        services.AddCors(options =>
        {
            options.AddPolicy(_myAllowSpecificOrigins, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
    }
}
