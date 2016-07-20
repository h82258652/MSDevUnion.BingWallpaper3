using System;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace BingoWallpaper.Uwp.Extensions
{
    public static class FrameworkElementExtensions
    {
        public static bool IsPointerOver(this FrameworkElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            var currentWindow = Window.Current;
            var rootVisual = currentWindow.Content;
            if (rootVisual == null)
            {
                return false;
            }
            var pointerPosition = currentWindow.CoreWindow.PointerPosition;
            var bounds = currentWindow.Bounds;
            pointerPosition = new Point(pointerPosition.X - bounds.X, pointerPosition.Y - bounds.Y);

            var elementRect = element.TransformToVisual(rootVisual).TransformBounds(new Rect(0, 0, element.ActualWidth, element.ActualHeight));
            return elementRect.Contains(pointerPosition);
        }
    }
}