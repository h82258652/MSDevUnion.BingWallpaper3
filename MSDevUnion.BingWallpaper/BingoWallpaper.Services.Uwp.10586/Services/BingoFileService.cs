using BingoWallpaper.Configuration;
using BingoWallpaper.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace BingoWallpaper.Services
{
    public class BingoFileService : IBingoFileService
    {
        private readonly IBingoWallpaperSettings _settings;

        public BingoFileService(IBingoWallpaperSettings settings)
        {
            _settings = settings;
        }

        public async Task<StorageFile> PickerSaveFilePathAsync(string suggestedFileName)
        {
            var savePicker = new FileSavePicker();
            savePicker.FileTypeChoices.Add(".jpg", new List<string>()
            {
                ".jpg"
            });
            savePicker.SuggestedFileName = suggestedFileName;
            savePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            var file = await savePicker.PickSaveFileAsync();
            return file;
        }

        public async Task<bool> SaveImageAsync(string fileName, byte[] bytes)
        {
            var saveLocation = _settings.SelectedSaveLocation;
            StorageFile file = null;
            switch (saveLocation)
            {
                case SaveLocation.PictureLibrary:
                    file = await KnownFolders.PicturesLibrary.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                    break;

                case SaveLocation.ChooseEveryTime:
                    file = await PickerSaveFilePathAsync(fileName);
                    break;

                case SaveLocation.SavedPictures:
                    file = await KnownFolders.SavedPictures.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                    break;
            }
            if (file != null)
            {
                await FileIO.WriteBytesAsync(file, bytes);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}