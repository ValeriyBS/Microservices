namespace Items.MinimalApi.Authentication
{
    public class ApiKeyAuthenticationEndpointFilter : IEndpointFilter
    {
        private readonly IConfiguration _configuration;

        public ApiKeyAuthenticationEndpointFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private const string ApiKeyHeaderName = "X-Api-Key";
        private const string OwnerNameHeaderName = "Realm";
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            string? apiKey = context.HttpContext.Request.Headers[ApiKeyHeaderName];
            string? ownerName = context.HttpContext.Request.Headers[OwnerNameHeaderName];

            if (!IsApiKeyValid(apiKey, ownerName))
            {
                return Results.Unauthorized();
            }
            return await next(context);
        }

        private bool IsApiKeyValid(string? apiKey, string? ownerName)
        {
            var actualApiKey = _configuration.GetValue<string>("ApiKey");
            var apiKeyOwner = _configuration.GetValue<string>("OwnerName");

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return false;
            }

            return (actualApiKey == apiKey)&&(apiKeyOwner == ownerName);
        }
    }
}
