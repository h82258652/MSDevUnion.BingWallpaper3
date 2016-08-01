using System;
using Windows.UI.Xaml;

namespace BingoWallpaper.Uwp.Controls
{
    public partial class BindableMargin
    {
        public static readonly DependencyProperty BottomProperty = DependencyProperty.Register(nameof(Bottom), typeof(double), typeof(BindableMargin), new PropertyMetadata(default(double), BottomChanged));

        public static readonly DependencyProperty LeftProperty = DependencyProperty.Register(nameof(Left), typeof(double), typeof(BindableMargin), new PropertyMetadata(default(double), LeftChanged));

        public static readonly DependencyProperty RightProperty = DependencyProperty.Register(nameof(Right), typeof(double), typeof(BindableMargin), new PropertyMetadata(default(double), RightChanged));

        public static readonly DependencyProperty TopProperty = DependencyProperty.Register(nameof(Top), typeof(double), typeof(BindableMargin), new PropertyMetadata(default(double), TopChanged));

        private readonly FrameworkElement _owner;

        public BindableMargin(FrameworkElement owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            _owner = owner;
        }

        public double Bottom
        {
            get
            {
                return (double)GetValue(BottomProperty);
            }
            set
            {
                SetValue(BottomProperty, value);
            }
        }

        public double Left
        {
            get
            {
                return (double)GetValue(LeftProperty);
            }
            set
            {
                SetValue(LeftProperty, value);
            }
        }

        public double Right
        {
            get
            {
                return (double)GetValue(RightProperty);
            }
            set
            {
                SetValue(RightProperty, value);
            }
        }

        public double Top
        {
            get
            {
                return (double)GetValue(TopProperty);
            }
            set
            {
                SetValue(TopProperty, value);
            }
        }

        private static void BottomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (BindableMargin)d;
            var value = (double)e.NewValue;

            var owner = obj._owner;
            var margin = owner.Margin;
            margin.Bottom = value;
            owner.Margin = margin;
        }

        private static void LeftChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (BindableMargin)d;
            var value = (double)e.NewValue;

            var owner = obj._owner;
            var margin = owner.Margin;
            margin.Left = value;
            owner.Margin = margin;
        }

        private static void RightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (BindableMargin)d;
            var value = (double)e.NewValue;

            var owner = obj._owner;
            var margin = owner.Margin;
            margin.Right = value;
            owner.Margin = margin;
        }

        private static void TopChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (BindableMargin)d;
            var value = (double)e.NewValue;

            var owner = obj._owner;
            var margin = owner.Margin;
            margin.Top = value;
            owner.Margin = margin;
        }
    }
}