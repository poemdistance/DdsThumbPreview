using SharpShell.Attributes;
using SharpShell.SharpThumbnailHandler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization; // 需要在项目引用里勾选 System.Web.Extensions

namespace XmhShellExtensions
{
    /// <summary>
    /// The DdsThumbnailHandler is a ThumbnailHandler for dds files
    /// </summary>
    [ComVisible(true)]
#pragma warning disable CS0618 // Type or member is obsolete
    [COMServerAssociation(AssociationType.FileExtension, ".dds")]
#pragma warning restore CS0618 // Type or member is obsolete
    public class DdsThumbnailHandler : SharpThumbnailHandler
    {
        private const int GenerationTimeout = 30000;
        private Random rand = new Random(System.Environment.TickCount);
        private static string ImgConvertCmdPath="";

        private static bool enableLog = false;

        private static DateTime lastConfigReadTime = DateTime.MinValue;
        private static readonly object configLock = new object();
        private static string logDir = "";

        // 采用根据文件时间戳的方式决定是否重载config.json
        // 每次dds预览回调都会调用当前函数, 为了避免当前预览dds的dll只加载一次, 导致期间修改config.json没法同步日志的输出配置
        private static void RefreshConfigAndLogIfNeeded()
        {
            try
            {
                ImgConvertCmdPath = RegHelper.ImgConverCmdPath();
                if (string.IsNullOrEmpty(ImgConvertCmdPath)) return;

                string imgConvertExeDir = Path.GetDirectoryName(ImgConvertCmdPath);
                string configDirPath = Path.Combine(imgConvertExeDir, "config");
                string configFilePath = Path.Combine(configDirPath, "config.json");

                logDir = Path.Combine(imgConvertExeDir, "log");

                // 如果连文件夹都不存在，说明是纯净环境，先初始化创建
                if (!Directory.Exists(configDirPath))
                {
                    Directory.CreateDirectory(configDirPath);
                }
                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }

                if (!File.Exists(configFilePath))
                {
                    string defaultJson = "{\r\n  \"log\": false\r\n}";
                    File.WriteAllText(configFilePath, defaultJson, System.Text.Encoding.UTF8);
                    enableLog = false;                    return;
                }

                DateTime currentWriteTime = File.GetLastWriteTime(configFilePath);

                // 如果当前的修改时间和内存里记录的时间一致，说明用户没改过配置，直接返回
                if (currentWriteTime == lastConfigReadTime)
                {
                    return;
                }

                // 走到这里说明用户改了配置文件，加锁进行安全重读
                lock (configLock)
                {
                    // 双重检查，防止并发冲突
                    if (File.GetLastWriteTime(configFilePath) == lastConfigReadTime) return;

                    string jsonContent = File.ReadAllText(configFilePath);
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var configData = serializer.Deserialize<Dictionary<string, object>>(jsonContent);

                    if (configData != null && configData.ContainsKey("log"))
                    {
                        enableLog = Convert.ToBoolean(configData["log"]);
                    }

                    // 刷新内存中的时间戳记录
                    lastConfigReadTime = currentWriteTime;
                }
            }
            catch
            {
                enableLog = true;
            }
        }

        private string LogFilepath()
        {
            return Path.Combine(logDir, "log.txt");
        }
        /// <summary>
        /// All these methods are called from a windows service host linked somehow to the shell.
        /// Crashing the runtime sounds risky. This is why we use this instead of just trusting that the
        /// log file will be correctly written.
        /// </summary>
        /// <param name="text"></param>
        private void TryLoggingLine(string logfile, string text)
        {
            try
            {
                File.AppendAllText(logfile, $"{DateTime.Now:HH:mm:ss:fff} - {text}\n" );
            }
            catch { }
        }

        private void LogSomethingReceived(int size)
        {
            if (!enableLog) { return; }

            var imgConvertCmdDesc = new XMadHackRegistry.ImgConvertCmdDescription();
            var imgConverterPath = imgConvertCmdDesc.ReadRegisteredPath();

            var id = rand.Next().ToString();
            var logfile = LogFilepath();
            TryLoggingLine(logfile, $"Received ID:{id} Begin");
            TryLoggingLine(logfile, $"Received ID:{id} Width:{size}");
            TryLoggingLine(logfile, $"Received ID:{id} Name:{SelectedItemStream.Name}");
            TryLoggingLine(logfile, $"Received ID:{id} Length:{SelectedItemStream.Length}");
        }


        /// <summary>
        /// Gets the thumbnail image
        /// </summary>
        /// <param name="width">The width of the image that should be returned.</param>
        /// <returns>
        /// The image for the thumbnail
        /// </returns>
        /// 
        //protected Bitmap GetThumbnailImageOld(uint width)
        //{

        //    if (!RegHelper.IsDdsThumbnailsEnabled())
        //    {
        //        return null;
        //    }
        //    ImgConvertCmdPath = RegHelper.ImgConverCmdPath();

        //    LogSomethingReceived((int)width);
        //    var file = SelectedItemStream.Name; // What if the stream doesnt include the filename?? :S

