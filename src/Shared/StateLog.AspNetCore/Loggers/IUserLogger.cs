namespace StateLog.AspNetCore
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;

    public interface IUserLogger : ITypeLogger
    {
        void LogWebUsage(string product, string layer, string message,
            HttpContext httpContext, Dictionary<string, object> additionalInfo);
    }
}