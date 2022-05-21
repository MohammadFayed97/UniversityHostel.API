namespace Microsoft.Extensions.DependencyInjection
{
    using StateLog.AspNetCore;
    using System;

    public static class ServiceCollectionExtentions
    {
        public static void AddStateLog(this IServiceCollection services, Action<StateLogConfigurations> configAction)
            => AddStateLog(services, (sp, options) => configAction?.Invoke(options));
        
        public static void AddStateLog(this IServiceCollection services, Action<IServiceProvider, StateLogConfigurations> configAction)
        {
            services.AddOptions<StateLogConfigurations>().Configure<IServiceProvider>((options, sp) => configAction(sp, options));

            services.AddSingleton<IPerforamnceLogger, PerformanceLogger>();
            services.AddSingleton<IUsageLogger, UsageLogger>();
            services.AddSingleton<IUserLogger, UserLogger>();
            services.AddSingleton<IErrorLogger, ErrorLogger>();
            services.AddSingleton<IPerformanceTracker, PerformanceTracker>();
        }
    }
}
