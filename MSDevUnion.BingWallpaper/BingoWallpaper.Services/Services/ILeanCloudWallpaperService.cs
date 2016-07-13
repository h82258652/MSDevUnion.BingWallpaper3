using BingoWallpaper.Models.LeanCloud;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public interface ILeanCloudWallpaperService : IWallpaperService
    {
        Task<Image> GetImageAsync(string objectId);

        Task<LeanCloudResultCollection<Image>> GetImagesAsync(IEnumerable<string> objectIds);
    }
}