using WebhooksCore.Options;
using WebhooksCore.Strategies;

namespace WebhooksCore;

public static class WebhookEventBroadcasterOptionsExtensions
{
    public static WebhookEventBroadcasterOptions UseStrategy<T>(this WebhookEventBroadcasterOptions options) where T : IBroadcasterStrategy
    {
        options.BroadcasterStrategy = typeof(T);
        return options;
    }
    
    public static WebhookEventBroadcasterOptions UseStrategy(this WebhookEventBroadcasterOptions options, Type strategyType)
    {
        if(!strategyType.IsAssignableTo(typeof(IBroadcasterStrategy)))
            throw new ArgumentException($"{nameof(strategyType)} must be assignable from {nameof(IBroadcasterStrategy)}");
        
        options.BroadcasterStrategy = strategyType;
        return options;
    }

    public static WebhookEventBroadcasterOptions UseSequentialBroadcasterStrategy(this WebhookEventBroadcasterOptions options) => options.UseStrategy<SequentialBroadcasterStrategy>();
    public static WebhookEventBroadcasterOptions UseParallelTaskBroadcasterStrategy(this WebhookEventBroadcasterOptions options) => options.UseStrategy<ParallelTaskBroadcasterStrategy>();
    public static WebhookEventBroadcasterOptions UseBackgroundProcessorBroadcasterStrategy(this WebhookEventBroadcasterOptions options) => options.UseStrategy<BackgroundProcessorBroadcasterStrategy>();
}