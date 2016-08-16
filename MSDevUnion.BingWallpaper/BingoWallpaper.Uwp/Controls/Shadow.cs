using BingoWallpaper.Uwp.Extensions;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Numerics;
using System.Reflection;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BingoWallpaper.Uwp.Controls
{
    [TemplatePart(Name = CanvasControlTemplateName, Type = typeof(CanvasControl))]
    public sealed class Shadow : ContentControl
    {
        public static readonly DependencyProperty BlurRadiusProperty = DependencyProperty.Register(nameof(BlurRadius), typeof(double), typeof(Shadow), new PropertyMetadata(2d, BlurRadiusChanged));

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(nameof(Color), typeof(Color), typeof(Shadow), new PropertyMetadata(Colors.Black, ColorChanged));

        public static readonly DependencyProperty DepthProperty = DependencyProperty.Register(nameof(Depth), typeof(double), typeof(Shadow), new PropertyMetadata(2d, DepthChanged));

        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(nameof(Direction), typeof(double), typeof(Shadow), new PropertyMetadata(270d, DirectionChanged));

        private const string CanvasControlTemplateName = "PART_CanvasControl";

        private CanvasControl _canvasControl;

        public Shadow()
        {
            DefaultStyleKey = typeof(Shadow);
            SizeChanged += Shadow_SizeChanged;
            Unloaded += Shadow_Unloaded;
        }

        public double BlurRadius
        {
            get
            {
                return (double)GetValue(BlurRadiusProperty);
            }
            set
            {
                SetValue(BlurRadiusProperty, value);
            }
        }

        public Color Color
        {
            get
            {
                return (Color)GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }

        public double Depth
        {
            get
            {
                return (double)GetValue(DepthProperty);
            }
            set
            {
                SetValue(DepthProperty, value);
            }
        }

        public double Direction
        {
            get
            {
                return (double)GetValue(DirectionProperty);
            }
            set
            {
                SetValue(DirectionProperty, value);
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _canvasControl = (CanvasControl)GetTemplateChild(CanvasControlTemplateName);
            _canvasControl.Draw += CanvasControl_Draw;
            ExpandCanvas();
        }

        private static void BlurRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Shadow)d;
            obj._canvasControl?.Invalidate();
        }

        private static void ColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Shadow)d;
            obj._canvasControl?.Invalidate();
        }

        private static void DepthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Shadow)d;
            obj._canvasControl?.Invalidate();
        }

        private static void DirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Shadow)d;
            obj._canvasControl?.Invalidate();
        }

        private void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (Content == null)
            {
                args.DrawingSession.Clear(sender.ClearColor);
            }
            else
            {
                ExpandCanvas();

                using (var cl = new CanvasCommandList(sender))
                {
                    using (var clds = cl.CreateDrawingSession())
                    {
                        var radius = (float)GetContentCornerRadius();
                        clds.FillRoundedRectangle(new Rect(0 - sender.Margin.Left, 0 - sender.Margin.Top, ActualWidth, ActualHeight), radius, radius, Colors.Black);
                    }

                    var translateX = (float)(Math.Cos(Math.PI / 180.0d * Direction) * Depth);
                    var translateY = 0 - (float)(Math.Sin(Math.PI / 180.0d * Direction) * Depth);

                    var finalEffect = new Transform2DEffect()
                    {
                        Source = new ShadowEffect()
                        {
                            Source = cl,
                            BlurAmount = (float)BlurRadius,
                            ShadowColor = GetShadowColor()
                        },
                        TransformMatrix = Matrix3x2.CreateTranslation(translateX, translateY)
                    };

                    args.DrawingSession.DrawImage(finalEffect);
                }
            }
        }

        private void ExpandCanvas()
        {
            if (_canvasControl != null)
            {
                _canvasControl.Margin = new Thickness(0 - (Depth + 10));
            }
        }

        private double GetContentCornerRadius()
        {
            if (Content != null)
            {
                var type = Content.GetType();
                var property = type.GetRuntimeProperty("CornerRadius");
                if (property != null && property.PropertyType == typeof(CornerRadius))
                {
                    var value = (CornerRadius)property.GetValue(Content);
                    return MathExtensions.Min(value.TopLeft, value.TopRight, value.BottomRight, value.BottomLeft);
                }
            }
            return 0;
        }

        private Color GetShadowColor()
        {
            var element = Content as UIElement;
            if (element == null)
            {
                return Colors.Transparent;
            }
            if (element.Visibility == Visibility.Collapsed)
            {
                return Colors.Transparent;
            }
            var alphaProportion = Math.Max(0, Math.Min(1, element.Opacity));
            return Color.FromArgb((byte)(Color.A * alphaProportion), Color.R, Color.G, Color.B);
        }

        private void Shadow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _canvasControl?.Invalidate();
        }

        private void Shadow_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_canvasControl != null)
            {
                _canvasControl.RemoveFromVisualTree();
                _canvasControl = null;
            }
        }
    }
}