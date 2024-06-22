using System.Threading.Channels;
using Microsoft.Extensions.Options;
using WebhooksCore.Options;

namespace WebhooksCore.Services;

public class BackgroundTaskChannel(IOptions<BackgroundTaskProcessorOptions> options) : IBackgroundTaskChannel
{
    public Channel<Func<Task>> Channel { get; } = System.Threading.Channels.Channel.CreateBounded<Func<Task>>(new BoundedChannelOptions(options.Value.ChannelCapacity)
    {
        FullMode = BoundedChannelFullMode.Wait
    });

    public ChannelWriter<Func<Task>> Writer => Channel.Writer;
    public ChannelReader<Func<Task>> Reader => Channel.Reader;
}