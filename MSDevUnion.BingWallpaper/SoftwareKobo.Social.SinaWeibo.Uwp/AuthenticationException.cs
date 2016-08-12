using System;
using Windows.Security.Authentication.Web;

namespace SoftwareKobo.Social.SinaWeibo
{
    public class AuthenticationException : Exception
    {
        internal AuthenticationException(WebAuthenticationResult result)
        {
            Result = result;
        }

        public WebAuthenticationResult Result
        {
            get;
            private set;
        }
    }
}