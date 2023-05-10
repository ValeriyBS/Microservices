namespace JwtAuthenticationManager.Models
{
    public abstract class TokenBase
    {
        public string RequestedAudience { get; set; } = string.Empty;
        public string RequestedScope { get; set; } = string.Empty;
    }
}
