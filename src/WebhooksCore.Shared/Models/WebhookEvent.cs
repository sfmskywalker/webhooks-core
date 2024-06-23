// ReSharper disable once CheckNamespace
namespace WebhooksCore;

/// A webhook event to send to a webhook endpoint.
public record WebhookEvent(string EventType, object? Payload, DateTimeOffset Timestamp);

/// A webhook event to send to a webhook endpoint.
public record WebhookEvent<T>(string EventType, T? Payload, DateTimeOffset Timestamp);