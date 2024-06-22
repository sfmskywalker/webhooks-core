namespace WebhooksCore;

public interface IBroadcasterStrategy
{
    Task BroadcastAsync(IEnumerable<WebhookEndpoint> webhookEndpoints, Func<WebhookEndpoint, Task> invocation, CancellationToken cancellationToken = default);
}