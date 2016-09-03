using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.System;

namespace BingoWallpaper.Services
{
    public class StoreService : IStoreService
    {
        public async Task OpenCurrentAppReviewPageAsync()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?PFN=" + Package.Current.Id.FamilyName));
        }
    }
}