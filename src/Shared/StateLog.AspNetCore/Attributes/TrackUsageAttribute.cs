namespace StateLog.AspNetCore
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class TrackUsageAttribute : ActionFilterAttribute
    {
        private string _product, _layer;
        private IUserLogger _userLogger;
        public TrackUsageAttribute(string product, string layer)
        {
            _product = product;
            _layer = layer;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _userLogger = (IUserLogger)context.HttpContext.RequestServices.GetService(typeof(IUserLogger));
            if (_userLogger is null)
                throw new ArgumentNullException("_userLogger", $"No service found for type {nameof(IUserLogger)}");

            //ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "123"));
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, "MohammadFayed"));

            //context.HttpContext.User.AddIdentity(claimsIdentity);

            var routeData = new Dictionary<string, object>();
            foreach (var key in context.RouteData.Values?.Keys)
                routeData.Add($"RouteData-{key}", context.RouteData.Values[key]);

            routeData.Add("Method", context.HttpContext.Request.Method);

            string message = $"The Action {context.ActionDescriptor.DisplayName} called";

            _userLogger.LogWebUsage(_product, _layer, message, context.HttpContext, routeData);
            
            return base.OnActionExecutionAsync(context, next);
        }
        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var routeData = new Dictionary<string, object>();
            foreach (var key in context.RouteData.Values?.Keys)
                routeData.Add($"RouteData-{key}", context.RouteData.Values[key]);

            routeData.Add("Method", context.HttpContext.Request.Method);

            string message = $"The Action {context.ActionDescriptor.DisplayName} Executed successfully";
            _userLogger.LogWebUsage(_product, _layer, message, context.HttpContext, routeData);

            return base.OnResultExecutionAsync(context, next);
        }
    }
}
