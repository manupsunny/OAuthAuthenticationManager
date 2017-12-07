using System;

namespace Authentication.Utilities.Models
{
    public class UserTokenLog
    {
        public string TokenId { get; set; }
        public string UserId { get; set; }
        public string Channel { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }

        public UserTokenLog(UserToken userToken)
        {
            TokenId = userToken.TokenID;
            UserId = userToken.UserID;
            Channel = userToken.ConsumerKey.Channel;
            StartTime = DateTime.UtcNow;
            EndTime = userToken.ExpiryTime;
        }

        protected bool Equals(UserTokenLog other)
        {
            return string.Equals(TokenId, other.TokenId)
                   && string.Equals(UserId, other.UserId)
                   && string.Equals(Channel, other.Channel)
                   && DateTimeOffset.Equals(StartTime, other.StartTime)
                   && DateTimeOffset.Equals(EndTime, other.EndTime);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UserTokenLog) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (TokenId != null ? TokenId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserId != null ? UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Channel != null ? Channel.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ StartTime.GetHashCode();
                hashCode = (hashCode * 397) ^ EndTime.GetHashCode();
                return hashCode;
            }
        }
    }
}