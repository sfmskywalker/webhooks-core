using Microsoft.Extensions.Options;
using WebhooksCore.Options;

namespace WebhooksCore.Sources;

/// A webhook endpoints source that provides webhook endpoints from configuration via <see ref="WebhookEndpointOptions"/>. 
public class ConfigurationWebhookEndpointsSource(IOptionsMonitor<WebhookEndpointsOptions> optionsMonitor) : IWebhookEndpointsSource
{
    public ValueTask<IEnumerable<WebhookEndpoint>> ListWebhooksForEventAsync(string eventType, CancellationToken cancellationToken = default)
    {
        var endpoints = optionsMonitor.CurrentValue.Endpoints;
        var matchingEndpoints = endpoints.Where(x => x.EventTypes.Contains(eventType) || x.EventTypes.Contains("*")).ToList();
        return new(matchingEndpoints);
    }
}