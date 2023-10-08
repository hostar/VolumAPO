#include "stdafx.h"
#include <string>
#define WIN32_LEAN_AND_MEAN
#include <windows.h>

#include "VolumAPO.h"
#include "ClassFactory.h"
#include "RegistryHelper.h"

using namespace std;

static HINSTANCE hModule;

BOOL WINAPI DllMain(HINSTANCE hModule, DWORD dwReason, void* lpReserved)
{
	if (dwReason == DLL_PROCESS_ATTACH)
		::hModule = hModule;

	return TRUE;
}

STDAPI DllCanUnloadNow()
{
	if (VolumAPO::instCount == 0 && ClassFactory::lockCount == 0)
		return S_OK;
	else
		return S_FALSE;
}

STDAPI DllGetClassObject(const CLSID& clsid, const IID& iid, void** ppv)
{
	if (clsid != __uuidof(VolumAPO))
		return CLASS_E_CLASSNOTAVAILABLE;

	ClassFactory* factory = new ClassFactory();
	if (factory == NULL)
		return E_OUTOFMEMORY;

	HRESULT hr = factory->QueryInterface(iid, ppv);
	factory->Release();

	return hr;
}

STDAPI DllRegisterServer()
{
	wchar_t filename[1024];
	GetModuleFileNameW(hModule, filename, sizeof(filename) / sizeof(wchar_t));

	HRESULT hr = RegisterAPO(VolumAPO::regProperties);
	if (FAILED(hr))
	{
		UnregisterAPO(__uuidof(VolumAPO));
		return 22;
	}

	wchar_t* guid;
	StringFromCLSID(__uuidof(VolumAPO), &guid);
	wstring guidString(guid);
	CoTaskMemFree(guid);

	HKEY keyHandle;
	RegCreateKeyExW(HKEY_LOCAL_MACHINE, (L"SOFTWARE\\Classes\\CLSID\\" + guidString).c_str(), 0, NULL, 0, KEY_SET_VALUE | KEY_WOW64_64KEY, NULL, &keyHandle, NULL);
	const wchar_t* value = L"VolumAPO";
	RegSetValueExW(keyHandle, L"", 0, REG_SZ, (const BYTE*)value, (DWORD)((wcslen(value) + 1) * sizeof(wchar_t)));
	RegCloseKey(keyHandle);

	RegCreateKeyExW(HKEY_LOCAL_MACHINE, (L"SOFTWARE\\Classes\\CLSID\\" + guidString + L"\\InprocServer32").c_str(), 0, NULL, 0, KEY_SET_VALUE | KEY_WOW64_64KEY, NULL, &keyHandle, NULL);
	value = filename;
	RegSetValueExW(keyHandle, L"", 0, REG_SZ, (const BYTE*)value, (DWORD)((wcslen(value) + 1) * sizeof(wchar_t)));
	value = L"Both";
	RegSetValueExW(keyHandle, L"ThreadingModel", 0, REG_SZ, (const BYTE*)value, (DWORD)((wcslen(value) + 1) * sizeof(wchar_t)));
	RegCloseKey(keyHandle);

	return S_OK;
}

STDAPI DllUnregisterServer()
{
	wchar_t* guid;
	StringFromCLSID(__uuidof(VolumAPO), &guid);
	wstring guidString(guid);
	CoTaskMemFree(guid);

	RegDeleteKeyExW(HKEY_LOCAL_MACHINE, (L"SOFTWARE\\Classes\\CLSID\\" + guidString + L"\\InprocServer32").c_str(), KEY_WOW64_64KEY, 0);
	RegDeleteKeyExW(HKEY_LOCAL_MACHINE, (L"SOFTWARE\\Classes\\CLSID\\" + guidString).c_str(), KEY_WOW64_64KEY, 0);

	HRESULT hr = UnregisterAPO(__uuidof(VolumAPO));

	return hr;
}

const wchar_t* DllRegTakeOwnership(const wchar_t* key, const wchar_t* name, const wchar_t* value, bool writeAsMultiSz)
{
	int result = 0;
	wstring msg;
	try
	{
		RegistryHelper::takeOwnership(key);
	}
	catch (RegistryException e)
	{
		msg = e.getMessage();
		result = 1;
	}

	try
	{
		RegistryHelper::makeWritable(key);
	}
	catch (RegistryException e)
	{
		msg = e.getMessage();
		result = 2;
	}

	try
	{
		if (writeAsMultiSz)
		{
			RegistryHelper::writeMultiValue(key, name, value);
		}
		else
		{
			RegistryHelper::writeValue(key, name, value);
		}
	}
	catch (RegistryException e)
	{
		msg = e.getMessage();
		result = 2;
	}

	/*
	wchar_t* strCopy = new wchar_t[msg.length() + 1];
	wcscpy(strCopy, msg.c_str());
	*/
	wchar_t* msgCopy = (wchar_t*)CoTaskMemAlloc((msg.length() + 1) * sizeof(wchar_t));
	wcscpy_s(msgCopy, msg.length() + 1, msg.c_str());

	return msgCopy;
}
