using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace HalloWCF.Host
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        decimal Add(double zahl1, double zahl2);

        [OperationContract]
        IEnumerable<Pizza> GetPizzaListe();
    }

    [DataContract]
    public class Pizza
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember()]
        public string Name { get; set; }
        [DataMember]
        public decimal Preis { get; set; }
    }
}
