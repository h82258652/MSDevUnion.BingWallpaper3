﻿using BingoWallpaper.Configuration;
using BingoWallpaper.Services;
using BingoWallpaper.Uwp.Views;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class ViewModelLocator
    {
        public const string DetailViewKey = "Detail";

        public const string MainViewKey = "Main";

        static ViewModelLocator()
        {
            var serviceLocator = new UnityServiceLocator(ConfigureUnityContainer());
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        public DetailViewModel Detail => ServiceLocator.Current.GetInstance<DetailViewModel>();

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        private static IUnityContainer ConfigureUnityContainer()
        {
            var unityContainer = new UnityContainer();

            unityContainer.RegisterInstance(CreateNavigationService());
            unityContainer.RegisterType<IWallpaperService, LeanCloudWallpaperService>();
            unityContainer.RegisterType<ILeanCloudWallpaperService, LeanCloudWallpaperService>();

            unityContainer.RegisterType<IBingoWallpaperSettings, BingoWallpaperSettings>();

            unityContainer.RegisterType<MainViewModel>();
            unityContainer.RegisterType<DetailViewModel>();

            return unityContainer;
        }

        private static INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(MainViewKey, typeof(MainView));
            navigationService.Configure(DetailViewKey, typeof(DetailView));
            return navigationService;
        }
    }
}