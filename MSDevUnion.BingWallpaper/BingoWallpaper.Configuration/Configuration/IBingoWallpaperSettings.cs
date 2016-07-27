using BingoWallpaper.Models;

namespace BingoWallpaper.Configuration
{
    public interface IBingoWallpaperSettings
    {
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