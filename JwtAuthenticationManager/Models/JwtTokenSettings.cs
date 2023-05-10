namespace JwtAuthenticationManager.Models
{
    public class JwtTokenSettings
    {
        public const string JwtToken = "JwtTokenSettings";
        public string ValidationIssuer { get; set; } = string.Empty;
        public string SigningIssuer { get; set; } = string.Empty;
        public IEnumerable<string> Audiences { get; set;}
        public string IssuerSecurityKey { get; set; } = string.Empty;
        public int ValidityMinutes { get; set; }
    }
}
