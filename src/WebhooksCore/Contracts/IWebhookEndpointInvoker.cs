namespace WebhooksCore;

public interface IWebhookEndpointInvoker
{
    Task InvokeAsync(WebhookEndpoint webhookEndpoint, WebhookEvent webhookEvent, CancellationToken cancellationToken = default);
}