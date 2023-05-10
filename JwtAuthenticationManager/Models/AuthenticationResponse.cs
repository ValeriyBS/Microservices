namespace JwtAuthenticationManager.Models
{
    public class AuthenticationResponse
    {
        public string ApiName { get; set; } = string.Empty;
        public string ExchangedToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
    }
}
