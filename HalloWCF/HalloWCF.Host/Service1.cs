using System;
using System.Collections.Generic;

namespace HalloWCF.Host
{
    public class Service1 : IService1
    {
        public decimal Add(double zahl1, double zahl2)
        {
            return Convert.ToDecimal(zahl1 + zahl2);
        }

        public string GetData(int value)
        {
            return $"You entered: {value}";
        }

        public IEnumerable<Pizza> GetPizzaListe()
        {
            yield return new Pizza() { Id = 1, Name = "Salami", Preis = 5.60m };
            yield return new Pizza() { Id = 2, Name = "4 Käse", Preis = 8.90m };
            yield return new Pizza() { Id = 3, Name = "Schinken", Preis = 9.90m };
        }
    }
}
