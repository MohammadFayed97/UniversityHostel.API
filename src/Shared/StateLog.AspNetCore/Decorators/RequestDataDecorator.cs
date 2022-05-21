namespace StateLog.AspNetCore
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Primitives;
    using System.Collections.Generic;

    public class RequestDataDecorator : LogDataDecorator
    {
        private readonly BaseLogDetails logDetails;
        private readonly HttpContext context;

        public RequestDataDecorator(BaseLogDetails logDetails, HttpContext context)
        {
            this.logDetails = logDetails;
            this.context = context;
        }
        public override LoggerDetails GetLoggerDetails()
        {
            var details = logDetails.GetLoggerDetails();

            var request = context.Request;
            if (request is null)
                return details;

            details.Location = request.Path;
            details.AdditionalInfo.Add("UserAgent", request.Headers["User-Agent"]);
            details.AdditionalInfo.Add("Languages", request.Headers["Accept-Language"]);

            Dictionary<string, StringValues> queryString = QueryHelpers.ParseQuery(request.QueryString.ToString());
            foreach (var item in queryString)
                details.AdditionalInfo.Add($"QueryString-{item.Key}", item.Value);

            return details;
        }
    }
}