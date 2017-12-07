using System.Collections.Generic;
using Authentication.Service;
using Authentication.Utilities;
using Authentication.Utilities.Common;
using Authentication.Utilities.Models;
using Authentication.Utilities.Services;
using Autofac;
using Common.Logging;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Responses;
using Nancy.Security;
using Nancy.Serialization.JsonNet;
using Newtonsoft.Json;

namespace Authentication.API.Hosting
{
    public class AuthenticationBootstrapper : AutofacNancyBootstrapper
    {
        private static readonly ILog RequestLog = LogManager.GetLogger("AuthenticationRequest");
        private static readonly ILog ResponseLog = LogManager.GetLogger("AuthenticationResponse");
        private static readonly ILog ErrorLog = LogManager.GetLogger("AuthenticationError");

        private IEnvironmentSettings EnvironmentSettings;

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(c => c.Serializers.Insert(0, typeof (JsonNetSerializer)));
            }
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            base.ConfigureApplicationContainer(container);
            StaticConfiguration.DisableErrorTraces = false;
            var builder = new ContainerBuilder();
            AuthenticationServiceAutofacRegistry.RegisterDependencies(builder);
            AuthenticationUtilitiesAutofacRegistry.RegisterDependencies(builder);
            builder.RegisterType<CustomJSONSerializer>().As<JsonSerializer>();
            builder.Update(container.ComponentRegistry);
            EnvironmentSettings = container.Resolve<IEnvironmentSettings>();
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            SSLProxy.RewriteSchemeUsingForwardedHeaders(pipelines);
            var consumerKeys = container.Resolve<IConsumerKeys>();
            var unauthenticatedRoutes = new List<string>
            {
                "/authentication/login"
            };
            var anonmyousRoutes = new List<string>();
            pipelines.BeforeRequest += context =>
            {
                RequestLog.InfoNancyRequest(context);
                return RequestAuthentication.Authenticate(context, consumerKeys, unauthenticatedRoutes, anonmyousRoutes,
                    EnvironmentSettings.JwtIssuer, EnvironmentSettings.SecretKey)
                    ? (Response) null
                    : HttpStatusCode.Unauthorized;
            };
            pipelines.AfterRequest += context => { ResponseLog.InfoNancyResponse(context); };
        }

        protected override void RequestStartup(ILifetimeScope requestContainer, IPipelines pipelines,
            NancyContext context)
        {
            pipelines.OnError.AddItemToEndOfPipeline((z, exception) =>
            {
                ErrorLog.ErrorNancyRequest(context, exception);
                Response error = new JsonResponse(exception.Message, new DefaultJsonSerializer());
                error.StatusCode = HttpStatusCode.InternalServerError;
                return error;
            });

            base.RequestStartup(requestContainer, pipelines, context);
        }
    }
}