using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    internal class Server
    {
        private static bool ServerWork = true;
        public static async Task AcceptMsg()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            UdpClient udpClient = new UdpClient(16874);
            Console.WriteLine("Сервер ожиждает сообщение");

            Task WordThread = Task.Run(() =>
            {
                Console.WriteLine("Нажмите любую клавишу для завершения работы сервера...");
                Console.ReadKey();
                ServerWork = false;
                udpClient.Close();
            });
            

            while (ServerWork)
            {
                try
                {
                    var data = udpClient.Receive(ref ep);
                    //byte[] buffer = data.Buffer;
                    string data1 = Encoding.UTF8.GetString(data);
                    
                    await Task.Run(async() =>
                    {
                        Message msg = Message.FromJson(data1);
                        Console.WriteLine(msg.ToString());
                        Message responseMsg = new Message("Server", "Message accept on serv!");
                        string responseMsgJs = responseMsg.ToJson();
                        byte[] responseDate = Encoding.UTF8.GetBytes(responseMsgJs);
                        await udpClient.SendAsync(responseDate, ep);
                    });
                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Сообщение об ошибке: " + ex.Message);
                }


            }
            WordThread.Wait();

        }
    }
}
