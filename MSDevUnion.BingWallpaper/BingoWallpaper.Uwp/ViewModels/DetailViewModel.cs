using BingoWallpaper.Configuration;
using BingoWallpaper.Models.LeanCloud;
using BingoWallpaper.Services;
using BingoWallpaper.Uwp.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class DetailViewModel : ViewModelBase, INavigable
    {
        private IImageLoader _imageLoader;

        private RelayCommand _saveCommand;

        public DetailViewModel(IWallpaperService wallpaperService, IBingoWallpaperSettings settings)
        {
            _wallpaperService = wallpaperService;
            _settings = settings;
        }

        private readonly IBingoWallpaperSettings _settings;

        private readonly IWallpaperService _wallpaperService;

        private Wallpaper _wallpaper;

        public RelayCommand SaveCommand
        {
            get
            {
                _saveCommand = _saveCommand ?? new RelayCommand(() =>
                {
                    // TODO
                    var url = _wallpaperService.GetUrl(Wallpaper.Image, _settings.Size);
                });
                return _saveCommand;
            }
        }

        public Wallpaper Wallpaper
        {
            get
            {
                return _wallpaper;
            }
            private set
            {
                Set(ref _wallpaper, value);
            }
        }

        public void Activate(object parameter)
        {
            Wallpaper = (Wallpaper)parameter;
        }

        public void Deactivate(object parameter)
        {
        }
    }
}