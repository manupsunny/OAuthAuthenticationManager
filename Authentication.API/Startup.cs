using System;
using Authentication.Utilities.Common;
using Microsoft.Owin.Hosting;
using Owin;

namespace Authentication.API
{
    public class Startup
    {
        private IDisposable WebAppInstance;

        public void Start()
        {
            var url = HostingSettings.StartupUrl;
            WebAppInstance = WebApp.Start(url);
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
