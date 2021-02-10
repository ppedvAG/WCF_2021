using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using WcfChat.Contracts;

namespace WcfChat.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** WCF Chat Server ***");

            var netBind = new NetTcpBinding();
            netBind.Security.Mode = SecurityMode.Transport;
            netBind.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            netBind.MaxReceivedMessageSize = int.MaxValue;

            var wsBind = new WSDualHttpBinding();
            wsBind.Security.Mode = WSDualHttpSecurityMode.Message;
            wsBind.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;

            wsBind.MaxReceivedMessageSize = int.MaxValue;


            var host = new ServiceHost(typeof(WcfChatServer));

            host.AddServiceEndpoint(typeof(IServer), netBind, "net.tcp://localhost:1");


            var ep = host.AddServiceEndpoint(typeof(IServer), wsBind, "http://localhost:2");
            //ep.Address = new EndpointAddress(new Uri("http://localhost:2"),  EndpointIdentity.CreateDnsIdentity("AAAAAA"));
            
            

            host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
            host.Credentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.Root, X509FindType.FindByThumbprint, "81812148a7d49349fbc4058404dad1fcb7015768");




            host.Open();
            Console.WriteLine("Server wurde gestartet");
            Console.ReadLine();

            host.Close();
            Console.WriteLine("Server wurde beendet");
            Console.WriteLine("Ende");
        }
    }
}
