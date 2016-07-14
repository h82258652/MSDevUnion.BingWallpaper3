using BingoWallpaper.Models.LeanCloud;
using BingoWallpaper.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ILeanCloudWallpaperService _leanCloudWallpaperService;

        private readonly INavigationService _navigationService;

        private bool _isBusy;

        private RelayCommand _refreshCommand;

        private WallpaperCollection _selectedWallpaperCollection;

        public MainViewModel(INavigationService navigationService, ILeanCloudWallpaperService leanCloudWallpaperService)
        {
            _navigationService = navigationService;
            _leanCloudWallpaperService = leanCloudWallpaperService;

            var wallpaperCollections = new List<WallpaperCollection>();
            var date = BingoWallpaper.Constants.MinimumViewMonth;
            while (date < DateTimeOffset.Now)
            {
                wallpaperCollections.Add(new WallpaperCollection(date.Year, date.Month));
                date = date.AddMonths(1);
            }
            WallpaperCollections = wallpaperCollections;
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            private set
            {
                Set(ref _isBusy, value);
            }
        }

        public RelayCommand RefreshCommand
        {
            get
            {
                _refreshCommand = _refreshCommand ?? new RelayCommand(() =>
                {
                    // TODO remove this test code.
                    IsBusy = !IsBusy;
                    return;

                    IsBusy = true;
                    try
                    {
                        var selectedWallpaperCollection = SelectedWallpaperCollection;
                        selectedWallpaperCollection.Clear();

                        throw new NotImplementedException();
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });
                return _refreshCommand;
            }
        }

        public WallpaperCollection SelectedWallpaperCollection
        {
            get
            {
                if (_selectedWallpaperCollection == null)
                {
                    SelectedWallpaperCollection = WallpaperCollections.LastOrDefault();
                }
                return _selectedWallpaperCollection;
            }
            set
            {
                Set(ref _selectedWallpaperCollection, value);
                if (_selectedWallpaperCollection != value)
                {
                    RefreshCommand.Execute(null);
                }
            }
        }

        public IReadOnlyList<WallpaperCollection> WallpaperCollections
        {
            get;
        }
    }
}