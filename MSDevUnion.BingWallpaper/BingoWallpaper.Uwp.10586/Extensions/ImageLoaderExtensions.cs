using BingoWallpaper.Uwp.Controls;
using System;

namespace BingoWallpaper.Uwp.Extensions
{
    public static class ImageLoaderExtensions
    {
        public static string CalculateCacheSizeString(this IImageLoader imageLoader)
        {
            if (imageLoader == null)
            {
                throw new ArgumentNullException(nameof(imageLoader));
            }

            var b = imageLoader.CalculateCacheSize();
            if (b < 1024)
            {
                return string.Format("{0}B", b);
            }
            var kb = b / 1024.0;
            if (kb < 1024)
            {
                return string.Format("{0:F2}KB", kb);
            }
            var mb = kb / 1024.0;
            if (mb < 1024)
            {
                return string.Format("{0:F2}MB", mb);
            }
            var gb = mb / 1024.0;
            if (gb < 1024)
            {
                return string.Format("{0:F2}GB", gb);
            }
            var tb = gb / 1024.0;
            return string.Format("{0:F2}TB", tb);
        }
    }
}