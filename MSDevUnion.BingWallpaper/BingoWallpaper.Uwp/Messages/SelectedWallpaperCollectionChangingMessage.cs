using BingoWallpaper.Models.LeanCloud;
using GalaSoft.MvvmLight.Messaging;
using System;

namespace BingoWallpaper.Uwp.Messages
{
    public class SelectedWallpaperCollectionChangingMessage : MessageBase
    {
        internal SelectedWallpaperCollectionChangingMessage(WallpaperCollection oldWallpaperCollection, WallpaperCollection newWallpaperCollection)
        {
            if (oldWallpaperCollection == null)
            {
                throw new ArgumentNullException(nameof(oldWallpaperCollection));
            }
            if (newWallpaperCollection == null)
            {
                throw new ArgumentNullException(nameof(newWallpaperCollection));
            }

            OldWallpaperCollection = oldWallpaperCollection;
            NewWallpaperCollection = newWallpaperCollection;
        }

        public WallpaperCollection OldWallpaperCollection
        {
            get;
        }

        public WallpaperCollection NewWallpaperCollection
        {
            get;
        }
    }
}