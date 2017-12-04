using System;

namespace Authentication.Model.Common
{
    public class EnvironmentSettings : IEnvironmentSettings
    {
        public string SecretKey => Environment.GetEnvironmentVariable("SECRET_KEY");

        public string JwtIssuer => Environment.GetEnvironmentVariable("JWT_ISSUER");

        public string ServiceUsername => Environment.GetEnvironmentVariable("SERVICE_USERNAME");

        public string ServicePassword => Environment.GetEnvironmentVariable("SERVICE_PASSWORD");
    }
}