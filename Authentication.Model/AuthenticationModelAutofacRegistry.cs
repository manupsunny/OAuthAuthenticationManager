using Authentication.Model.Common;
using Authentication.Model.Models;
using Authentication.Model.Repositories;
using Authentication.Model.Services;
using Autofac;

namespace Authentication.Model
{
    public class AuthenticationModelAutofacRegistry
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