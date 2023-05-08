using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Warehouse.MinimalApi.Tests
{
    public class JwtToken
    {
        public const string JWT_SECURITY_KEY = "thisisthesecretforgeneratingakey(mustbeatleast32bitlong)";
        private const int JWT_TOKEN_VALIDITY_MINS = 80;

        [Fact]
        public void CreateJwtToken()
        {
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

            var securityKey = new SymmetricSecurityKey(tokenKey);

            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,"sub"),
            });

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "https://localhost:5010",
                Audience = "apigateway",
                Subject = claimsIdentity,
                NotBefore = DateTime.UtcNow,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            //var claimsForToken = new List<Claim>();
            //claimsForToken.Add(new Claim("sub", "UserId"));
            //claimsForToken.Add(new Claim("given_name", "FirstName"));
            //claimsForToken.Add(new Claim("family_name", "LastName"));
            //claimsForToken.Add(new Claim("city", "city"));


            //var jwtSecurityToken = new JwtSecurityToken("https://localhost:5010",
            //    "Warehouse.MinimalApi",
            //claimsForToken,
            //DateTime.UtcNow,
            //DateTime.UtcNow.AddHours(1),
            //signingCredentials);

            //var tokenToReturn = new JwtSecurityTokenHandler()
            //    .WriteToken(jwtSecurityToken);
        }
    }
}