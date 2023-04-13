using Items.MinimalApi.Client.Interfaces;
using Items.MinimalApi.Client.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Items.MinimalApi.Client.Extensions
{
    public static class ServiceRegistrationExtension
    {
        public static IServiceCollection AddItemsApiClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IItemsClient, ItemsClient>();

            return services;
        }
    }
}
