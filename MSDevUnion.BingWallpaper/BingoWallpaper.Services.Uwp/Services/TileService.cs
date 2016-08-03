using BingoWallpaper.Models;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace BingoWallpaper.Services
{
    public class TileService : ITileService
    {
        private readonly IWallpaperService _wallpaperService;

        public TileService(IWallpaperService wallpaperService)
        {
            _wallpaperService = wallpaperService;
        }

        public void UpdatePrimaryTile(IImage image, string text)
        {
            var document = new XmlDocument();

            // tile 根节点。
            var tileeElement = document.CreateElement("tile");
            document.AppendChild(tileeElement);

            // visual 元素。
            var visualeElement = document.CreateElement("visual");
            tileeElement.AppendChild(visualeElement);

            // Medium, 150x150。
            {
                // binding。
                var bindingElement = document.CreateElement("binding");
                bindingElement.SetAttribute("template", "TileMedium");
                visualeElement.AppendChild(bindingElement);

                // image。
                var imageElement = document.CreateElement("image");
                imageElement.SetAttribute("src", _wallpaperService.GetUrl(image, new WallpaperSize(150, 150)));
                imageElement.SetAttribute("placement", "background");
                bindingElement.AppendChild(imageElement);

                // text。
                var textElement = document.CreateElement("text");
                textElement.AppendChild(document.CreateTextNode(text));
                textElement.SetAttribute("hint-wrap", "true");
                bindingElement.AppendChild(textElement);
            }

            // Wide，310x150。
            {
                // binding。
                var bindingElement = document.CreateElement("binding");
                bindingElement.SetAttribute("template", "TileWide");
                visualeElement.AppendChild(bindingElement);

                // image。
                var imageElement = document.CreateElement("image");
                imageElement.SetAttribute("src", _wallpaperService.GetUrl(image, new WallpaperSize(310, 150)));
                imageElement.SetAttribute("placement", "background");
                bindingElement.AppendChild(imageElement);

                // text。
                var textElement = document.CreateElement("text");
                textElement.AppendChild(document.CreateTextNode(text));
                textElement.SetAttribute("hint-wrap", "true");
                bindingElement.AppendChild(textElement);
            }

            var tileNotification = new TileNotification(document);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }
    }
}