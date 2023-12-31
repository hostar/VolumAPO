#pragma once
#include <string>
#include <vector>
#include <stdexcept>
#define WIN32_LEAN_AND_MEAN
#include <windows.h>

/*
// {EACD2258-FCAC-4FF4-B36D-419E924A6D79}
const GUID EQUALIZERAPO_PRE_MIX_GUID = { 0xeacd2258, 0xfcac, 0x4ff4, {0xb3, 0x6d, 0x41, 0x9e, 0x92, 0x4a, 0x6d, 0x79} };
// {EC1CC9CE-FAED-4822-828A-82A81A6F018F}
const GUID EQUALIZERAPO_POST_MIX_GUID = { 0xec1cc9ce, 0xfaed, 0x4822, {0x82, 0x8a, 0x82, 0xa8, 0x1a, 0x6f, 0x01, 0x8f} };
*/

// {656BCCDE-BAD1-4BEF-960A-1FD82B332258}
static const GUID VOLUMAPO_GUID =
{ 0x656bccde, 0xbad1, 0x4bef, { 0x96, 0xa, 0x1f, 0xd8, 0x2b, 0x33, 0x22, 0x58 } };


#define APP_REGPATH L"HKEY_LOCAL_MACHINE\\SOFTWARE\\VolumAPO"
#define USER_REGPATH L"HKEY_CURRENT_USER\\SOFTWARE\\VolumAPO"

class RegistryHelper
{
public:
	static std::wstring getSystemErrorString(long status);
	static std::wstring toUpperCase(std::wstring& s);
	static std::wstring readValue(std::wstring key, std::wstring valuename);
	static unsigned long readDWORDValue(std::wstring key, std::wstring valuename);
	static std::vector<std::wstring> readMultiValue(std::wstring key, std::wstring valuename);
	static std::vector<unsigned char> readBinaryValue(std::wstring key, std::wstring valuename);
	static void writeValue(std::wstring key, std::wstring valuename, std::wstring value);
	static void writeDWORDValue(std::wstring key, std::wstring valuename, unsigned long value);
	static void writeMultiValue(std::wstring key, std::wstring valuename, std::wstring value);
	static void writeMultiValue(std::wstring key, std::wstring valuename, std::vector<std::wstring> values);
	static void deleteValue(std::wstring key, std::wstring valuename);
	static void createKey(std::wstring key);
	static void deleteKey(std::wstring key);
	static void makeWritable(std::wstring key);
	static void takeOwnership(std::wstring key);
	static ACCESS_MASK getFileAccessForUser(std::wstring path, unsigned long rid);
	static std::vector<std::wstring> enumSubKeys(std::wstring key);
	static bool keyExists(std::wstring key);
	static bool valueExists(std::wstring key, std::wstring valuename);
	static bool keyEmpty(std::wstring key);
	static void saveToFile(std::wstring key, std::vector<std::wstring> valuenames, std::wstring filepath);
	static std::wstring getGuidString(GUID guid);
	static bool isWindowsVersionAtLeast(unsigned major, unsigned minor);
	static HKEY openKey(const std::wstring& key, REGSAM samDesired);

private:
	static std::wstring splitKey(const std::wstring& key, HKEY* rootKey);

	static unsigned long windowsVersion;
};

class RegistryException
{
public:
	RegistryException(const std::wstring& message)
		: message(message) {}

	std::wstring getMessage()
	{
		return message;
	}

private:
	std::wstring message;
};