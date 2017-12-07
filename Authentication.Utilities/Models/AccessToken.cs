using System;
using System.Collections.Generic;
using System.Configuration;
using Authentication.Utilities.Exceptions;

namespace Authentication.Utilities.Models
{
    public class AccessToken : UserToken
    {
        public AccessToken(IDictionary<string, object> claimsList) : this((string) claimsList["userId"],
            (string) claimsList["profileId"], (string) claimsList["userName"],
            (string) claimsList["role"], (ConsumerKey) claimsList["consumerKey"], (string) claimsList["tokenId"],
            (string) claimsList["iss"])
        {
        }

        public AccessToken(string userId, string profileId, string userName, string role, ConsumerKey consumerKey,
            string tokenId, string issuer)
            : base(userId, userName, role, profileId, consumerKey, tokenId, issuer)
        {
        }

        public static AccessToken FromJWT(string tokenString, ConsumerKey consumerKey, string issuer, string secretKey)
        {
            var decodedToken = FromJWT(tokenString, secretKey);
            if (decodedToken == null || !string.Equals(decodedToken["consumerKey"].ToString(), consumerKey.Value)
                || !string.Equals(decodedToken["scope"].ToString(), TokenScope.ACCESS.ToString())
                || !string.Equals(decodedToken["iss"].ToString(), issuer))
            {
                throw new UnauthorizedException("Access token passed is not valid!");
            }
            decodedToken["consumerKey"] = consumerKey;
            var accessToken = new AccessToken(decodedToken);
            if (!IsExpired(accessToken.ExpiryTime)) return accessToken;
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expiryTime = epoch.AddSeconds(long.Parse(decodedToken["exp"].ToString()));
            accessToken.ExpiryTime = expiryTime;
            return accessToken;
        }

        private static bool IsExpired(DateTime currentExpiry)
        {
            return currentExpiry.Ticks <= DateTime.UtcNow.Ticks;
        }

        protected override void SetScope()
        {
            Scope = TokenScope.ACCESS;
        }

        protected override void SetExpiryTime()
        {
            var minutesString = ConfigurationManager.AppSettings["authTokenValidityInMinutes"];
            ExpiryTime =
                DateTime.UtcNow.AddMinutes(string.IsNullOrEmpty(minutesString) ? 0 : double.Parse(minutesString));
        }
    }
}