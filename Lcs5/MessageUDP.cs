using Lcs5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lcs5
{


    public enum Command
    {
        Register,
        Message,
        Confirmation
    }

    public class MessageUDP
    {
        public Command command { get; set; }
        public int? Id { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
        public string Text { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public static MessageUDP FromJson(string json)
        {
            return JsonSerializer.Deserialize<Message>(json);
        }
    }
    

    
}
