using System;
using Microsoft.Owin.Hosting;
using Owin;

namespace Authentication.API
{
    public class Startup
    {
        private IDisposable WebAppInstance;

        public void Start()
        {
            var url = "http://localhost:9001";
            WebAppInstance = WebApp.Start(url);
        }

        public void Stop()
        {
            WebAppInstance?.Dispose();
        }

        public void Configuration(IAppBuilder app)
        {
            app.Run(context =>
            {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("Authentication Service started..");
            });
        }
    }
}
