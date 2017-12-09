using System;
using System.Diagnostics;
using Authentication.Utilities.Models;
using Common.Logging;
using Nancy;

namespace Authentication.API
{
    public static class LoggerExtensions
    {
        public static void InfoNancyRequest(this ILog logger, NancyContext context)
        {
            StopWatchStart(context);
            var requestScheme = context.Request.Url.Scheme;
            var requestHost = context.Request.Url.HostName;
            var requestURL = context.Request.Path;
            var requestPort = context.Request.Url.Port;
            var requestMethod = context.Request.Method;
            var requestQueryParams = context.Request.Url.Query;
            var requestClientIP = context.Request.UserHostAddress;
            var requestUserAgent = context.Request.Headers.UserAgent;
            var requestReferralURL = context.Request.Headers.Referrer;
            logger.InfoFormat("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}", requestScheme, requestHost, requestURL,
                requestPort,
                requestMethod, requestClientIP, requestUserAgent, requestQueryParams, requestReferralURL);
        }

        public static void InfoNancyResponse(this ILog logger, NancyContext context)
        {
            var elapsedMilliseconds = StopWatchStop(context);
            var requestScheme = context.Request.Url.Scheme;
            var requestHost = context.Request.Url.HostName;
            var requestURL = context.Request.Path;
            var requestPort = context.Request.Url.Port;
            var requestMethod = context.Request.Method;
            var requestClientIP = context.Request.UserHostAddress;
            var requestUserAgent = context.Request.Headers.UserAgent;
            var requestReferralURL = context.Request.Headers.Referrer;

            var consumerChannel = string.Empty;
            if (context.Items.ContainsKey("ConsumerKey"))
            {
                consumerChannel = ((ConsumerKey) context.Items["ConsumerKey"]).Channel;
            }
            var tokenID = string.Empty;
            if (context.CurrentUser != null)
            {
                var userIdentity = (UserIdentity) context.CurrentUser;
                tokenID = userIdentity.AccessToken.TokenID;
            }
            var responseStatus = context.Response.StatusCode.ToString();
            logger.InfoFormat("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}",
                requestScheme, requestHost, requestURL, requestPort, requestMethod, requestClientIP,
                requestUserAgent, requestReferralURL, tokenID, consumerChannel, responseStatus, elapsedMilliseconds);
        }

        public static void ErrorNancyRequest(this ILog logger, NancyContext context, Exception exception)
        {
            var requestClientIP = context.Request.UserHostAddress;
            var requestUserAgent = context.Request.Headers.UserAgent;
            var requestReferralURL = context.Request.Headers.Referrer;
            var requestURL = context.Request.Url;
            logger.ErrorFormat("{0}|{1}|{2}|{3}|{4}|{5}", requestURL, requestClientIP, requestUserAgent,
                requestReferralURL, exception.Message, exception.StackTrace);
        }

        private static void StopWatchStart(NancyContext ctx)
        {
            var stopwatch = new Stopwatch();
            ctx.Items["stopwatch"] = stopwatch;
            stopwatch.Start();
        }

        private static long StopWatchStop(NancyContext ctx)
        {
            if (!ctx.Items.ContainsKey("stopwatch")) return 0;
            var stopwatch = (Stopwatch) ctx.Items["stopwatch"];
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}