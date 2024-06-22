using Microsoft.Extensions.Logging;

namespace WebhooksCore.Services;

/// <summary>
/// A webhook event broadcaster that sends HTTP requests one by one.
/// </summary>
public class DefaultWebhookEventBroadcaster(
    IWebhookEndpointsSource webhookEndpointsSource, 
    IWebhookEndpointInvoker webhookEndpointInvoker,
    IBroadcasterStrategy strategy,
    ILogger<DefaultWebhookEventBroadcaster> logger) : IWebhookEventBroadcaster
{
    public async Task BroadcastAsync(WebhookEvent webhookEvent, CancellationToken cancellationToken = default)
    {
        var endpoints = (await webhookEndpointsSource.ListWebhooksForEventAsync(webhookEvent.EventType, cancellationToken)).ToList();

        await strategy.BroadcastAsync(endpoints, InvokeEndpointAsync, cancellationToken);
        return;
        
        async Task InvokeEndpointAsync(WebhookEndpoint endpoint) => await SendWebhookEventAsync(webhookEvent, endpoint, cancellationToken);
    }

    private async Task SendWebhookEventAsync(WebhookEvent webhookEvent, WebhookEndpoint endpoint, CancellationToken cancellationToken)
    {
        try
        {
            await webhookEndpointInvoker.InvokeAsync(endpoint, webhookEvent, cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while sending webhook event {EventType} to {Url}.", webhookEvent.EventType, endpoint.Url);
        }
    }
}