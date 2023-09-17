#include "stdafx.h"
#include <sstream>

#include "VolumAPO.h"
#include "Logger.h"
#include "RegistryHelper.h"

//#include <Windows.h>
//#include <stdio.h>
using namespace std;

int (WINAPIV* __vsnprintf)(char*, size_t, const char*, va_list) = _vsnprintf;
int (WINAPIV* __vsnwprintf)(wchar_t*, size_t, const wchar_t*, va_list) = _vsnwprintf;

long VolumAPO::instCount = 0;
const CRegAPOProperties<1> VolumAPO::regProperties(
	 __uuidof(VolumAPO), L"VolumAPO", L"", 1, 0, __uuidof(IAudioProcessingObject),
	(APO_FLAG)(APO_FLAG_SAMPLESPERFRAME_MUST_MATCH | APO_FLAG_FRAMESPERSECOND_MUST_MATCH | APO_FLAG_BITSPERSAMPLE_MUST_MATCH | APO_FLAG_INPLACE));

VolumAPO::VolumAPO(IUnknown* pUnkOuter)
	: CBaseAudioProcessingObject(regProperties)
{
	refCount = 1;
	if (pUnkOuter != NULL)
		this->pUnkOuter = pUnkOuter;
	else
		this->pUnkOuter = reinterpret_cast<IUnknown*>(static_cast<INonDelegatingUnknown*>(this));

	InterlockedIncrement(&instCount);
}

VolumAPO::~VolumAPO()
{
	InterlockedDecrement(&instCount);
}

HRESULT VolumAPO::QueryInterface(const IID& iid, void** ppv)
{
	return pUnkOuter->QueryInterface(iid, ppv);
}

ULONG VolumAPO::AddRef()
{
	return pUnkOuter->AddRef();
}

ULONG VolumAPO::Release()
{
	return pUnkOuter->Release();
}

HRESULT VolumAPO::GetLatency(HNSTIME* pTime)
{
	if (!pTime)
		return E_POINTER;

	if (!m_bIsLocked)
		return APOERR_ALREADY_UNLOCKED;

	*pTime = 0;

	return S_OK;
}

HRESULT VolumAPO::Initialize(UINT32 cbDataSize, BYTE* pbyData)
{
	if ((NULL == pbyData) && (0 != cbDataSize))
		return E_INVALIDARG;
	if ((NULL != pbyData) && (0 == cbDataSize))
		return E_POINTER;
	if (cbDataSize != sizeof(APOInitSystemEffects))
		return E_INVALIDARG;

	return S_OK;
}

HRESULT VolumAPO::IsInputFormatSupported(IAudioMediaType* pOutputFormat,
	IAudioMediaType* pRequestedInputFormat, IAudioMediaType** ppSupportedInputFormat)
{
	if (!pRequestedInputFormat)
		return E_POINTER;

	UNCOMPRESSEDAUDIOFORMAT inFormat;
	HRESULT hr = pRequestedInputFormat->GetUncompressedAudioFormat(&inFormat);
	if (FAILED(hr))
	{
		return hr;
	}

	UNCOMPRESSEDAUDIOFORMAT outFormat;
	hr = pOutputFormat->GetUncompressedAudioFormat(&outFormat);
	if (FAILED(hr))
	{
		return hr;
	}

	hr = CBaseAudioProcessingObject::IsInputFormatSupported(pOutputFormat, pRequestedInputFormat, ppSupportedInputFormat);

	return hr;
}

HRESULT VolumAPO::LockForProcess(UINT32 u32NumInputConnections,
	APO_CONNECTION_DESCRIPTOR** ppInputConnections, UINT32 u32NumOutputConnections,
	APO_CONNECTION_DESCRIPTOR** ppOutputConnections)
{
	HRESULT hr;

	UNCOMPRESSEDAUDIOFORMAT outFormat;
	hr = ppOutputConnections[0]->pFormat->GetUncompressedAudioFormat(&outFormat);
	if (FAILED(hr))
		return hr;

	hr = CBaseAudioProcessingObject::LockForProcess(u32NumInputConnections, ppInputConnections,
			u32NumOutputConnections, ppOutputConnections);
	if (FAILED(hr))
		return hr;

	channelCount = outFormat.dwSamplesPerFrame;

	return hr;
}

HRESULT VolumAPO::UnlockForProcess()
{
	return CBaseAudioProcessingObject::UnlockForProcess();
}

