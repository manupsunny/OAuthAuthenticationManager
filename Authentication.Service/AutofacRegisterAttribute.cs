using System;
using System.ComponentModel;

namespace Authentication.Service
{
    public enum RegisterType
    {
        [Description("Register Single Instance (Singleton) that implments IStartable")] SingletonWithStartup = 1,
        [Description("Register Single Instance (Singleton)")] Singleton = 2,
        [Description("Register Instance Per Request")] PerRequest = 3,
        [Description("Register Instance Per Lifetime Scope")] PerLifetime = 4,
        [Description("Do Not Register Dependency")] DoNotRegister = 5
    }

    public class AutofacRegisterAttribute : Attribute
    {
        public virtual RegisterType RegisterType { get; }

        public AutofacRegisterAttribute(RegisterType registerType)
        {
            RegisterType = registerType;
        }
    }
}