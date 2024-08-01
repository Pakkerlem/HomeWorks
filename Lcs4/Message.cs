using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lcs4
{
    internal class Message
    {
        public string FromName { get; set; }
        public string ToName { get; set; }
        public DateTime DateTime { get; set; }
        public Message(string u, string text)
        {
            FromName = u;
            ToName = text;
            DateTime = DateTime.Now;
        }
        public Message() { }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
        public static Message? FromJson(string jsonData)
        {
            return JsonSerializer.Deserialize<Message>(jsonData);
        }

        public override string ToString()
        {
            return String.Format($"{this.DateTime.ToString()} - {this.FromName} : {this.ToName}");
        }

        public string ShortMes()
        {
            return String.Format(this.ToName);
        }
    }
}
