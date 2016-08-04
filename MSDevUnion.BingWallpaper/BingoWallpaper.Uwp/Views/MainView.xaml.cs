﻿using BingoWallpaper.Uwp.Controls;
using System;
using Windows.Devices.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
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
                Canvas.SetZIndex(element, 2);
                var border = element.GetFirstDescendantOfType<Border>();
                var textBlock = border.GetFirstDescendantOfType<TextBlock>();
                var storyboard = new Storyboard();
                {
                    var animation = new DoubleAnimation()
                    {
                        To = 1.05,
                        Duration = TimeSpan.FromSeconds(0.1)
                    };
                    Storyboard.SetTarget(animation, element);
                    Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(ScaleTransform.ScaleX)");
                    storyboard.Children.Add(animation);
                }
                {
                    var animation = new DoubleAnimation()
                    {
                        To = 1.05,
                        Duration = TimeSpan.FromSeconds(0.1)
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

        private void GridViewEx_ItemPointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
            {
                var element = (UIElement)sender;
                Canvas.SetZIndex(element, 1);
                var border = element.GetFirstDescendantOfType<Border>();
                var textBlock = border.GetFirstDescendantOfType<TextBlock>();
                var storyboard = new Storyboard();
                {
                    var animation = new DoubleAnimation()
                    {
                        To = 1,
                        Duration = TimeSpan.FromSeconds(0.1)
                    };
                    Storyboard.SetTarget(animation, element);
                    Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(ScaleTransform.ScaleX)");
                    storyboard.Children.Add(animation);
                }
                {
                    var animation = new DoubleAnimation()
                    {
                        To = 1,
                        Duration = TimeSpan.FromSeconds(0.1)
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
                        KeyTime = TimeSpan.FromSeconds(0.1),
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
    }
}