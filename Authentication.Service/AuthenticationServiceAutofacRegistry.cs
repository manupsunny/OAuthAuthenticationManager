using System;
using System.Linq;
using System.Reflection;
using Autofac;

namespace Authentication.Service
{
    public class AuthenticationServiceAutofacRegistry
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            // Register Services Default
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .InNamespace("Authentication.Service")
                .Where(
                    y => y.CustomAttributes.Any(
                             x =>
                                 x.AttributeType == Type.GetType("Authentication.Service.AutofacRegisterAttribute"))
                         == false
                )
                .AsImplementedInterfaces()
                .InstancePerDependency();

            // Register Singleton Services
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .InNamespace("Authentication.Service")
                .Where(
                    y => y.CustomAttributes.Any(
                             x =>
                                 x.AttributeType == Type.GetType("Authentication.Service.AutofacRegisterAttribute"))
                         && ((AutofacRegisterAttribute) y.GetCustomAttributes(typeof(AutofacRegisterAttribute)).First())
                         .RegisterType
                         == RegisterType.Singleton)
                .AsImplementedInterfaces()
                .SingleInstance();

            // Register Services Per Request scope
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .InNamespace("Authentication.Service")
                .Where(
                    y => y.CustomAttributes.Any(
                             x =>
                                 x.AttributeType == Type.GetType("Authentication.Service.AutofacRegisterAttribute"))
                         && ((AutofacRegisterAttribute) y.GetCustomAttributes(typeof(AutofacRegisterAttribute)).First())
                         .RegisterType
                         == RegisterType.PerRequest)
                .AsImplementedInterfaces()
                .InstancePerRequest();

            // Register Services per Lifetime scope
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .InNamespace("Authentication.Service")
                .Where(
                    y => y.CustomAttributes.Any(
                             x =>
                                 x.AttributeType == Type.GetType("Authentication.Service.AutofacRegisterAttribute"))
                         && ((AutofacRegisterAttribute) y.GetCustomAttributes(typeof(AutofacRegisterAttribute)).First())
                         .RegisterType
                         == RegisterType.PerLifetime)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}