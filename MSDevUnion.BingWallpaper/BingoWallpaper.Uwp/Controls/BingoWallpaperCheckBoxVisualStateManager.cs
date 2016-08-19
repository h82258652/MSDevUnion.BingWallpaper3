using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BingoWallpaper.Uwp.Controls
{
    public class BingoWallpaperCheckBoxVisualStateManager : VisualStateManager
    {
        protected override bool GoToStateCore(Control control, FrameworkElement templateRoot, string stateName, VisualStateGroup group, VisualState state, bool useTransitions)
        {
            var commonStatesResult = false;
            if (stateName.Contains("Normal"))
            {
                commonStatesResult = GoToState(control, "Normal", useTransitions);
            }
            else if (stateName.Contains("PointerOver"))
            {
                commonStatesResult = GoToState(control, "PointerOver", useTransitions);
            }
            else if (stateName.Contains("Pressed"))
            {
                commonStatesResult = GoToState(control, "Pressed", useTransitions);
            }
            else if (stateName.Contains("Disabled"))
            {
                commonStatesResult = GoToState(control, "Disabled", useTransitions);
            }
            var checkStatesResult = false;
            if (stateName.Contains("Unchecked"))
            {
                checkStatesResult = GoToState(control, "Unchecked", useTransitions);
            }
            else if (stateName.Contains("Checked"))
            {
                checkStatesResult = GoToState(control, "Checked", useTransitions);
            }
            else if (stateName.Contains("Indeterminate"))
            {
                checkStatesResult = GoToState(control, "Indeterminate", useTransitions);
            }

            return commonStatesResult && checkStatesResult;
        }
    }
}