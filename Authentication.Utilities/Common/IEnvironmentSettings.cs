﻿namespace Authentication.Utilities.Common
{
    public interface IEnvironmentSettings
    {
        string SecretKey { get; }
        string JwtIssuer { get; }
        string ServiceUsername { get; }
        string ServicePassword { get; }
    }
}
