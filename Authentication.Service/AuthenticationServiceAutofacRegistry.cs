using Authentication.Service.Services;
using Autofac;

namespace Authentication.Service
{
    public class AuthenticationServiceAutofacRegistry
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<LoginService>().AsImplementedInterfaces().InstancePerRequest();
        }
    }
}