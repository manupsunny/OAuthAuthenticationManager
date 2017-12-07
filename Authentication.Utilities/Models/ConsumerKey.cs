using System;

namespace Authentication.Utilities.Models
{
    public class ConsumerKey
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string Channel { get; set; }

        protected bool Equals(ConsumerKey other)
        {
            return Equals(Id, other.Id) && string.Equals(Value, other.Value) && string.Equals(Channel, other.Channel);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((ConsumerKey) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Channel != null ? Channel.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}