namespace JwtAuthenticationManager.Models
{
    public class AuthenticationRequest : TokenBase
    {
        public string ApiName { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
