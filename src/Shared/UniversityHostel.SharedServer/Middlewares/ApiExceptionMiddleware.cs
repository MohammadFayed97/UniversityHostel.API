namespace CommonLibrary.Server.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using StateLog.AspNetCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    public class ApiExceptionMiddleware
    {
        private readonly ApiExceptionOptions _options;
        private readonly RequestDelegate _next;
        private readonly IErrorLogger _errorLogger;

        public ApiExceptionMiddleware(ApiExceptionOptions options,RequestDelegate next, IErrorLogger errorLogger)
        {
            _options = options;
            _next = next;
            _errorLogger = errorLogger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _options);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, ApiExceptionOptions options)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            ApiErrorDetails errorDetails = new ApiErrorDetails
            {
                ErrorId = Guid.NewGuid(),
                StatusCode = context.Response.StatusCode,
                Message = exception.GetMessageFromException(),
            };

            options.AddResponseDetails?.Invoke(context, exception, errorDetails);

            _errorLogger.LogWebError(options.Product, options.Layer, exception, context, new Dictionary<string, object> { { "errorId", errorDetails.ErrorId} });

            await context.Response.WriteAsync(errorDetails.ToString());

        }
    }
}
