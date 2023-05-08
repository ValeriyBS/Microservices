namespace JwtAuthenticationManager.Models
{
    public class AuthenticationRequest
    {
        public string ApiName { get; set; }
        public string ApiKey { get; set; }
        public string RequestedAudience { get; set; }
        public string RequestedScope { get; set;}
        public string Token { get; set;}
    }
}
