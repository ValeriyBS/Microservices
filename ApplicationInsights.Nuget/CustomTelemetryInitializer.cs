using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace ApplicationInsights.Nuget
{
    internal class CustomTelemetryInitializer : ITelemetryInitializer
    {
        private readonly string _cloudRoleName;

        public CustomTelemetryInitializer(string cloudRoleName)
        {
            _cloudRoleName = cloudRoleName;
        }
        public void Initialize(ITelemetry telemetry)
        {
            if (telemetry is DependencyTelemetry rt && rt.Name == Constants.ServiceBusReceiver)
            {
                ((ISupportSampling)telemetry).SamplingPercentage = 5;
            }

            if (string.IsNullOrEmpty(telemetry.Context.Cloud.RoleName))
            {
                telemetry.Context.Cloud.RoleName = _cloudRoleName;
            }
        }
    }
}
