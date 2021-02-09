using System.ServiceModel;
using WCF_SelfHost.Contracts;

namespace WCF_SelfHost.Host
{
    [ServiceContract]
    public interface IWetterService
    {
        [OperationContract]
        [FaultContract(typeof(ErrorInfo))]
        double GetTemperature(string location);

        [OperationContract]
        string GetWetter(string location);
    }
}
