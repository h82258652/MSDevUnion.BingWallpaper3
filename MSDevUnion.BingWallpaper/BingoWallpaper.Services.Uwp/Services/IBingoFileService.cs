using System.Threading.Tasks;
using Windows.Storage;

namespace BingoWallpaper.Services
{
    public interface IBingoFileService
    {
        Task<StorageFile> PickerSaveFilePathAsync(string suggestedFileName);

        Task<bool> SaveImageAsync(string fileName, byte[] bytes);
    }
}