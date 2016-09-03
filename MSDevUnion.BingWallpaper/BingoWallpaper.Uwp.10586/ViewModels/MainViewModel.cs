using BingoWallpaper.Configuration;
using BingoWallpaper.Models.LeanCloud;
using BingoWallpaper.Services;
using BingoWallpaper.Uwp.Messages;
using BingoWallpaper.Uwp.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IAppToastService _appToastService;

        private readonly ILeanCloudWallpaperService _leanCloudWallpaperService;

        private readonly IBingoWallpaperSettings _settings;

        private int _loadingCollectionCount;

        private RelayCommand _refreshCommand;

        private WallpaperCollection _selectedWallpaperCollection;

        public MainViewModel(ILeanCloudWallpaperService leanCloudWallpaperService, IBingoWallpaperSettings settings, IAppToastService appToastService)
        {
            _leanCloudWallpaperService = leanCloudWallpaperService;
            _settings = settings;
            _appToastService = appToastService;

            var wallpaperCollections = new List<WallpaperCollection>();
            var date = BingoWallpaper.Constants.MinimumViewMonth;
            while (date < DateTimeOffset.Now)
            {
                wallpaperCollections.Add(new WallpaperCollection(date.Year, date.Month));
                date = date.AddMonths(1);
            }
            WallpaperCollections = wallpaperCollections;

            MessengerInstance.Register<SelectedAreaChangedMessage>(this, message =>
            {
                foreach (var wallpaperCollection in WallpaperCollections)
                {
                    wallpaperCollection.Clear();
                    LoadWallpapersAsync(SelectedWallpaperCollection);
                }
            });
        }

        public bool IsBusy => _loadingCollectionCount > 0;

        public RelayCommand RefreshCommand
        {
            get
            {
                _refreshCommand = _refreshCommand ?? new RelayCommand(async () =>
                {
                    var selectedWallpaperCollection = SelectedWallpaperCollection;
                    var year = selectedWallpaperCollection.Year;
                    var month = selectedWallpaperCollection.Month;
                    if (selectedWallpaperCollection.Count >= DateTime.DaysInMonth(year, month))
                    {
                        return;
                    }

                    LoadingCollectionCount++;
                    try
                    {
                        var wallpapers = await _leanCloudWallpaperService.GetWallpapersAsync(year, month, _settings.SelectedArea);
                        FillWallpaperCollection(selectedWallpaperCollection, wallpapers);
                        _appToastService.ShowMessage(LocalizedStrings.RefreshSuccess);
                    }
                    catch (HttpRequestException ex)
                    {
                        _appToastService.ShowError(ex.Message);
                    }
                    finally
                    {
                        LoadingCollectionCount--;
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
                if (_selectedWallpaperCollection != value)
                {
                    if (_selectedWallpaperCollection != null && value != null)
                    {
                        MessengerInstance.Send(new SelectedWallpaperCollectionChangingMessage(_selectedWallpaperCollection, value));
                    }
                    Set(ref _selectedWallpaperCollection, value);
                    LoadWallpapersAsync(value);
                }
            }
        }

        public IReadOnlyList<WallpaperCollection> WallpaperCollections
        {
            get;
        }

        private int LoadingCollectionCount
        {
            get
            {
                return _loadingCollectionCount;
            }
            set
            {
                _loadingCollectionCount = value;
                RaisePropertyChanged(nameof(IsBusy));
            }
        }

        public override void Cleanup()
        {
            base.Cleanup();

            MessengerInstance.Unregister(this);
        }

        private async void FillWallpaperCollection(WallpaperCollection collection, IEnumerable<Wallpaper> wallpapers)
        {
            collection.Clear();
            foreach (var wallpaper in wallpapers)
            {
                collection.Add(wallpaper);
                await Task.Delay(TimeSpan.FromMilliseconds(50));
            }
        }

        private async void LoadWallpapersAsync(WallpaperCollection collection)
        {
            if (collection.Count > 0)
            {
                return;
            }

            LoadingCollectionCount++;
            try
            {
                var wallpapers = await _leanCloudWallpaperService.GetWallpapersAsync(collection.Year, collection.Month, _settings.SelectedArea);
                FillWallpaperCollection(collection, wallpapers);
            }
            catch (HttpRequestException ex)
            {
                _appToastService.ShowError(ex.Message);
            }
            finally
            {
                LoadingCollectionCount--;
            }
        }
    }
}