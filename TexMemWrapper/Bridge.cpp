#include "DirectXTex.h"
#include <windows.h>
#include <memory>

#include <unknwn.h>      // 引入 Windows 未知组件接口（包含 GUID 的底层定义）
#include <objbase.h>     // 引入 COM 组件基础（确保 GUID 完全被识别）
#include <winerror.h>    // 引入 Windows 错误处理宏（确保 FAILED 和 SUCCEEDED 宏被识别）
#include "DirectXTex.h"
#include <windows.h>
#include <memory>
#include <fstream>
#include <chrono>
#include <string>
#include <iomanip>
 
// 提前声明我们要使用微软的 DirectX 命名空间，这样就不用在下面反复写 DirectX:: 了
using namespace DirectX;


#include <windows.h>
#include <fstream>
#include <chrono>
#include <string>
#include <iomanip>

#include "bridge.h"

#include <vector>
#include <string>
#include <fstream>
#include <chrono>
#include <iomanip>
#include <cstdarg>


const wchar_t* g_currentLogDir = nullptr;
bool enableLog = false;

// 日志工具：接收 C# 传过来的绝对目录，并写入文件
static void WriteDebugLog(const wchar_t* logDir, const wchar_t* format, ...) {
    if (!enableLog) {
        return;
    }
    try {
        if (!logDir) return;

        std::wstring fullLogPath = std::wstring(logDir);
        if (!fullLogPath.empty() && fullLogPath.back() != L'\\' && fullLogPath.back() != L'/') {
            fullLogPath += L"\\";
        }
        fullLogPath +=   L"log\\preview_callback.log";

        std::wofstream logFile(fullLogPath, std::ios::app);
        if (logFile.is_open()) {
            auto now = std::chrono::system_clock::now();
            auto in_time_t = std::chrono::system_clock::to_time_t(now);
            auto ms = std::chrono::duration_cast<std::chrono::milliseconds>(now.time_since_epoch()) % 1000;

            struct tm timeinfo;
            localtime_s(&timeinfo, &in_time_t);

            // 处理变长参数列表
            wchar_t buffer[1024];
            va_list args;
            va_start(args, format);
            vswprintf_s(buffer, format, args);
            va_end(args);

            logFile << L"[" << std::setw(2) << std::setfill(L'0') << timeinfo.tm_hour << L":"
                << std::setw(2) << std::setfill(L'0') << timeinfo.tm_min << L":"
                << std::setw(2) << std::setfill(L'0') << timeinfo.tm_sec << L"."
                << std::setw(3) << std::setfill(L'0') << ms.count() << L"] [C++ Core] "
                << buffer << std::endl;
        }
    }
    catch (...) {}
}

static const wchar_t* GetErrorDesc(HRESULT hr) {
    static wchar_t desc[64];
    swprintf_s(desc, L"0x%08X", (unsigned int)hr);
    return desc;
}

extern "C" __declspec(dllexport) bool ConvertMemoryDDsToPng(
    const wchar_t *ddsFileName,
    const uint8_t * ddsBuffer, size_t ddsSize,
    uint8_t * *outPngBuffer, size_t * outPngSize,
    const wchar_t* logDir,
    uint32_t targetWidth,
    bool needLog)

{
    enableLog = needLog;
    g_currentLogDir = logDir; // Texconv-modified.cpp使用

    // 1. 接口入口日志
    WriteDebugLog(logDir, L"=== ConvertDdsMemToPngMem Started ===");
    WriteDebugLog(logDir, L"Input Parameters -> ddsBuffer: 0x%p, ddsSize: %zu bytes, targetWidth: %u", ddsBuffer, ddsSize, targetWidth);

    // 2. 基础入参合法性校验与日志
    if (!ddsBuffer || ddsSize == 0 || !outPngBuffer || !outPngSize) {
        WriteDebugLog(logDir, L"ERROR: Invalid input arguments! ddsBuffer=%p, ddsSize=%zu, outPngBuffer=%p, outPngSize=%p", 
            ddsBuffer, ddsSize, outPngBuffer, outPngSize);
        return false;
    }

    // 3. 构造基础的转换命令参数
    std::vector<std::wstring> args;
	// 注意最后并不是调用该执行文件, 只是因为texconv的main函数被我们迁移过来没怎么改动逻辑,
    // 为了配合C/C++它原本需要的argc, argv入参而拼接的
    args.push_back(L"texconv.exe");   
    args.push_back(L"-w"); args.push_back(std::to_wstring(targetWidth)); 
    args.push_back(L"-h"); args.push_back(std::to_wstring(targetWidth)); 
    args.push_back(L"-ft"); args.push_back(L"png");                       
    args.push_back(ddsFileName); 

    // 打印伪造的命令行，方便核对参数
    std::wstring cmdLineLog = L"Fake Command Line: ";
    for (const auto& arg : args) {
        cmdLineLog += arg + L" ";
    }
    WriteDebugLog(logDir, L"%ls", cmdLineLog.c_str());

    // 4. 转成 wchar_t* 数组
    std::vector<wchar_t*> argv;
    for (auto& s : args) {
        argv.push_back(&s[0]);
    }
    int argc = static_cast<int>(argv.size());

    // 5. 进入核心逻辑前留痕
    WriteDebugLog(logDir, L"Calling OriginMain standard pipeline...");
    
    // 调用核心处理函数
    int result = OriginMain(argc, argv.data(), ddsBuffer, ddsSize, outPngBuffer, outPngSize);

    // 6. 核心逻辑执行结果日志
    if (result == 0) {
        // 成功时，打印传回给 C# 的内存状态
        if (outPngBuffer && *outPngBuffer && outPngSize) {
            WriteDebugLog(logDir, L"SUCCESS: OriginMain returned 0. Output PNG buffer address: 0x%p, Size: %zu bytes", 
                *outPngBuffer, *outPngSize);
        } else {
            WriteDebugLog(logDir, L"WARNING: OriginMain returned 0 but output pointers are null/empty. outPngBuffer content: 0x%p", 
                outPngBuffer ? *outPngBuffer : nullptr);
        }
    } else {
        // 失败时记录返回值
        WriteDebugLog(logDir, L"ERROR: OriginMain execution FAILED with exit code: %d", result);
    }

    WriteDebugLog(logDir, L"=== ConvertDdsMemToPngMem Finished (Result: %s) ===\n", (result == 0) ? L"TRUE" : L"FALSE");

    return (result == 0);
}
