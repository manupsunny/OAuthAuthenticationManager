using System.Configuration;

namespace Authentication.Models.Common
{
    public class HostingSettings
    {
        public static string StartupUrl => ConfigurationManager.AppSettings["startupUrl"];
    }
}