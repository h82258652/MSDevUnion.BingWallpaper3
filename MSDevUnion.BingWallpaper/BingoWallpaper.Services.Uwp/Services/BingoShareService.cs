using MicroMsg.sdk;
using SoftwareKobo.Social.SinaWeibo;
using System;
using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public class BingoShareService : IBingoShareService
    {
        public async Task<bool> ShareToSinaWeiboAsync(byte[] image, string text)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            var client = new SinaWeiboClient(Constants.SinaWeiboAppKey, Constants.SinaWeiboAppSecret, Constants.SinaWeiboRedirectUri);
            var status = await client.UploadAsync(text, image);
            if (status.ErrorCode <= 0)
            {
                return true;
            }
            else
            {
                throw new Exception(status.ErrorMessage);
            }
        }

        public async Task ShareToWechatAsync(byte[] image, string text)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            const int scene = SendMessageToWX.Req.WXSceneChooseByUser;
            var message = new WXImageMessage()
            {
                Title = text,
                ImageData = image
            };
            var req = new SendMessageToWX.Req(message, scene);
            var api = WXAPIFactory.CreateWXAPI(Constants.WechatAppId);
            var isValid = await api.SendReq(req);
        }
    }
}