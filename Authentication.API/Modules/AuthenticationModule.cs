using System.Threading.Tasks;
using Nancy;

namespace Authentication.API.Modules
{
    public class AuthenticationModule : NancyModule
    {
        public AuthenticationModule() : base("/authentication")
        {
            Post["/login", true] = async (x, ct) =>
            {
                return await Login();
            };

            Post["/logout", true] = async (x, ct) =>
            {
                return await Logout();
            };
        }

        private Task<dynamic> Logout()
        {
            throw new System.NotImplementedException();
        }

        private Task<dynamic> Login()
        {
            throw new System.NotImplementedException();
        }
    }
}