using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task3
{
    internal class Server
    {
        private static bool ServerWork = true;
        public static void AcceptMsg()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);   
            UdpClient udpClient = new UdpClient(16874);
            Console.WriteLine("Сервер ожиждает сообщение");

            Thread WordThread = new Thread(() =>
            {
                Console.WriteLine("Нажмите любую клавишу для завершения работы сервера...");
                Console.ReadKey();
                ServerWork = false;
                udpClient.Close();
            });
            WordThread.Start();
            
            while (ServerWork)
            {
                try 
                {
                    byte[] buffer = udpClient.Receive(ref ep);
                    string data = Encoding.UTF8.GetString(buffer);
                    Thread tf = new Thread(() =>
                    {
                        Message msg = Message.FromJson(data);
                        Console.WriteLine(msg.ToString());
                        Message responseMsg = new Message("Server", "Message accept on serv!");
                        string responseMsgJs = responseMsg.ToJson();
                        byte[] responseDate = Encoding.UTF8.GetBytes(responseMsgJs);
                        udpClient.Send(responseDate, ep);
                    });
                    tf.Start();
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Сообщение об ошибке: " + ex.Message);
                }
               

            }
        }
    }
}
