using BingoWallpaper.Configuration;
using BingoWallpaper.Services;
using BingoWallpaper.Uwp.Controls;
using BingoWallpaper.Uwp.Services;
using BingoWallpaper.Uwp.Views;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class ViewModelLocator
    {
        public const string AboutViewKey = "About";

        public const string DetailViewKey = "Detail";

        public const string MainViewKey = "Main";

        public const string SettingViewKey = "Setting";

        static ViewModelLocator()
        {
            var serviceLocator = new UnityServiceLocator(ConfigureUnityContainer());
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        public AboutViewModel About => ServiceLocator.Current.GetInstance<AboutViewModel>();

        public DetailViewModel Detail => ServiceLocator.Current.GetInstance<DetailViewModel>();

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public SettingViewModel Setting => ServiceLocator.Current.GetInstance<SettingViewModel>();

        public static void Cleanup()
        {
            Messenger.Reset();
        }

        private static IUnityContainer ConfigureUnityContainer()
        {
            var unityContainer = new UnityContainer();

            unityContainer.RegisterInstance(CreateNavigationService());
            unityContainer.RegisterType<IWallpaperService, LeanCloudWallpaperServiceWithCache>();
            unityContainer.RegisterType<ILeanCloudWallpaperService, LeanCloudWallpaperServiceWithCache>();
            unityContainer.RegisterType<IScreenService, ScreenService>();
            unityContainer.RegisterType<ISystemSettingService, SystemSettingService>();
            unityContainer.RegisterType<IBingoFileService, BingoFileService>();
            unityContainer.RegisterType<IAppToastService, AppToastService>();
            unityContainer.RegisterType<IBingoShareService, BingoShareService>();

            unityContainer.RegisterType<IBingoWallpaperSettings, BingoWallpaperSettings>();

            unityContainer.RegisterInstance(DefaultImageLoader.Instance);

            unityContainer.RegisterType<MainViewModel>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<DetailViewModel>();
            unityContainer.RegisterType<SettingViewModel>();
            unityContainer.RegisterType<AboutViewModel>();

            return unityContainer;
        }

        private static INavigationService CreateNavigationService()
        {
            var navigationService = new BingoWallpaperNavigationService();
            navigationService.Configure(MainViewKey, typeof(MainView));
            navigationService.Configure(DetailViewKey, typeof(DetailView));
            navigationService.Configure(SettingViewKey, typeof(SettingView));
            navigationService.Configure(AboutViewKey, typeof(AboutView));
            return navigationService;
        }
    }
}