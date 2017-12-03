using Topshelf;

namespace Authentication.API.Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<AuthenticationHostingService>(s =>
                {
                    s.ConstructUsing(name => new AuthenticationHostingService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription("OAuth Authentication Service");
                x.SetDisplayName("OAuth Authentication Service");
                x.SetServiceName("OAuth-Authentication-Service");
                x.StartAutomatically();
            });
        }
    }
}
