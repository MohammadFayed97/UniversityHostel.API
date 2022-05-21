namespace StateLog.AspNetCore
{
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    public class UserDataDecorator : LogDataDecorator
    {
        private readonly BaseLogDetails logDetails;
        private readonly HttpContext context;

        public UserDataDecorator(BaseLogDetails logDetails, HttpContext context)
        {
            this.logDetails = logDetails;
            this.context = context;
        }
        public override LoggerDetails GetLoggerDetails()
        {
            var details = logDetails.GetLoggerDetails();

            string userId = "";
            string userName = "";
            ClaimsPrincipal user = context.User;

            if (user is null)
                return details;

            foreach (var claim in user.Claims)
            {
                if (claim.Type == ClaimTypes.NameIdentifier)
                    userId = claim.Value;
                else if (claim.Type == ClaimTypes.Name)
                    userName = claim.Value;
                else
                    details.AdditionalInfo.Add($"User-Claim-{claim.Type}", claim.Value);
            }

            details.UserId = userId;
            details.UserName = userName;

            return details;
        }
    }
}