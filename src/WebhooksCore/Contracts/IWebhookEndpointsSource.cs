namespace WebhooksCore;

/// Provides a list of all registered webhook endpoints that are interested in a given event.
public interface IWebhookEndpointsSource
{
    ValueTask<IEnumerable<WebhookEndpoint>> ListWebhooksForEventAsync(string eventType, CancellationToken cancellationToken = default);
}