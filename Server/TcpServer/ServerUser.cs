using Queue;
using System.Threading.Channels;
using TcpServer.Types;

namespace TcpServer
{
    public class ServerUser
    {
        private readonly Consumer<ClientRequest> _consumer;
        private readonly Producer<ClientRequest> _producer;
        private readonly Server _server;
        private readonly Channel<ClientRequest> _unboundedChannel;

        public ServerUser()
        {
            _unboundedChannel = Channel.CreateUnbounded<ClientRequest>();
            _consumer = new Consumer<ClientRequest>(_unboundedChannel.Reader);
            _producer = new Producer<ClientRequest>(_unboundedChannel.Writer);
            _server = new Server(65001, _producer);
        }

        public void Close()
        {
            _consumer.Dispose();
            _producer.Dispose();
            _server.Dispose();
        }

        public async Task StartConsume()
        {
            await _consumer.ConsumeWorkAsync();
        }
    }
}
