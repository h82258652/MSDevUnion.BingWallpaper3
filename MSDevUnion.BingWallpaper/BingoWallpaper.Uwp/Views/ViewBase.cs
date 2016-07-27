using Windows.ApplicationModel;
using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Input;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace BingoWallpaper.Uwp.Views
{
    public abstract class ViewBase : BindablePage
    {
        private readonly SystemNavigationManager _systemNavigationManager;

        protected ViewBase()
        {
            if (DesignMode.DesignModeEnabled == false)
            {
                _systemNavigationManager = SystemNavigationManager.GetForCurrentView();
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
        }

        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            base.OnPointerReleased(e);

            var properties = e.GetCurrentPoint(null).Properties;
            switch (properties.PointerUpdateKind)
            {
                case PointerUpdateKind.XButton1Released:
                    if (Frame.CanGoBack)
                    {
                        e.Handled = true;
                        Frame.GoBack();
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
            if (Frame.CanGoBack && ApiInformation.IsTypePresent("Windows.Phone.UI.Input.BackPressedEventArgs"))
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }
    }
}