using System;
using Authentication.Utilities.Common;
using Microsoft.Owin.Hosting;
using Owin;

namespace Authentication.API.Hosting
{
    public class AuthenticationHostingService
    {
        private IDisposable WebAppInstance;

        public void Start()
        {
            var url = HostingSettings.StartupUrl;
            WebAppInstance = WebApp.Start<AuthenticationStartup>(url);
            Console.WriteLine("Authentication service started at " + url);
        }

        public void Stop()
        {
            WebAppInstance?.Dispose();
        }

        public void Configuration(IAppBuilder app)
        {

        }
    }
}
