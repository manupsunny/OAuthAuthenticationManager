using System.Configuration;

namespace Authentication.Model.Common
{
    public class HostingSettings
    {
        public static string StartupUrl => ConfigurationManager.AppSettings["startupUrl"];
    }
}