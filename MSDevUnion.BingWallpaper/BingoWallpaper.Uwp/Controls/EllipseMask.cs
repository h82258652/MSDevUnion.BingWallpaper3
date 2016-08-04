using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;
using WinRTXamlToolkit.AwaitableUI;

namespace BingoWallpaper.Uwp.Controls
{
    [TemplatePart(Name = PathTemplateName, Type = typeof(Path))]
    public sealed class EllipseMask : Control
    {
        private const string PathTemplateName = "PART_Path";

        private Path _path;

        public EllipseMask()
        {
            DefaultStyleKey = typeof(EllipseMask);
        }

        public async Task LightOffAsync()
        {
            if (_path == null)
            {
                ApplyTemplate();
            }

            var storyboard = new Storyboard();
            {
                var animation = new DoubleAnimation()
                {
                    EnableDependentAnimation = true,
                    From = 50 * Math.Sqrt(2),
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.8)
                };
                Storyboard.SetTarget(animation, _path);
                Storyboard.SetTargetProperty(animation, "(Path.Data).(GeometryGroup.Children)[1].(EllipseGeometry.RadiusX)");
                storyboard.Children.Add(animation);
            }
            {
                var animation = new DoubleAnimation()
                {
                    EnableDependentAnimation = true,
                    From = 50 * Math.Sqrt(2),
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.8)
                };
                Storyboard.SetTarget(animation, _path);
                Storyboard.SetTargetProperty(animation, "(Path.Data).(GeometryGroup.Children)[1].(EllipseGeometry.RadiusY)");
                storyboard.Children.Add(animation);
            }
            await storyboard.BeginAsync();
        }

        public async Task LightOnAsync()
        {
            if (_path == null)
            {
                ApplyTemplate();
            }

            var storyboard = new Storyboard();
            {
                var animation = new DoubleAnimation()
                {
                    EnableDependentAnimation = true,
                    From = 0,
                    To = 50 * Math.Sqrt(2),
                    Duration = TimeSpan.FromSeconds(0.8)
                };
                Storyboard.SetTarget(animation, _path);
                Storyboard.SetTargetProperty(animation, "(Path.Data).(GeometryGroup.Children)[1].(EllipseGeometry.RadiusX)");
                storyboard.Children.Add(animation);
            }
            {
                var animation = new DoubleAnimation()
                {
                    EnableDependentAnimation = true,
                    From = 0,
                    To = 50 * Math.Sqrt(2),
                    Duration = TimeSpan.FromSeconds(0.8)
                };
                Storyboard.SetTarget(animation, _path);
                Storyboard.SetTargetProperty(animation, "(Path.Data).(GeometryGroup.Children)[1].(EllipseGeometry.RadiusY)");
                storyboard.Children.Add(animation);
            }
            await storyboard.BeginAsync();
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _path = (Path)GetTemplateChild(PathTemplateName);
        }
    }
}