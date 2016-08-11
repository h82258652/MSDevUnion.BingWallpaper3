using System;
using System.Threading.Tasks;

namespace SoftwareKobo.Social.SinaWeibo
{
    public abstract class WeiboClientBase
    {
        protected WeiboClientBase(string appKey, string appSecret, string redirectUri, string scope = null)
        {
            if (appKey == null)
            {
                throw new ArgumentNullException(nameof(appKey));
            }
            if (appSecret == null)
            {
                throw new ArgumentNullException(nameof(appSecret));
            }
            if (redirectUri == null)
            {
                throw new ArgumentNullException(nameof(redirectUri));
            }

            AppKey = appKey;
            AppSecret = appSecret;
            RedirectUri = redirectUri;
            Scope = scope;
        }

        public abstract bool IsAuthorized
        {
            get;
        }

        public string Uid
        {
            get;
            protected set;
        }

        protected string AccessToken
        {
            get;
            set;
        }

        protected string AppKey
        {
            get;
        }

        protected string AppSecret
        {
            get;
        }

        protected string RedirectUri
        {
            get;
        }

        protected string Scope
        {
            get;
        }

        public abstract Task AuthorizeAsync();

        public abstract void ClearAuthorize();
    }
}