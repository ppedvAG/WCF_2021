namespace WCF_SelfHost.Host
{
    public class WetterService : IWetterService
    {
        public double GetTemperature(string location)
        {
            if (location.ToLower().Contains("nord"))
                return -7;
            else
                return 0;

        }

        public string GetWetter(string location)
        {
            if (location.ToLower().Contains("nord"))
                return "Schnee";
            else
                return "Bäh";
        }
    }
}
