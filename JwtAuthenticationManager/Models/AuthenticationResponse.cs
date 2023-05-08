namespace JwtAuthenticationManager.Models
{
    public class AuthenticationResponse
    {
        public string ApiName { get; set; }
        public string JwtToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
