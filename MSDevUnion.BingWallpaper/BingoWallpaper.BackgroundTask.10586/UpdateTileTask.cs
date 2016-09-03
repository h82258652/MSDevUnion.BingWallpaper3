using BingoWallpaper.Configuration;
using BingoWallpaper.Services;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using Windows.ApplicationModel.Background;

namespace BingoWallpaper.BackgroundTask
{
    public sealed class UpdateTileTask : IBackgroundTask
    {
        private readonly IBingoWallpaperSettings _bingoWallpaperSettings;

        private readonly IBingWallpaperService _bingWallpaperService;

        private readonly ISystemSettingService _systemSettingService;

        private readonly ITileService _tileService;

        public UpdateTileTask()
        {
            _bingWallpaperService = new BingWallpaperService();
            IScreenService screenService = new ScreenService();
            _bingoWallpaperSettings = new BingoWallpaperSettings(_bingWallpaperService, screenService);
            _tileService = new TileService(_bingWallpaperService);
            _systemSettingService = new SystemSettingService();
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance?.GetDeferral();
            try
            {
                var result = await _bingWallpaperService.GetAsync(0, 1, _bingoWallpaperSettings.SelectedArea);
                var image = result?.Images.FirstOrDefault();
                if (image != null)
                {
                    var copyright = image.Copyright;
                    var text = Regex.Replace(copyright, @"\(©.*", string.Empty).Trim();
                    _tileService.UpdatePrimaryTile(image, text);

                    if (_bingoWallpaperSettings.IsAutoUpdateWallpaper || _bingoWallpaperSettings.IsAutoUpdateLockScreen)
                    {
                        using (var client = new HttpClient())
                        {
                            var bytes = await client.GetByteArrayAsync(_bingWallpaperService.GetUrl(image, _bingoWallpaperSettings.SelectedWallpaperSize));
                            if (_bingoWallpaperSettings.IsAutoUpdateWallpaper)
                            {
                                await _systemSettingService.SetWallpaperAsync(bytes);
                            }
                            if (_bingoWallpaperSettings.IsAutoUpdateLockScreen)
                            {
                                await _systemSettingService.SetLockScreenAsync(bytes);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                deferral?.Complete();
            }
        }
    }
}