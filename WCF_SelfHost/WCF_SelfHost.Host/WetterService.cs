using System.ServiceModel;
using WCF_SelfHost.Contracts;

namespace WCF_SelfHost.Host
{
    public class WetterService : IWetterService
    {
        public double GetTemperature(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                throw new FaultException<ErrorInfo>(new ErrorInfo() { Msg = "Die Location muss immer einen Wert haben" },
                                                   "Location can not be null or empty");

            if (location.ToLower().Contains("nord"))
                return -7;
            else
                return 0;

        }

        public string GetWetter(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                throw new FaultException("Location can not be null or empty");

            if (location.ToLower().Contains("nord"))
                return "Schnee 🌨🌨";
            else
                return "Leichter Schneefall 🌨";
        }
    }
}
