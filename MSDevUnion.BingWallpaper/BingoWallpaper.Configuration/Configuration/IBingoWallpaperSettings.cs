using BingoWallpaper.Models;

namespace BingoWallpaper.Configuration
{
    public interface IBingoWallpaperSettings
    {
        bool IsAutoUpdateLockScreen
        {
            get;
            set;
        }

        bool IsAutoUpdateWallpaper
        {
            get;
            set;
        }

        string SelectedArea
        {
            get;
            set;
        }

        SaveLocation SelectedSaveLocation
        {
            get;
            set;
        }

        WallpaperSize SelectedWallpaperSize
        {
            get;
            set;
        }
    }
}