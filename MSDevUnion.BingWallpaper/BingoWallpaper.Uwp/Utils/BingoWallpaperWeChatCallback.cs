using BingoWallpaper.Uwp.Services;
using MicroMsg.sdk;

namespace BingoWallpaper.Uwp.Utils
{
    internal sealed class BingoWallpaperWeChatCallback : WXEntryBasePage
    {
        private readonly IAppToastService _appToastService;

        internal BingoWallpaperWeChatCallback()
        {
            _appToastService = new AppToastService();
        }

        public override void OnSendMessageToWXResponse(SendMessageToWX.Resp response)
        {
            base.OnSendMessageToWXResponse(response);
            if (response.ErrCode == 0)
            {
                _appToastService.ShowMessage(LocalizedStrings.ShareSuccess);
            }
            else
            {
                _appToastService.ShowError(response.ErrStr);
            }
        }
    }
}