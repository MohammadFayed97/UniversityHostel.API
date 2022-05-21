namespace StateLog.AspNetCore
{
    public abstract class BaseLogDetails
    {
        protected LoggerDetails loggerDetails;

        public abstract LoggerDetails GetLoggerDetails();
    }
}