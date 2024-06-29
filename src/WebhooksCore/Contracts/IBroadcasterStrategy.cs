namespace WebhooksCore;

public interface IBroadcasterStrategy
{
    Task BroadcastAsync(IEnumerable<WebhookSink> webhookEndpoints, Func<WebhookSink, Task> invocation, CancellationToken cancellationToken = default);
}