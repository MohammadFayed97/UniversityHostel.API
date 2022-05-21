namespace StateLog.AspNetCore
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Serilog;
    using Serilog.Formatting.Json;
    using System;
    using System.Collections.Generic;

    public class ErrorLogger : IErrorLogger
    {
        private readonly StateLogConfigurations _configurations;
        private TemplateBuilder templateBuilder;

        public ErrorLogger(IOptions<StateLogConfigurations> options) => _configurations = options.Value;
        public void WriteInformation(LoggerDetails details)
        {
            ILogger logger = LoggerManger.ErrorLogger;
            if (logger == null)
            {
                TemplateBuilder builder = new TemplateBuilder(_configurations.ErrorLogPath);
                builder.Add("userid", details.UserId);
                string fullPath = builder.Build();

                logger = new LoggerConfiguration().WriteTo.File(_configurations.Formatter, fullPath, shared: true).CreateLogger();
            }
            logger.Information("{@LoggerDetails}", details);
        }

        public void LogWebError(string product, string layer, Exception exception,
            HttpContext httpContext, Dictionary<string, object> additionalInfo)
        {
            LoggerDetails details = LogHelpers.GetLogWebError(product, layer, exception, httpContext, additionalInfo);
            WriteInformation(details);
        }
    }
}
