using Authentication.Model.Common;
using Autofac;

namespace Authentication.Model
{
    public class AuthenticationUtilitiesAutofacRegistry
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<EnvironmentSettings>().AsImplementedInterfaces().SingleInstance();
        }
    }
}