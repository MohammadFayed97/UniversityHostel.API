namespace StateLog.AspNetCore
{
    using Serilog.Formatting;

    public class StateLogConfigurations
    {
        public string PerformanceLogPath { get; set; }
        public string UsageLogPath { get; set; }
        public string UserLogPath { get; set; }
        public string ErrorLogPath { get; set; }
        public ITextFormatter Formatter { get; set; }
    }
}
