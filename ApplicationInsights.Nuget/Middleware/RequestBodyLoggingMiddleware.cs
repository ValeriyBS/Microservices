using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationInsights.Nuget.Middleware
{
    public class RequestBodyLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestBodyLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var method = context.Request.Method;

            context.Request.EnableBuffering();

            if (context.Request.Body.CanRead && (method == HttpMethods.Post || method == HttpMethods.Put || method == HttpMethods.Patch))
            {
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, bufferSize: 512, leaveOpen: true))
                {
                    var requestBody = reader.ReadToEndAsync();

                    context.Request.Body.Position = 0;

                    var requestTelemetry = context.Features.Get<RequestTelemetry>();
                    requestTelemetry?.Properties.Add("RequestBody", await requestBody);
                };
            }

            await _next(context);
        }
    }
};
