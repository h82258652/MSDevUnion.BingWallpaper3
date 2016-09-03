namespace BingoWallpaper.Services
{
    public interface ILeanCloudWallpaperServiceWithCache : ILeanCloudWallpaperService
    {
        void DeleteAllCache();

        string GetCacheFolderPath();

        long CalculateSize();
    }
}