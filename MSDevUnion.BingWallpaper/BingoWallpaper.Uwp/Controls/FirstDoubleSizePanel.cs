using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BingoWallpaper.Uwp.Controls
{
    public class FirstDoubleSizePanel : Panel
    {
        public static readonly DependencyProperty MaximumRowsOrColumnsProperty = DependencyProperty.Register(nameof(MaximumRowsOrColumns), typeof(int), typeof(FirstDoubleSizePanel), new PropertyMetadata(3, MaximumRowsOrColumnsChanged));

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(FirstDoubleSizePanel), new PropertyMetadata(Orientation.Horizontal, OrientationChanged));

        public int MaximumRowsOrColumns
        {
            get
            {
                return (int)GetValue(MaximumRowsOrColumnsProperty);
            }
            set
            {
                SetValue(MaximumRowsOrColumnsProperty, value);
            }
        }

        public Orientation Orientation
        {
            get
            {
                return (Orientation)GetValue(OrientationProperty);
            }
            set
            {
                SetValue(OrientationProperty, value);
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Orientation == Orientation.Horizontal)
            {
                var columnWidth = finalSize.Width / MaximumRowsOrColumns;
                var firstElementSize = default(Size);
                for (var i = 0; i < Children.Count; i++)
                {
                    var child = Children[i];
                    if (i == 0)
                    {
                        // 第一个。
                        child.Arrange(new Rect(0, 0, columnWidth * 2, child.DesiredSize.Height));
                        firstElementSize = child.DesiredSize;
                    }
                    else if (i < MaximumRowsOrColumns - 1)
                    {
                        // 第一行。
                        var x = (i + 1) * columnWidth;
                        child.Arrange(new Rect(x, 0, columnWidth, firstElementSize.Height / 2));
                    }
                    else
                    {
                        // 剩下的。
                        var x = (i + 3) % MaximumRowsOrColumns * columnWidth;
                        var row = (i + 3) / MaximumRowsOrColumns;
                        var y = row * firstElementSize.Height / 2;
                        child.Arrange(new Rect(x, y, columnWidth, firstElementSize.Height / 2));
                    }
                }
            }
            else
            {
                var rowHeight = finalSize.Height / MaximumRowsOrColumns;
                var firstElementSize = default(Size);
                for (var i = 0; i < Children.Count; i++)
                {
                    var child = Children[i];
                    if (i == 0)
                    {
                        // 第一个。
                        child.Arrange(new Rect(0, 0, child.DesiredSize.Width, rowHeight * 2));
                        firstElementSize = child.DesiredSize;
                    }
                    else if (i < MaximumRowsOrColumns - 1)
                    {
                        // 第一列。
                        var y = (i + 1) * rowHeight;
                        child.Arrange(new Rect(0, y, firstElementSize.Width / 2, rowHeight));
                    }
                    else
                    {
                        // 剩下的。
                        var column = (i + 3) / MaximumRowsOrColumns;
                        var x = column * firstElementSize.Width / 2;
                        var y = (i + 3) % MaximumRowsOrColumns * rowHeight;
                        child.Arrange(new Rect(x, y, firstElementSize.Width / 2, rowHeight));
                    }
                }
            }

            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Orientation == Orientation.Horizontal)
            {
                var columnWidth = availableSize.Width / MaximumRowsOrColumns;
                var firstElementMeasureSize = default(Size);
                for (var i = 0; i < Children.Count; i++)
                {
                    var child = Children[i];
                    if (i == 0)
                    {
                        child.Measure(new Size(columnWidth * 2, availableSize.Height));
                        firstElementMeasureSize = child.DesiredSize;
                    }
                    else
                    {
                        child.Measure(new Size(columnWidth, firstElementMeasureSize.Height / 2));
                    }
                }
            }
            else
            {
                var rowHeight = availableSize.Height / MaximumRowsOrColumns;
                var firstElementMeasureSize = default(Size);
                for (var i = 0; i < Children.Count; i++)
                {
                    var child = Children[i];
                    if (i == 0)
                    {
                        child.Measure(new Size(availableSize.Width, rowHeight * 2));
                        firstElementMeasureSize = child.DesiredSize;
                    }
                    else
                    {
                        child.Measure(new Size(firstElementMeasureSize.Width / 2, rowHeight));
                    }
                }
            }

            var width = availableSize.Width;
            var height = availableSize.Height;
            width = double.IsPositiveInfinity(width) ? 0 : width;
            height = double.IsPositiveInfinity(height) ? 0 : height;
            return new Size(width, height);
        }

        private static void MaximumRowsOrColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = (int)e.NewValue;
            if (value < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(MaximumRowsOrColumns));
            }

            var obj = (FirstDoubleSizePanel)d;
            obj.InvalidateMeasure();
        }

        private static void OrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (Enum.IsDefined(typeof(Orientation), e.NewValue) == false)
            {
                throw new ArgumentOutOfRangeException(nameof(Orientation));
            }

            var obj = (FirstDoubleSizePanel)d;
            obj.InvalidateMeasure();
        }
    }
}