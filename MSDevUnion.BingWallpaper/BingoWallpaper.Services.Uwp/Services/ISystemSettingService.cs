using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public interface ISystemSettingService
    {
        Task OpenLockScreenSettingAsync();

        Task OpenWallpaperSettingAsync();
    }
}