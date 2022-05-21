namespace StateLog.AspNetCore
{
    using System;
    using System.Collections.Generic;

    public class InitialDetails : BaseLogDetails
    {
        public InitialDetails(string product, string layer, string message, Dictionary<string, object> additionalInfo)
        {
            loggerDetails = new LoggerDetails
            {
                Product = product,
                Layer = layer,
                Message = message,
                HostName = Environment.MachineName,
                AdditionalInfo = additionalInfo ?? new Dictionary<string, object>()
            };
        }

        public override LoggerDetails GetLoggerDetails() => loggerDetails;

    }
}