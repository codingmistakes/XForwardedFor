using Microsoft.AspNetCore.Http;
using System.Net;
using System;

namespace XForwardedFor.Utility
{
    public static class HttpContextExtensions
    {
        public static IPAddress GetRemoteIPAddress(this HttpContext context)
        {
            String headerValue = context.Request.Headers["X-Forwarded-For"];
            if (string.IsNullOrEmpty(headerValue))
            {
                return context.Connection.RemoteIpAddress;
            }

            // get the first value of a comma separated IP list
            headerValue = headerValue.Split(new char[] { ',' })[0];

            if (IPAddress.TryParse(headerValue.Trim(), out IPAddress ip))
            {
                return ip;
            }
            
            return context.Connection.RemoteIpAddress;
        }
    }
}
