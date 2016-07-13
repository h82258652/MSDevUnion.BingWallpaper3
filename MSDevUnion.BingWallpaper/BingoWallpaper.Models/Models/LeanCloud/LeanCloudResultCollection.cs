using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace BingoWallpaper.Models.LeanCloud
{
    [JsonObject]
    public class LeanCloudResultCollection<T> : LeanCloudResultBase, IEnumerable<T> where T : AVObject
    {
        [JsonProperty("results")]
        public IReadOnlyList<T> Results
        {
            get;
            set;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Results.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}