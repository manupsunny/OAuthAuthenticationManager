using Authentication.Models.Common;
using Autofac;

namespace Authentication.Models
{
    public class AuthenticationUtilitiesAutofacRegistry
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<EnvironmentSettings>().AsImplementedInterfaces().SingleInstance();
        }
    }
}