using BingoWallpaper.Uwp.Controls;
using System;
using Windows.Devices.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.AwaitableUI;
using WinRTXamlToolkit.Controls.Extensions;

namespace BingoWallpaper.Uwp.Views
{
    public sealed partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        private void FirstDoubleSizePanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var panel = (FirstDoubleSizePanel)sender;
            var width = e.NewSize.Width;
            panel.MaximumRowsOrColumns = Math.Max((int)(width / 320), 2);
        }

        private void GridViewEx_ItemPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
            {
                var element = (UIElement)sender;
                Canvas.SetZIndex(element, int.MaxValue);
                var textBlock = element.GetFirstDescendantOfType<TextBlock>();
                var border = element.GetFirstAncestorOfType<Border>();
                var storyboard = new Storyboard();
                {
                    var animation = new DoubleAnimation
                    {
                        To = 1.05,
                        Duration = TimeSpan.FromSeconds(0.1)
                    };
                    Storyboard.SetTarget(animation, element);
                    Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(ScaleTransform.ScaleX)");
                    storyboard.Children.Add(animation);
                }
                {
                    var animation = new DoubleAnimation
                    {
                        To = 1.05,
                        Duration = TimeSpan.FromSeconds(0.1)
                    };
                    Storyboard.SetTarget(animation, element);
                    Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(ScaleTransform.ScaleY)");
                    storyboard.Children.Add(animation);
                }
                {
                    var animation = new DoubleAnimation
                    {
                        To = border.ActualWidth,
                        Duration = TimeSpan.FromSeconds(0.1)
                    };
                    Storyboard.SetTarget(animation, textBlock);
                    Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(TranslateTransform.Y)");
                    storyboard.Children.Add(animation);
                }
                {
                    var animation = new DoubleAnimation
                    {
                        To = 0,
                        Duration = TimeSpan.FromSeconds(0.1)
                    };
                    Storyboard.SetTarget(animation, border);
                    Storyboard.SetTargetProperty(animation, "Opacity");
                    storyboard.Children.Add(animation);
                }
                storyboard.Begin();
            }
        }

        private async void GridViewEx_ItemPointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
            {
                var element = (UIElement)sender;
                Canvas.SetZIndex(element, 1);
                var textBlock = element.GetFirstDescendantOfType<TextBlock>();
                var border = element.GetFirstAncestorOfType<Border>();
                var storyboard = new Storyboard();
                {
                    var animation = new DoubleAnimation
                    {
                        To = 1,
                        Duration = TimeSpan.FromSeconds(0.1)
                    };
                    Storyboard.SetTarget(animation, element);
                    Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(ScaleTransform.ScaleX)");
                    storyboard.Children.Add(animation);
                }
                {
                    var animation = new DoubleAnimation
                    {
                        To = 1,
                        Duration = TimeSpan.FromSeconds(0.1)
                    };
                    Storyboard.SetTarget(animation, element);
                    Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(ScaleTransform.ScaleY)");
                    storyboard.Children.Add(animation);
                }
                {
                    var animation = new DoubleAnimation
                    {
                        To = 0,
                        Duration = TimeSpan.FromSeconds(0.1)
                    };
                    Storyboard.SetTarget(animation, textBlock);
                    Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(TranslateTransform.Y)");
                    storyboard.Children.Add(animation);
                }
                {
                    var animation = new DoubleAnimation
                    {
                        To = 1,
                        Duration = TimeSpan.FromSeconds(0.1)
                    };
                    Storyboard.SetTarget(animation, border);
                    Storyboard.SetTargetProperty(animation, "Opacity");
                    storyboard.Children.Add(animation);
                }
                await storyboard.BeginAsync();
                Canvas.SetZIndex(element, 0);
            }
        }
    }
}