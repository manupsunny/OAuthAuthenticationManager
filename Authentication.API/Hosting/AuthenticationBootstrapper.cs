using Authentication.Service;
using Authentication.Utilities;
using Autofac;
using Nancy.Bootstrappers.Autofac;

namespace Authentication.API.Hosting
{
    public class AuthenticationBootstrapper : AutofacNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            base.ConfigureApplicationContainer(container);
            var builder = new ContainerBuilder();
            AuthenticationServiceAutofacRegistry.RegisterDependencies(builder);
            AuthenticationUtilitiesAutofacRegistry.RegisterDependencies(builder);
            builder.Update(container.ComponentRegistry);
        }
    }
}