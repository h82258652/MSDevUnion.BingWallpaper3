using BingoWallpaper.Configuration;
using BingoWallpaper.Services;
using System.Linq;
using Windows.ApplicationModel.Background;

namespace BingoWallpaper.BackgroundTask
{
    public sealed class UpdateTileTask : IBackgroundTask
    {
        private readonly IBingoWallpaperSettings _bingoWallpaperSettings;

        private readonly IBingWallpaperService _bingWallpaperService;

        public UpdateTileTask()
        {
            _bingWallpaperService = new BingWallpaperService();
            IScreenService screenService = new ScreenService();
            _bingoWallpaperSettings = new BingoWallpaperSettings(_bingWallpaperService, screenService);
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            try
            {
                var result = await _bingWallpaperService.GetAsync(0, 1, _bingoWallpaperSettings.SelectedArea);
                var image = result?.Images.FirstOrDefault();
                if (image != null)
                {
                    // TODO
                }
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}