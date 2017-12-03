using Owin;

namespace Authentication.API.Hosting
{
    public class AuthenticationStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}