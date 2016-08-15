using System.Linq;

namespace BingoWallpaper.Uwp.Extensions
{
    public static class MathExtensions
    {
        public static double Min(params double[] parameters)
        {
            return parameters.Min();
        }
    }
}