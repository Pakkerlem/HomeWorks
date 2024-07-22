using System;

namespace Lcs1
{
    internal class Program
    {

        static void Main(string[] args)

        {
            Chat caht = new Chat();
            if (args.Length == 0)
            {
                Chat.Server();
            }
            else
            {
                Chat.Client(args[0]);
            }

        }
    }
}
