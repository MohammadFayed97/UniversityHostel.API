namespace StateLog.AspNetCore
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Collections.Generic;

    public class TrackPerformanceFilter : IActionFilter
    {
        private IPerformanceTracker _performanceTracker;
        private string _product, _layer;

        public TrackPerformanceFilter(string product, string layer)
        {
            _product = product;
            _layer = layer;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _performanceTracker = (IPerformanceTracker)context.HttpContext.RequestServices.GetService(typeof(IPerformanceTracker));
            if (_performanceTracker is null)
                throw new ArgumentNullException("_performanceTracker", $"No service found for type {nameof(IPerformanceTracker)}");

            HttpRequest request = context.HttpContext.Request;
            string activity = $"{request.Path} => {request.Method}";

            var routeData = new Dictionary<string, object>();
            foreach (var key in context.RouteData.Values?.Keys)
                routeData.Add($"RouteData-{key}", context.RouteData.Values[key]);

            LoggerDetails details = LogHelpers.GetWebLogDetails(_product, _layer, activity, context.HttpContext, routeData);
            _performanceTracker.Start(details);
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_performanceTracker is null)
                return;

            _performanceTracker.Stop();
        }

    }
}
