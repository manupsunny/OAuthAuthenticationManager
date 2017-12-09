using System.Text.RegularExpressions;

namespace Authentication.Utilities.Validators
{
    public class UserNameValidator
    {
        public static UserNameValidatorResponse Validate(string userName)
        {
            return new UserNameValidatorResponse(userName);
        }
    }

    public class UserNameValidatorResponse
    {
        public bool IsValid => IsValidEmailAddress(UserName);
        private readonly string UserName;

        public UserNameValidatorResponse(string userName)
        {
            UserName = userName;
        }

        private static bool IsValidEmailAddress(string emailAddress)
        {
            const string emailAddressPattern = @"^[a-zA-Z0-9](\w|\.)*@[a-zA-Z0-9](\w)+\.[a-zA-Z]+";
            return Regex.Match(emailAddress, emailAddressPattern).Success;
        }
    }
}