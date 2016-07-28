﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace BingoWallpaper.Services
{
    public class FileService : IFileService
    {
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
    }
}