float VolumAPO::SafeStringToFloat(wstring str, float defaultValue)
{
	float result = defaultValue;
	try {
		// Use std::stof to convert the string to a float
		result = std::stof(str);
	}
	catch (const std::invalid_argument& e) {
		return result;
	}
	catch (const std::out_of_range& e) {
		return result;
	}
	catch (...) {
		return result;
	}

	return result;
}

#pragma AVRT_CODE_BEGIN
void VolumAPO::APOProcess(UINT32 u32NumInputConnections,
	APO_CONNECTION_PROPERTY** ppInputConnections, UINT32 u32NumOutputConnections,
	APO_CONNECTION_PROPERTY** ppOutputConnections)
{
	switch (ppInputConnections[0]->u32BufferFlags)
	{
	case BUFFER_VALID:
		{
			float* inputFrames  = reinterpret_cast<float*>(ppInputConnections [0]->pBuffer);
			float* outputFrames = reinterpret_cast<float*>(ppOutputConnections[0]->pBuffer);

			wstringstream stream;

			stream << "channelCount: " << channelCount << std::endl;
			stream << "ppOutputConnections[0]->u32ValidFrameCount: " << ppOutputConnections[0]->u32ValidFrameCount << std::endl;
			

			float leftChannel = VolumAPO::SafeStringToFloat(RegistryHelper::readValue(APP_REGPATH, L"LChannel"), 1);
			float rightChannel = VolumAPO::SafeStringToFloat(RegistryHelper::readValue(APP_REGPATH, L"RChannel"), 1);
			float volume = VolumAPO::SafeStringToFloat(RegistryHelper::readValue(APP_REGPATH, L"Volume"), 1);

			stream << "leftChannel: " << leftChannel << std::endl;
			stream << "rightChannel: " << rightChannel << std::endl;
			stream << "volume: " << volume << std::endl;

			for (unsigned i = 0; i < ppOutputConnections[0]->u32ValidFrameCount; i++)
			{
				for (unsigned j = 0; j < channelCount; j++)
				{
					float s = inputFrames[i * channelCount + j];

					//stream << "s before: " << s << std::endl;

					if (j <= 1)
					{
						if (j == 0)
						{ 
							// update left channel
							s *= leftChannel;
							s *= volume;
						}
						if (j == 1)
						{
							// update right channel
							s *= rightChannel;
							s *= volume;
						}
					}
					//stream << "s after: " << s << std::endl;
					outputFrames[i * channelCount + j] = s;
				}
			}

			TraceF(L"%s", stream.str().c_str());

			ppOutputConnections[0]->u32ValidFrameCount = ppInputConnections[0]->u32ValidFrameCount;
			ppOutputConnections[0]->u32BufferFlags = ppInputConnections[0]->u32BufferFlags;

			break;
		}
	case BUFFER_SILENT:
		ppOutputConnections[0]->u32ValidFrameCount = ppInputConnections[0]->u32ValidFrameCount;
		ppOutputConnections[0]->u32BufferFlags = ppInputConnections[0]->u32BufferFlags;

		break;
	}
}
#pragma AVRT_CODE_END

HRESULT VolumAPO::NonDelegatingQueryInterface(const IID& iid, void** ppv)
{
	if (iid == __uuidof(IUnknown))
		*ppv = static_cast<INonDelegatingUnknown*>(this);
	else if (iid == __uuidof(IAudioProcessingObject))
		*ppv = static_cast<IAudioProcessingObject*>(this);
	else if (iid == __uuidof(IAudioProcessingObjectRT))
		*ppv = static_cast<IAudioProcessingObjectRT*>(this);
	else if (iid == __uuidof(IAudioProcessingObjectConfiguration))
		*ppv = static_cast<IAudioProcessingObjectConfiguration*>(this);
	else if (iid == __uuidof(IAudioSystemEffects))
		*ppv = static_cast<IAudioSystemEffects*>(this);
	else
	{
		*ppv = NULL;
		return E_NOINTERFACE;
	}

	reinterpret_cast<IUnknown*>(*ppv)->AddRef();
	return S_OK;
}

ULONG VolumAPO::NonDelegatingAddRef()
{
	return InterlockedIncrement(&refCount);
}

ULONG VolumAPO::NonDelegatingRelease()
{
	if (InterlockedDecrement(&refCount) == 0)
	{
		delete this;
		return 0;
	}

	return refCount;
}