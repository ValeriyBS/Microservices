using Identity.MinimalApi.Interfaces;
using Identity.MinimalApi.Services;
using JwtAuthenticationManager;

namespace Identity.MinimalApi;

public static class ServiceConfig
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<JwtTokenHandler>();
        services.AddTransient<IExchangeToken, ExchangeToken>();

        return services;
    }
}