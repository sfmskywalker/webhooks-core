using WebhookEvents;
using WebhooksCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/webhooks", (WebhookEvent<Heartbeat> webhookEvent) =>
{
    var heartbeat = webhookEvent.Payload!;
    Console.WriteLine($"Heartbeat event received at: {heartbeat.Timestamp}");
});

app.Run();