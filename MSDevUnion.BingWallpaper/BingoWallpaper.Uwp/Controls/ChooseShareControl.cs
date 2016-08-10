using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace BingoWallpaper.Uwp.Controls
{
    [TemplatePart(Name = SinaWeiboControlTemplateName, Type = typeof(UIElement))]
    [TemplatePart(Name = WechatControlTemplateName, Type = typeof(UIElement))]
    [TemplatePart(Name = SystemShareControlTemplateName, Type = typeof(UIElement))]
    public sealed class ChooseShareControl : Control
    {
        private const string SinaWeiboControlTemplateName = "PART_SinaWeiboControl";

        private const string SystemShareControlTemplateName = "PART_SystemShareControl";

        private const string WechatControlTemplateName = "PART_WechatControl";

        private UIElement _sinaWeiboControl;

        private UIElement _systemShareControl;

        private UIElement _wechatControl;

        public ChooseShareControl()
        {
            DefaultStyleKey = typeof(ChooseShareControl);
        }

        public event EventHandler SinaWeiboSelected;

        public event EventHandler SystemShareSelected;

        public event EventHandler WechatSelected;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _sinaWeiboControl = (UIElement)GetTemplateChild(SinaWeiboControlTemplateName);
            Debug.Assert(_sinaWeiboControl != null);
            _sinaWeiboControl.Tapped += SinaWeiboControl_Tapped;

            _wechatControl = (UIElement)GetTemplateChild(WechatControlTemplateName);
            if (_wechatControl != null)
            {
                _wechatControl.Tapped += WechatControl_Tapped;
            }

            _systemShareControl = (UIElement)GetTemplateChild(SystemShareControlTemplateName);
            Debug.Assert(_systemShareControl != null);
            _systemShareControl.Tapped += SystemShareControl_Tapped;
        }

        private void SinaWeiboControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SinaWeiboSelected?.Invoke(this, EventArgs.Empty);
        }

        private void SystemShareControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SystemShareSelected?.Invoke(this, EventArgs.Empty);
        }

        private void WechatControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            WechatSelected?.Invoke(this, EventArgs.Empty);
        }
    }
}