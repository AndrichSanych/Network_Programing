using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Network_Programing
{
    internal class Program
    {
        static int port = 8080;
        static string address = "127.0.0.1";
        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            EndPoint remoteIpPoint = new IPEndPoint(IPAddress.Any, 0);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            Console.WriteLine("Endter the message: ");
            string message = "";
            message = Console.ReadLine();
            byte[] data = Encoding.Unicode.GetBytes(message);

            socket.SendTo(data, ipPoint);
        }
    }
}