namespace FakeTcpClient
{
    public class CommandDispatcher
    {
        private readonly int _port;

        public CommandDispatcher(int port)
        {
            _port = port;
        }

        public void SendCommand()
        {
            var random = new Random();

            while (true)
            {
                Task.Factory.StartNew(() =>
                {
                    var tcpClient = new TcpClientFake("127.0.0.1", _port);

                    string cmd = $"SOF|{Faker.Identification.UkPassportNumber()}|{Faker.Name.FullName()}|" +
                        $"{Faker.Identification.DateOfBirth()}|EOF";

                    tcpClient.SendCommandToServer(cmd);
                    Thread.Sleep(250);
                    tcpClient.CloseConnection();
                });
            }
        }
    }
}
