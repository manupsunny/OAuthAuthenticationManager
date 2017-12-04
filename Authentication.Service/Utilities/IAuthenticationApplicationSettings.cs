namespace Authentication.Service.Utilities
{
    public interface IAuthenticationApplicationSettings
    {
        string RefreshTokenValidityInDays { get; }
    }
}