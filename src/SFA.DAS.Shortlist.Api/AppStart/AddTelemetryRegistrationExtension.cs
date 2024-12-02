using Azure.Monitor.OpenTelemetry.AspNetCore;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.Shortlist.Api.AppStart;

[ExcludeFromCodeCoverage]
public static class AddTelemetryRegistrationExtension
{
    private static readonly string AppInsightsConnectionString = "APPLICATIONINSIGHTS_CONNECTION_STRING";

    public static IServiceCollection AddTelemetryRegistration(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddOpenTelemetry().UseAzureMonitor(options =>
        {
            options.ConnectionString = AppInsightsConnectionString;
        });

        return services;
    }
}
