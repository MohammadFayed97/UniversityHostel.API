namespace StateLog.AspNetCore
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;

    public static class LogHelpers
    {

        public static LoggerDetails GetLogWebError(string product, string layer, Exception exception,
            HttpContext context, Dictionary<string, object> additionalInfo)
        {
            LoggerDetails details = GetWebLogDetails(product, layer, null, context, additionalInfo);
            details.Exception = exception;
            details.Message = exception.GetMessageFromException();

            return details;
        }

        public static LoggerDetails GetWebLogDetails(string product, string layer, string message, HttpContext httpContext, Dictionary<string, object> additionalInfo)
        {
            BaseLogDetails details = new InitialDetails(product, layer, message, additionalInfo);
            details = new UserDataDecorator(details, httpContext);
            details = new RequestDataDecorator(details, httpContext);

            return details.GetLoggerDetails();
        }
    }
}
