namespace StateLog.AspNetCore
{
    using Microsoft.Extensions.Options;
    using Serilog;

    public class PerformanceLogger : IPerforamnceLogger
    {
        private readonly StateLogConfigurations _configurations;

        public PerformanceLogger(IOptions<StateLogConfigurations> options) => _configurations = options.Value;

        public void WriteInformation(LoggerDetails details)
        {
            ILogger logger = LoggerManger.PerformanceLogger;
            if(logger == null)
            {
                TemplateBuilder builder = new TemplateBuilder(_configurations.PerformanceLogPath);
                string fullPath = builder.Build();

                logger = new LoggerConfiguration().WriteTo.File(_configurations.Formatter, fullPath, shared: true).CreateLogger();
            }
            logger.Information("{@LoggerDetails}", details);
        }
    }
}
