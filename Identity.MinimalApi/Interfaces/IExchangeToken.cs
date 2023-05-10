using JwtAuthenticationManager.Models;

namespace Identity.MinimalApi.Interfaces
{
    public interface IExchangeToken
    {
        Task<AuthenticationResponse> Execute(AuthenticationRequest request);
    }
}
