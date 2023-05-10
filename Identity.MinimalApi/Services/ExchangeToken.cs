using Identity.MinimalApi.Interfaces;
using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using System.Security.Claims;

namespace Identity.MinimalApi.Services
{
    public class ExchangeToken : IExchangeToken
    {
        private readonly JwtTokenHandler _jwtTokenHandler;

        public ExchangeToken(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }
        public async Task<AuthenticationResponse> Execute(AuthenticationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ApiName) || string.IsNullOrWhiteSpace(request.ApiKey))
                return null;

            //Validate credentials and fill userAccount
            var userAccount = new UserAccount
            {
                ApiName = "SomeApp",
                ApiKey = "SomeAppKey"
            };

            var claims = new ClaimsIdentity();
            switch (userAccount.ApiName)
            {
                case "SomeApp":
                {
                    claims.AddClaim(new Claim("scope", "Warehouse.MinimalApi.Read"));
                } 
                    break;

                default:
                    break;

            }

            var token = _jwtTokenHandler.GenerateJwtToken(new TokenInfo
            {
                RequestedAudience = request.RequestedAudience,
                RequestedScope = request.RequestedScope,
                Claims = claims
            });

            return new AuthenticationResponse
            {
                ApiName = userAccount.ApiName,
                ExchangedToken = token
            };
        }
    }
}
