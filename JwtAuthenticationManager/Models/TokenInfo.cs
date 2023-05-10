using System.Security.Claims;

namespace JwtAuthenticationManager.Models
{
    public class TokenInfo : TokenBase
    {
        public ClaimsIdentity Claims { get; set; }
    }
}
