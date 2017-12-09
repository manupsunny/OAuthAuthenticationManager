using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Authentication.Service.Services.Login;
using Authentication.Utilities.Common;
using Authentication.Utilities.Exceptions;
using Authentication.Utilities.Models;
using Common.Logging;
using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;

namespace Authentication.API.Modules
{
    public class AuthenticationModule : NancyModule
    {
        private readonly ILoginService LoginService;
        private static readonly ILog Log = LogManager.GetLogger<AuthenticationModule>();

        public AuthenticationModule(ILoginService loginService) : base(
            "/authentication")
        {
            LoginService = loginService;

            Post["/login", true] = async (x, ct) => await Login();

            Get["/logout", true] = async (x, ct) => await Logout();
        }

        private async Task<dynamic> Login()
        {
            var status = HttpStatusCode.Unauthorized;
            var issuer = ApplicationSettings.JwtIssuer;

            var responseNegotiator = Negotiate.WithHeader("Content-Type", "application/json");
            try
            {
                var loginRequest = JsonConvert.DeserializeObject<LoginRequest>(Request.Body.AsString());
                loginRequest.ConsumerKey = (ConsumerKey) Context.Items["ConsumerKey"];

                LoginResponse loginResponse = null;
                var loginType = ValidateLoginRequest(loginRequest);

                if (loginType == LoginType.Device)
                    loginResponse = await LoginService.LoginUsingPassword(loginRequest, issuer);

                if (loginType == LoginType.Google)
                    loginResponse = await LoginService.LoginUsingGoogle(loginRequest, issuer);

                if (loginType == LoginType.Facebook)
                    loginResponse = await LoginService.LoginUsingFacebook(loginRequest, issuer);

                if (loginResponse == null) throw new UnauthorizedException();
                responseNegotiator.WithModel(new
                {
                    loginResponse.AccessToken.UserID,
                    loginResponse.AccessToken.ProfileID,
                    loginResponse.AccessToken.Role
                });
                responseNegotiator.WithHeader("Access-Token",
                    loginResponse.AccessToken.ToJWT(ApplicationSettings.SecretKey));
                responseNegotiator.WithHeader("Refresh-Token",
                    loginResponse.RefreshToken.ToJWT(ApplicationSettings.SecretKey));

                status = HttpStatusCode.OK;
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
            return await Task.Run(() =>
            {
                var existingUserIdentity = (UserIdentity) Context.CurrentUser;
                existingUserIdentity.AccessToken.ExpireToken();

                var authToken = existingUserIdentity.AccessToken.ToJWT(ApplicationSettings.JwtIssuer);
                var responseNegotiator = Negotiate.WithHeader("Content-Type", "application/json");
                responseNegotiator.WithHeader("Access-Token", authToken)
                    .WithStatusCode(HttpStatusCode.OK);

                return responseNegotiator;
            });
        }

        private static LoginType ValidateLoginRequest(LoginRequest loginRequest)
        {
            var isLoginTypeValid = Enum.TryParse(loginRequest.loginType, out LoginType loginType);

            if(!isLoginTypeValid)
                throw new AuthenticationException("Logintype invalid !!");

            if(loginRequest.userName == null)
                throw new AuthenticationException("Username invalid !!");

            if(loginRequest.password == null)
                throw new AuthenticationException("Password invalid !!");

            return loginType;
        }
    }
}