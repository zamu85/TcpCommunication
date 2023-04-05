using System.Threading.Channels;

namespace Queue
{
    public class Producer<T> : IDisposable where T : class
    {
        private readonly ChannelWriter<T> _channelWriter;

        public Producer(ChannelWriter<T> channelWriter)
        {
            _channelWriter = channelWriter;
        }

        public void Dispose()
        {
            _channelWriter.Complete();
        }

        public async Task EnqueueMessageAsync(T message)
        {
            if (!_channelWriter.TryWrite(message))
            {
                await Console.Out.WriteLineAsync("Cannot write item to queue");
            }
        }
    }
}
