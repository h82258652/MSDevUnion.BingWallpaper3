using Newtonsoft.Json;

namespace BingoWallpaper.Models
{
    [JsonObject]
    public interface IImage
    {
        [JsonProperty("urlbase")]
        string UrlBase
        {
            get;
            set;
        }
    }
}