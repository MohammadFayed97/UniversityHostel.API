namespace StateLog.AspNetCore
{
    using Serilog;
    using System.Threading;

    public class LoggerManger
    {
        public static ILogger PerformanceLogger { get; set; }
        public static ILogger UsageLogger { get; set; }
        public static ILogger ErrorLogger { get; set; }
        public static ILogger UserLogger { get; set; }
    }
}
