namespace WebhooksCore;

/// Represents a webhook events sink interested in a given set of events. 
public class WebhookSink
{
    /// A unique identifier for this sink.
    public string Id { get; set; } = default!;

    /// A friendly name for this endpoint.
    public string? Name { get; set; }

    /// The URL to send the webhook event to.
    public Uri Url { get; set; } = default!;

    /// A whitelist of event types to deliver.
    public HashSet<string> EventTypes { get; set; } = new();
}