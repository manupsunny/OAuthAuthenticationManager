using System.Collections.Generic;
using Authentication.Model.Models;
using Authentication.Model.Validators;
using Nancy;

namespace Authentication.Model
{
    public static class RequestAuthentication
    {
        public static bool Authenticate(NancyContext context, IConsumerKeys consumerKeys,
            List<string> unauthenticatedRoutes, List<string> anonmyousRoutes, string issuer, string secretKey)
        {
            var isConsumerKeyValid = ConsumerKeyValidator.Validate(context, consumerKeys.GetAll());
            var isUnauthenticatedRoute = IsUnauthenticatedRoute(context.Request.Path, unauthenticatedRoutes);
            var isAnonymousRoute = IsAnonymousRoute(context.Request.Path, anonmyousRoutes);
            var isValidRequest = isUnauthenticatedRoute ||
                                 (isAnonymousRoute && IsValidAnonymousUser(context, issuer, secretKey)) ||
                                 IsValidAuthenticatedUser(context, issuer, secretKey);

            return isConsumerKeyValid && isValidRequest;
        }

        private static bool IsUnauthenticatedRoute(string requestPath, List<string> allowedRoots)
        {
            return allowedRoots.Exists(x => requestPath.TrimEnd('/').Equals(x));
        }

        private static bool IsAnonymousRoute(string requestPath, List<string> anonymousRoutes)
        {
            return anonymousRoutes.Exists(x => requestPath.TrimEnd('/').Equals(x));
        }

        private static bool IsValidAuthenticatedUser(NancyContext context, string issuer, string secretKey)
        {
            return AccessTokenValidator.Validate(context, issuer, secretKey) && !string.IsNullOrWhiteSpace(context.CurrentUser != null ? context.CurrentUser.UserName : null);
        }

        private static bool IsValidAnonymousUser(NancyContext context, string issuer, string secretKey)
        {
            return AccessTokenValidator.Validate(context, issuer, secretKey);
        }
    }
}