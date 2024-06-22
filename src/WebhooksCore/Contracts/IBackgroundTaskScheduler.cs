namespace WebhooksCore;

public interface IBackgroundTaskScheduler
{
    Task EnqueueWork(Func<Task> work, CancellationToken cancellationToken = default);
}