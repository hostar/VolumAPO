#pragma once

#define TraceF(format, ...) Logger::log(__FILE__, __LINE__, this, true, format, __VA_ARGS__)

class Logger
{
public:
	static void log(const char* file, int line, const void* caller, bool trace, const wchar_t* format, ...);
	static void reset();
	static void set(FILE* fp, bool enableTrace, bool compact, bool useConsoleColors);

private:
	static bool initialized;
	static std::wstring logPath;
	static bool enableTrace;
	static FILE* presetFP;
	static bool compact;
	static bool useConsoleColors;
};

