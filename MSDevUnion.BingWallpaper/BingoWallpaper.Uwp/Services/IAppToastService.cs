namespace BingoWallpaper.Uwp.Services
{
    public interface IAppToastService
    {
        void ShowMessage(string message);

        void ShowError(string message);
    }
}