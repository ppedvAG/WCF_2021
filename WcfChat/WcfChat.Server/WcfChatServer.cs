using System;
using System.IO;
using WcfChat.Contracts;

namespace WcfChat.Server
{
    public class WcfChatServer : IServer
    {
        public void Login(string username)
        {
            Console.WriteLine($"Login {username}");
        }

        public void Logout()
        {
            Console.WriteLine($"Logout");
        }

        public void SendImage(Stream image)
        {
            Console.WriteLine($"SendImage");
        }

        public void SendText(string text)
        {
            Console.WriteLine($"SendText {text}");
        }
    }
}
