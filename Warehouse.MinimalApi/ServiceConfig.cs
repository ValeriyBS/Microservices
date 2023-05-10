using Items.MinimalApi.Client.Extensions;
using JwtAuthenticationManager;

namespace Warehouse.MinimalApi;
public static class ServiceConfig
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddItemsApiClient(configuration);

        services.AddCustomJwtAuthentication(configuration);

        services.AddAuthorization();

        return services;
        }
    }
