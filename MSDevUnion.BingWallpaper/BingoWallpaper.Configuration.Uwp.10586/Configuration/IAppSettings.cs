using System;
using System.ComponentModel;
using Windows.Storage;

namespace BingoWallpaper.Configuration
{
    public interface IAppSettings : INotifyPropertyChanged
    {
        bool Contains(string key, ApplicationDataLocality dataLocality);

        T Get<T>(string key, ApplicationDataLocality dataLocality, Func<T> defaultValue = null);

        bool Remove(string key, ApplicationDataLocality dataLocality);

        void Set<T>(string key, T value, ApplicationDataLocality dataLocality);
    }
}