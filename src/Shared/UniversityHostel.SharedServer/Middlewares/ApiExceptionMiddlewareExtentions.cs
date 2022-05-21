namespace CommonLibrary.Server.Middlewares
{
    using Microsoft.AspNetCore.Builder;
    using System;

    public static class ApiExceptionMiddlewareExtentions
    {
        public static void UseApiExceptionHandler(this IApplicationBuilder application)
        {
            ApiExceptionOptions options = new ApiExceptionOptions();
            application.UseMiddleware<ApiExceptionMiddleware>(options);
        }
        public static void UseApiExceptionHandler(this IApplicationBuilder application, Action<ApiExceptionOptions> configureOptions)
        {
            ApiExceptionOptions options = new ApiExceptionOptions();
            configureOptions(options);

            application.UseMiddleware<ApiExceptionMiddleware>(options);
        }
    }
}
