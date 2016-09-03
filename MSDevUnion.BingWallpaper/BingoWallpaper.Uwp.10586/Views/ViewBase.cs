﻿using BingoWallpaper.Uwp.Extensions;
using Windows.ApplicationModel;
using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.AwaitableUI;

namespace BingoWallpaper.Uwp.Views
{
    public abstract class ViewBase : BindablePage
    {
        public static readonly DependencyProperty EnterStoryboardProperty = DependencyProperty.Register(nameof(EnterStoryboard), typeof(Storyboard), typeof(ViewBase), new PropertyMetadata(default(Storyboard)));

        public static readonly DependencyProperty LeaveStoryboardProperty = DependencyProperty.Register(nameof(LeaveStoryboard), typeof(Storyboard), typeof(ViewBase), new PropertyMetadata(default(Storyboard)));

        private readonly SystemNavigationManager _systemNavigationManager;

        private bool _isLeaving;

        protected ViewBase()
        {
            if (DesignMode.DesignModeEnabled == false)
            {
                _systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            }

            base.Loaded += ViewBase_Loaded;
        }

        public new event RoutedEventHandler Loaded;

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

        private static ContentControl PreviousPageContainer
        {
            get
            {
                var rootView = (RootView)Window.Current.Content;
                return rootView.PreviousPageContainer;
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

            var coreWindow = Window.Current.CoreWindow;
            coreWindow.PointerReleased -= CurrentWindow_PointerReleased;

            FrameExtensions.SetPreviousPage(Frame, this);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _systemNavigationManager.AppViewBackButtonVisibility = Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            _systemNavigationManager.BackRequested += SystemNavigationManager_BackRequested;

            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }

            var coreWindow = Window.Current.CoreWindow;
            coreWindow.PointerReleased += CurrentWindow_PointerReleased;

            if (e.NavigationMode == NavigationMode.Back)
            {
                var previousPage = FrameExtensions.GetPreviousPage(Frame);
                if (previousPage != null)
                {
                    previousPage._isLeaving = true;
                    PreviousPageContainer.Content = previousPage;
                    await previousPage.WaitForLoadedAsync();
                    previousPage.PrepareLeaveStoryboard();
                    var leaveStoryboard = previousPage.LeaveStoryboard;
                    if (leaveStoryboard != null)
                    {
                        await leaveStoryboard.BeginAsync();
                    }
                    PreviousPageContainer.Content = null;
                    FrameExtensions.SetPreviousPage(Frame, null);
                    previousPage._isLeaving = false;
                }
            }
        }

        protected virtual void PrepareLeaveStoryboard()
        {
        }

        private void CurrentWindow_PointerReleased(CoreWindow sender, PointerEventArgs args)
        {
            switch (args.CurrentPoint.Properties.PointerUpdateKind)
            {
                case PointerUpdateKind.XButton1Released:
                    if (Frame.CanGoBack)
                    {
                        args.Handled = true;
                        Frame.GoBack();
                    }
                    break;

                case PointerUpdateKind.XButton2Released:
                    if (Frame.CanGoForward)
                    {
                        args.Handled = true;
                        Frame.GoForward();
                    }
                    break;
            }
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.BackPressedEventArgs"))
            {
                if (Frame.CanGoBack)
                {
                    e.Handled = true;
                    Frame.GoBack();
                }
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

        private void ViewBase_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isLeaving == false)
            {
                Loaded?.Invoke(sender, e);
                EnterStoryboard?.Begin();
            }
        }
    }
}