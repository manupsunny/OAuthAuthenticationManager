using System.Linq;
using Authentication.Model.Exceptions;
using Authentication.Model.Models;
using Common.Logging;
using Nancy;

namespace Authentication.Model.Validators
{
    public static class AccessTokenValidator
    {
        private static readonly ILog Log = LogManager.GetLogger("AccessTokenValidator");

        public static bool Validate(NancyContext context, string issuer, string secretKey)
        {
            try
            {
                var accessTokenJWT = context.Request.Headers["Authorization"].FirstOrDefault();
                var consumerKey = context.Items.ContainsKey("ConsumerKey") ? context.Items["ConsumerKey"] : null;
                if (consumerKey == null || accessTokenJWT == null) return false;

                var accessToken = AccessToken.FromJWT(accessTokenJWT, (ConsumerKey)consumerKey, issuer, secretKey);
                if (accessToken == null) return false;

                context.CurrentUser = new UserIdentity(accessToken);
                return true;
            }
            catch (UnauthorizedException e)
            {
                Log.ErrorFormat("Message: {0}, Target: {1}, Stacktrace: {2}", e.Message, e.TargetSite, e.StackTrace);
                return false;
            }
        }
    }
}