namespace StateLog.AspNetCore
{
    using System.Collections.Generic;

    public interface IPerformanceTracker
    {
        void Start(LoggerDetails details);
        void Start(LoggerDetails details, Dictionary<string, object> performanceParams);
        void Stop();
    }
}