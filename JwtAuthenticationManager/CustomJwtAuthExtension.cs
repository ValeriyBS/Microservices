using System.Runtime.InteropServices.JavaScript;
using System.Text;
using JwtAuthenticationManager.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthenticationManager
{
    public static class CustomJwtAuthExtension
    {
        private static JwtTokenSettings? JwtTokenSettings { get; set; }

        public static void AddCustomJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtTokenSettings = configuration.GetSection(JwtTokenSettings.JwtToken).Get<JwtTokenSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = JwtTokenSettings.ValidationIssuer,
                    ValidAudiences = JwtTokenSettings.Audiences,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtTokenSettings.IssuerSecurityKey))
                };
            });
        }
    }
}