using Microsoft.Extensions.Options;
using WebhooksCore.Options;

namespace WebhooksCore.SinkProviders;

/// A webhook endpoints source that provides webhook endpoints from configuration via <see ref="WebhookSinksOptions"/>. 
public class OptionsWebhookSinkProvider(IOptionsMonitor<WebhookSinksOptions> optionsMonitor) : IWebhookSinkProvider
{
    public ValueTask<IEnumerable<WebhookSink>> ListWebhooksForEventAsync(string eventType, CancellationToken cancellationToken = default)
    {
        var sinks = optionsMonitor.CurrentValue.Sinks;
        var matchingEndpoints = sinks.Where(x => x.EventTypes.Contains(eventType) || x.EventTypes.Contains("*")).ToList();
        return new(matchingEndpoints);
    }
}