namespace WebhooksCore;

/// Provides a list of all registered webhook sinks that are interested in a given event.
public interface IWebhookSinkProvider
{
    ValueTask<IEnumerable<WebhookSink>> ListWebhooksForEventAsync(string eventType, CancellationToken cancellationToken = default);
}