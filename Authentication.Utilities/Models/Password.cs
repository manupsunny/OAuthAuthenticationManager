using System;
using System.Text.RegularExpressions;

namespace Authentication.Models.Models
{
    public class Password
    {
        private readonly string _password;
        private readonly int _passkey;

        private const string STRONG_PASSWORD_REGEX = @"^[a-zA-Z0-9_!@#=+\$%\^&\*\(\)\-]{3,20}$";

        public Password(string password)
        {
            _password = password;
            _passkey =
                new Random(DateTime.Now.Millisecond * DateTime.Now.Second * DateTime.Now.Minute * DateTime.Now.Hour)
                    .Next(1000, 9999);
        }

        public string Value()
        {
            return !string.IsNullOrEmpty(_password) ? _password : _passkey.ToString();
        }

        public bool IsValid()
        {
            var password = this.Value();
            return Regex.IsMatch(password, STRONG_PASSWORD_REGEX, RegexOptions.Singleline);
        }
    }
}