using BingoWallpaper.Uwp.Controls;
using BingoWallpaper.Uwp.Messages;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
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

        private Point CalculateElementRenderTransformOrigin(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            var renderTransformOrigin = new Point(0.5, 0.5);
            var itemsControl = ItemsControl.ItemsControlFromItemContainer(element);
            var panel = itemsControl?.ItemsPanelRoot as FirstDoubleSizePanel;
            if (panel != null)
            {
                var index = itemsControl.IndexFromContainer(element);
                var row = panel.GetRowFromIndex(index);
                var column = panel.GetColumnFromIndex(index);
                if (row == 0)
                {
                    renderTransformOrigin.Y = 0;
                }
                else if (row == panel.GetRowCount() - 1)
                {
                    renderTransformOrigin.Y = 1;
                }
                if (column == 0)
                {
                    renderTransformOrigin.X = 0;
                }
                else if (column == panel.GetColumnCount() - 1)
                {
                    renderTransformOrigin.X = 1;
                }

                // 修正第一个元素。
                if (index == 0 && panel.MaximumRowsOrColumns == 2)
                {
                    renderTransformOrigin.X = 0.5;
                }
            }
            return renderTransformOrigin;
        }

        private void FirstDoubleSizePanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var panel = (FirstDoubleSizePanel)sender;
            var width = e.NewSize.Width;
            panel.MaximumRowsOrColumns = Math.Min(Math.Max((int)(width / 210), 2), 6);
        }

        private void GridViewEx_ItemClick(object sender, ItemClickEventArgs e)
        {
            var wallpaper = e.ClickedItem;
            var itemsControl = (ItemsControl)sender;
            var element = (FrameworkElement)itemsControl.ContainerFromItem(wallpaper);
            Debug.Assert(element != null);
            var center = element.TransformToVisual(null).TransformPoint(new Point());
            center.X = center.X + element.ActualWidth / 2;
            center.Y = center.Y + element.ActualHeight / 2;

            // TODO process center in detail view.
            var parameter = new Dictionary<string, object>
            {
                ["Wallpaper"] = wallpaper,
                ["Center"] = center
            };

            Frame.Navigate(typeof(DetailView), parameter);
        }

        private void GridViewEx_ItemPointerEntered(object sender, ItemPointerEventArgs e)
        {
            if (e.Args.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
            {
                var element = e.Element;
                element.RenderTransformOrigin = CalculateElementRenderTransformOrigin(element);
                Canvas.SetZIndex(element, 2);
                var border = element.GetFirstDescendantOfType<Border>();
                var textBlock = border.GetFirstDescendantOfType<TextBlock>();
                var storyboard = new Storyboard();
                {
                    var animation = new DoubleAnimation()
                    {
                        To = 1.05,
                        Duration = TimeSpan.FromSeconds(0.15)
                    };
                    Storyboard.SetTarget(animation, element);
                    Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(ScaleTransform.ScaleX)");
                    storyboard.Children.Add(animation);
                }
                {
                    var animation = new DoubleAnimation()
                    {
                        To = 1.05,
                        Duration = TimeSpan.FromSeconds(0.15)
                    };
                    Storyboard.SetTarget(animation, element);
                    Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(ScaleTransform.ScaleY)");
                    storyboard.Children.Add(animation);
                }
                {
                    var animation = new DoubleAnimation()
                    {
                        To = border.ActualHeight,
                        Duration = TimeSpan.FromSeconds(0.3)
                    };
                    Storyboard.SetTarget(animation, textBlock);
                    Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(TranslateTransform.Y)");
                    storyboard.Children.Add(animation);
                }
                {
                    var animation = new ColorAnimation()
                    {
                        To = Colors.Transparent,
                        Duration = TimeSpan.FromSeconds(0.3)
                    };
                    Storyboard.SetTarget(animation, border);
                    Storyboard.SetTargetProperty(animation, "(Border.Background).(SolidColorBrush.Color)");
                    storyboard.Children.Add(animation);
                }
                storyboard.Begin();
            }
        }

        private void GridViewEx_ItemPointerExited(object sender, ItemPointerEventArgs e)
        {
            if (e.Args.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
            {
                var element = e.Element;
                Canvas.SetZIndex(element, 1);
                var border = element.GetFirstDescendantOfType<Border>();
                var textBlock = border.GetFirstDescendantOfType<TextBlock>();
                var storyboard = new Storyboard();
                {
                    var animation = new DoubleAnimation()
                    {
                        To = 1,
                        Duration = TimeSpan.FromSeconds(0.15)
                    };
                    Storyboard.SetTarget(animation, element);
                    Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(ScaleTransform.ScaleX)");
                    storyboard.Children.Add(animation);
                }
                {
                    var animation = new DoubleAnimation()
                    {
                        To = 1,
                        Duration = TimeSpan.FromSeconds(0.15)
                    };
                    Storyboard.SetTarget(animation, element);
                    Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(ScaleTransform.ScaleY)");
                    storyboard.Children.Add(animation);
                }
                {
                    var animation = new ObjectAnimationUsingKeyFrames()
                    {
                        EnableDependentAnimation = true
                    };
                    animation.KeyFrames.Add(new DiscreteObjectKeyFrame()
                    {
                        KeyTime = TimeSpan.FromSeconds(0.15),
                        Value = 0
                    });
                    Storyboard.SetTarget(animation, element);
                    Storyboard.SetTargetProperty(animation, "(Canvas.ZIndex)");
                    storyboard.Children.Add(animation);
                }
                {
                    var animation = new DoubleAnimation()
                    {
                        To = 0,
                        Duration = TimeSpan.FromSeconds(0.3)
                    };
                    Storyboard.SetTarget(animation, textBlock);
                    Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(TranslateTransform.Y)");
                    storyboard.Children.Add(animation);
                }
                if (border != null)
                {
                    var animation = new ColorAnimation()
                    {
                        To = Color.FromArgb(128, 128, 128, 128),
                        Duration = TimeSpan.FromSeconds(0.3)
                    };
                    Storyboard.SetTarget(animation, border);
                    Storyboard.SetTargetProperty(animation, "(Border.Background).(SolidColorBrush.Color)");
                    storyboard.Children.Add(animation);
                }
                storyboard.Begin();
            }
        }

        private void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<SelectedWallpaperCollectionChangingMessage>(this, message =>
            {
                var oldWallpaperCollection = message.OldWallpaperCollection;
                var newWallpaperCollection = message.NewWallpaperCollection;

                var oldYear = oldWallpaperCollection.Year;
                var newYear = newWallpaperCollection.Year;
                var oldMonth = oldWallpaperCollection.Month;
                var newMonth = newWallpaperCollection.Month;
                var oldTimeOffset = new DateTimeOffset(new DateTime(oldYear, oldMonth, 1));
                var newTimeOffset = new DateTimeOffset(new DateTime(newYear, newMonth, 1));

                NarrowOldMonthTextBlock.DataContext = oldMonth;
                WideOldMonthTextBlock.DataContext = oldMonth;
                if (oldTimeOffset < newTimeOffset)
                {
                    MonthIncreasedStoryboard.Begin();
                }
                else if (oldTimeOffset > newTimeOffset)
                {
                    MonthDecreasedStoryboard.Begin();
                }
            });
        }

        private void MainView_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this);
        }
    }
}