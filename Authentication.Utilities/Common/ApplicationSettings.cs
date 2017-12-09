using System.Configuration;

namespace Authentication.Utilities.Common
{
    public class ApplicationSettings
    {
        public static string StartupUrl => ConfigurationManager.AppSettings["StartupUrl"];

        public static string RefreshTokenValidityInDays =>
            ConfigurationManager.AppSettings["RefreshTokenValidityInDays"];

        public static string SecretKey => ConfigurationManager.AppSettings["SecretKey"];
        public static string JwtIssuer => ConfigurationManager.AppSettings["JwtIssuer"];

        public static string AccessTokenRedisHostName => ConfigurationManager.AppSettings["AccessTokenRedisHostName"];
        public static string RefreshTokenRedisHostName => ConfigurationManager.AppSettings["RefreshTokenRedisHostName"];

        public static string ValidRefreshTokenRedisHostName =>
            ConfigurationManager.AppSettings["ValidRefreshTokenRedisHostName"];
    }
}