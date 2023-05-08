using JwtAuthenticationManager.Models;

namespace ApiGateway.WebApi.DelegatingHandlers
{
    public class TokenExchangeDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TokenExchangeDelegatingHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
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
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", newToken.JwtToken);

            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<AuthenticationResponse> ExchangeToken(string incomingToken, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var url = "http://identity.minimalapi:80/identity/exchangetoken";//_configuration.GetSection(Constants.Url).Value;

            var request = new AuthenticationRequest
            {
                ApiName = "apigateway",
                ApiKey = "apigatewaykey",
                RequestedAudience = "Warehouse.MinimalApi",
                RequestedScope = "Warehouse.MinimalApi.Read",
                Token = incomingToken
            };

            using HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, request, cancellationToken);

            response.EnsureSuccessStatusCode();

            var authenticationResponse = await response.Content.ReadFromJsonAsync<AuthenticationResponse>(cancellationToken: cancellationToken);

            return authenticationResponse ?? new AuthenticationResponse
            {
                ApiName = "",
                JwtToken = "",
                ExpiresIn = 0
            };
        }
    }
}
