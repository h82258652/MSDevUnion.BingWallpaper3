using BingoWallpaper.Uwp.Views;
using System;
using UmengSDK;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace BingoWallpaper.Uwp
{
    public sealed partial class App
    {
        public App()
        {
            InitializeComponent();
            Resuming += OnResuming;
            Suspending += OnSuspending;

#if DEBUG
            // 下面语句用于测试其他语言。
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "";
#endif
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);

            await UmengAnalytics.StartTrackAsync(Constants.UmengAppKey);
        }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 将在启动应用程序以打开特定文件等情况下使用。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            var rootView = Window.Current.Content as RootView;
            if (rootView == null)
            {
                rootView = new RootView(e.SplashScreen);
                rootView.RootFrame.NavigationFailed += OnNavigationFailed;
                Window.Current.Content = rootView;
            }

            await UmengAnalytics.StartTrackAsync(Constants.UmengAppKey);
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private async void OnResuming(object sender, object e)
        {
            await UmengAnalytics.StartTrackAsync(Constants.UmengAppKey);
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            try
            {
                await UmengAnalytics.EndTrackAsync();
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}