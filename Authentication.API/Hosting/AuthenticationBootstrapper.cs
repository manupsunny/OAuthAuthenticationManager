using Authentication.Models;
using Authentication.Service;
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