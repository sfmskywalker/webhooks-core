using System.Threading.Channels;

namespace WebhooksCore;

public interface IBackgroundTaskChannel
{
    ChannelWriter<Func<Task>> Writer { get; }
    ChannelReader<Func<Task>> Reader { get; }
}