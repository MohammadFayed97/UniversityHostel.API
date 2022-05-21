namespace CommonLibrary.Server.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using System;

    public class ApiExceptionOptions
    {
        public Action<HttpContext, Exception, ApiErrorDetails> AddResponseDetails { get; set; }
        public string Product { get; set; }
        public string Layer { get; set; } = "API";
    }
}
