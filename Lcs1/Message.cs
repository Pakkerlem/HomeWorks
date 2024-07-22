using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;
using System.Net.Sockets;


namespace Lcs1
{
    internal class Message
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime Stime { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public static Message? FromJson(string somemassage)
        {
            return JsonSerializer.Deserialize<Message>(somemassage);

        }

        public Message(string nikname, string text)
        {
            this.Name = nikname;
            this.Text = text;
            this.Stime = DateTime.Now;
        }

        public Message() { }

        public override string ToString()
        {
            return $"Получено сообщение от {Name} ({Stime.ToShortTimeString()}): \n {Text}";
        }
    }
}

