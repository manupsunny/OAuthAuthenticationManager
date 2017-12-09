using System;

namespace Authentication.Utilities.Common
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