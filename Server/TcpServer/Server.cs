using Queue;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TcpServer.Types;

namespace TcpServer
{
    public class Server : IDisposable
    {
        private bool _isRunning;
        private Producer<ClientRequest> _producer;
        private TcpListener _server;

        public Server(int port, Producer<ClientRequest> producer)
        {
            _producer = producer;
            _server = new TcpListener(IPAddress.Any, port);
            _server.Start();

            _isRunning = true;

            LoopClients();
        }

        public void Dispose()
        {
            _isRunning = false;
        }

        public async void HandleClient(object obj)

        {
            TcpClient client = (TcpClient)obj;

            // sets two streams
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.ASCII);

            //while (bClientConnected)
            //{
            // reads from stream
            string? sData = sReader.ReadLine();

            // shows content on the console.
            Console.WriteLine("<<<<< " + sData);

            ClientRequest cr = new ClientRequest(client, sData);
            await _producer.EnqueueMessageAsync(cr);

            //}
        }

        public async void LoopClients()
        {
            while (_isRunning)
            {
                TcpClient handler = await _server.AcceptTcpClientAsync();

                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(handler);
            }
            _server.Stop();
        }
    }
}
