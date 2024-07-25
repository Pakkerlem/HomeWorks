﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    internal class Client
    {
        public static void SendMsg(string name)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 16874);
            UdpClient udpClient = new UdpClient();

            while (true)
            {
                Console.WriteLine("Введите Exit для завершения");
                string userText = Console.ReadLine();
                
                if (userText.Equals("Exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                Message msg = new Message(name, "Привет");
                string responseMsgJs = msg.ToJson();
                byte[] responseDate = Encoding.UTF8.GetBytes(responseMsgJs);
                udpClient.Send(responseDate, ep);

                byte[] answerData = udpClient.Receive(ref ep);
                string answerMsgJs = Encoding.UTF8.GetString(answerData);
                Message answerMsg = Message.FromJson(answerMsgJs);
                Console.WriteLine(answerMsg.ToString());
            }

            udpClient.Close();
        }
    }
}
