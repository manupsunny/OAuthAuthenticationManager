using System;
using System.Threading.Tasks;
using Authentication.Service.Models;
using Authentication.Service.Services;
using Authentication.Utilities.Common;
using Authentication.Utilities.Exceptions;
using Common.Logging;
using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;

namespace Authentication.API.Modules
{
    public class AuthenticationModule : NancyModule
    {
        private readonly ILoginService LoginService;
        private readonly IEnvironmentSettings EnvironmentSettings;
        private static readonly ILog Log = LogManager.GetLogger<AuthenticationModule>();

        public AuthenticationModule(ILoginService loginService, IEnvironmentSettings environmentSettings) : base("/authentication")
        {
            LoginService = loginService;
            EnvironmentSettings = environmentSettings;

            Post["/login", true] = async (x, ct) => await Login();

            Get["/logout", true] = async (x, ct) => await Logout();
        }

        private async Task<dynamic> Login()
        {
            var status = HttpStatusCode.Unauthorized;
            var responseNegotiator = Negotiate.WithHeader("Content-Type", "application/json");
            var issuer = EnvironmentSettings.JwtIssuer;

            try
            {
                var loginRequest = JsonConvert.DeserializeObject<LoginRequest>(Request.Body.AsString());
                if (ValidateLoginRequest(loginRequest))
                {
                    var loginResponse = LoginService.Login(loginRequest, issuer);
                    if(!loginResponse) throw new UnauthorizedException();
                    status = HttpStatusCode.OK;
                }
            }
            catch (UnauthorizedException e)
            {
                Log.ErrorFormat("Message: {0}, Target: {1}, Stacktrace: {2}", e.Message, e.TargetSite, e.StackTrace);
            }
            catch (JsonSerializationException e)
            {
                Log.ErrorFormat("Message: {0}, Target: {1}, Stacktrace: {2}", e.Message, e.TargetSite, e.StackTrace);
            }

            responseNegotiator.WithStatusCode(status);
            return responseNegotiator;
        }

        private async Task<dynamic> Logout()
        {
            return Response.AsText("Logged out..");
        }

        private bool ValidateLoginRequest(LoginRequest loginRequest)
        {
            return loginRequest.userName != null && loginRequest.password != null;
        }
    }
}