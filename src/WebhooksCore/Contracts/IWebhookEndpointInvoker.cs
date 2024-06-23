namespace WebhooksCore;

public interface IWebhookEndpointInvoker
{
    Task InvokeAsync(WebhookEndpoint webhookEndpoint, NewWebhookEvent newWebhookEvent, CancellationToken cancellationToken = default);
}