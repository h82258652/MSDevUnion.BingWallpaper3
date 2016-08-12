using BingoWallpaper.BackgroundTask;
using BingoWallpaper.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using WinRTXamlToolkit.AwaitableUI;

namespace BingoWallpaper.Uwp.Views
{
    public sealed partial class ExtendedSplashScreenView
    {
        private BackgroundTaskRegistration _backgroundTaskRegistration;

        public ExtendedSplashScreenView()
        {
            InitializeComponent();
        }

        public ExtendedSplashScreenView(SplashScreen splashScreen) : this()
        {
            var rect = splashScreen.ImageLocation;
            SplashScreenImage.Width = rect.Width;
            SplashScreenImage.Height = rect.Height;
        }

        public event EventHandler Completed;

        public async Task DismissAsync()
        {
            await DismissStoryboard.BeginAsync();
        }

        private static async Task HideStatusBarAsync()
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                await StatusBar.GetForCurrentView().HideAsync();
            }
        }

        private static void InitTitleBar()
        {
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            var systemAccentColor = ColorExtensions.AccentColor;
            titleBar.BackgroundColor = systemAccentColor;
            titleBar.ForegroundColor = Colors.White;
            titleBar.InactiveBackgroundColor = systemAccentColor;
            titleBar.ButtonBackgroundColor = systemAccentColor;
            titleBar.ButtonForegroundColor = Colors.White;
            titleBar.ButtonHoverBackgroundColor = systemAccentColor.Lighter();
            titleBar.ButtonPressedBackgroundColor = systemAccentColor.Darker();
            titleBar.ButtonInactiveBackgroundColor = systemAccentColor;
        }

        private static void UpdatePrimaryTile()
        {
            new UpdateTileTask().Run(null);
        }

        private async Task RegisterBackgroundTaskAsync()
        {
            _backgroundTaskRegistration = BackgroundTaskRegistration.AllTasks.Values.OfType<BackgroundTaskRegistration>().FirstOrDefault(temp => temp.Name == Constants.UpdateTileTaskName);

            if (_backgroundTaskRegistration != null)
            {
                // 已经注册后台任务。
                return;
            }

            var access = await BackgroundExecutionManager.RequestAccessAsync();
            if (access != BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity)
            {
                // 没有权限。
                return;
            }

            var builder = new BackgroundTaskBuilder();
            // 仅在网络可用下执行后台任务。
            builder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));

            builder.Name = Constants.UpdateTileTaskName;
            builder.TaskEntryPoint = "BingoWallpaper.BackgroundTask.UpdateTileTask";
            builder.SetTrigger(new TimeTrigger(15, false));
            _backgroundTaskRegistration = builder.Register();
        }

        private async void SplashScreenImage_ImageOpened(object sender, RoutedEventArgs e)
        {
            // 图片加载完毕后激活当前窗口，系统 SplashScreen 将会消失。
            Window.Current.Activate();

            InitTitleBar();
            await Task.WhenAll(HideStatusBarAsync(), RegisterBackgroundTaskAsync());
            UpdatePrimaryTile();
            Completed?.Invoke(this, EventArgs.Empty);
        }
    }
}