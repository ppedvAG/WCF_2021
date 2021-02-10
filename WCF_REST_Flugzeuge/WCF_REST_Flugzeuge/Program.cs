using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace WCF_REST_Flugzeuge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Flugzeuge Service ***");

            var host = new ServiceHost(typeof(FlugService));

            var webBind = new WebHttpBinding();
            var ep = host.AddServiceEndpoint(typeof(IFlugService), webBind, "http://localhost:1");
            ep.EndpointBehaviors.Add(new WebHttpBehavior() { AutomaticFormatSelectionEnabled = true });

            host.Open();
            Console.WriteLine("Service läuft");
            Console.ReadLine();
            host.Close();


            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
