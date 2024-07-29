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
            Console.WriteLine("Сервер ожидает сообщение");

            CancellationTokenSource cls = new CancellationTokenSource();
            CancellationToken token = cls.Token;

            Task WordThread = Task.Run(() =>
            {
                Console.WriteLine("Нажмите любую клавишу для завершения работы сервера...");
                Console.ReadKey();
                ServerWork = false;
                cls.Cancel();
                udpClient.Close();
            });

            while (ServerWork && !token.IsCancellationRequested)
            {
                try
                {
                    UdpReceiveResult result = await udpClient.ReceiveAsync();
                    var data = result.Buffer;
                    ep = result.RemoteEndPoint;

                    string data1 = Encoding.UTF8.GetString(data);

                    Message msl = Message.FromJson(data1);
                    string input = await Task.Run(() => msl.ShortMes());

                    if (input.Equals("Exit", StringComparison.OrdinalIgnoreCase))
                    {
                        cls.Cancel();
                        break;
                    }

                    await Task.Run(async () =>
                    {
                        Message msg = Message.FromJson(data1);
                        Console.WriteLine(msg.ToString());
                        Message responseMsg = new Message("Server", "Message accepted on server!");
                        string responseMsgJs = responseMsg.ToJson();
                        byte[] responseData = Encoding.UTF8.GetBytes(responseMsgJs);
                        await udpClient.SendAsync(responseData, responseData.Length, ep);
                    });

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Сообщение об ошибке: " + ex.Message);
                }
            }

            await WordThread;
        }
    }
}

