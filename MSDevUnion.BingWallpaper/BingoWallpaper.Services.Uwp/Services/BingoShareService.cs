using MicroMsg.sdk;
using SoftwareKobo.Social.SinaWeibo;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage.Streams;

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

        public void ShareToSystem(byte[] image, string text)
        {
            var dataTransferManager = DataTransferManager.GetForCurrentView();
            TypedEventHandler<DataTransferManager, DataRequestedEventArgs> handler = (sender, args) =>
            {
                var request = args.Request;
                var deferral = request.GetDeferral();
                request.Data.Properties.Title = text;
                request.Data.SetBitmap(RandomAccessStreamReference.CreateFromStream(new MemoryStream(image).AsRandomAccessStream()));
                deferral.Complete();
            };
            dataTransferManager.DataRequested += handler;
            DataTransferManager.ShowShareUI();
            dataTransferManager.DataRequested -= handler;
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