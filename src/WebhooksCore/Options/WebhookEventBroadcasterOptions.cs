using Microsoft.Extensions.Options;
using WebhooksCore.Strategies;

namespace WebhooksCore.Options;

public class WebhookEventBroadcasterOptions
{
    public Type BroadcasterStrategy { get; set; } = typeof(SequentialBroadcasterStrategy);
}

public class ConfigureWebhookEventBroadcasterOptions : IValidateOptions<WebhookEventBroadcasterOptions>
{
    public ValidateOptionsResult Validate(string? name, WebhookEventBroadcasterOptions options)
    {
        if(!options.BroadcasterStrategy.IsAssignableTo(typeof(IBroadcasterStrategy)))
            return ValidateOptionsResult.Fail($"BroadcasterStrategy type is not assignable to IBroadcasterStrategy");
        
        return ValidateOptionsResult.Success;
    }
}