using Authentication.Utilities.Common;
using Autofac;

namespace Authentication.Utilities
{
    public class AuthenticationUtilitiesAutofacRegistry
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<EnvironmentSettings>().AsImplementedInterfaces().SingleInstance();
        }
    }
}