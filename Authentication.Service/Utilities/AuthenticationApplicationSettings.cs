using System.Configuration;

namespace Authentication.Service.Utilities
{
    [AutofacRegister(RegisterType.Singleton)]
    public class AuthenticationApplicationSettings : IAuthenticationApplicationSettings
    {
        public string RefreshTokenValidityInDays => ConfigurationManager.AppSettings["refreshTokenValidityInDays"];
    }
}