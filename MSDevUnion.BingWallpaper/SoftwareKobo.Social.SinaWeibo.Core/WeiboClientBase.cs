using System;

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
            // TODO
        }

        protected string Scope
        {
            get;
            private set;
        }

        public string Uid
        {
            get;
            protected set;
        }

        protected string AppKey
        {
            get;
            private set;
        }
    }
}