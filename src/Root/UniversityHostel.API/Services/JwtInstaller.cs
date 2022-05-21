namespace UniversityHostel.Server.Services;

using CommonLibrary.AssemplyScanning;
using CommonLibrary.Server;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

public class JwtInstaller : IInstaller
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        JwtConfiguration jwtConfiguration = new JwtConfiguration();
        configuration.GetSection("JwtConfiguration").Bind(jwtConfiguration);

        string? secretKey = Environment.GetEnvironmentVariable("SECRET");
        if (string.IsNullOrEmpty(secretKey))
            throw new Exception("SecretKey Doesn't Exist In Environment Variables");

        jwtConfiguration.Key = secretKey;
        services.AddSingleton<JwtConfiguration>(jwtConfiguration);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtConfiguration.Issuer,
                ValidAudience = jwtConfiguration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key))
            };
        });
        
    }
}
