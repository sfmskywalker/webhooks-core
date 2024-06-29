using Microsoft.Extensions.Logging;

namespace WebhooksCore.Services;

/// <summary>
/// A webhook event broadcaster that sends HTTP requests one by one.
/// </summary>
public class DefaultWebhookEventBroadcaster(
    IWebhookSinkProvider webhookSinkProvider, 
    IWebhookEndpointInvoker webhookEndpointInvoker,
    IBroadcasterStrategy strategy,
    ILogger<DefaultWebhookEventBroadcaster> logger) : IWebhookEventBroadcaster
{
    public async Task BroadcastAsync(NewWebhookEvent webhookEvent, CancellationToken cancellationToken = default)
    {
        var endpoints = (await webhookSinkProvider.ListWebhooksForEventAsync(webhookEvent.EventType, cancellationToken)).ToList();

        await strategy.BroadcastAsync(endpoints, InvokeEndpointAsync, cancellationToken);
        return;
        
        async Task InvokeEndpointAsync(WebhookSink endpoint) => await SendWebhookEventAsync(webhookEvent, endpoint, cancellationToken);
    }

    private async Task SendWebhookEventAsync(NewWebhookEvent webhookEvent, WebhookSink sink, CancellationToken cancellationToken)
    {
        try
        {
            await webhookEndpointInvoker.InvokeAsync(sink, webhookEvent, cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while sending webhook event {EventType} to {Url}.", webhookEvent.EventType, sink.Url);
        }
    }
}