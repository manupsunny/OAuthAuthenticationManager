using System;

namespace Authentication.Service.Utilities
{
    public static class IDUtility
    {
        public static string GenerateId()
        {
            var id = Guid.NewGuid();

            return id.ToString().Replace("-", "");
        }
    }
}