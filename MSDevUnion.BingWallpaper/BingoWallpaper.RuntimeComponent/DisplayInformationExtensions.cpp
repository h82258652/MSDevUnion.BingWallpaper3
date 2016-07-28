#include "pch.h"
#include "DisplayInformationExtensions.h"

using namespace BingoWallpaper::Utils;

typedef HMODULE WINAPI LoadLibraryExWPtr(_In_ LPCWSTR lpLibFileName, _Reserved_ HANDLE hFile, _In_ DWORD dwFlags);

typedef WINUSERAPI int WINAPI GetSystemMetricsPtr(int smIndex);

uint32 DisplayInformationExtensions::ScreenWidthInRawPixels::get()
{
	MEMORY_BASIC_INFORMATION info = {};
	if (VirtualQuery(VirtualQuery, &info, sizeof(info)))
	{
		auto kernelAddr = (HMODULE)info.AllocationBase;
		auto loadlibraryPtr = (int64_t)GetProcAddress(kernelAddr, "LoadLibraryExW");
		auto loadLibrary = (LoadLibraryExWPtr *)loadlibraryPtr;
		auto user32 = loadLibrary(L"user32.dll", nullptr, 0);
		auto getSystemMetrics = (GetSystemMetricsPtr *)GetProcAddress(user32, "GetSystemMetrics");
		return(getSystemMetrics(SM_CXSCREEN));
	}
	return 0;
}

uint32 DisplayInformationExtensions::ScreenHeightInRawPixels::get()
{
	MEMORY_BASIC_INFORMATION info = {};
	if (VirtualQuery(VirtualQuery, &info, sizeof(info)))
	{
		auto kernelAddr = (HMODULE)info.AllocationBase;
		auto loadlibraryPtr = (int64_t)GetProcAddress(kernelAddr, "LoadLibraryExW");
		auto loadLibrary = (LoadLibraryExWPtr*)loadlibraryPtr;
		auto user32 = loadLibrary(L"user32.dll", nullptr, 0);
		auto getSystemMetrics = (GetSystemMetricsPtr*)GetProcAddress(user32, "GetSystemMetrics");
		return(getSystemMetrics(SM_CYSCREEN));
	}
	return 0;
}