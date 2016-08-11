using System;
using System.Threading.Tasks;

namespace SoftwareKobo.Social.SinaWeibo
{
    public class WeiboClient : WeiboClientBase
    {
        public WeiboClient(string appKey, string appSecret, string redirectUri, string scope = null) : base(appKey, appSecret, redirectUri, scope)
        {
            throw new NotImplementedException();
        }

        public override bool IsAuthorized
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Task AuthorizeAsync()
        {
            throw new NotImplementedException();
        }

        public override void ClearAuthorize()
        {
            throw new NotImplementedException();
        }
    }
}