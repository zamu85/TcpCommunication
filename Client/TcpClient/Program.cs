// See https://aka.ms/new-console-template for more information
using FakeTcpClient;

Console.WriteLine("Hello, World!");

CommandDispatcher commandDispatcher = new(65001);
commandDispatcher.SendCommand();
