using BingoWallpaper.Uwp.Messages;
using GalaSoft.MvvmLight.Messaging;
using System;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.AwaitableUI;

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
            var extendedSplashScreenView = new ExtendedSplashScreenView(splashScreen);
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

        private void ChooseShareControl_SinaWeiboSelected(object sender, EventArgs e)
        {
            CloseChooseShareGrid();

            Messenger.Default.Send(new SinaWeiboSelectedMessage());
        }

        private void ChooseShareControl_SystemShareSelected(object sender, EventArgs e)
        {
            CloseChooseShareGrid();

            Messenger.Default.Send(new SystemShareSelectedMessage());
        }

        private void ChooseShareControl_WechatSelected(object sender, EventArgs e)
        {
            CloseChooseShareGrid();

            Messenger.Default.Send(new WechatSelectedMessage());
        }

        private void ChooseShareGrid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
            CloseChooseShareGrid();
        }

        private async void CloseChooseShareGrid()
        {
            var storyboard = new Storyboard();
            {
                var animation = new DoubleAnimation()
                {
                    From = 1,
                    To = 0.2,
                    Duration = TimeSpan.FromSeconds(0.25)
                };
                Storyboard.SetTarget(animation, ChooseShareControl);
                Storyboard.SetTargetProperty(animation, "Opacity");
                storyboard.Children.Add(animation);
            }
            {
                var animation = new DoubleAnimation()
                {
                    From = 0,
                    To = 150,
                    Duration = TimeSpan.FromSeconds(0.25),
                    EasingFunction = new QuinticEase()
                    {
                        EasingMode = EasingMode.EaseOut
                    }
                };
                Storyboard.SetTarget(animation, ChooseShareControl);
                Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(TranslateTransform.Y)");
                storyboard.Children.Add(animation);
            }
            await storyboard.BeginAsync();
            ChooseShareGrid.Visibility = Visibility.Collapsed;
        }

        private void RootFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (ChooseShareGrid.Visibility == Visibility.Visible)
            {
                e.Cancel = true;
                CloseChooseShareGrid();
            }
        }

        private void RootView_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<OpenSharePopupMessage>(this, message =>
            {
                ChooseShareGrid.Visibility = Visibility.Visible;
                var storyboard = new Storyboard();
                {
                    var animation = new DoubleAnimation()
                    {
                        From = 0.2,
                        To = 1,
                        Duration = TimeSpan.FromSeconds(0.25)
                    };
                    Storyboard.SetTarget(animation, ChooseShareControl);
                    Storyboard.SetTargetProperty(animation, "Opacity");
                    storyboard.Children.Add(animation);
                }
                {
                    var animation = new DoubleAnimation()
                    {
                        From = 150,
                        To = 0,
                        Duration = TimeSpan.FromSeconds(0.25),
                        EasingFunction = new QuinticEase()
                        {
                            EasingMode = EasingMode.EaseIn
                        }
                    };
                    Storyboard.SetTarget(animation, ChooseShareControl);
                    Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(TranslateTransform.Y)");
                    storyboard.Children.Add(animation);
                }
                storyboard.Begin();
            });
        }

        private void RootView_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this);
        }
    }
}