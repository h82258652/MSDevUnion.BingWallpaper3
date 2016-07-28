using System.Threading.Tasks;
using Windows.Storage;

namespace BingoWallpaper.Services
{
    public interface IFileService
    {
        Task<StorageFile> PickerSaveFilePathAsync(string suggestedFileName);
    }
}