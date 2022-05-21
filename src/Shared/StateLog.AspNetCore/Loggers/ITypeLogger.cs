namespace StateLog.AspNetCore
{
    public interface ITypeLogger
    {
        void WriteInformation(LoggerDetails details);
    }
}