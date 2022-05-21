namespace StateLog.AspNetCore
{
    using System;
    using System.Collections.Generic;

    public class LoggerDetails
    {
        public DateTime TimeStamp { get; private set; }
        public string Message { get; set; }

        // WHERE
        public string Product { get; set; }
        public string Layer { get; set; }
        public string Location { get; set; }
        public string HostName { get; set; }

        // WHO
        public string UserId { get; set; }
        public string UserName { get; set; }

        // EVERYTHING ELSE
        public Exception Exception { get; set; }
        public long? TimeElapsedInMilliseconds { get; set; } // Only For Performance 
        public Dictionary<string, object> AdditionalInfo { get; set; }

        public LoggerDetails()
        {
            TimeStamp = DateTime.Now;
        }

    }
}
