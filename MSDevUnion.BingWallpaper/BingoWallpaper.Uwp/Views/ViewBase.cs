using System;
using Windows.ApplicationModel;
using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace BingoWallpaper.Uwp.Views
{
    public abstract class ViewBase : BindablePage
    {
        public static readonly DependencyProperty EnterStoryboardProperty = DependencyProperty.Register(nameof(EnterStoryboard), typeof(Storyboard), typeof(ViewBase), new PropertyMetadata(default(Storyboard)));

        public static readonly DependencyProperty LeaveStoryboardProperty = DependencyProperty.Register(nameof(LeaveStoryboard), typeof(Storyboard), typeof(ViewBase), new PropertyMetadata(default(Storyboard)));

        private readonly SystemNavigationManager _systemNavigationManager;

        protected ViewBase()
        {
            if (DesignMode.DesignModeEnabled == false)
            {
                _systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            }
        }

        public Storyboard EnterStoryboard
        {
            get
            {
                return (Storyboard)GetValue(EnterStoryboardProperty);
            }
            set
            {
                SetValue(EnterStoryboardProperty, value);
            }
        }

        public Storyboard LeaveStoryboard
        {
            get
            {
                return (Storyboard)GetValue(LeaveStoryboardProperty);
            }
            set
            {
                SetValue(LeaveStoryboardProperty, value);
            }
        }

        protected virtual void GoBack(ref bool isHandled)
        {
            if (Frame.CanGoBack)
            {
                isHandled = true;
                if (LeaveStoryboard != null)
                {
                    EventHandler<object> handler = null;
                    handler = (sender, e) =>
                    {
                        LeaveStoryboard.Completed -= handler;
                        if (Frame.CanGoBack)
                        {
                            Frame.GoBack();
                        }
                    };
                    LeaveStoryboard.Completed += handler;
                    LeaveStoryboard.Begin();
                }
                else
                {
                    if (Frame.CanGoBack)
                    {
                        Frame.GoBack();
                    }
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            _systemNavigationManager.AppViewBackButtonVisibility = Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            _systemNavigationManager.BackRequested -= SystemNavigationManager_BackRequested;

            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _systemNavigationManager.AppViewBackButtonVisibility = Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            _systemNavigationManager.BackRequested += SystemNavigationManager_BackRequested;

            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }

            EnterStoryboard?.Begin();
        }

        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            base.OnPointerReleased(e);

            var properties = e.GetCurrentPoint(null).Properties;
            switch (properties.PointerUpdateKind)
            {
                case PointerUpdateKind.XButton1Released:
                    {
                        var isHandled = e.Handled;
                        GoBack(ref isHandled);
                        e.Handled = isHandled;
                    }
                    break;

                case PointerUpdateKind.XButton2Released:
                    if (Frame.CanGoForward)
                    {
                        e.Handled = true;
                        Frame.GoForward();
                    }
                    break;
            }
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.BackPressedEventArgs"))
            {
                var isHandled = e.Handled;
                GoBack(ref isHandled);
                e.Handled = isHandled;
            }
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            var isHandled = e.Handled;
            GoBack(ref isHandled);
            e.Handled = isHandled;
        }
    }
}