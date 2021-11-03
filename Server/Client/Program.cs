using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            byte[] data = new byte[1024];

            string input, stringData;

            IPEndPoint IpEnd = new IPEndPoint(IPAddress.Loopback, 9000);

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(IpEnd);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Unable to connect to server.");

                Console.WriteLine(e.ToString());

                return;
            }

            int recive = server.Receive(data);

            stringData = Encoding.UTF8.GetString(data, 0, recive);

            Console.WriteLine(stringData);

            while (true)
            {
                input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
                Console.WriteLine("You: " + input);
                server.Send(Encoding.UTF8.GetBytes(input));

                data = new byte[1024];

                recive = server.Receive(data);

                stringData = Encoding.UTF8.GetString(data, 0, recive);

                byte[] utf8string = System.Text.Encoding.UTF8.GetBytes(stringData);

                Console.WriteLine("Server: " + stringData);
            }
            Console.WriteLine("Disconnecting from server...");

            server.Shutdown(SocketShutdown.Both);

            server.Close();

            Console.WriteLine("Disconnected!");

            Console.ReadLine();
        }

    }
}
