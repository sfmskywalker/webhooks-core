using WebhookEvents.Generator.Web.HostedServices;
using WebhooksCore.Extensions;
using WebhooksCore.Options;
using WebhooksCore.Strategies;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services
    .AddWebhooksCore()
    .AddWebhooksBackgroundProcessor()
    .AddSingleton(TimeProvider.System)
    .AddHostedService<EventGenerator>()
    ;

services.Configure<WebhookEndpointsOptions>(options => configuration.GetSection("Webhooks").Bind(options));
services.Configure<WebhookEventBroadcasterOptions>(options => options.BroadcasterStrategy = typeof(BackgroundProcessorBroadcasterStrategy));
var app = builder.Build();

app.UseHttpsRedirection();
app.Run();