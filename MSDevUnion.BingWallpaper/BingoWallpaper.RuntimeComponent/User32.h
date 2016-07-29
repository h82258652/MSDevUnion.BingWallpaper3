#pragma once

namespace BingoWallpaper
{
	namespace Utils
	{
		namespace WinAPI
		{
			public ref class User32 sealed
			{
			private:
				User32()
				{
				}

			public:
				static	int GetSystemMetrics(int nIndex);
			};
		}
	}
}