#include <sal.h>
#include <cstdint>
#ifndef __BRIDGE_TEXTURE_CONVERT__


int __cdecl OriginMain(_In_ int argc, _In_z_count_(argc) wchar_t* argv[],
    const uint8_t* ddsBuffer = nullptr, // 默认为空，不影响原有的普通调用
    size_t ddsSize = 0,
    uint8_t** outPngBuffer = nullptr,      // [输出] 用于返回 PNG 内存指针
    size_t* outPngSize = nullptr // [输出] 用于返回 PNG 内存大小 
);

#define __BRIDGE_TEXTURE_CONVERT__
#endif
