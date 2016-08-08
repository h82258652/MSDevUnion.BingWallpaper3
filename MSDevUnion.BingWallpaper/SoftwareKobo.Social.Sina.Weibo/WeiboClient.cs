using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareKobo.Social.Sina.Weibo
{
    public class WeiboClient
    {
        private WeiboClient()
        {
        }

        public static async Task<WeiboClient> CreateAsync()
        {
            WeiboClient client = new WeiboClient();

            return client;
        }

        public async Task ShareImageAsync(byte[] image, string text)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException($"{nameof(text)} 不能为空白字符。", nameof(text));
            }
        }
    }
}