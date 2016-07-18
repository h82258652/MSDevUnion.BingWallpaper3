﻿using BingoWallpaper.Configuration;
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

        private readonly IBingoWallpaperSettings _settings;

        private bool _isBusy;

        private RelayCommand _refreshCommand;

        private WallpaperCollection _selectedWallpaperCollection;

        public MainViewModel(INavigationService navigationService, ILeanCloudWallpaperService leanCloudWallpaperService, IBingoWallpaperSettings settings)
        {
            _navigationService = navigationService;
            _leanCloudWallpaperService = leanCloudWallpaperService;
            _settings = settings;

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
                _refreshCommand = _refreshCommand ?? new RelayCommand(async () =>
                {
                    //// TODO remove this test code.
                    //IsBusy = !IsBusy;
                    //return;

                    IsBusy = true;
                    try
                    {
                        var selectedWallpaperCollection = SelectedWallpaperCollection;
                        selectedWallpaperCollection.Clear();

                        var wallpapers = await _leanCloudWallpaperService.GetWallpapersAsync(selectedWallpaperCollection.Year, selectedWallpaperCollection.Month, _settings.SelectedArea);
                        foreach (var wallpaper in wallpapers)
                        {
                            selectedWallpaperCollection.Add(wallpaper);
                        }
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
                if (_selectedWallpaperCollection != value)
                {
                    Set(ref _selectedWallpaperCollection, value);
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