using Items.MinimalApi.Client.Extensions;

namespace Warehouse.MinimalApi;
public static class ServiceConfig
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddItemsApiClient(configuration);
            return services;
        }
    }
