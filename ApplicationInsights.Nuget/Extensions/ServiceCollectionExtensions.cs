using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationInsights.Nuget.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationInsights(this IServiceCollection services, string cloudRoleName = null)
        {
            services.AddSingleton<ITelemetryInitializer>(new CustomTelemetryInitializer(cloudRoleName));
            services.AddApplicationInsightsTelemetry();
            services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) => { module.EnableSqlCommandTextInstrumentation = true; });

            return services;
        }
        public static IServiceCollection AddApplicationInsights(this IServiceCollection services, IConfiguration configuration)
        {
            return AddApplicationInsights(services, configuration.GetValue<string>(Constants.RoleNameParameter));
        }
    }
};
