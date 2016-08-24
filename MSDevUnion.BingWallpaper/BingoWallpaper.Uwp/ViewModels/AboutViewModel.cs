using BingoWallpaper.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        private readonly IStoreService _storeService;

        private RelayCommand _rateCommand;

        public AboutViewModel(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public RelayCommand RateCommand
        {
            get
            {
                _rateCommand = _rateCommand ?? new RelayCommand(async () =>
                {
                    await _storeService.OpenCurrentAppReviewPageAsync();
                });
                return _rateCommand;
            }
        }
    }
}