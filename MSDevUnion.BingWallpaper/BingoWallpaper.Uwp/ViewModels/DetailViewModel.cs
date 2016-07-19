using BingoWallpaper.Models.LeanCloud;
using GalaSoft.MvvmLight;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class DetailViewModel : ViewModelBase, INavigable
    {
        private Wallpaper _wallpaper;

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