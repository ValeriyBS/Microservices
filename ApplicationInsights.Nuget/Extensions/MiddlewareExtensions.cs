using Microsoft.AspNetCore.Builder;
using ApplicationInsights.Nuget.Middleware;

namespace ApplicationInsights.Nuget.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder AppInsightsRequestBodyLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestBodyLoggingMiddleware>();
        }

        public static IApplicationBuilder AppInsightsResponseBodyLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseBodyLoggingMiddleware>();
        }
    }
};