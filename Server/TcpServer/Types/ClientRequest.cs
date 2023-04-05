using Queue;
using System.Net.Sockets;
using System.Text;

namespace TcpServer.Types
{
    public class ClientRequest : IQueueConsumer
    {
        private TcpClient _client;

        private string _incomingMessage;

        public ClientRequest(TcpClient client, string incomingMessage)
        {
            _client = client;
            _incomingMessage = incomingMessage;
        }

        public void CloseConnection()
        {
            _client.Dispose();
        }

        public async void Consume()
        {
            var random = new Random();
            int rand = random.Next(2);

            await Task.Delay(1500);
            string header = rand == 0 ? "ACK" : "NACK";
            string responseMessage = $"{header}|{_incomingMessage}|EOF";

            SendResponse(responseMessage);
            CloseConnection();
        }

        public void Dispose()
        {
        }

        public void SendResponse(string message)
        {
            StreamWriter sWriter = null!;

            try
            {
                sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);

                // to write something back.
                sWriter.WriteLine(message);
                sWriter?.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception {e}");
            }
        }

        public override string ToString()
        {
            return _incomingMessage;
        }
    }
}
