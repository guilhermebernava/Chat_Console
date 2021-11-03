using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int recive;

            byte[] data = new byte[1024];

            IPEndPoint IPend = new IPEndPoint(IPAddress.Loopback, 9000);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(IPend);
            socket.Listen(1);

            Console.WriteLine("Waiting for a client....");

            Socket client = socket.Accept();//cria um novo socket com base numa conexao

            IPEndPoint clientEp = (IPEndPoint)client.RemoteEndPoint;

            Console.WriteLine($"Connected with {clientEp.Address} at port {clientEp.Port}");

            string welcome = " <3 Welcome to server chat <3";

            data = Encoding.UTF8.GetBytes(welcome);//codifica o dado em bytes para mandar pro client

            client.Send(data, data.Length, SocketFlags.None);

            string input;

            while (true)
            {
                data = new byte[1024];

                recive = client.Receive(data);

                if(recive == 0)
                {
                    break;
                }

                Console.WriteLine("Client: " + Encoding.UTF8.GetString(data,0,recive));
                input = Console.ReadLine();
                Console.WriteLine("You: " + input);

                client.Send(Encoding.UTF8.GetBytes(input));

            }
            Console.WriteLine("Disconnected from {0}", clientEp.Address);

            client.Close();

            socket.Close();

            Console.ReadLine();


        }
    }
}
