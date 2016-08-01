using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Metadata;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using WinRTXamlToolkit.AwaitableUI;

namespace BingoWallpaper.Uwp.Views
{
    public sealed partial class ExtendedSplashScreenView
    {
        public ExtendedSplashScreenView()
        {
            InitializeComponent();
        }

        public ExtendedSplashScreenView(SplashScreen splashScreen) : this()
        {
            var r = splashScreen.ImageLocation;
            SplashScreenImage.Width = r.Width;
            SplashScreenImage.Height = r.Height;
        }

        public event EventHandler Completed;

        public async Task Dismiss()
        {
            await DismissStoryboard.BeginAsync();
        }

        private async Task HideStatusBar()
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                await StatusBar.GetForCurrentView().HideAsync();
            }
        }

        private async Task RegisterBackgroundTask()
        {
            // TODO
            await Task.Delay(1000);
        }

        private async void SplashScreenImage_ImageOpened(object sender, RoutedEventArgs e)
        {
            Window.Current.Activate();

            await Task.WhenAll(HideStatusBar(), RegisterBackgroundTask(), UpdatePrimaryTile());
            Completed?.Invoke(this, EventArgs.Empty);
        }

        private async Task UpdatePrimaryTile()
        {
            // TODO
            await Task.Delay(1000);
        }
    }
}