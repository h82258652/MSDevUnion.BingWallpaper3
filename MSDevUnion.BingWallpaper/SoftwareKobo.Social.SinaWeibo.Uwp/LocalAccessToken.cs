using System;
using Windows.Storage;

namespace SoftwareKobo.Social.SinaWeibo
{
    internal static class LocalAccessToken
    {
        internal static DateTime ExpiresAt
        {
            get
            {
                return (DateTime)DataContainer.Values["ExpiresAt"];
            }
            set
            {
                DataContainer.Values["ExpiresAt"] = value;
            }
        }

        internal static bool IsUseable
        {
            get
            {
                if (Value == null)
                {
                    return false;
                }
                if (Uid == null)
                {
                    return false;
                }
                return ExpiresAt > DateTime.Now;
            }
        }

        internal static string Uid
        {
            get
            {
                return (string)DataContainer.Values["Uid"];
            }
            set
            {
                DataContainer.Values["Uid"] = value;
            }
        }

        internal static string Value
        {
            get
            {
                return (string)DataContainer.Values["AccessToken"];
            }
            set
            {
                DataContainer.Values["AccessToken"] = value;
            }
        }

        private static ApplicationDataContainer DataContainer => ApplicationData.Current.LocalSettings.CreateContainer(Constants.LocalAccessTokenDataContainerName, ApplicationDataCreateDisposition.Always);
    }
}