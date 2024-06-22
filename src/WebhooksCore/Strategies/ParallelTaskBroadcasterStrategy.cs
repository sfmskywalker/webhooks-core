namespace WebhooksCore.Strategies;

public class ParallelTaskBroadcasterStrategy : IBroadcasterStrategy
{
    public async Task BroadcastAsync(IEnumerable<WebhookEndpoint> webhookEndpoints, Func<WebhookEndpoint, Task> invocation, CancellationToken cancellationToken = default)
    {
        await Task.WhenAll(webhookEndpoints.Select(invocation));
    }
}