namespace StateLog.AspNetCore
{
    using System;

    public static class ExceptionExtentions
    {
        public static string GetMessageFromException(this Exception exception)
        {
            if (exception.InnerException != null)
                return GetMessageFromException(exception.InnerException);

            return exception.Message;
        }
    }
}
