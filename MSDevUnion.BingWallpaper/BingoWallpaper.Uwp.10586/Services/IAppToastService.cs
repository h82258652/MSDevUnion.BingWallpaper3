namespace BingoWallpaper.Uwp.Services
{
    public interface IAppToastService
    {
        void ShowError(string message);

        void ShowInformation(string message);

        void ShowMessage(string message);

        void ShowWarning(string message);
    }
}