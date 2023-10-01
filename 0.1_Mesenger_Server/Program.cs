﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _0._1_Mesenger_Server
{
    internal class Program
    {
        //static int port = 8080;
        //static void Main(string[] args)
        //{
        //    IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
        //    IPEndPoint ipPoint = new IPEndPoint(iPAddress, port);

        //    EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        //    Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //    try
        //    {
        //        // связываем сокет с локальной точкой, по которой будем принимать данные
        //        listenSocket.Bind(ipPoint);
        //        Console.WriteLine("Server started! Waiting for connection...");

        //        while (true)
        //        {
        //            // получаем сообщение
        //            int bytes = 0;
        //            byte[] data = new byte[1024];
        //            bytes = listenSocket.ReceiveFrom(data, ref remoteEndPoint);

        //            string msg = Encoding.Unicode.GetString(data, 0, bytes);
        //            Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {msg} from {remoteEndPoint}");

        //            // отправляем ответ
        //            //string message = "Message was send!";
        //            //data = Encoding.Unicode.GetBytes(message);
        //            //listenSocket.SendTo(data, remoteEndPoint);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        static string address = "127.0.0.1";
        static int port = 8080;
        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            IPEndPoint remoteEndPoint = null;

            UdpClient listener = new UdpClient(ipPoint);

            try
            {
                Console.WriteLine("Server started! Waiting for connection...");

                while (true)
                {
                    // get message
                    byte[] data = listener.Receive(ref remoteEndPoint);

                    string msg = Encoding.Unicode.GetString(data);
                    Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {msg} from {remoteEndPoint}");

                    msg.ToLower();
                    string message = "";
                    Random rand = new Random();
                    int randNum = 0;
                    List<string> Weathers = new List<string>() { "Cloudy", "Sunny", "Fine", "Rain", "Snow", "Money rain", "Shtorm" };
                    List<string> Greetings = new List<string>() { "Hi!", "Hello!", "Priveeet!", "Hello!, nice to see you!", "Hello, whats new?", "Hi, how is the weather today?", "Hello how are you?", "Whats up" };
                    List<string> Farewells = new List<string>() { "Bye!", "Goodbye!", "Okay, bye(", "Finally!!!", "Omg", "YEEEES", "Noooooo(" };
                    List<string> Default = new List<string>() { "Wow", "Realy?", "Cool", "Hmm", "Great!", "Its bored", "No", "Heeey", "I don't like it", "Sorry", "Ohh", "No(", "Bad...", "Hhehhehehehehehehe", "LOL",
                    "What????", "I love my work", "OMG", "I don't belive you", "Please, end this chat..", "GOODBYE!", "hahahahahhahaahha", "Ask me about the weather pls", "Okay..."};
                    if (msg.Contains("weather"))
                    {
                        randNum = rand.Next(0, Weathers.Count);
                        message = $"{DateTime.Now.ToShortTimeString()} : the weather is {Weathers[randNum]}";
                    }
                    else if (msg == "hi" || msg == "hello" || msg == "hey" || msg == "privet")
                    {
                        randNum = rand.Next(0, Greetings.Count);
                        message = $"{Greetings[randNum]}";
                    }
                    else if (msg == "count")
                    {
                        int from = rand.Next(-100, 100);
                        int to = rand.Next(-100, 100);
                        if (from > to)
                        {
                            int tmp;
                            tmp = from;
                            from = to;
                            to = tmp;
                        }
                        for (int i = from; i <= to; i++)
                        {
                            message += $"{i} ";
                        }
                    }
                    else if (msg.Contains("bye") || msg.Contains("goodbye"))
                    {
                        randNum = rand.Next(0, Farewells.Count);
                        message = $"{Farewells[randNum]}";
                    }
                    else
                    {
                        randNum = rand.Next(0, Default.Count);
                        message = $"{Default[randNum]}";
                    }

                    // send answer
                    data = Encoding.Unicode.GetBytes(message);
                    listener.Send(data, data.Length, remoteEndPoint);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
    