using System.Text;
using Items.MinimalApi.Client.Extensions;
using JwtAuthenticationManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Warehouse.MinimalApi;
public static class ServiceConfig
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddItemsApiClient(configuration);

        //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    .AddJwtBearer(options =>
        //    {
        //        options.TokenValidationParameters = new()
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateIssuerSigningKey = true,
        //            ValidIssuer = "https://localhost:5010",
        //            ValidAudience = "Warehouse.MinimalApi",
        //            IssuerSigningKey = new SymmetricSecurityKey(
        //                Encoding.ASCII.GetBytes("thisisthesecretforgeneratingakey(mustbeatleast32bitlong)"))
        //        };
        //    });
        services.AddCustomJwtAuthentication(new List<string> { "Warehouse.MinimalApi" }, "https://localhost:5010");

        services.AddAuthorization();

        return services;
        }
    }
