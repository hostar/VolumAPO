#include "stdafx.h"
#include "Logger.h"


#include <cstdarg>
#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include "RegistryHelper.h"

/*

This file was ripped from EqualizerAPO project

*/


using namespace std;

bool Logger::initialized = false;
wstring Logger::logPath;
bool Logger::enableTrace = false;
FILE* Logger::presetFP = NULL;
bool Logger::compact = false;
bool Logger::useConsoleColors = false;

void Logger::log(const char* file, int line, const void* caller, bool trace, const wchar_t* format, ...)
{
	if (!initialized)
	{
		// Do not try to initialize again, even in case of error
		initialized = true;

		wchar_t temp[255];
		GetTempPathW(sizeof(temp) / sizeof(wchar_t), temp);

		logPath = temp;
		logPath += L"VolumAPO.log";

		try
		{
			if (RegistryHelper::readValue(APP_REGPATH, L"EnableTrace") != L"false")
			{
				enableTrace = true;
			}
		}
		catch (RegistryException e)
		{
			//LogFStatic(L"%s", e.getMessage());
		}
	}

	if (trace && !enableTrace)
	{
		return;
	}

	FILE* fp;
	if (presetFP == NULL)
	{
		errno_t err = _wfopen_s(&fp, logPath.c_str(), L"at");
		if (err != 0)
			return;
	}
	else
	{
		fp = presetFP;
	}

	if (useConsoleColors)
	{
		HANDLE con = GetStdHandle(STD_OUTPUT_HANDLE);
		if (trace)
			SetConsoleTextAttribute(con, 2);// Set console color to green
		else
			SetConsoleTextAttribute(con, 12);// Set console color to red
	}

	if (!compact)
	{
		SYSTEMTIME ___st;
		GetLocalTime(&___st);
		DWORD threadId = GetCurrentThreadId();
		fwprintf(fp, L"%04d-%02d-%02d %02d:%02d:%02d.%03d %d %08X (%S:%d): ",
			___st.wYear, ___st.wMonth, ___st.wDay, ___st.wHour, ___st.wMinute, ___st.wSecond, ___st.wMilliseconds, threadId, (DWORD)(unsigned long long)caller, file, line);
	}

	if (trace)
		fwprintf(fp, L"(TRACE) ");

	va_list varArgs;
	va_start(varArgs, format);
	vfwprintf(fp, format, varArgs);
	va_end(varArgs);

	fwprintf(fp, L"\n");

	if (useConsoleColors)
	{
		HANDLE con = GetStdHandle(STD_OUTPUT_HANDLE);
		SetConsoleTextAttribute(con, 7); // Set console color to light grey (default)
	}

	if (presetFP == NULL)
		fclose(fp);
	else
		fflush(fp);
}

void Logger::reset()
{
	initialized = false;
	logPath = L"";
	enableTrace = false;
}

void Logger::set(FILE* fp, bool enableTrace, bool compact, bool useConsoleColors)
{
	Logger::initialized = true;

	Logger::presetFP = fp;
	Logger::enableTrace = enableTrace;
	Logger::compact = compact;
	Logger::useConsoleColors = useConsoleColors;
}
