#include "pch.h"
#include "User32.h"

using namespace BingoWallpaper::Utils::WinAPI;

typedef HMODULE WINAPI LoadLibraryExWPtr(_In_ LPCWSTR lpLibFileName, _Reserved_ HANDLE hFile, _In_ DWORD dwFlags);

typedef WINUSERAPI int WINAPI GetSystemMetricsPtr(_In_ int nIndex);

int User32::GetSystemMetrics(int nIndex) {
	MEMORY_BASIC_INFORMATION info = {};
	if (VirtualQuery(VirtualQuery, &info, sizeof(info)))
	{
		auto kernelAddr = (HMODULE)info.AllocationBase;
		auto loadlibraryPtr = (int64_t)GetProcAddress(kernelAddr, "LoadLibraryExW");
		auto loadLibrary = (LoadLibraryExWPtr *)loadlibraryPtr;
		auto user32 = loadLibrary(L"user32.dll", nullptr, 0);
		auto getSystemMetrics = (GetSystemMetricsPtr *)GetProcAddress(user32, "GetSystemMetrics");
		return getSystemMetrics(nIndex);
	}
	throw ref new Platform::Exception(-1, "NotSupportedException");
};