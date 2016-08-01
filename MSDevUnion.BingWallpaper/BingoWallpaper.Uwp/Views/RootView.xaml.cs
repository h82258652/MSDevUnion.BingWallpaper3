using System;
using Windows.ApplicationModel.Activation;

namespace BingoWallpaper.Uwp.Views
{
    public sealed partial class RootView
    {
        public RootView()
        {
            InitializeComponent();
        }

        public RootView(SplashScreen splashScreen) : this()
        {
            ExtendedSplashScreenView extendedSplashScreenView = new ExtendedSplashScreenView(splashScreen);
            EventHandler completedHandler = null;
            completedHandler = async (sender, e) =>
            {
                extendedSplashScreenView.Completed -= completedHandler;
                RootFrame.Navigate(typeof(MainView));
                await extendedSplashScreenView.DismissAsync();
                RootGrid.Children.Remove(extendedSplashScreenView);
            };
            extendedSplashScreenView.Completed += completedHandler;
            RootGrid.Children.Add(extendedSplashScreenView);
        }
    }
}