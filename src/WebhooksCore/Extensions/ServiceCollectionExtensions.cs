using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebhooksCore.HostedServices;
using WebhooksCore.Options;
using WebhooksCore.Services;
using WebhooksCore.Sources;

namespace WebhooksCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebhooksCore(this IServiceCollection services)
    {
        services.AddOptions<WebhookEndpointsOptions>();
        services.AddOptions<BackgroundTaskProcessorOptions>();
        services.AddOptions<WebhookEventBroadcasterOptions>();

        return services
            .AddHttpClient()
            .AddSingleton<IWebhookEventBroadcaster, DefaultWebhookEventBroadcaster>()
            .AddSingleton<IWebhookEndpointsSource, ConfigurationWebhookEndpointsSource>()
            .AddSingleton<IWebhookEndpointInvoker, HttpWebhookEndpointInvoker>()
            .AddSingleton<IBackgroundTaskProcessor, ChannelBackgroundTaskProcessor>()
            .AddSingleton<IBackgroundTaskScheduler, ChannelBackgroundTaskScheduler>()
            .AddSingleton<IBackgroundTaskChannel, BackgroundTaskChannel>()
            .AddSingleton<ISystemClock, DefaultSystemClock>()
            .AddSingleton(CreateBroadcasterStrategy);
    }

    public static IServiceCollection AddWebhooksBackgroundProcessor(this IServiceCollection services)
    {
        return services.AddHostedService<StartBackgroundProcessor>();
    }

    private static IBroadcasterStrategy CreateBroadcasterStrategy(IServiceProvider serviceProvider)
    {
        var options = serviceProvider.GetRequiredService<IOptions<WebhookEventBroadcasterOptions>>();
        var type = options.Value.BroadcasterStrategy;
        var broadcasterStrategy = (IBroadcasterStrategy)ActivatorUtilities.CreateInstance(serviceProvider, type);
        return broadcasterStrategy;
    }
}