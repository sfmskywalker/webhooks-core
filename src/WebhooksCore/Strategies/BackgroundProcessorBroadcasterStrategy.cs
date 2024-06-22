using WebhooksCore.Services;

namespace WebhooksCore.Strategies;

public class BackgroundProcessorBroadcasterStrategy(IBackgroundTaskScheduler scheduler) : IBroadcasterStrategy
{
    public async Task BroadcastAsync(IEnumerable<WebhookEndpoint> webhookEndpoints, Func<WebhookEndpoint, Task> invocation, CancellationToken cancellationToken = default)
    {
        foreach (var webhookEndpoint in webhookEndpoints)
            await scheduler.EnqueueWork(async () => await invocation(webhookEndpoint), cancellationToken);
    }
}