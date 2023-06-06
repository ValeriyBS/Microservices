using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace ApplicationInsights.Nuget.Middleware
{
    public class ResponseBodyLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseBodyLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    context.Response.Body = memoryStream;

                    await _next(context);

                    memoryStream.Position = 0;

                    await memoryStream.CopyToAsync(originalBodyStream);

                    memoryStream.Position = 0;

                    using (var reader = new StreamReader(memoryStream))
                    {
                        var responseBody = reader.ReadToEndAsync();

                        var requestTelemetry = context.Features.Get<RequestTelemetry>();

                        requestTelemetry?.Properties.Add("ResponseBody", await responseBody);
                    } ;                    
                } ; 
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
    }
};


