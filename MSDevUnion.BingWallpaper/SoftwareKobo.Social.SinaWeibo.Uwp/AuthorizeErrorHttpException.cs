﻿using System.Net;
using Windows.Security.Authentication.Web;

namespace SoftwareKobo.Social.SinaWeibo
{
    public class AuthorizeErrorHttpException : AuthenticationException
    {
        public AuthorizeErrorHttpException(WebAuthenticationResult result) : base(result)
        {
        }

        public HttpStatusCode StatusCode => (HttpStatusCode)Result.ResponseErrorDetail;
    }
}