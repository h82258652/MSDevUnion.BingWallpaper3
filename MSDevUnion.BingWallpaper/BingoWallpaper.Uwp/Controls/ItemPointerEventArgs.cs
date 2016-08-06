using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace BingoWallpaper.Uwp.Controls
{
    public class ItemPointerEventArgs
    {
        internal ItemPointerEventArgs(UIElement element, PointerRoutedEventArgs args)
        {
            Element = element;
            Args = args;
        }

        public PointerRoutedEventArgs Args
        {
            get;
        }

        public UIElement Element
        {
            get;
        }

        public object Item
        {
            get
            {
                var itemsControl = ItemsControl.ItemsControlFromItemContainer(Element);
                return itemsControl?.ItemFromContainer(Element);
            }
        }
    }
}