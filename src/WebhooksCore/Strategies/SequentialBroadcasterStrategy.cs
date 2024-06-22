namespace WebhooksCore.Strategies;

public class SequentialBroadcasterStrategy : IBroadcasterStrategy
{
    public async Task BroadcastAsync(IEnumerable<WebhookEndpoint> webhookEndpoints, Func<WebhookEndpoint, Task> invocation, CancellationToken cancellationToken = default)
    {
        foreach (var webhookEndpoint in webhookEndpoints) await invocation(webhookEndpoint);
    }
}