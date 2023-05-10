using JwtAuthenticationManager.Models;

namespace ApiGateway.WebApi.DelegatingHandlers
{
    public class TokenExchangeDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public TokenExchangeDelegatingHandler(IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
           HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // extract the current token
            var incomingToken = request.Headers.Authorization.Parameter;

            // exchange it
            var newToken = await ExchangeToken(incomingToken, cancellationToken);

            // replace the incoming bearer token with our new one
            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", newToken.ExchangedToken);

            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<AuthenticationResponse?> ExchangeToken(string incomingToken, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var url = _configuration.GetSection("IdentityServerUrl").Value;

            var request = new AuthenticationRequest
            {
                ApiName = "ApiGateway.WebApi",
                ApiKey = "apigatewaykey",
                RequestedAudience = "Warehouse.MinimalApi",
                RequestedScope = "Warehouse.MinimalApi.Read",
                Token = incomingToken
            };

            using HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, request, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AuthenticationResponse>(cancellationToken: cancellationToken);
        }
    }
}
