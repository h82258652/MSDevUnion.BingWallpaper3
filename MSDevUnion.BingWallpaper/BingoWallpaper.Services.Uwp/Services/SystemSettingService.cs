using System;
using System.Threading.Tasks;
using Windows.System;

namespace BingoWallpaper.Services
{
    public class SystemSettingService : ISystemSettingService
    {
        public async Task OpenLockScreenSettingAsync()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-settings:lockscreen"));
        }

        public async Task OpenWallpaperSettingAsync()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-settings:personalization"));
        }
    }
}