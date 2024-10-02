using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebhooksCore.HostedServices;
using WebhooksCore.Options;
using WebhooksCore.Serialization.Converters;
using WebhooksCore.Services;
using WebhooksCore.SinkProviders;
using WebhooksCore.SourceProviders;

namespace WebhooksCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebhooksCore(this IServiceCollection services, Action<IHttpClientBuilder>? configureHttpClient = null)
    {
        TypeDescriptor.AddAttributes(typeof(Type), new TypeConverterAttribute(typeof(TypeTypeConverter)));
        
        services.AddOptions<WebhookSinksOptions>();
        services.AddOptions<WebhookSourcesOptions>();
        services.AddOptions<BackgroundTaskProcessorOptions>();
        services.AddOptions<WebhookBroadcasterOptions>();

        var httpClientBuilder = services.AddHttpClient<HttpWebhookEndpointInvoker>();
        configureHttpClient?.Invoke(httpClientBuilder);
        
        return services.AddSingleton<IWebhookEventBroadcaster, DefaultWebhookEventBroadcaster>()
            .AddSingleton<IWebhookSinkProvider, OptionsWebhookSinkProvider>()
            .AddSingleton<IWebhookSourceProvider, OptionsWebhookSourceProvider>()
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
        var options = serviceProvider.GetRequiredService<IOptions<WebhookBroadcasterOptions>>();
        var type = options.Value.BroadcasterStrategy;
        var broadcasterStrategy = (IBroadcasterStrategy)ActivatorUtilities.CreateInstance(serviceProvider, type);
        return broadcasterStrategy;
    }
}