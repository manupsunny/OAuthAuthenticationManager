using System.Text.RegularExpressions;

namespace Authentication.Service.Validators
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
        public readonly bool IsValid;
        private string UserName;

        public UserNameValidatorResponse(string userName)
        {
            UserName = userName;
            IsValid = IsValidEmailAddress(userName);
        }

        private bool IsValidEmailAddress(string emailAddress)
        {
            var emailAddressPattern = @"^[a-zA-Z0-9](\w|\.)*@[a-zA-Z0-9](\w)+\.[a-zA-Z]+";
            return Regex.Match(emailAddress, emailAddressPattern).Success;
        }
    }
}