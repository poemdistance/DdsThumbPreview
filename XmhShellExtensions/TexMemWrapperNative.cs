using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace XmhShellExtensions
{
    public static unsafe class TexMemWrapperNative
    {
        // 对接 C++ 的全内存转换函数
        [DllImport("TexMemWrapper.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ConvertMemoryDDsToPng", ExactSpelling = false)]
        public static extern bool ConvertMemoryDDsToPng(
            [MarshalAs(UnmanagedType.LPWStr)]string ddsFileName, // 注意需要[MarshalAs(UnmanagedType.LPWStr)]进行转换, 不然c++那边接受到的值异常
            byte* ddsBuffer, // c#传入的dds内存地址
            UIntPtr ddsSize,
            out byte* outPngBuffer, // 转换后的png存入到当前变量地址
            out UIntPtr outPngSize,
            [MarshalAs(UnmanagedType.LPWStr)] string logDir, // c#传递日志目录
            uint targetWidth, // 源dds的长度
            bool enableLog // 是否打印日志
        );

        // C++ Texconv-modified.cpp 那边用了 CoTaskMemAlloc，C# 这边必须用符合 COM 的 CoTaskMemFree
        public static void FreeNativeMemory(byte* pointer)
        {
            if (pointer != null)
            {
                // 如果使用下面注释里的fre, 不使用Marshal.FreeCoTaskMem，会导致dds虽然成功被texconv转换成了png并写入内存，但是文件管理器里
                // 还是不显示缩略图，估计是触发了windows的一些保护机制，那片内存
                // [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "free")]
                // public static extern void FreeNativeMemory(byte* pointer);
                Marshal.FreeCoTaskMem((IntPtr)pointer);
            }
        }
    }
}