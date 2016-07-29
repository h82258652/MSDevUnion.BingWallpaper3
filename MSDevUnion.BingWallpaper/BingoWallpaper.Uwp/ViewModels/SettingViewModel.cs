using BingoWallpaper.Configuration;
using BingoWallpaper.Models;
using GalaSoft.MvvmLight;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        private readonly IBingoWallpaperSettings _settings;

        public SettingViewModel(IBingoWallpaperSettings settings)
        {
            _settings = settings;
        }

        public WallpaperSize SelectedWallpaperSize
        {
            get
            {
                return _settings.SelectedWallpaperSize;
            }
            set
            {
                _settings.SelectedWallpaperSize = value;
                RaisePropertyChanged();
            }
        }
    }
}