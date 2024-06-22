using Microsoft.Extensions.Options;

namespace WebhooksCore.Options;

/// <summary>
/// An options class that can be used to bind webhook endpoints from configuration.
/// </summary>
public class WebhookEndpointsOptions
{
    public ICollection<WebhookEndpoint> Endpoints { get; set; } = new List<WebhookEndpoint>();
}

public class ConfigureWebhookEndpointOptions(Action<WebhookEndpointsOptions>? action) : ConfigureOptions<WebhookEndpointsOptions>(action);