using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Authentication.Model.Common;
using Authentication.Model.Exceptions;
using Authentication.Model.Models;
using Authentication.Service.Services.Login;
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

        public AuthenticationModule(ILoginService loginService, IEnvironmentSettings environmentSettings) : base(
            "/authentication")
        {
            LoginService = loginService;
            EnvironmentSettings = environmentSettings;

            Post["/login", true] = async (x, ct) => await Login();

            Get["/logout", true] = async (x, ct) => await Logout();
        }

        private async Task<dynamic> Login()
        {
            var status = HttpStatusCode.Unauthorized;
            var issuer = EnvironmentSettings.JwtIssuer;

            var responseNegotiator = Negotiate.WithHeader("Content-Type", "application/json");
            try
            {
                var loginRequest = JsonConvert.DeserializeObject<LoginRequest>(Request.Body.AsString());
                loginRequest.ConsumerKey = (ConsumerKey) Context.Items["ConsumerKey"];
                if (ValidateLoginRequest(loginRequest))
                {
                    var loginResponse = await LoginService.Login(loginRequest, issuer);
                    if (loginResponse == null) throw new UnauthorizedException();
                    responseNegotiator.WithModel(new
                    {
                        loginResponse.AccessToken.UserID,
                        loginResponse.AccessToken.ProfileID,
                        loginResponse.AccessToken.Role
                    });
                    responseNegotiator.WithHeader("Access-Token",
                        loginResponse.AccessToken.ToJWT(EnvironmentSettings.SecretKey));
                    responseNegotiator.WithHeader("Refresh-Token",
                        loginResponse.RefreshToken.ToJWT(EnvironmentSettings.SecretKey));
                    status = HttpStatusCode.OK;
                }
            }
            catch (AuthenticationException e)
            {
                Log.ErrorFormat("Message: {0}, Target: {1}, Stacktrace: {2}", e.Message, e.TargetSite, e.StackTrace);
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
            var existingUserIdentity = (UserIdentity) Context.CurrentUser;
            existingUserIdentity.AccessToken.ExpireToken();

            var authToken = existingUserIdentity.AccessToken.ToJWT(EnvironmentSettings.JwtIssuer);
            var responseNegotiator = Negotiate.WithHeader("Content-Type", "application/json");
            responseNegotiator.WithHeader("Access-Token", authToken)
                .WithStatusCode(HttpStatusCode.OK);

            return responseNegotiator;
        }

        private static bool ValidateLoginRequest(LoginRequest loginRequest)
        {
            return loginRequest.userName != null && loginRequest.password != null && Enum.TryParse(loginRequest.loginType, out LoginType _);
        }
    }
}