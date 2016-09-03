using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public interface ISystemSettingService
    {
        Task OpenLockScreenSettingAsync();

        Task OpenWallpaperSettingAsync();

        Task<bool> SetLockScreenAsync(byte[] imageBytes);

        Task<bool> SetWallpaperAsync(byte[] imageBytes);
    }
}