using System.Threading.Channels;

namespace Queue
{
    public class Consumer<T> : IDisposable where T : IQueueConsumer
    {
        private readonly ChannelReader<T> _channelReader;

        private CancellationTokenSource _source;
        private CancellationToken _token;

        public Consumer(ChannelReader<T> channelReader)
        {
            _channelReader = channelReader;
            _source = new CancellationTokenSource();
            _token = _source.Token;
        }

        public async Task ConsumeWorkAsync()
        {
            try
            {
                while (!_token.IsCancellationRequested)
                {
                    try
                    {
                        var todo = await _channelReader.ReadAsync(_token);
                        Console.WriteLine($"\t\t[Dequeue] >>>>> {DateTime.Now} - {todo}");
                        todo.Consume();
                        Thread.Sleep(100);
                    }
                    catch (OperationCanceledException)
                    {
                        await Console.Out.WriteLineAsync("Closing...");
                    }
                }

                await Console.Out.WriteLineAsync("Close consumer");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception {e}");
            }
        }

        public void Dispose()
        {
            _source.Cancel();
        }
    }
}
