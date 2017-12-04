using Authentication.Model.Common;
using Autofac;

namespace Authentication.Model
{
    public class AuthenticationModelAutofacRegistry
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<EnvironmentSettings>().AsImplementedInterfaces().SingleInstance();
        }
    }
}