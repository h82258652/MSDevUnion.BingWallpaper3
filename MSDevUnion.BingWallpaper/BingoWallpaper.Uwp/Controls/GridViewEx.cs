using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace BingoWallpaper.Uwp.Controls
{
    public delegate void ItemPointerEventHandler(object sender, ItemPointerEventArgs e);

    public class GridViewEx : GridView
    {
        public event ItemPointerEventHandler ItemPointerEntered;

        public event ItemPointerEventHandler ItemPointerExited;

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            var gridViewItem = (GridViewItem)element;
            gridViewItem.PointerEntered -= GridViewItem_PointerEntered;
            gridViewItem.PointerExited -= GridViewItem_PointerExited;
            base.ClearContainerForItemOverride(element, item);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            var gridViewItem = (GridViewItem)base.GetContainerForItemOverride();
            gridViewItem.PointerEntered += GridViewItem_PointerEntered;
            gridViewItem.PointerExited += GridViewItem_PointerExited;
            return gridViewItem;
        }

        private void GridViewItem_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ItemPointerEntered?.Invoke(this, new ItemPointerEventArgs((UIElement)sender, e));
        }

        private void GridViewItem_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ItemPointerExited?.Invoke(this, new ItemPointerEventArgs((UIElement)sender, e));
        }
    }
}