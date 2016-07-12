using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}