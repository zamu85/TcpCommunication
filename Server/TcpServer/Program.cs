// See https://aka.ms/new-console-template for more information

namespace TcpServer
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            ServerUser s = new();

            Console.WriteLine("Server is now listening on port 65001");

            Console.CancelKeyPress += delegate (object? sender, ConsoleCancelEventArgs e)
            {
                e.Cancel = true;
                s.Close();
            };

            await s.StartConsume();

            Console.WriteLine("Close gracefully");
        }
    }
}
