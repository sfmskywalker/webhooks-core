namespace WebhooksCore;

/// Broadcasts webhook events to all registered webhook endpoints.
public interface IWebhookEventBroadcaster
{
    /// Broadcasts a webhook event to all registered webhook endpoints.
    /// <param name="webhookEvent">The webhook event to broadcast.</param>
    /// <param name="cancellationToken"></param>
    Task BroadcastAsync(WebhookEvent webhookEvent, CancellationToken cancellationToken = default);
}