        //    //  Attempt to open the stream with a reader
        //    try
        //    {
        //        var filenameonly = Path.GetFileName(file);
        //        var infile = Path.Combine(Path.GetDirectoryName(ImgConvertCmdPath), "Thumbnails", filenameonly + "_" + rand.Next().ToString() + "_t.dds");
        //        var outfile = Path.Combine(Path.GetDirectoryName(ImgConvertCmdPath), "Thumbnails", filenameonly + "_" + rand.Next().ToString() + "_t.png");
        //        using (var ms = new MemoryStream())
        //        {
        //            SelectedItemStream.CopyTo(ms);
        //            File.WriteAllBytes(infile, ms.ToArray());
        //        }

        //        var p = new Process();
        //        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //        p.StartInfo.CreateNoWindow = true;
        //        p.StartInfo.FileName = ImgConvertCmdPath;
        //        p.StartInfo.Arguments = $"-thumbnail {width} \"{infile}\" \"{outfile}\"";
        //        p.Start();
        //        p.WaitForExit(GenerationTimeout);
        //        try
        //        {
        //            var bytes = File.ReadAllBytes(outfile);//We do this so the file can be deleted
        //            using (var outms = new MemoryStream(bytes))
        //            {
        //                var bmp = new Bitmap(outms);
        //                return bmp;
        //            }
        //        }
        //        finally
        //        {
        //            if (File.Exists(infile)) File.Delete(infile);
        //            if (File.Exists(outfile)) File.Delete(outfile);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //  Log the exception and return null for failure
        //        TryLoggingLine(LogFilepath(), "An exception occurred during thumbnail generation. Name: {} \n" + ex.ToString());
        //        return null;
        //    }
        //}

        /// <summary>
        /// Gets the thumbnail image (全内存高速重构版)
        /// </summary>
        protected override unsafe Bitmap GetThumbnailImage(uint width)
        {
            if (!RegHelper.IsDdsThumbnailsEnabled())
            {
                return null;
            }

            RefreshConfigAndLogIfNeeded();

            if(logDir == "")
            {
                ImgConvertCmdPath = RegHelper.ImgConverCmdPath();
                logDir = Path.GetDirectoryName(ImgConvertCmdPath);
            }


            LogSomethingReceived((int)width);


            // 高效精简版日志工具，生产环境只记录关键节点
            string logPath = LogFilepath();
            Action<string> LogInfo = (msg) =>
            {
                if(!enableLog)
                {
                    return;
                }
                try
                {
                    string timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    System.IO.File.AppendAllText(logPath, $"[{timeStamp}] [C# Shell] {msg}\r\n");
                }
                catch { }
            };

            var totalSw = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                byte[] ddsData;
                if (SelectedItemStream.CanSeek)
                {
                    ddsData = new byte[SelectedItemStream.Length];
                    SelectedItemStream.Read(ddsData, 0, ddsData.Length);
                }
                else
                {
                    using (var ms = new MemoryStream())
                    {
                        SelectedItemStream.CopyTo(ms);
                        ddsData = ms.ToArray();
                    }
                }

                if (ddsData == null || ddsData.Length == 0)
                {
                    LogInfo("WARNING: SelectedItemStream is empty or null. Aborting.");
                    return null;
                }

                //固定内存并调用 C++ 引擎
                fixed (byte* pDdsData = ddsData)
                {
                    byte* pOutPngBuffer = null;
                    UIntPtr outPngSize = UIntPtr.Zero;


                    bool success = TexMemWrapperNative.ConvertMemoryDDsToPng(
                        SelectedItemStream.Name,
                        pDdsData,
                        (UIntPtr)ddsData.Length,
                        out pOutPngBuffer,
                        out outPngSize,
                        logDir,
                        width,
                        enableLog
                    );

                    if (!success || pOutPngBuffer == null || outPngSize == UIntPtr.Zero)
                    {
                        LogInfo($"ERROR: Native engine failed. Success: {success}, Ptr: 0x{(long)pOutPngBuffer:X}, Size: {outPngSize}");
                        return null;
                    }

                    try
                    {
                        // 3. 【非托管数据拷贝】
                        int pngLength = (int)outPngSize.ToUInt64();
                        byte[] pngData = new byte[pngLength];
                        Marshal.Copy((IntPtr)pOutPngBuffer, pngData, 0, pngLength);

                        // 4. 【流式加载 Bitmap】
                        // 保持 MemoryStream 的存活，使其生命周期与 Bitmap 同步，免去物理 Clone 的 CPU 开销
                        var outms = new MemoryStream(pngData);
                        Bitmap bmp = new Bitmap(outms);

                        totalSw.Stop();
                        LogInfo($"[SUCCESS] Resolution: {bmp.Width}x{bmp.Height}, InSize: {ddsData.Length} bytes, OutSize: {pngLength} bytes, TotalCost: {totalSw.ElapsedMilliseconds} ms.");

                        return bmp;
                    }
                    finally
                    {
                        // 5. 【安全拆桥】
                        if (pOutPngBuffer != null)
                        {
                            TexMemWrapperNative.FreeNativeMemory(pOutPngBuffer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                totalSw.Stop();
                LogInfo($"CRITICAL EXCEPTION: Cost: {totalSw.ElapsedMilliseconds} ms. \r\n{ex.ToString()}");
                TryLoggingLine(LogFilepath(), "An exception occurred during memory thumbnail generation. \n" + ex.ToString());
                return null;
            }
        }
    }
}
