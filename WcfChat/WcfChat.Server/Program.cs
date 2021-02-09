using System;
using System.ServiceModel;
using WcfChat.Contracts;

namespace WcfChat.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** WCF Chat Server ***");

            var bind = new NetTcpBinding();
            bind.MaxReceivedMessageSize = int.MaxValue;
            var host = new ServiceHost(typeof(WcfChatServer));

            host.AddServiceEndpoint(typeof(IServer), bind, "net.tcp://localhost:1");

            host.Open();
            Console.WriteLine("Server wurde gestartet");
            Console.ReadLine();

            host.Close();
            Console.WriteLine("Server wurde beendet");
            Console.WriteLine("Ende");
        }
    }
}
