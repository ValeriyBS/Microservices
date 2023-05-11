namespace JwtAuthenticationManager.Models
{
    public class JwtTokenSettings
    {
        public const string JwtToken = "JwtTokenSettings";
        public string ValidIssuer { get; set; } = string.Empty;
        public string SigningIssuer { get; set; } = string.Empty;
        public IEnumerable<string> ValidAudiences { get; set;}
        public string IssuerSigningKey { get; set; } = string.Empty;
        public int ValidityInMinutes { get; set; }
    }
}
