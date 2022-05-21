namespace StateLog.AspNetCore
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Serilog;
    using System.Collections.Generic;

    public class UsageLogger : IUsageLogger
    {
        private readonly StateLogConfigurations _configurations;

        public UsageLogger(IOptions<StateLogConfigurations> options) => _configurations = options.Value;
        public void WriteInformation(LoggerDetails details)
        {
            ILogger logger = LoggerManger.UsageLogger;
            
            if (logger is null)
            {
                TemplateBuilder builder = new TemplateBuilder(_configurations.UsageLogPath);
                builder.Add("userid", details.UserId);
                string fullPath = builder.Build();

                logger = new LoggerConfiguration().WriteTo.File(_configurations.Formatter, fullPath, shared: true).CreateLogger();
            }
            logger.Information("{@LoggerDetails}", details);
        }
        public void LogWebUsage(string product, string layer, string message,
            HttpContext httpContext, Dictionary<string, object> additionalInfo)
        {
            LoggerDetails details = LogHelpers.GetWebLogDetails(product, layer, message, httpContext, additionalInfo);
            WriteInformation(details);
        }
    }
}
