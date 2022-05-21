namespace StateLog.AspNetCore
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;

    public class PerformanceTracker : IPerformanceTracker
    {
        private readonly IPerforamnceLogger _performanceLogger;
        private Stopwatch _stopwatch;
        private LoggerDetails _loggerDetails;

        public PerformanceTracker(IPerforamnceLogger performanceLogger) => _performanceLogger = performanceLogger;
        public void Start(LoggerDetails details)
        {
            _stopwatch = Stopwatch.StartNew();
            _loggerDetails = details;

            DateTime begainTime = DateTime.Now;
            _loggerDetails.AdditionalInfo = new Dictionary<string, object>()
            {
                {"Started At", begainTime.ToString(CultureInfo.InvariantCulture)}
            };
        }
        public void Start(LoggerDetails details, Dictionary<string, object> performanceParams)
        {
            Start(details);

            foreach (var item in performanceParams)
                _loggerDetails.AdditionalInfo.Add($"Input-{item.Key}", item.Value);
        }

        public void Stop()
        {
            _stopwatch.Stop();
            _loggerDetails.TimeElapsedInMilliseconds = _stopwatch.ElapsedMilliseconds;
            _performanceLogger.WriteInformation(_loggerDetails);
        }
    }
}
