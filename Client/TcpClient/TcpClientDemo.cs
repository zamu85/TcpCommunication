using System.Net.Sockets;
using System.Text;

namespace FakeTcpClient
{
    public class TcpClientFake
    {
        private TcpClient _client;

        public TcpClientFake(String ipAddress, int portNum)
        {
            _client = new TcpClient();
            _client.Connect(ipAddress, portNum);
        }

        public void CloseConnection()
        {
            _client.Close();
            _client.Dispose();
        }

        public void SendCommandToServer(string sData)
        {
            var _sReader = new StreamReader(_client.GetStream(), Encoding.ASCII);
            var _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);

            // write data and make sure to flush, or the buffer will continue to grow, and your data
            // might not be sent when you want it, and will only be sent once the buffer is filled.
            Console.WriteLine($">>>>> {sData}");
            _sWriter.WriteLine(sData);
            _sWriter.Flush();

            // if you want to receive anything
            String sDataIncoming = _sReader.ReadLine();
            Console.WriteLine($"\t\t<<<<< {DateTime.Now} - {sDataIncoming}");
            _sWriter.Close();
            _sReader.Close();
        }
    }
}
