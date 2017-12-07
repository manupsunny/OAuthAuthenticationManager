using System;
using System.Collections.Generic;
using System.Configuration;
using Authentication.Utilities.Exceptions;

namespace Authentication.Utilities.Models
{
    public class RefreshToken : UserToken
    {
        public RefreshToken(IDictionary<string, object> claimsList) : this((string) claimsList["userId"],
            (string) claimsList["profileId"], (string) claimsList["userName"],
            (string) claimsList["role"], (ConsumerKey) claimsList["consumerKey"], (string) claimsList["tokenId"],
            (string) claimsList["iss"])
        {
        }

        public RefreshToken(string userId, string profileId, string userName, string role, ConsumerKey consumerKey,
            string tokenId, string issuer)
            : base(userId, userName, role, profileId, consumerKey, tokenId, issuer)
        {
        }

        public static RefreshToken FromJWT(string tokenString, ConsumerKey consumerKey, string issuer, string secretKey)
        {
            var decodedToken = FromJWT(tokenString, secretKey);
            if (decodedToken == null || !string.Equals(decodedToken["consumerKey"].ToString(), consumerKey.Value)
                || !string.Equals(decodedToken["scope"].ToString(), TokenScope.REFRESH.ToString())
                || !string.Equals(decodedToken["iss"].ToString(), issuer))
            {
                throw new UnauthorizedException("Refresh token passed is not valid!");
            }
            decodedToken["consumerKey"] = consumerKey;
            var refreshToken = new RefreshToken(decodedToken);
            if (IsExpired(refreshToken.ExpiryTime))
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var expiryTime = epoch.AddSeconds(long.Parse(decodedToken["exp"].ToString()));
                refreshToken.ExpiryTime = expiryTime;
            }
            return refreshToken;
        }

        private static bool IsExpired(DateTime currentExpiry)
        {
            return currentExpiry.Ticks <= DateTime.UtcNow.Ticks;
        }

        protected override void SetScope()
        {
            Scope = TokenScope.REFRESH;
        }

        protected override void SetExpiryTime()
        {
            var daysString = ConfigurationManager.AppSettings["refreshTokenValidityInDays"];
            ExpiryTime = DateTime.UtcNow.AddDays(string.IsNullOrEmpty(daysString) ? 0 : double.Parse(daysString));
        }
    }
}