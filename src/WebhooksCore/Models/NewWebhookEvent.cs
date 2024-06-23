namespace WebhooksCore;

public record NewWebhookEvent(string EventType, object? Payload = null);