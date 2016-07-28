#pragma once

namespace BingoWallpaper
{
	namespace Utils
	{
		public ref class DisplayInformationExtensions sealed
		{
		private:
			DisplayInformationExtensions()
			{
			}

		public:
			static property uint32 ScreenWidthInRawPixels
			{
				uint32 get();
			}

			static property uint32 ScreenHeightInRawPixels
			{
				uint32 get();
			}
		};
	}
}