namespace Authentication.Service.Models
{
    public class LoginRequest
    {
        public string userName { get; set; }
        public string password { get; set; }

        protected bool Equals(LoginRequest other)
        {
            return string.Equals(userName, other.userName) 
                && string.Equals(password, other.password);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LoginRequest) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = userName.GetHashCode();
                hashCode = (hashCode*397) ^ password.GetHashCode();
                return hashCode;
            }
        }
    }
}