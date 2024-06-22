namespace WebhooksCore;

public interface IBackgroundTaskProcessor
{
    ValueTask StartAsync();
    ValueTask StopAsync();
}