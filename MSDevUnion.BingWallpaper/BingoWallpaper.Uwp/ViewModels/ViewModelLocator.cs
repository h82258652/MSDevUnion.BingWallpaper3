using BingoWallpaper.Configuration;
using BingoWallpaper.Services;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BingoWallpaper.Uwp.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            var serviceLocator = new UnityServiceLocator(ConfigureUnityContainer());
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        private static IUnityContainer ConfigureUnityContainer()
        {
            var unityContainer = new UnityContainer();

            unityContainer.RegisterInstance(CreateNavigationService());
            unityContainer.RegisterType<ILeanCloudWallpaperService, LeanCloudWallpaperService>();

            unityContainer.RegisterType<IBingoWallpaperSettings, BingoWallpaperSettings>();

            unityContainer.RegisterType<MainViewModel>();

            return unityContainer;
        }

        private static INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            return navigationService;
        }
    }
}