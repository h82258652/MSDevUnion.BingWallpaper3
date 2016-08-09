using MicroMsg.sdk;
using System;
using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public class BingoShareService : IBingoShareService
    {
        public Task ShareToSinaWeiboAsync(byte[] image, string text)
        {
            throw new NotImplementedException();
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