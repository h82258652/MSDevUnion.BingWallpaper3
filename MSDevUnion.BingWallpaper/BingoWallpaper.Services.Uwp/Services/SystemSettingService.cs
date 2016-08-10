using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;
using Windows.System.UserProfile;

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

        public async Task<bool> SetLockScreenAsync(byte[] imageBytes)
        {
            if (UserProfilePersonalizationSettings.IsSupported())
            {
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(Constants.LockScreenFileName, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteBytesAsync(file, imageBytes);
                return await UserProfilePersonalizationSettings.Current.TrySetLockScreenImageAsync(file);
            }
            return false;
        }

        public async Task<bool> SetWallpaperAsync(byte[] imageBytes)
        {
            if (UserProfilePersonalizationSettings.IsSupported())
            {
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(Constants.WallpaperFileName, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteBytesAsync(file, imageBytes);
                return await UserProfilePersonalizationSettings.Current.TrySetWallpaperImageAsync(file);
            }
            return false;
        }
    }
}