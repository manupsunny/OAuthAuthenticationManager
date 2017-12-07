using Authentication.Utilities.Common;
using Authentication.Utilities.Models;
using Authentication.Utilities.Repositories;
using Authentication.Utilities.Services;
using Autofac;

namespace Authentication.Utilities
{
    public class AuthenticationUtilitiesAutofacRegistry
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<EnvironmentSettings>().AsImplementedInterfaces().SingleInstance();
            builder.Register(repo => new ConsumerKeyRepository())
                .AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ConsumerKeys>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ConsumerKeyService>().AsImplementedInterfaces().SingleInstance();
        }
    }
}