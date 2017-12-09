using System;
using System.Threading.Tasks;
using Authentication.Service.Services.Token;
using Authentication.Utilities.Exceptions;
using Authentication.Utilities.Models;
using Authentication.Utilities.Validators;
using Common.Logging;

namespace Authentication.Service.Services.Login
{
    [AutofacRegister(RegisterType.Singleton)]
    public class LoginService : ILoginService
    {
        private static readonly ILog Log = LogManager.GetLogger<LoginService>();

        private readonly IUserTokenService UserTokenService;
        private readonly ILoginManager LoginManager;
        private readonly IUserInfoManager InfoManager;

        public LoginService(ILoginManager loginManager, IUserInfoManager userInfoManager,
            IUserTokenService userTokenService)
        {
            UserTokenService = userTokenService;
            LoginManager = loginManager;
            InfoManager = userInfoManager;
        }

        public async Task<LoginResponse> LoginUsingPassword(LoginRequest loginRequest, string issuer)
        {
            if (!UserNameValidator.Validate(loginRequest.userName).IsValid)
                throw new UnauthorizedException("Username not valid !!");

            var userId = GetUserIdIfLoginValid(loginRequest);
            var role = GetUserRole(userId);

            if (userId == -1 || role == null) return null;

            var loginResponse = await CreateLoginResponse(userId.ToString(),
                loginRequest.userName, (Role) role, userId.ToString(), loginRequest.ConsumerKey, issuer);

            return loginResponse;
        }

        public Task<LoginResponse> LoginUsingGoogle(LoginRequest loginRequest, string issuer)
        {
            throw new NotImplementedException("Feature not implemented !!");
        }

        public Task<LoginResponse> LoginUsingFacebook(LoginRequest loginRequest, string issuer)
        {
            throw new NotImplementedException("Feature not implemented !!");
        }

        private int GetUserIdIfLoginValid(LoginRequest loginRequest)
        {
            try
            {
                if (LoginManager.LoginPasswordMatch(loginRequest.userName, new Password(loginRequest.password),
                    out var userId))
                {
                    return userId;
                }
            }

            catch (Exception e)
            {
                Log.ErrorFormat("Message: {0}, Target: {1}, Stacktrace: {2}", e.Message, e.TargetSite, e.StackTrace);
                throw new UnauthorizedException("Error occured!", e);
            }

            Log.DebugFormat("Just before throwing Unauthorized");
            throw new UnauthorizedException("Error occured!");
        }

        private Role? GetUserRole(int userId)
        {
            try
            {
                if (InfoManager.TryLoad(userId, out var userInfo))
                {
                    var role = Role.ANONYMOUS;

                    if (userInfo.IsAdmin)
                    {
                        role = Role.ADMIN;
                    }
                    else if (userInfo.IsUser)
                    {
                        role = Role.USER;
                    }

                    return role;
                }
            }
            catch (Exception e)
            {
                Log.ErrorFormat("Message: {0}, Target: {1}, Stacktrace: {2}", e.Message, e.TargetSite, e.StackTrace);
                throw new UnauthorizedException($"There was no user found with Id : {userId}");
            }
            return null;
        }

        private async Task<LoginResponse> CreateLoginResponse(string userId, string userName, Role role,
            string profileId, ConsumerKey consumerKey, string issuer)
        {
            var refreshToken =
                await UserTokenService.CreateRefreshToken(userId, userName, role, profileId, consumerKey, issuer);
            var accessToken = await UserTokenService.CreateAccessToken(refreshToken);

            return new LoginResponse(accessToken, refreshToken);
        }
    }
}