namespace Authentication.Model.Models
{
    public class LoginResponse
    {
        public AccessToken AccessToken { get; private set; }
        public RefreshToken RefreshToken { get; private set; }

        public LoginResponse(AccessToken accessToken, RefreshToken refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}