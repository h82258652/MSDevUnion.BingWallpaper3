using System;

namespace BingoWallpaper.Uwp.Controls
{
    public class ExceptionEventArgs : EventArgs
    {
        internal ExceptionEventArgs(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage
        {
            get;
        }
    }
}