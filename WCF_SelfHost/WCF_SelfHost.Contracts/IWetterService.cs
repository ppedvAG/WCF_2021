using System.ServiceModel;

namespace WCF_SelfHost.Host
{
    [ServiceContract]
    public interface IWetterService
    {
        [OperationContract]
        double GetTemperature(string location);

        [OperationContract]
        string GetWetter(string location);
    }
}
