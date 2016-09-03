using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using WinRTXamlToolkit.AwaitableUI;
using WinRTXamlToolkit.Controls.Extensions;

namespace BingoWallpaper.Uwp.Helpers
{
    public static class ScrollViewerHelper
    {
        public static readonly DependencyProperty HorizontalScrollBarStyleProperty = DependencyProperty.RegisterAttached("HorizontalScrollBarStyle", typeof(Style), typeof(ScrollViewerHelper), new PropertyMetadata(default(Style), HorizontalScrollBarStyleChanged));

        public static readonly DependencyProperty VerticalScrollBarStyleProperty = DependencyProperty.RegisterAttached("VerticalScrollBarStyle", typeof(Style), typeof(ScrollViewerHelper), new PropertyMetadata(default(Style), VerticalScrollBarStyleChanged));

        public static Style GetHorizontalScrollBarStyle(FrameworkElement obj)
        {
            return (Style)obj.GetValue(HorizontalScrollBarStyleProperty);
        }

        public static Style GetVerticalScrollBarStyle(FrameworkElement obj)
        {
            return (Style)obj.GetValue(VerticalScrollBarStyleProperty);
        }

        public static void SetHorizontalScrollBarStyle(FrameworkElement obj, Style value)
        {
            obj.SetValue(HorizontalScrollBarStyleProperty, value);
        }

        public static void SetVerticalScrollBarStyle(FrameworkElement obj, Style value)
        {
            obj.SetValue(VerticalScrollBarStyleProperty, value);
        }

        private static async void HorizontalScrollBarStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (FrameworkElement)d;
            var value = (Style)e.NewValue;

            var horizontalScrollBar = obj as ScrollBar;
            if (horizontalScrollBar != null)
            {
                if (horizontalScrollBar.Orientation == Orientation.Horizontal)
                {
                    horizontalScrollBar.Style = value;
                }
            }
            else
            {
                await obj.WaitForLoadedAsync();
                var scrollViewer = obj as ScrollViewer ?? obj.GetFirstDescendantOfType<ScrollViewer>();
                if (scrollViewer != null)
                {
                    scrollViewer.ApplyTemplate();
                    horizontalScrollBar = scrollViewer.GetDescendantsOfType<ScrollBar>().FirstOrDefault(temp => temp.Orientation == Orientation.Horizontal);
                    if (horizontalScrollBar != null)
                    {
                        horizontalScrollBar.Style = value;
                    }
                }
            }
        }

        private static async void VerticalScrollBarStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (FrameworkElement)d;
            var value = (Style)e.NewValue;

            var verticalScrollBar = obj as ScrollBar;
            if (verticalScrollBar != null)
            {
                if (verticalScrollBar.Orientation == Orientation.Vertical)
                {
                    verticalScrollBar.Style = value;
                }
            }
            else
            {
                await obj.WaitForLoadedAsync();
                var scrollViewer = obj as ScrollViewer ?? obj.GetFirstDescendantOfType<ScrollViewer>();
                if (scrollViewer != null)
                {
                    scrollViewer.ApplyTemplate();
                    verticalScrollBar = scrollViewer.GetDescendantsOfType<ScrollBar>().FirstOrDefault(temp => temp.Orientation == Orientation.Vertical);
                    if (verticalScrollBar != null)
                    {
                        verticalScrollBar.Style = value;
                    }
                }
            }
        }
    }
}