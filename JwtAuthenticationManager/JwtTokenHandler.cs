using JwtAuthenticationManager.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtAuthenticationManager;
public class JwtTokenHandler
    {
        private readonly IConfiguration _configuration;
        
        private static JwtTokenSettings? JwtTokenSettings { get; set; }

        public JwtTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (string? value, int expires) GenerateJwtToken(TokenInfo tokenInfo)
        {
            JwtTokenSettings = _configuration.GetSection(JwtTokenSettings.JwtToken).Get<JwtTokenSettings>();

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JwtTokenSettings.ValidityMinutes);
            var tokenKey = Encoding.ASCII.GetBytes(JwtTokenSettings.IssuerSecurityKey);

            var securityKey = new SymmetricSecurityKey(tokenKey);

            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = JwtTokenSettings.SigningIssuer,
                Audience = tokenInfo.RequestedAudience,
                Subject = tokenInfo.Claims,
                NotBefore = DateTime.UtcNow,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return (token, (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds);
        }
    }
