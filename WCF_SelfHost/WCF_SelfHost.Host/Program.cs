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
            host.AddServiceEndpoint(typeof(IWetterService), new BasicHttpBinding(), "http://localhost:2");
            host.AddServiceEndpoint(typeof(IWetterService), new WSHttpBinding(), "http://localhost:3");
            host.AddServiceEndpoint(typeof(IWetterService), new NetNamedPipeBinding(), "net.pipe://localhost/Wetter");

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
