namespace StateLog.AspNetCore
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;

    public interface IErrorLogger : ITypeLogger
    {
        void LogWebError(string product, string layer, Exception exception,
            HttpContext httpContext, Dictionary<string, object> additionalInfo);
    }
}