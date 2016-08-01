using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.Extensions;

namespace BingoWallpaper.Uwp.Services
{
    public class BingoWallpaperNavigationService : INavigationService
    {
        public const string RootPageKey = "-- ROOT --";

        public const string UnknownPageKey = "-- UNKNOWN --";

        protected readonly Dictionary<string, Type> PagesByKey = new Dictionary<string, Type>();

        public string CurrentPageKey
        {
            get
            {
                lock (PagesByKey)
                {
                    var rootFrame = Window.Current.Content.GetFirstDescendantOfType<Frame>();

                    if (rootFrame.BackStackDepth == 0)
                    {
                        return RootPageKey;
                    }

                    if (rootFrame.Content == null)
                    {
                        return UnknownPageKey;
                    }

                    var currentType = rootFrame.Content.GetType();

                    if (PagesByKey.All(temp => temp.Value != currentType))
                    {
                        return UnknownPageKey;
                    }

                    var item = PagesByKey.FirstOrDefault(temp => temp.Value == currentType);

                    return item.Key;
                }
            }
        }

        public void Configure(string key, Type pageType)
        {
            lock (PagesByKey)
            {
                if (PagesByKey.ContainsKey(key))
                {
                    throw new ArgumentException("This key is already used: " + key);
                }

                if (PagesByKey.Any(temp => temp.Value == pageType))
                {
                    throw new ArgumentException("This type is already configured with key " + PagesByKey.First(temp => temp.Value == pageType).Key);
                }

                PagesByKey.Add(key, pageType);
            }
        }

        public void GoBack()
        {
            var rootFrame = Window.Current.Content.GetFirstDescendantOfType<Frame>();
            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            lock (PagesByKey)
            {
                if (PagesByKey.ContainsKey(pageKey) == false)
                {
                    throw new ArgumentException($"No such page: {pageKey}. Did you forget to call NavigationService.Configure?", nameof(pageKey));
                }

                var rootFrame = Window.Current.Content.GetFirstDescendantOfType<Frame>();
                rootFrame.Navigate(PagesByKey[pageKey], parameter);
            }
        }
    }
}