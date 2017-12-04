using System;
using System.Collections.Generic;
using System.ComponentModel;
using Authentication.Model.Exceptions;
using JWT;

namespace Authentication.Model.Models
{
    public abstract class UserToken
    {
        public string UserName { get; private set; }
        public string UserID { get; private set; }
        public string ProfileID { get; private set; }
        public string TokenID { get; private set; }
        public ConsumerKey ConsumerKey { get; private set; }
        public DateTime ExpiryTime { get; protected set; }
        public TokenScope Scope { get; protected set; }
        public string JwtIssuer;

        public enum TokenScope
        {
            ACCESS,
            REFRESH
        }

        public Role Role { get; private set; }

        protected UserToken(string userId, string userName, string role, string profileId, ConsumerKey consumerKey,
            string tokenId, string jwtIssuer)
        {
            UserName = userName;
            Role = GetRoleFromString(role);
            ConsumerKey = consumerKey;
            UserID = userId;
            ProfileID = profileId;
            TokenID = tokenId;
            JwtIssuer = jwtIssuer;
            SetExpiryTime();
            SetScope();
        }

        protected abstract void SetScope();

        protected abstract void SetExpiryTime();

        private static Role GetRoleFromString(string roleString)
        {
            Role role;

            if (Enum.TryParse(roleString, true, out role))
            {
                return role;
            }
            throw new InvalidEnumArgumentException(string.Format("Role {0} provided in the token is invalid",
                roleString));
        }

        public void ExpireToken()
        {
            ExpiryTime = DateTime.UtcNow;
        }

        private Dictionary<string, object> ToJWTPayload()
        {
            return new Dictionary<string, object>()
            {
                {"iss", JwtIssuer},
                {"exp", ExpiryInSeconds()},
                {"tokenId", TokenID},
                {"userId", UserID},
                {"userName", UserName},
                {"role", Role.ToString()},
                {"profileId", ProfileID},
                {"consumerKey", ConsumerKey.Value},
                {"scope", Scope.ToString()}
            };
        }

        private double ExpiryInSeconds()
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expiry = Math.Round((ExpiryTime - unixEpoch).TotalSeconds);
            return expiry;
        }

        public string ToJWT(string secretKey)
        {
            var jwtPayload = ToJWTPayload();
            return JsonWebToken.Encode(jwtPayload, secretKey, JwtHashAlgorithm.HS256);
        }

        protected static IDictionary<string, object> FromJWT(string userToken, string secretKey)
        {
            IDictionary<string, object> claimsDictionary;
            try
            {
                claimsDictionary =
                    JsonWebToken.DecodeToObject(userToken, secretKey, true) as IDictionary<string, object>;
            }
            catch (Exception e)
            {
                throw new UnauthorizedException("Error occured!!", e);
            }

            return claimsDictionary;
        }

        protected bool Equals(UserToken other)
        {
            return string.Equals(JwtIssuer, other.JwtIssuer) && string.Equals(UserName, other.UserName) &&
                   string.Equals(UserID, other.UserID) && string.Equals(ProfileID, other.ProfileID) &&
                   string.Equals(TokenID, other.TokenID) && Equals(ConsumerKey, other.ConsumerKey) &&
                   Scope == other.Scope && Role == other.Role;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UserToken) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (JwtIssuer != null ? JwtIssuer.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserName != null ? UserName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserID != null ? UserID.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ProfileID != null ? ProfileID.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (TokenID != null ? TokenID.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ConsumerKey != null ? ConsumerKey.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) Scope;
                hashCode = (hashCode * 397) ^ (int) Role;
                return hashCode;
            }
        }
    }
}