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
            var systemAccentColor = (Color)Application.Current.Resources["SystemAccentColor"];
            titleBar.BackgroundColor = systemAccentColor;
            titleBar.ButtonBackgroundColor = systemAccentColor;
        }

        private static async Task RegisterBackgroundTaskAsync()
        {
            if (BackgroundTaskRegistration.AllTasks.Any(task => task.Value.Name == Constants.UpdateTileTaskName))
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
            var registration = builder.Register();
        }

        private async void SplashScreenImage_ImageOpened(object sender, RoutedEventArgs e)
        {
            // 图片加载完毕后激活当前窗口，系统 SplashScreen 将会消失。
            Window.Current.Activate();

            // 等待所有初始化执行完毕，最后的等待 1 秒纯粹是为了此扩展 SplashScreen 不太快消失。
            InitTitleBar();
            await Task.WhenAll(HideStatusBarAsync(), RegisterBackgroundTaskAsync(), UpdatePrimaryTileAsync(), Task.Delay(TimeSpan.FromSeconds(1)));
            Completed?.Invoke(this, EventArgs.Empty);
        }

        private async Task UpdatePrimaryTileAsync()
        {
            // TODO
            await Task.Delay(1000);
        }
    }
}