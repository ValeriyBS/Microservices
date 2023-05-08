using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationManager;
public class JwtTokenHandler
    {
        internal const string JWT_SECURITY_KEY = "thisisthesecretforgeneratingakey(mustbeatleast32bitlong)";
        private const int JWT_TOKEN_VALIDITY_MINS = 80;

        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrWhiteSpace(authenticationRequest.ApiName) || string.IsNullOrWhiteSpace(authenticationRequest.ApiKey))
                return null;

            //Validate user creds
            var userAccount = new UserAccount
            {
                ApiName = "SomeApp",
                ApiKey = "SomeAppKey"
            };

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

            var securityKey = new SymmetricSecurityKey(tokenKey);

            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, authenticationRequest.ApiName),
                new Claim(JwtRegisteredClaimNames.Sub,"sub"),
            });

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "https://localhost:5010",
                Audience = authenticationRequest.RequestedAudience,
                Subject = claimsIdentity,
                NotBefore = DateTime.UtcNow,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

        return new AuthenticationResponse
        {
            ApiName = userAccount.ApiName,
            ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
            JwtToken = token
        };
    }
    }
