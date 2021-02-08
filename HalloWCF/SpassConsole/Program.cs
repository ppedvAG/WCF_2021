using HalloWCF.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpassConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var s1 = new Service1();

            foreach (var item in s1.GetPizzaListe())
            {
                Console.WriteLine($"{item.Name}");
            }
            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
