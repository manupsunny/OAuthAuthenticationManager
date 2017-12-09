using System;
using Authentication.Utilities.Common;
using Microsoft.Owin.Hosting;

namespace Authentication.API.Hosting
{
    public class AuthenticationHostingService
    {
        private IDisposable WebAppInstance;

        public void Start()
        {
            var url = ApplicationSettings.StartupUrl;
            WebAppInstance = WebApp.Start<AuthenticationStartup>(url);
            Console.WriteLine("Authentication service started at " + url);
        }

        public void Stop()
        {
            WebAppInstance?.Dispose();
        }
    }
}