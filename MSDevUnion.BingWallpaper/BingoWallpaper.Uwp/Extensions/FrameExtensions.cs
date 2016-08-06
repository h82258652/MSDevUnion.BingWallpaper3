using BingoWallpaper.Uwp.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BingoWallpaper.Uwp.Extensions
{
    internal static class FrameExtensions
    {
        internal static readonly DependencyProperty PreviousPageProperty = DependencyProperty.RegisterAttached("PreviousPage", typeof(ViewBase), typeof(FrameExtensions), new PropertyMetadata(default(ViewBase)));

        internal static void SetPreviousPage(Frame obj, ViewBase value)
        {
            obj.SetValue(PreviousPageProperty, value);
        }

        internal static ViewBase GetPreviousPage(Frame obj)
        {
            return (ViewBase)obj.GetValue(PreviousPageProperty);
        }
    }
}