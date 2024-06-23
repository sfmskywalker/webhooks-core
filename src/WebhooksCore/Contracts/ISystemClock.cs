namespace WebhooksCore;

public interface ISystemClock
{
    DateTimeOffset UtcNow { get; }
}