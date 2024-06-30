namespace WebhooksCore;

public class WebhookSourceEventType
{
    public string EventType { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string? Description { get; set; }
    public Type? PayloadType { get; set; }
}