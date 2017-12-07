using System.Collections.Generic;
using Nancy.Security;

namespace Authentication.Utilities.Models
{
    public class UserIdentity : IUserIdentity
    {
        public string UserName { get; private set; }

        public IEnumerable<string> Claims { get {return new List<string>();} }

        public AccessToken AccessToken { get; private set; }

        public UserIdentity(AccessToken accessToken)
        {
            AccessToken = accessToken;
            UserName = accessToken.UserName;
        }
    }
}
