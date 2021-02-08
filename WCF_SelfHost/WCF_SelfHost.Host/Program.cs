using System;
using System.ServiceModel;

namespace WCF_SelfHost.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** WCF Host ***");

            var host = new ServiceHost(typeof(WetterService));

            host.AddServiceEndpoint(typeof(IWetterService), new NetTcpBinding(), "net.tcp://localhost:1");

            host.Open();
            Console.WriteLine("Service gestartet");
            Console.ReadLine();
            host.Close();
            Console.WriteLine("Service wurde beendet");


            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
