using System.Configuration;

namespace Authentication.Utilities.Common
{
    public class HostingSettings
    {
        public static string StartupUrl => ConfigurationManager.AppSettings["startupUrl"];
    }
}